using StudyHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.Cryptography
{
   public interface ICryptographyService
    {
        HashDetail GenerateHash(string input);
        bool ValidateHash(string input, string salt, string hashedValue);
        string Base64Encode(string plainText);
        string Base64Decode(string base64EncodedData);
        string GenerateInvitationCode(string email);
        string BuildToken(string key, string issuer, AccountUser user);
        bool ValidateToken(string key, string issuer, string audience, string token);
        UserClaim GetClaimsPrincipal(ClaimsIdentity claims);
        string encrypt(string text);
        string Decrypt(string cipherText);
        bool GetAccessToken(out string token);
        //UserClaim GetClaimsPrincipal(string token);

    }
}
