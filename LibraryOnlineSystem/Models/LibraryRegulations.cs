using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryOnlineSystem.Models
{
    [Table("LibraryRegulations")]
    public class LibraryRegulations
    {
        [Key]
        public int LibraryRegulationsId { get; set; }
        public int BorrowTime { get; set; }
        public int Fine { get; set; }

    }
}