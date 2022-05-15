using StudyHub.Payment.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Payment.Interfaces
{
    public interface IMerchantRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Merchant>> GetAllAsync();

        Merchant GetByMerchantId(Guid merchantId);

        public Licence GetLicense(Guid merchantId);

        Task<Merchant> GetByMerchantIdAsync(Guid merchantId);

        void Add(Merchant merchant);

        /// <summary>
        /// Persist changed data to durable storage
        /// </summary>
        Task PersistAsync();
    }
}
