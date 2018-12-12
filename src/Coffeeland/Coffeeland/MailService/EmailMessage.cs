using System;
using MailKit.Net.Smtp;
using MimeKit;


namespace Coffeeland.MailService
{
    class EmailMessage
    {
        const String host = "smtp.gmail.com";
        const int port = 465;
        const bool useSsl = true;

        const String fromName = "Coffeeland";
        const String fromEmailAddress = "coffeeland.store@gmail.com";
        const String password = ""; //fill

        MimeMessage message;
        BodyBuilder bodyBuilder;

        public EmailMessage()
        {
            message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName,fromEmailAddress));
            bodyBuilder = new BodyBuilder();
        }

        public void AddReceiver(String toName, String toEmailAddress)
        {
            message.To.Add(new MailboxAddress(toName, toEmailAddress));
        }

        public void SetSubject(String subject)
        {
            message.Subject = subject;
        }

        public void SetBody(String body)
        {
            bodyBuilder.TextBody = body;
            message.Body = bodyBuilder.ToMessageBody();
        }

        public bool Send()
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Connect(host, port, useSsl);
                client.Authenticate(fromEmailAddress, password);
                client.Send(message);
                client.Disconnect(true);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        

    }
}
