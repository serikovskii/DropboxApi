using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;

namespace Dropbox_ControlWork
{
    public class EncryptionService
    {
        public static string HashPassword(string password)
        {
            return BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
        }

        public static bool VerifyPassword(string candidatePassword, string hashedPassword)
        {
            return BCryptHelper.CheckPassword(candidatePassword, hashedPassword);
        }
    }
}
