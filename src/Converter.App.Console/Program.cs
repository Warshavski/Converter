using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Ninject;

using Escyug.Converter.Common.Logging;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Services;
using Escyug.Converter.Models.Utils.MailSender;
using Escyug.Converter.Models.Utils.XmlConverter;
using System.Configuration;
using System.Globalization;
using Escyug.Converter.App.Console.Configurations;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.RecipesServiceReference;
using static System.Console;

namespace Escyug.Converter.App.Console
{
    internal class Program
    {
        private class EqualTest
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public EqualTest(int id, string name)
            {
                Id = id;
                Name = name;
            }


            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }

                var p = obj as EqualTest;
                if ((System.Object)p == null)
                {
                    return false;
                }

                // Return true if the fields match:
                return (Id == p.Id) && (Name == p.Name);
            }

            public bool Equals(EqualTest p)
            {
                // If parameter is null return false:
                if ((object)p == null)
                {
                    return false;
                }

                // Return true if the fields match:
                return (Id == p.Id) && (Name == p.Name);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;

                    hash = 23 * Id.GetHashCode();
                    hash = 23*(string.IsNullOrEmpty(Name) ? 0 : Name.GetHashCode());

                    return hash;
                }
            }
        }


        private class RangeEntity
        {
            public string Series { get; set; }
            public string Number { get; set; }

            public int StartIndex { get; set; }
            public int Range { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }

                var range = obj as RangeEntity;
                if ((object) range == null)
                {
                    return false;
                }

                return (Number == range.Number) && (Series == range.Series);
            }

            public bool Equals(RangeEntity range)
            {
                if ((object) range == null)
                {
                    return false;
                }

                return Series == range.Series && Number == range.Number;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;

                    hash = hash*23 + (string.IsNullOrEmpty(Series) ? 0 : Series.GetHashCode());
                    hash = hash*23 + (string.IsNullOrEmpty(Number) ? 0 : Number.GetHashCode());

                    return hash;
                }
            }
        }

        private class Entity
        {
            public string Series { get; set; }
            public string Number { get; set; }
        }

        static void Main(string[] args)
        {
            /** TEST FTP-SERVER CREDENTIALS 
               var remoteAddress = "ftp://31.31.196.56/public_html/escyug.ru/ftp_test/";
               var localAddress = @"C:\test\ftp_test\";
               var files = new string[] { "1.doc", "2.doc" };
            */

            /**
            var entityList = new List<Entity>();
            var rnd = new Random();
            for (int i = 0; i < 1000; ++i)
            {
                if (i%2 == 0)
                {
                    var linesTotal = rnd.Next(10, 2);
                    for (var j = 0; j < linesTotal; ++j)
                    {
                        entityList.Add(new Entity()
                        {
                            Number = i.ToString(),
                            Series = (i + 10).ToString()
                        });
                    }
                }
                else
                {
                    entityList.Add(new Entity()
                    {
                        Number = i.ToString(),
                        Series = (i + 10).ToString()
                    });
                }
            }
            */

            var watch = System.Diagnostics.Stopwatch.StartNew();

            /**
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var remainsRepo = kernel.Get<IRepository<RemainRow>>();

            var remainsList = remainsRepo.GetAll("remains.dbf");
            */

            /**
            var duplicates = from x in entityList
                             group x by new
                             {
                                x.Series,
                                x.Number    
                             }
                             into grouped
                             where grouped.Count() > 1
                             select grouped.Key;
            */


            var itemsList = new List<int>()
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10, //batch #1

                11,
                11,
                11,
                11,
                11,
                11,
                12,
                13,
                14,
                15, //batch #2

                16,
                17,
                18,
                18,
                18,
                19,
                19,
                20, //batch #3

                21,
                21,
                21,
                22,
                23, //batch #4

                24,
                24,
                24,
                24,
                24,
                24,
                24,
                25,
                26,
                27 //batch #5

            };

            //var batchList = CreateBatchList(itemsList);

            var connString =
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\test\converter\remains\;User ID=Admin;Password=;Extended Properties='dBASE IV';";
            var connectionItems = connString.Split(';');

            var removeString = "Data Source=";

            int index = connectionItems[1].IndexOf(removeString);
            string folderPath = (index < 0)
                ? connectionItems[1]
                : connectionItems[1].Remove(index, removeString.Length);

            WriteLine(folderPath);

            watch.Stop();

            WriteLine($"{Environment.NewLine}Elapsed time : {watch.ElapsedMilliseconds}ms.");
        }







        // RECIPE BATCH CREATION ALGO

        private class RecipeRange
        {
            public int StartIndex { get; set; }
            public int Range { get; set; }
        }

        private static IList<IList<int>> CreateBatchList(IList<int> entityList)
        {
            const int BatchSizeMax = 10;

            var batchList = new List<IList<int>>();


            var batchSize = 0;
            int startBatchIndex = 0;
            var recipesRangeMap = CreateRecipesRangeMap(entityList);
            
            var recipeRangeTotal = recipesRangeMap.Count;
            for (int recipeRangeCount = 0; recipeRangeCount < recipeRangeTotal; ++recipeRangeCount)
            {
                var recipeRange = recipesRangeMap[recipeRangeCount].Range;
                if (batchSize + recipeRange <= BatchSizeMax)
                {
                    batchSize += recipesRangeMap[recipeRangeCount].Range;
                }
                else
                {
                    batchList.Add(CreateBatch(entityList, batchSize, startBatchIndex));
                    startBatchIndex = recipesRangeMap[recipeRangeCount].StartIndex;
                    batchSize = recipesRangeMap[recipeRangeCount].Range;
                    
                }

                if (recipeRangeCount == recipeRangeTotal - 1)
                {
                    batchList.Add(CreateBatch(entityList, batchSize, startBatchIndex));
                }
            }

            return batchList;
        }

        private static IList<RecipeRange> CreateRecipesRangeMap(IList<int> recipesRowsList)
        {
            var recipesRange = new List<RecipeRange>();

            int recipesTotal = recipesRowsList.Count;
            int previousRecipe = -1;
            for (int recipeCount = 0; recipeCount < recipesTotal; ++recipeCount)
            {
                if (recipesRowsList[recipeCount] == previousRecipe)
                {
                    continue;
                }

                int recipeRange = 1; //count
                if (recipeCount != recipesTotal - 1)
                {
                    var recipeLineCount = recipeCount + 1; //j
                    while (recipeLineCount < recipesTotal &&
                        recipesRowsList[recipeCount] == recipesRowsList[recipeLineCount])
                    {
                        ++recipeRange;
                        ++recipeLineCount;
                    }

                    previousRecipe = recipesRowsList[recipeCount];
                }

                recipesRange.Add(new RecipeRange()
                {
                    StartIndex = recipeCount,
                    Range = recipeRange
                });
            }

            return recipesRange;
        }

        private static IList<int> CreateBatch(IList<int> batchData, int batchSize, int startItemIndex)
        {
            var batch = new List<int>();
            var batchLastIndex = batchSize + startItemIndex;
            for (int batchItemCount = startItemIndex; batchItemCount < batchLastIndex; ++batchItemCount)
            {
                batch.Add(batchData[batchItemCount]);
            }

            return batch;
        }







        // HTTP PROTOTYPING SECTION
        //---------------------------------------------------------------------

        public static void DownloadFile(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            var name = url.Substring(url.LastIndexOf('/') + 1);
            using (var res = (HttpWebResponse)req.GetResponse())
            {
                using (var resStream = res.GetResponseStream())
                {
                    using (var fs = new FileStream("C:\\" + name, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        // Save to file
                        var buffer = new byte[8 * 1024]; // 8 KB buffer
                        int len; // Read count
                        while ((len = resStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
                
            }
            
        }



        // LOWER LEVEL OF HTTP
        //---------------------------------------------------------------------

        private static void FtpDownloadFile(string fileName)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[2048];

            var request = (FtpWebRequest)WebRequest.Create("ftp://31.31.196.56/public_html/escyug.ru/ftp_test/" + fileName);
            request.Credentials = new NetworkCredential("u0223748", "V_hn3_SZ");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using (var response = request.GetResponse())
            {
                var responseStream = response.GetResponseStream();
                using (var fileStream = new FileStream(@"C:\test\ftp_test\" + fileName, FileMode.Create))
                {
                    while (true)
                    {
                        bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                            break;

                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        private static async Task FtpDownloadFileAsync(string fileName)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[2048];

            var request = (FtpWebRequest)WebRequest.Create("ftp://31.31.196.56/public_html/escyug.ru/ftp_test/" + fileName);
            request.Credentials = new NetworkCredential("u0223748", "V_hn3_SZ");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using (var response = await request.GetResponseAsync())
            {
                var responseStream = response.GetResponseStream();
                using (var fileStream = new FileStream(@"C:\test\ftp_test\" + fileName, FileMode.Create))
                {
                    while (true)
                    {
                        bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                            break;

                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                    }
                }
            }
        }
        
    }
}





// * HISTORY * TEST CODE SECTION
//-----------------------------------------------------------------------------

/**
    Configuration config =
        ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

    SenderConfiguration gui = (SenderConfiguration)config.Sections["senderConfiguration"];

    var hostName = gui.Host.Address;

    var receiver1 = gui.Receivers[0].Address;
    var receiver2 = gui.Receivers[1].Address;

    System.Console.WriteLine(receiver1 + Environment.NewLine + receiver2);
*/
// Get the object used to communicate with the server.

/**
 * private static void NinjectTest()
        {
            /* Ninject test
            * 
           var kernel = new StandardKernel();
           kernel.Load(Assembly.GetExecutingAssembly());
            
           var logger = kernel.Get<ILogManager>();
           var mailSender = kernel.Get<IMailSender>();
           var remainRepository = kernel.Get<IRepository<RemainRow>>();
           var xmlConverter = kernel.Get<IXmlConverter<RemainRow>>();
           var remainsSender = kernel.Get<BaseService<RemainRow>>();

           var log = logger.GetLog(typeof(Program));
            
           var remains = remainRepository.GetAll("www");

           remainsSender.SendData(remains, 1);

           //mailSender.SenderNotify += (message) => { log.Info(message); };
           mailSender.Send("sender test", "test xml file in attach", new string[] { "xml/test0.xml", "logs/application.log" });

           System.Console.WriteLine("test done!");
        }
 */

/**
 * var request = (FtpWebRequest)WebRequest.Create("ftp://31.31.196.56/public_html/escyug.ru/ftp_test/files.xml");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            ///public_html/escyug.ru/ftp_test

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("u0223748", "V_hn3_SZ");

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                using (var reader = new StreamReader(responseStream))
                {
                    System.Console.WriteLine(reader.ReadToEnd());
                    System.Console.WriteLine("Download Complete, status {0}", response.StatusDescription);
                }
            }
 */

/**
 * private static async Task DownloadAsync()
        {
            var request = (FtpWebRequest)WebRequest.Create("ftp://31.31.196.56/public_html/escyug.ru/ftp_test/files.xml");
            request.Credentials = new NetworkCredential("u0223748", "V_hn3_SZ");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using (var response = await request.GetResponseAsync())
            {
                var responseStream = response.GetResponseStream();
                using (var reader = new StreamReader(responseStream))
                {
                    System.Console.WriteLine(await reader.ReadToEndAsync());
                }
            }
        }
 */
  
/**
 * 
 * var entityRangeList = new List<RecipeRange>();

    var entityList = new List<int>()
    {
        1, //0
        1, //0
        2, //2
        3, //3
        4, //4
        4, //4
        4, //4
        4, //4
        5, //8
        5, //8
        6, //10
        7 //11
                
    };

            int prevNum = 0;
            for (int i = 0; i < entityList.Count; ++i)
            {
                if (entityList[i] == prevNum)
                {
                    continue;
                }

                int count = 1;

                if (i != entityList.Count - 1)
                {
                    
                    int j = i + 1;
                    while (j < entityList.Count &&
                        entityList[i] == entityList[j])
                    {
                        ++count;
                        ++j;
                    }

                    prevNum = entityList[i];
                }
                
                entityRangeList.Add(new RecipeRange()
                {
                    StartIndex = i,
                    Range = count
                });
            }

            foreach (var entityRange in entityRangeList)
            {
                WriteLine($"index: {entityRange.StartIndex}\t|\trange: {entityRange.Range}");
            }
 */
