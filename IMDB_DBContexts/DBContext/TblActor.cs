using System;
using System.Collections.Generic;

#nullable disable

namespace IMDB_DBContexts.DBContext
{
    public partial class TblActor
    {
        public Guid ActorId { get; set; }
        public string ActorName { get; set; }
        public string ActorSex { get; set; }
        public DateTime? ActorDob { get; set; }
        public string ActorBio { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
