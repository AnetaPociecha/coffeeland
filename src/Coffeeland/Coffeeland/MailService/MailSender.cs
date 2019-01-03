using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


namespace Coffeeland.MailService
{
    public static class MailSender
    {
        const string registrationEmailPath = @"D:\coffeeland\src\Coffeeland\Coffeeland\MailService\registration.txt"; 
                            // TO DO: store paths somewhere

        public static void SendRegistrationEmail(string email, string name)
        {
            var message = new EmailMessage();
            var contents = File.ReadAllText(registrationEmailPath);

            message.AddReceiver(name, email);
            message.SetSubject("Welcome in CoffeeLand " + name + "!");
            message.SetBody(contents);
            message.Send();
        }
    }
}