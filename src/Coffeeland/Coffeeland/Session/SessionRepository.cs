using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Coffeeland.Session
{
    public static class SessionRepository
    {
        static List<ClientSession> sessions;

        static SessionRepository()
        {
            sessions = new List<ClientSession>();
        }

        public static string StartNewSession(int clientId)
        {
            var newToken = GenerateRandomStringToken();
            sessions.Add(new ClientSession()
            {
                clientId = clientId,
                sessionToken = newToken,
                expirationDate = DateTime.Now.AddMinutes(15)
            });
            return newToken;
        }

        public static void RemoveSession(string sessionToken)
        {
            sessions.RemoveAll(s => s.sessionToken.Equals(sessionToken));
        }

        public static int GetClientIdOfSession(string sessionToken)
        {
            sessions.RemoveAll(s => s.expirationDate < DateTime.Now);
            var foundSessions = sessions.FindAll(s => s.sessionToken.Equals(sessionToken));
            if (foundSessions.Count != 1)
            {
                return -1;
            }
            else
            {
                foundSessions[0].expirationDate = DateTime.Now.AddMinutes(15);
                return foundSessions[0].clientId;
            }
        }

        public static string GenerateRandomStringToken()
        {
            var randomBytes = GenerateRandomBytes(512);
            var newToken = HttpServerUtility.UrlTokenEncode(randomBytes);
            if (sessions.FindIndex(s => s.sessionToken.Equals(newToken)) == -1)
                return newToken;
            else
                return GenerateRandomStringToken();
        }

        private static byte[] GenerateRandomBytes(int keyBitLength)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                var lengthInByte = keyBitLength / 8;
                var randomNumber = new byte[lengthInByte];
                provider.GetBytes(randomNumber);

                return randomNumber;
            }
        }
    }
}