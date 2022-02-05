using IMDB_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using IMDB_EntityModels.Resources;
using IMDB_EntityModels.Models;

namespace IMDB_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepo _moviesRepo;
        
        public MoviesController(IMoviesRepo moviesRepo)
        {
            _moviesRepo = moviesRepo;
        }

        /// <summary>
		/// Fetch All Movie for Show List of Movie in Selection
		/// </summary>
		/// <returns></returns>
        [HttpGet("GetAllMoviesList")]
        public IActionResult GetAllMoviesList()
        {
            try
            {
                var lstMovies = _moviesRepo.GetAllMovies();
                if (lstMovies != null && lstMovies.Count > 0)
                {
                    return Ok(new { Message = string.Format(CommonResource.GetList, "Movie"), Code = (int)HttpStatusCode.OK, Data = lstMovies });
                }
                else
                {
                    return NotFound(new { Message = string.Format(CommonResource.NotFound,"Movie"), Code = (int)HttpStatusCode.NotFound });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Code = (int)HttpStatusCode.InternalServerError });
            }
        }

        /// <summary>
		/// Fetch All Actors for Show List of Actors in Selection
		/// </summary>
		/// <returns></returns>
        [HttpGet("GetAllActorsList")]
        public IActionResult GetAllActorsList()
        {
            try
            {
                var lstActors = _moviesRepo.GetAllActors();
                if (lstActors != null && lstActors.Count > 0)
                {
                    return Ok(new { Message = string.Format(CommonResource.GetList, "Actor"), Code = (int)HttpStatusCode.OK, Data = lstActors });
                }
                else
                {
                    return NotFound(new { Message = string.Format(CommonResource.NotFound, "Actor"), Code = (int)HttpStatusCode.NotFound });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Code = (int)HttpStatusCode.InternalServerError });
            }
        }

        /// <summary>
		/// Fetch All Producers for Show List of Producers in Selection
		/// </summary>
		/// <returns></returns>
        [HttpGet("GetAllProducersList")]
        public IActionResult GetAllProducersList()
        {
            try
            {
                var lstProducers = _moviesRepo.GetAllProducers();
                if (lstProducers != null && lstProducers.Count > 0)
                {
                    return Ok(new { Message = string.Format(CommonResource.GetList, "Producer"), Code = (int)HttpStatusCode.OK, Data = lstProducers });
                }
                else
                {
                    return NotFound(new { Message = string.Format(CommonResource.NotFound, "Producer"), Code = (int)HttpStatusCode.NotFound });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Code = (int)HttpStatusCode.InternalServerError });
            }
        }

        /// <summary>
		/// Single Method for Insert Actor Details from Web Request
		/// </summary>
		/// <returns></returns>
        [HttpPost("InsertActor")]
        public IActionResult InsertActor(ActorsViewModel actorsViewModel)
        {
            try
            {
                var actorId = _moviesRepo.InsertActor(actorsViewModel);
                if (actorId != Guid.Empty)
                {
                    return Ok(new { Message = CommonResource.Insert, Code = (int)HttpStatusCode.OK });
                }
                else
                {
                    return NotFound(new { Message = CommonResource.Error_Insert, Code = (int)HttpStatusCode.InternalServerError });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Code = (int)HttpStatusCode.InternalServerError });
            }
        }

        /// <summary>
		/// Single Method for Insert Producer Details from Web Request
		/// </summary>
		/// <returns></returns>
        [HttpPost("InsertProducer")]
        public IActionResult InsertProducer(ProducerViewModel producerViewModel)
        {
            try
            {
                var producerId = _moviesRepo.InsertProducer(producerViewModel);
                if (producerId != Guid.Empty)
                {
                    return Ok(new { Message = CommonResource.Insert, Code = (int)HttpStatusCode.OK });
                }
                else
                {
                    return NotFound(new { Message = CommonResource.Error_Insert, Code = (int)HttpStatusCode.InternalServerError });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Code = (int)HttpStatusCode.InternalServerError });
            }
        }

        /// <summary>
		/// Single Method for Insert Movie Details from Web Request
		/// </summary>
		/// <returns></returns>
        [HttpPost("InsertMovie")]
        public IActionResult InsertMovie(MoviesViewModel moviesViewModel)
        {
            try
            {
                var result = _moviesRepo.InsertMovie(moviesViewModel);
                if (result)
                {
                    return Ok(new { Message = CommonResource.Insert, Code = (int)HttpStatusCode.OK });
                }
                else
                {
                    return NotFound(new { Message = CommonResource.Error_Insert, Code = (int)HttpStatusCode.InternalServerError });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Code = (int)HttpStatusCode.InternalServerError });
            }
        }

        /// <summary>
		/// Single Method for Update Movie Details from Web Request
		/// </summary>
		/// <returns></returns>
        [HttpPost("UpdateMovie")]
        public IActionResult UpdateMovie(MoviesViewModel moviesViewModel)
        {
            try
            {
                var result = _moviesRepo.UpdateMovie(moviesViewModel);
                if (result)
                {
                    return Ok(new { Message = CommonResource.Update, Code = (int)HttpStatusCode.OK });
                }
                else
                {
                    return NotFound(new { Message = CommonResource.Error_Edit, Code = (int)HttpStatusCode.InternalServerError });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Code = (int)HttpStatusCode.InternalServerError });
            }
        }

        /// <summary>
		/// Single Method for Delete Movie Details from Web Request
		/// </summary>
		/// <returns></returns>
        [HttpGet("DeleteMovie")]
        public IActionResult DeleteMovie(Guid movieId)
        {
            try
            {
                var result = _moviesRepo.DeleteMovie(movieId);
                if (result)
                {
                    return Ok(new { Message = CommonResource.Delete, Code = (int)HttpStatusCode.OK });
                }
                else
                {
                    return NotFound(new { Message = CommonResource.Erorr_Delete, Code = (int)HttpStatusCode.InternalServerError });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Code = (int)HttpStatusCode.InternalServerError });
            }
        }
    }
}
