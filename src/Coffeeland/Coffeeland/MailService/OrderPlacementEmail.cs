using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database.Records;
using Coffeeland.Database;
using System.Text;

namespace Coffeeland.MailService
{
    public class OrderPlacementEmail : EmailMessage
    {
        public override void Build(int clientId)
        { 
            var client = DatabaseQueryProcessor.GetClient(clientId);

            header = "Dear " + client.firstName + ", \n";
            content = "Thank you for your trust! \n We are still waiting for your payment but do not worry"
                + " - we'll inform you about everything as soon as possible. You can check the details and status of the transaction at your Orders page.\n ";
                
            footer = "Best regards!\n"
                      + "John Doe \n"
                      + "Head of Sales";
            subject = "CoffeeLand - order placement!";

            body.Append(header);
            body.AppendLine();
            body.Append(content); 
            body.AppendLine();
            body.Append(footer);

            SetSubject(subject);
            SetBody();
            AddReceiver(client.firstName, client.email);
        }

        string getOrderInfo(int orderId)
        {
            var orderEntries = DatabaseQueryProcessor.GetOrderEntries(orderId);
            var builder = new StringBuilder();
            

            foreach (var entry in orderEntries)
            {
                builder.Append(String.Format("|{0,5}|{1,5}|{2,5}|{3,5}|", "", "product", "quantity", "price"));
            }
            return builder.ToString();
        }
    }
}