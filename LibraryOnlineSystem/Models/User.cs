using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Win32;

namespace LibraryOnlineSystem.Models
{
    public class User
    {
       
        public int UserId{get; set;}

        [Required(ErrorMessage = "Please Input user name")]
        public string Name{get; set;}

        [Required(ErrorMessage = "Please Input user surname")]
        public string Surname{get; set;}

        [Required(ErrorMessage = "Please Input Date of Birth")]
        public DateTime DateOfBirth{get; set;}

        [Required(ErrorMessage = "Please Input user email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Input user House number")]
        public int HouseNo{get; set;}

        [Required(ErrorMessage = "Please Input your zip code")]
        public int ZipCode{get; set;}

        [Required(ErrorMessage = "Please Input user's role")]

        public string UserRole{get; set;}

        public bool? IsBanned { get; set; }
        public virtual List<PaymentLibrary> ListOfPayment { get; set; }

      

        public virtual List<Booking> Bookings { get; set; }

        public  virtual List<BookReview> ListOfReviews { get; set; }

        [Required(ErrorMessage = "Please Input user password")]
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