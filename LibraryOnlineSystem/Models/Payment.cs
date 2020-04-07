using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryOnlineSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int UserId { get; set; }

        public DateTime? DatePaid { get; set; }

        public int Amount { get; set; }

        public string Status { get; set; }

        public Booking Booking { get; set; }

        public void AddPayment()
        {
            throw new System.NotImplementedException();
        }

        public void ArchivePayment()
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePayment()
        {
            throw new System.NotImplementedException();
        }
    }
}