using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskAPI.Models.DTO
{
    public class TSS_TicketsActionsDTO
    {
        public long ActionID { get; set; }
        public Nullable<long> TicketID { get; set; }
        [Required]
        public string msg { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<bool> IsSeen { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}