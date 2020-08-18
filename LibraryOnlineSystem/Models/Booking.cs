using System;

namespace LibraryOnlineSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int BookCodeId { get; set; }
        public int UserId { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        public User User
        { get; set; }

        public DateTime DateCreated
        { get; set; }

        public DateTime? DateReturned
        { get; set; }




        //public void AddBooking()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void DeleteBooking()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void UpdateBooking()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void GetBookingByUserId()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void GetUserById()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}