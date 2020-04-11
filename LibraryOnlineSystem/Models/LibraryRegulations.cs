using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryOnlineSystem.Models
{
    [Table("LibraryRegulations")]
    public  class LibraryRegulations
    {
        [Key]
        public int LibraryRegulationsId { get; set; }
        public  int BorrowTime { get; set; }
        public  int Fine { get; set; }

    }
}