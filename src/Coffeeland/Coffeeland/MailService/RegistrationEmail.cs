using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database.Records;
using Coffeeland.Database;

namespace Coffeeland.MailService
{
    public class RegistrationEmail : EmailMessage
    {
        public override void Build(int clientId)
        {
            var client = DatabaseQueryProcessor.GetClient(clientId);

            header = "Dear " + client.firstName + ", \n";
            content = "We are glad that you have joined Coffeeland community! If you have any questions do not hesitate to contact us.\n";
            footer = "Best regards,\n"
                      + "Jane Doe \n"
                      + "Customer Service Coordinator";
            subject = "Welcome in CoffeeLand " + client.firstName + "!";

            body.Append(header);
            body.AppendLine();
            body.Append(content);
            body.AppendLine();
            body.Append(footer);

            SetSubject(subject);
            SetBody();
            AddReceiver(client.firstName, client.email);
        } 
    }
}