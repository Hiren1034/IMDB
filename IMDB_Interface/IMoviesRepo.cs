using IMDB_EntityModels.Models;
using System;
using System.Collections.Generic;

namespace IMDB_Interface
{
    public interface IMoviesRepo
    {
        List<MoviesViewModel> GetAllMovies();
        List<ActorsViewModel> GetAllActors();
        List<ProducerViewModel> GetAllProducers();
        Guid InsertActor(ActorsViewModel actorsViewModel);
        Guid InsertProducer(ProducerViewModel producerViewModel);
        bool InsertMovie(MoviesViewModel moviesViewModel);
        bool UpdateMovie(MoviesViewModel moviesViewModel);
        bool DeleteMovie(Guid movieId);
    }
}
