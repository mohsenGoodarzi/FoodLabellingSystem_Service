using System.Security.Cryptography;
using System.Text;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Others
{
    public class HashHelper: IHashHelper
    {
        private string specialChars;

        public HashHelper() {
            specialChars = "Ω≤∫∂¬∆";
        }
        public string Hash512(string code)
        {

            byte[] byteCode = Encoding.ASCII.GetBytes(code);

            SHA512 shaM = SHA512.Create();
            byte[] hashedCode = shaM.ComputeHash(byteCode);

            StringBuilder stringBuilder = new StringBuilder(hashedCode.Length * 2);

            foreach (byte hashedByte in hashedCode)
            {
                stringBuilder.Append(hashedByte.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Generates Random Activation Code
        /// </summary>
        /// <returns>string</returns>
        public string GenerateRAC() {
            var random = new Random();
            int code = random.Next(100000, 999999);
        return code.ToString();
        }

        /// <summary>
        /// Hashes RAC(Random Activation Code) that contains special characters. 
        /// </summary>
        /// <returns> Hashed RAC : string</returns>
        public string HashRAC(string code) {
            

            byte[] salt = Encoding.ASCII.GetBytes(specialChars);
            byte[] codes = Encoding.ASCII.GetBytes(code);
            byte[] saltedCodes = new byte[12];
            for (int index = 0; index < code.Length; index = index + 2)
            {
                saltedCodes[index] = codes[index];
                saltedCodes[index + 1] = salt[index];
            }
            SHA512 shaM = SHA512.Create();
            byte[] hashedCode = shaM.ComputeHash(saltedCodes);

            StringBuilder stringBuilder = new StringBuilder(hashedCode.Length * 2);

            foreach (byte hashedByte in hashedCode)
            {
                stringBuilder.Append(hashedByte.ToString("X2"));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generates Hashed Request for Reset Password
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string generateHRPR(string userName) {

            byte[] salt = Encoding.ASCII.GetBytes(specialChars);
            byte[] codes = Encoding.ASCII.GetBytes(userName);
            byte[] saltedCodes = new byte[specialChars.Length+ userName.Length];

            if (userName.Length > specialChars.Length)
            {
                for (int index = 0; index < userName.Length; index = index + 2)
                {
                    saltedCodes[index] = codes[index];
                    if (salt.Length > index) {
                        saltedCodes[index + 1] = salt[index];
                    }
                    
                }
            }
            else {
                for (int index = 0; index < specialChars.Length; index = index + 2)
                {
                    if (codes.Length > index) {
                        saltedCodes[index] = codes[index];
                    }
                    saltedCodes[index + 1] = salt[index];
                }
            }
            SHA512 sha = SHA512.Create();
            byte[] hashedCode = sha.ComputeHash(saltedCodes);

            StringBuilder stringBuilder = new StringBuilder(hashedCode.Length * 2);

            foreach (byte hashedByte in hashedCode)
            {
                stringBuilder.Append(hashedByte.ToString("X2"));
            }
            return stringBuilder.ToString();
           
        }
    }
}
