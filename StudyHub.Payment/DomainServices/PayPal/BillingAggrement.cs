using StudyHub.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudyHub.Payment.DomainServices.PayPal
{
    public class BillingAggrement : IPaypalConnector
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Aggrement Name
        /// </summary>
        [Required, MaxLength(128)] 
        public string Name { get; set; }
        [Required, MaxLength(128)]
        public string Description { get; set; }
        [Required]
        public DateTimeOffset Start_date { get; set; }
        public object Agreement_details { get; set; }
        [Required]
        public object Payer { get; set; }

    }
    public enum State
    {
        /// <summary>
        /// The agreement awaits initial payment completion.
        /// </summary>
        Pending,
        /// <summary>
        /// The agreement is active and payments are scheduled.
        /// </summary>
        Active,
        /// <summary>
        /// The agreement is suspended and payments are not scheduled until the agreement is reactivated.
        /// </summary>
        Suspended,
        /// <summary>
        /// The agreement is cancelled and payments are not scheduled.
        /// </summary>
        Cancelled,
        /// <summary>
        /// The agreement is expired and no more payments remain to be scheduled.
        /// </summary>
        Expired
    }
}
