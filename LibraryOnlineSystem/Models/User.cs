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

        [Required(ErrorMessage = "Please Input user name"), MaxLengthAttribute(30)]
        public string Name{get; set;}

        [Required(ErrorMessage = "Please Input user surname"), MaxLengthAttribute(30)]
        public string Surname{get; set;}

        [Required(ErrorMessage = "Please Input Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth{get; set;}

        [Required(ErrorMessage = "Please Input user email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Input user House number")]
        [Range(0, 1000, ErrorMessage = "Enter number between 0 to 1000")]
        public int HouseNo{get; set;}

        [Required(ErrorMessage = "Please Input your zip code")]
        [RegularExpression(@"^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$", ErrorMessage = "This is not right")]
        public string ZipCode{get; set;}

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