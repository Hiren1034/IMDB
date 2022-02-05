using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_EntityModels.Models
{
    public class MovieParamsViewModel
    {
        public MoviesViewModel moviesViewModel { get; set; }    
        public List<string> ActorsId { get; set; }
     }
}
