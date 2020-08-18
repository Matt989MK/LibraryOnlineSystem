using System;

namespace LibraryOnlineSystem.Models
{
    public class PaymentLibrary
    {
        public int PaymentLibraryId { get; set; }

        public int UserId { get; set; }

        public DateTime? DatePaid { get; set; }

        public int Amount { get; set; }

        public string Status { get; set; }

        public int BookingId { get; set; }
        public string? guId { get; set; }

        //public Booking Booking { get; set; }
    }
}