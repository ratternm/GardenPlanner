using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class DbManager
    {
        private string password;
        private static int _workFactor = 20;
        private static int _saltSize = 16;

        public string Salt { get; set; }
        public string Hash { get; set; }


        public DbManager(string password)
        {
            this.password = password;
            GenerateSalt();
            GenerateHash();
        }

        public DbManager(string password, string salt)
        {
            this.password = password;
            this.Salt = salt;
            GenerateHash();
        }

        public bool Verify(string hash)
        {
            return Hash == hash;
        }



        #region Private methods

        private void GenerateSalt()
        {
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, _saltSize, _workFactor);
            Salt = GetSalt(rfc);
        }

        private void GenerateHash()
        {
            Rfc2898DeriveBytes rfc = HashPasswordWithPBKDF2(password, Salt);
            Hash = GetHash(rfc);
        }

        private static Rfc2898DeriveBytes HashPasswordWithPBKDF2(string password, string salt)
        {
            // Creates the crypto service provider and provides the salt - usually used to check for a password match
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), _workFactor);

            //byte[] hash = rfc2898DeriveBytes.GetBytes(20);      //gets the hased password

            return rfc2898DeriveBytes;
        }

        private static string GetHash(Rfc2898DeriveBytes rfc)
        {
            return Convert.ToBase64String(rfc.GetBytes(20));
        }

        private static string GetSalt(Rfc2898DeriveBytes rfc)
        {
            return Convert.ToBase64String(rfc.Salt);
        }
    }
    #endregion
}
