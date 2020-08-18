using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
namespace LibraryOnlineSystem.Models
{
    public class User
    {

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Input user name"), MaxLengthAttribute(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Input user surname"), MaxLengthAttribute(30)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please Input Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please Input user email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote("isUserExists", "Home", HttpMethod = "POST", ErrorMessage = "Email address already registered.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Input user House number")]
        [Range(0, 1000, ErrorMessage = "Enter number between 0 to 1000")]
        public string HouseNo { get; set; }

        [Required(ErrorMessage = "Please Input your zip code")]
        [RegularExpression(@"^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$", ErrorMessage = "This is not right")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please Input user's role")]
        public string UserRole { get; set; }

        public bool? IsBanned { get; set; }
        public virtual List<PaymentLibrary> ListOfPayment { get; set; }

        public virtual List<BookReserve> ListOfReserves { get; set; }

        public virtual List<Booking> Bookings { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public virtual DateTime JoinDate { get; set; }

        public virtual List<BookReview> ListOfReviews { get; set; }

        [Required]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
            ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
            MinRequiredPasswordLength = 6
        )]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        // [NotMapped]
        // [DataType(DataType.Password)]
        // [CompareAttribute("Password",ErrorMessage = "Confirm password doesn't match, Type again!")]
        //public  string ConfirmPassword { get; set; }
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