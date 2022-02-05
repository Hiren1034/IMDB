using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_EntityModels.Models
{
    public class ActorsViewModel
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
