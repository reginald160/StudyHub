using Microsoft.AspNetCore.Http;
using StudyHub.Domain.Models;
using StudyHub.Payment.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Payment.Interfaces
{
    public interface IPaymentRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PaymentModel>> GetAllAsync();

        Task<PaymentModel> GetByPaymentIdAsync(Guid paymentId);

        void Add(PaymentModel payment);

        bool UpsetOrder(PaymentOrder order);

        /// <summary>
        /// Persist changed data to durable storage
        /// </summary>
        Task PersistAsync();
    }

}
