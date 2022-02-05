using System;
using System.Collections.Generic;

#nullable disable

namespace IMDB_DBContexts.DBContext
{
    public partial class TblMovie
    {
        public Guid MovieId { get; set; }
        public Guid ProducerId { get; set; }
        public string MovieName { get; set; }
        public string MovieReleaseYear { get; set; }
        public string MoviePlot { get; set; }
        public string MoviePoster { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
