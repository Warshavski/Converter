using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

using Escyug.Converter.Common.Logging;

using Escyug.Converter.Models.Utils.MailSender;

namespace Escyug.Converter.Models.Services.Prototype
{
    public class TestSender
    {
        private readonly ILog _log;
        private readonly IMailSender _mailSender;

        public TestSender(ILogManager logManager, IMailSender mailSender)
        {
            _log = logManager.GetLog(typeof(TestSender));
            _mailSender = mailSender;
        }

        public void SendTest()
        {
            _log.Info("Application is working");

            _mailSender.SenderNotify += (message) => { _log.Info(message); };

            var subject = "test logs message";
            var body = "test logs";
            var attachment = new string[] { "application.log" };

            _mailSender.Send(subject, body, attachment);
        }
    }
}
