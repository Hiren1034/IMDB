using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_EntityModels.Models
{
    public class ProducerViewModel
    {
        public Guid ProducerId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime? Dob { get; set; }
        public string Bio { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
