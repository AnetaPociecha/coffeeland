using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database;

namespace Coffeeland.MailService
{
    public class SuccessfullPaymentEmail : EmailMessage
    {
        public override void Build(int clientId)
        {
            var client = DatabaseQueryProcessor.GetClient(clientId);

            header = "Dear " + client.firstName + ", \n";
            content = "we have just received your payment, you can check details of your order status on Orders page of your MyAccount."
                        + "The standard shipping takes 3-7 buissness days. Due to the nature of pre-order sales and often in-demand products, shipping delays may occur."
                        + "We will do our best to contact you regarding your shipment as soon as possible with an updated timeline.";
            footer = "Best regards,\n"
                      + "John Doe \n"
                      + "Head of Sales";
            subject = "CoffeeLand - we have received your payment";

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