using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnlineSystem.Models
{
    public class BookReserve
    {
        public int ReservationId { get; set; }
        public int BookCodeId { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationRequestTime { get; set; }
        public DateTime EarliestDue { get; set; }

    }
}