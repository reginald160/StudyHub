using Microsoft.AspNetCore.Http;
using StudyHub.Payment.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.Interfaces
{
    public interface ILicenceRepository
    {
        string ActivateLicence(Guid merchantIdId, double duration, DateTime date);
        bool IsActiveLicence(out Licence licence, string key);
    }
}
