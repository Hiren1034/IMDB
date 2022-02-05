using System;
using System.Collections.Generic;

#nullable disable

namespace IMDB_DBContexts.DBContext
{
    public partial class Producer
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
