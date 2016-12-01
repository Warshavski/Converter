using NUnit.Framework;
using Moq;
using Escyug.Converter.Models.Services.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Repositories;

namespace Escyug.Converter.Models.Tests.Services.Prototype
{
    [TestFixture()]
    public class GuideServiceTests
    {
        [Test()]
        public void GuideServiceTest()
        {
            var mnnRepositoryMock = new Mock<IGuideRepository<Mnn>>();
            //mnnRepositoryMock.Setup(mnnRepo => mnnRepo.GetIds("sp_mnn.dbf")).Returns()
            Assert.Fail();
        }

        [Test()]
        public void GetMetadataTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetMetadataForDownloadTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DownloadGuidesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetMnnIdsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetTradeNameIdsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetDrugIdsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetDrugformIdsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetMnnIdsAsyncTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetTradeNameIdsAsyncTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetDrugIdsAsyncTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetDrugformIdsAsyncTest()
        {
            Assert.Fail();
        }
    }
}