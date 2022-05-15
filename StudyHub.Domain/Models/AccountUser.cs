using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Models
{
    public class AccountUser  : NumenclatureModel
    {
        public AccountUser()
        {
            IsDeleted = false;
            IsActive = true;
            IsEmailVerified = false;
            IsProfiled = false;
        }
        public Guid UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string HashPasswordSalt { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Imageurl { get; set; }
        public bool IsEmailVerified { get; set; }
        public string RefereeCode { get; set; }
        public string ReferalCode { get; set; }
        public string InvitationCode { get; set; }

        public bool IsActive { get; set; }
        public bool IsProfiled { get; set; }

    }
}
