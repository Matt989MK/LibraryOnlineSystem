using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryOnlineSystem
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int UserId
        { get; set; }

        public int BookId
        { get; set; }

        public DateTime DateCreated
        { get; set; }

        public DateTime DateDue
        { get; set; }

        public int IsOverdue
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