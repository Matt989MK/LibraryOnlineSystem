using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryOnlineSystem
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int BookCodeId { get; set; }
        public int userId { get; set; }
        public User User
        { get; set; }

        public Book Book
        { get; set; }

        public DateTime DateCreated
        { get; set; }

        public DateTime DateDue
        { get; set; }

        public bool IsOverdue
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