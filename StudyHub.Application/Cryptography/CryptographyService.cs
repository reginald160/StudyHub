using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StudyHub.Application.Settings;
using StudyHub.Domain.Models;
using StudyHub.Payment.DomainServices;
using StudyHub.Payment.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.Cryptography
{
    public class CryptographyService : ICryptographyService
    {
        private readonly JwtSetting jwtSetting;
        private readonly PayPalSettings _payPalSettings;
        private PayPalResponseModel userCrdential { get; set; }
        public CryptographyService(IOptions<JwtSetting> options, IOptions<PayPalSettings> payPaloptions)
        {
            jwtSetting = options.Value;
            _payPalSettings = payPaloptions.Value;
        }
        private readonly int _hashSize = 32;
        private readonly int _hashIterations = 128;
        private const double EXPIRY_DURATION_MINUTES = 30;


        private byte[] CreateSalt()
        {
            byte[] salt;
            using (RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rNGCryptoServiceProvider.GetBytes(salt = new byte[_hashSize]);
            }
            return salt;
        }
        private byte[] CreateHash(string input, byte[] salt)
        {
            byte[] hash;
            using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(input, salt, _hashIterations))
            {
                hash = hashGenerator.GetBytes(_hashSize);
            }
            return hash;
        }
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public HashDetail GenerateHash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            byte[] salt = CreateSalt();
            byte[] hash = CreateHash(input, salt);

            return new HashDetail { Salt = Convert.ToBase64String(salt), HashedValue = Convert.ToBase64String(hash) };
        }

        public bool ValidateHash(string input, string salt, string hashedValue)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(hashedValue))
                return false;
            byte[] saltByte = Convert.FromBase64String(salt);
            byte[] inputHash = CreateHash(input, saltByte);
            string hashedString = Convert.ToBase64String(inputHash);

            if (hashedString.Equals(hashedValue))
                return true;

            return false;
        }
        public string GenerateInvitationCode(string email)
        {
            Random random = new Random();
            const string chars = "0123678901456789012678903456789";
            var randtext = new string(Enumerable.Repeat(chars, 3)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            var value = $"@{email} @Admin {randtext}";

            var refCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(value));
            return refCode;
        }

        public string BuildToken(string key, string issuer, AccountUser user)
        {
            var claims = new[] {
            new Claim(ClaimTypes.Name, user.EmailAddress),
            new Claim("UserId",user.UserId.ToString()),
             new Claim("LastName",user.LastName),
              new Claim("FirstName",user.FirstName),
              new Claim("Email",user.EmailAddress),
              new Claim("Role",user.Role),
            //new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier,
            Guid.NewGuid().ToString())
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public bool ValidateToken(string key, string issuer, string audience, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }


        public bool GetAccessToken(out string token)
        {

            var baseUrl = _payPalSettings.BaseAuthUrl;
            var grant_type = _payPalSettings.GrantType;
            var client_credentials = _payPalSettings.ClientCredential;
            var clientId = _payPalSettings.ClientId;
            var clientSecret = _payPalSettings.ClientScrete;


            using (var client = new HttpClient())
            {

                Uri baseUri = new Uri(baseUrl + "/v1/oauth2/token");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.ConnectionClose = true;

                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>(grant_type, client_credentials));
                var content = new FormUrlEncodedContent(values);
                //content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                var authenticationString = $"{clientId}:{clientSecret}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

                var requestMessage = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, baseUri);
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                requestMessage.Content = content;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback +=
        (sender, certificate, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //| SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var task = client.SendAsync(requestMessage);
                var response = task.Result;


                if (response.IsSuccessStatusCode)
                {
                    var readResponse = response.Content.ReadAsStringAsync();
                    userCrdential = JsonConvert.DeserializeObject<PayPalResponseModel>(readResponse.Result);
                    token = userCrdential.access_token;
                    token = userCrdential.access_token;

                    return true;
                }
                var error = response.Content.ReadAsAsync<System.Web.Http.HttpError>().Result.Message.ToString();
                new ErrorLog("Access Token", error);
                token = null;
                return false;


            }

        }


        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public UserClaim GetClaimsPrincipal(ClaimsIdentity claim)
        {
            try
            {
                IEnumerable<Claim> claims = claim.Claims;
                return new UserClaim
                {
                    UserId = Guid.Parse(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value),
                    LastName = claim.Claims.ToList().Find(idetity => idetity.Type == "LastName")?.Value,
                    FirstName = claim.Claims.ToList().Find(idetity => idetity.Type == "FirstName")?.Value,
                    EmailAddress = claim.Claims.ToList().Find(idetity => idetity.Type == "Email")?.Value,
                    Role = claim.Claims.ToList().Find(idetity => idetity.Type == "Role")?.Value,
                };

                // return principal;
            }
            catch (Exception)
            {

                return null;
            }
        }




    }
}
