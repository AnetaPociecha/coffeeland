using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database;

namespace Coffeeland.MailService
{
    public class UnsuccessfullPaymentEmail : EmailMessage
    {
        public override void Build(int clientId)
        {
            var client = DatabaseQueryProcessor.GetClient(clientId);

            header = "Dear " + client.firstName + ", \n";
            content = "Unfortunatelly your payment was not successfull. Please try again or contact support.";

            footer = "Best regards,\n"
                      + "John Doe \n"
                      + "Head of Sales";
            subject = "CoffeeLand - your payment was not successfull";

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
