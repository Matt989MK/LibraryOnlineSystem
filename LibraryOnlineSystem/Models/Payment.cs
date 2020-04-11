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

        public int BookingId { get; set; }

        //public Booking Booking { get; set; }
    }
}