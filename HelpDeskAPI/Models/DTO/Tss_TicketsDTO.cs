using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDeskAPI.Models.DTO
{
    public class TSS_TicketsDTO
    {
        public long TicketsID { get; set; }
        public string TicketsSubject { get; set; }
        public System.DateTime TicketsCreatedTime { get; set; }

        public long Tickets_TypeID { get; set; }
        public string TicketTypeDesc { get; set; }

        public long Tickets_StatusID { get; set; }
        public string TicketStatusDesc { get; set; }

        public long Tickets_Priority_ID { get; set; }
        public string TicketPriorityDesc { get; set; }

        public string FgColor { get; set; }
        public string BgColor { get; set; }

        public long Tickets_Client_ID { get; set; }
        public string TicketClientName { get; set; }

        public Nullable<long> Tickets_Mod_ID { get; set; }
        public string ModuleDesc { get; set; }

        public Nullable<long> Tickets_Smod_ID { get; set; }
        public Nullable<long> Tickets_DeveloperID { get; set; }
        public string TicketsAction { get; set; }
        public string TicketsSummery { get; set; }
        public string TicketsDesc { get; set; }
        public Nullable<bool> IsClosed { get; set; }
        public Nullable<System.DateTime> ClosedTime { get; set; }
        public Nullable<bool> IsSolved { get; set; }
        public Nullable<System.DateTime> SolvedTime { get; set; }
        public Nullable<bool> IsArchived { get; set; }
        public Nullable<long> TakenFromClientUserID { get; set; }
        public string TakenFromClientUserFullName { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<long> CreatedByID { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<long> UpdatedByID { get; set; }
        public Nullable<int> UpdatedCounts { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<long> DeletedByID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        //public virtual List<TSS_TicketsActionsDTO> TSS_TicketsActions { get; set; }
        //public virtual List<TSS_TicketsAttachedFilesDTO> TSS_TicketsAttachedFiles { get; set; }
    }
}