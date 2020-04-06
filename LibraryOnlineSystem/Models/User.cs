using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace LibraryOnlineSystem.Models
{
    public class User
    {
        public int UserId
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Surname
        {
            get; set;
        }

        public DateTime DateOfBirth
        {
            get; set;
        }

        public string Email { get; set; }

        public int HouseNo
        {
            get; set;
        }

        public int ZipCode
        {
            get; set;
        }
    

        public string UserRole
        {
            get; set;
        }

        public bool? IsBanned { get; set; }
        public virtual List<Payment> ListOfPayment { get; set; }

      

        public virtual List<Booking> Bookings { get; set; }

        public  virtual List<BookReview> ListOfReviews { get; set; }

        public string Password { get; set; }

        public void AuthoriseUser()
        {
            throw new System.NotImplementedException();
        }

        public void GetUserById()
        {
            throw new System.NotImplementedException();
        }

        public void GetUserRole()
        {
            throw new System.NotImplementedException();
        }

        public void GetUserList()
        {
            throw new System.NotImplementedException();
        }

        public void AddUser()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser()
        {
            throw new System.NotImplementedException();
        }
    }

   
}