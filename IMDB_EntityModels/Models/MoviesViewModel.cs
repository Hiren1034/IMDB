using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IMDB_EntityModels.Models
{
    public class MoviesViewModel
    {
        public Guid MovieId { get; set; }
        public Guid ProducerId { get; set; }
        public string MovieName { get; set; }
        public string MovieReleaseYear { get; set; }
        public string MoviePlot { get; set; }
        public string MoviePoster { get; set; }
        public string ProducerName { get; set; }
        public string Actors { get; set; }
        public List<string> ActorsId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
