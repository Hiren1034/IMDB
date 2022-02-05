using System;
using System.Collections.Generic;

#nullable disable

namespace IMDB_DBContexts.DBContext
{
    public partial class TblMovieActor
    {
        public int Id { get; set; }
        public Guid? MovieId { get; set; }
        public Guid? ActorId { get; set; }
    }
}
