using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace BikeCommander.MotorBike.Core.Security
{
    class Authentication
    {

        private static string bikePath = @"D:\ebr2r\key.eb";
        private static string bikeSecret;
        internal static bool keyPresent;
        internal static string AuthKey()
        {
            if (!File.Exists(bikePath))
            {
                return null;
            }

            bikeSecret = File.ReadAllText(bikePath);
            return bikeSecret;
        }

        internal static void CheckKey()
        {
            KeyPresent();
        }

        private const int Keysize = 256;
        private const int DerivationIterations = 1000;

        public static bool Decrypt(string securityChallenge, string bikeSecret)
        {
            try
            {
                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(securityChallenge);
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

                using (var password = new Rfc2898DeriveBytes(bikeSecret, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    var plainTextBytes = new byte[cipherTextBytes.Length];
                                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    keyPresent = true;
                                    MotorBike.Core.MotorBikeCore.SendMessage(" Eastwood Bikes   Key Accepted");
                                    MotorBike.Core.MotorBikeCore.key = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                    return true;
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                MotorBike.Core.MotorBikeCore.SendMessage(" Eastwood Bikes   Invalid Key!");
                return false;
            }
        }
        public static bool KeyPresent()
        {

            if (Decrypt(MotorBike.Core.MainConstructor.CoreParams["BikeSecret"], AuthKey()) && AuthKey() != null)
            {
                return true;
            }

            return false;
        }
    }
}
