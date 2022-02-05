using IMDB_EntityModels.Models;
using IMDB_Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace IMDB_Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApiClient _apiClient;
		private IHostingEnvironment _hostingEnv;
		public MovieController(ApiClient apiClient, IHostingEnvironment env)
        {
            _apiClient = apiClient;
			_hostingEnv = env;
		}

        public IActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// Fetch All Movie for Show List of Movie in Selection
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetAllMovieList()
		{
			try
			{
				var response = await _apiClient.GetAsync("Movies/GetAllMoviesList");

				if (response.Code != (int)HttpStatusCode.OK)
				{
					return Ok(new { Message = string.Format(response.Message), Code = response.Code });
				}
				var lstMemberTypes = JsonConvert.DeserializeObject<IList<MoviesViewModel>>(response.Data.ToString());
				return Ok(new { Message = string.Format(response.Message), Code = response.Code, Data = lstMemberTypes });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Fetch All Producers for Show List of Producers in Selection
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetAllProducersList()
		{
			try
			{
				var response = await _apiClient.GetAsync("Movies/GetAllProducersList");

				if (response.Code != (int)HttpStatusCode.OK)
				{
					return Ok(new { Message = string.Format(response.Message), Code = response.Code });
				}
				var lstMemberTypes = JsonConvert.DeserializeObject<IList<ProducerViewModel>>(response.Data.ToString());
				return Ok(new { Message = string.Format(response.Message), Code = response.Code, Data = lstMemberTypes });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Fetch All Actors for Show List of Actors in Selection
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetAllActorsList()
		{
			try
			{
				var response = await _apiClient.GetAsync("Movies/GetAllActorsList");

				if (response.Code != (int)HttpStatusCode.OK)
				{
					return Ok(new { Message = string.Format(response.Message), Code = response.Code });
				}
				var lstMemberTypes = JsonConvert.DeserializeObject<IList<ActorsViewModel>>(response.Data.ToString());
				return Ok(new { Message = string.Format(response.Message), Code = response.Code, Data = lstMemberTypes });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Single Method for Insert Actors Details from Web Request
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> InsertActor()
		{
			try
			{
				var actorobj = HttpContext.Request.Form["ACTORREQUEST"];
				var actorRequest = JsonConvert.DeserializeObject<ActorsViewModel>(actorobj);
				actorRequest.IsActive = true;
				actorRequest.CreatedOn = DateTime.Now;
				var response = await _apiClient.PostAsync(actorRequest, "Movies/InsertActor");

				if (response.Code != (int)HttpStatusCode.OK)
				{
					return Ok(new { Message = string.Format(response.Message), Code = response.Code });
				}
				return Ok(new { Message = string.Format(response.Message), Code = response.Code });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Single Method for Insert Producers Details from Web Request
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> InsertProducer()
		{
			try
			{
				var producerobj = HttpContext.Request.Form["PRODUCERREQUEST"];
				var producerRequest = JsonConvert.DeserializeObject<ProducerViewModel>(producerobj);
				producerRequest.IsActive = true;
				producerRequest.CreatedOn = DateTime.Now;
				var response = await _apiClient.PostAsync(producerRequest, "Movies/InsertProducer");

				if (response.Code != (int)HttpStatusCode.OK)
				{
					return Ok(new { Message = string.Format(response.Message), Code = response.Code });
				}
				return Ok(new { Message = string.Format(response.Message), Code = response.Code });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Single Method for Insert Movie Details from Web Request
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> InsertMovie()
		{
			try
			{
				var movieobj = HttpContext.Request.Form["MOVIEREQUEST"];
				var imgMovie = HttpContext.Request.Form.Files["MOVIEIMGREQUEST"];
				var movieRequest = JsonConvert.DeserializeObject<MoviesViewModel>(movieobj);
				if (imgMovie != null)
				{
					if (imgMovie.Length > 0)
					{
						var fileName = Path.GetFileName(imgMovie.FileName);
						var fileExtension = Path.GetExtension(fileName);

						var webRoot = _hostingEnv.WebRootPath;
						var PathWithFolderName = System.IO.Path.Combine(webRoot, "MovieImage");
						if (Directory.Exists(PathWithFolderName))
						{
							// Try to create the directory.
							DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);

							string filePaths = Path.Combine(PathWithFolderName, fileName);
							using (var fileStream = new FileStream(filePaths, FileMode.Create))
							{
								imgMovie.CopyTo(fileStream);
							}
						}
					}
				}
				movieRequest.IsActive = true;
				movieRequest.CreatedOn = DateTime.Now;
                var response = await _apiClient.PostAsync(movieRequest, "Movies/InsertMovie");

                if (response.Code != (int)HttpStatusCode.OK)
                {
                    return Ok(new { Message = string.Format(response.Message), Code = response.Code });
                }
                return Ok(new { Message = string.Format(response.Message), Code = response.Code });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Single Method for Insert Movie Details from Web Request
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> UpdateMovie()
		{
			try
			{
				var movieobj = HttpContext.Request.Form["MOVIEREQUEST"];
				var imgMovie = HttpContext.Request.Form.Files["MOVIEIMGREQUEST"];
				var movieRequest = JsonConvert.DeserializeObject<MoviesViewModel>(movieobj);
				if (imgMovie != null)
				{
					if (imgMovie.Length > 0)
					{
						var fileName = Path.GetFileName(imgMovie.FileName);
						var fileExtension = Path.GetExtension(fileName);

						var webRoot = _hostingEnv.WebRootPath;
						var PathWithFolderName = System.IO.Path.Combine(webRoot, "MovieImage");
						if (Directory.Exists(PathWithFolderName))
						{ 
							// Try to create the directory.
							DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);

							string filePaths = Path.Combine(PathWithFolderName, fileName);
							using (var fileStream = new FileStream(filePaths, FileMode.Create))
							{
								imgMovie.CopyTo(fileStream);
							}
						}
					}
				}
				movieRequest.IsActive = true;
				movieRequest.CreatedOn = DateTime.Now;
				var response = await _apiClient.PostAsync(movieRequest, "Movies/UpdateMovie");

				if (response.Code != (int)HttpStatusCode.OK)
				{
					return Ok(new { Message = string.Format(response.Message), Code = response.Code });
				}
				return Ok(new { Message = string.Format(response.Message), Code = response.Code });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Single Method for Delete Movie Details from Web Request
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> DeleteMovie(Guid movieId)
		{
			try
			{
				//var movieId = HttpContext.Request.Form["MOVIEID"];
				var response = await _apiClient.GetAsync("movieId=" + movieId, "Movies/DeleteMovie");

				if (response.Code != (int)HttpStatusCode.OK)
				{
					return Ok(new { Message = string.Format(response.Message), Code = response.Code });
				}
				return Ok(new { Message = string.Format(response.Message), Code = response.Code });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
