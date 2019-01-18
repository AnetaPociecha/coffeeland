using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;

namespace Coffeeland.MailService
{
    public static class MailSender
    {
        public const String fromName = "Coffeeland";
        public const String fromEmailAddress = "coffeeland.store@gmail.com";
        public const String password = "@dmin1234";

        public const String host = "smtp.gmail.com";
        public const int port = 465;
        public const bool useSsl = true;

        public static void Send(MimeMessage message)
        {
            SmtpClient client = new SmtpClient();
            client.Connect(host, port, useSsl);
            client.Authenticate(fromEmailAddress, password);
            client.Send(message);
            client.Disconnect(true);
        }
 
    }
}