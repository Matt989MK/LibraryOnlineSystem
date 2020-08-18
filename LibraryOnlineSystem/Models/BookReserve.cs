using System;
namespace LibraryOnlineSystem.Models
{
    public class BookReserve
    {
        public int BookReserveId { get; set; }
        public int BookCodeId { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationRequestTime { get; set; }

        public User User
        { get; set; }

        //public Book Book
        //{ get; set; }
    }
}

//new DateTime(Math.Min(Date1.Ticks, Date2.Ticks))