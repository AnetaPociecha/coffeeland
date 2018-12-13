using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class ClientInfoRecord : IRecord
    {
        public int clientId;
        public string email;
        public string firstName;
        public string lastName;
        public string password;
        public string newsletterEmail;
        public bool isSignedUpForNewsletter;

        public void Fill(DataRow dr)
        {
            clientId = Convert.ToInt32(dr["clientId"]);
            email = dr["email"].ToString();
            firstName = dr["firstName"].ToString();
            lastName = dr["lastName"].ToString();
            password = dr["password"].ToString();
            newsletterEmail = dr["newsletterEmail"].ToString();
            isSignedUpForNewsletter = newsletterEmail.Length == 0  ? false : true;
        }
    }
}