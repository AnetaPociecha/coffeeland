using System;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.MailService
{
    public abstract class EmailMessage
    {

        public MimeMessage message;
        public BodyBuilder bodyBuilder;

        public StringBuilder body;
        public string header;
        public string footer;
        public string content;
        public string subject;

        public EmailMessage()
        {
            body = new StringBuilder();
            message = new MimeMessage();
            message.From.Add(new MailboxAddress(MailSender.fromName, MailSender.fromEmailAddress));
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

        public void SetBody()
        {
            bodyBuilder.TextBody = body.ToString();
            message.Body = bodyBuilder.ToMessageBody();
        }

        public void Send(int fieldId)
        {
            Build(fieldId);
            MailSender.Send(message);
        }

        public abstract void Build(int fieldId);

    }
}
