using Microsoft.EntityFrameworkCore;
using StudyHub.Payment.Db;
using StudyHub.Payment.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Payment.Interfaces.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        public PaymentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private readonly DataContext _dataContext;

        public async Task<IEnumerable<PaymentModel>> GetAllAsync()
        {
            // NOTE: The ConfigureAwait is not strictly necessary as the only consumer of this method
            // is asp.net core based with the corresponding absence of a synchronization context
            // but since this class might be used by a different context in the future this is more flexible.
            return await _dataContext.Payments.ToListAsync().ConfigureAwait(false);
        }

        public Task<PaymentModel> GetByPaymentIdAsync(Guid paymentId)
        {
            return _dataContext.Payments.SingleOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        public void Add(PaymentModel payment)
        {
            _dataContext.Payments.Add(payment);
        }

        public Task PersistAsync()
        {
            return _dataContext.SaveChangesAsync();
        }


        public bool UpsetOrder(PaymentOrder order)
        {
            if(order.IsNewOrder)
            {
                try
                {
                    order.IsNewOrder = false;
                    _dataContext.PaymentOrders.Add(order);


                    return _dataContext.SaveChanges() == 1 ? true : false;

                }
                catch (Exception exp)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    _dataContext.Update(order);
                    return _dataContext.SaveChanges() == 1 ? true : false;



                }
                catch (Exception exp)
                {
                    return false;
                }
            }
           

        }
        


    }
}
