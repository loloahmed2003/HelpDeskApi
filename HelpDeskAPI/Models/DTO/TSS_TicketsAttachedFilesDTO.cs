using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDeskAPI.Models.DTO
{
    public class TSS_TicketsAttachedFilesDTO
    {
        public long FilesID { get; set; }
        public Nullable<long> Files_TicketsID { get; set; }
        public Nullable<long> ActionID { get; set; }
        public string FilesName { get; set; }
        public string FilesTypes { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<long> CreatedByID { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<long> UpdatedByID { get; set; }
        public int UpdatedCounts { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<long> DeletedByID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}