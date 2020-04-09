using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryOnlineSystem.Models
{
    public class BookCode
    {
        public int BookCodeId { get; set; }
        [Required(ErrorMessage = "Please Input book Id")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Please Input book book serial number")]
        public string BookSerialNumber { get; set; }
        public List<BookReserve> BookReserves { get; set; }
        public bool IsInLibrary { get; set; }

    }
}