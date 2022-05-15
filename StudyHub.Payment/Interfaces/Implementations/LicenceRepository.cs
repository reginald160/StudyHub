using Microsoft.AspNetCore.Http;
using StudyHub.Payment.Db;
using StudyHub.Payment.DomainServices;
using StudyHub.Payment.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace StudyHub.Payment.Interfaces.Implementations
{
    public class LicenceRepository : ILicenceRepository
    {
        private readonly DataContext _dataContext;
        public LicenceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


      
        public string ActivateLicence(Guid merchantIdId, double duration, DateTime date)
        {
            var dd = "Property_Sale_Transaction".ToUpper();
            try
            {
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {
                    var expiryDate = date.AddMinutes(duration);
                    var IsActive = "YES";
                    var seed = $"@{merchantIdId.ToString()} @{duration} @{date.ToString()} @{expiryDate.ToString()} @{IsActive}"; 
                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    var key = myRijndael.Key;
                    var iv = myRijndael.IV;
                    // Encrypt the string to an array of bytes.

                    byte[] licenceKey = EncryptStringToBytes(seed, key, iv);
                    var lKey = RandomClass.GenerateLicenceKey("Name");

                    var license = new Licence()
                    {
                        MerchantIdId = merchantIdId,
                        ActivationKey = key,
                        ActivationIV = iv,
                        Token = licenceKey,
                        LicenceKey = lKey
                    };

                    _dataContext.Licences.Add(license);
                    _dataContext.SaveChanges();


                    return license.LicenceKey;
                }
            }


            catch
            {
                return null;
            }
            
        }

        public bool IsActiveLicence( out Licence licence, string key)
        {
            var dd = "Property_Sale_Transaction".ToUpper();
            try
            {
                var entity = _dataContext.Licences.FirstOrDefault(x => x.LicenceKey.Contains(key));
                if(entity == null)
                {
                    licence = null;
                    return false;            
                }

                string stringKey = DecryptStringFromBytes(entity.Token, entity.ActivationKey, entity.ActivationIV);

                MatchCollection matches = Regex.Matches(stringKey, @"\@\b\S+[a-zA-Z0-9_-]");
                
                Guid merchantIdId; double duration; DateTime date;  DateTime expiryDate; string IsActive;
                if (matches != null && matches.Count > 0 && 1 <= matches.Count)
                {
                    merchantIdId = new Guid(matches[0].ToString().Substring(1));
                    duration = Convert.ToDouble(matches[1].ToString().Substring(1));
                    date = DateTime.Parse(matches[2].ToString().Substring(1));
                    expiryDate = DateTime.Parse(matches[3].ToString().Substring(1));
                    IsActive = matches[3].ToString().Substring(1);
                   
                    var diff = expiryDate - DateTime.Now;
                    if (diff.TotalDays < 0 || IsActive != "YES")
                    {
                        licence = null;
                        return false;
                    }

                }

                licence = entity;
                return true;
            }
            catch
            {
                licence = null;
                return false;
            }

        }



        byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

         string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }



    }
}
