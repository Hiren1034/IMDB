using IMDB_DBContexts.DBContext;
using IMDB_EntityModels.Common;
using IMDB_EntityModels.Models;
using IMDB_Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMBD_Repository
{
    public class MoviesRepo : IMoviesRepo
    {
        private readonly IMDBContext _dBContext;
        private readonly IConfiguration _configuration;

        public MoviesRepo(IMDBContext dBContext, IConfiguration configuration)
        {
            _dBContext = dBContext;
            _configuration = configuration;
        }

        public List<MoviesViewModel> GetAllMovies()
        {
            try
            {
                List<MoviesViewModel> lstMoviewViewModel = new List<MoviesViewModel>();

                string dbConnectionStr = _configuration.GetConnectionString("IMDBCon");
                var sqlPar = new SqlParameter[] { };

                var DS = Common.GetDataSet("[sp_GetAllMovies]", System.Data.CommandType.StoredProcedure, sqlPar, dbConnectionStr);
                if (DS != null && DS.Tables.Count > 0)
                {
                    lstMoviewViewModel = Common.ConvertDataTable<MoviesViewModel>(DS.Tables[0]).ToList();
                }
                return lstMoviewViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActorsViewModel> GetAllActors()
        {
            try
            {
                List<ActorsViewModel> lstActorsViewModel = new List<ActorsViewModel>();

                string dbConnectionStr = _configuration.GetConnectionString("IMDBCon");
                var sqlPar = new SqlParameter[] { };

                var DS = Common.GetDataSet("[sp_GetAllActors]", System.Data.CommandType.StoredProcedure, sqlPar, dbConnectionStr);
                if (DS != null && DS.Tables.Count > 0)
                {
                    lstActorsViewModel = Common.ConvertDataTable<ActorsViewModel>(DS.Tables[0]).ToList();
                }
                return lstActorsViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProducerViewModel> GetAllProducers()
        {
            try
            {
                List<ProducerViewModel> lstProducerViewModel = new List<ProducerViewModel>();

                string dbConnectionStr = _configuration.GetConnectionString("IMDBCon");
                var sqlPar = new SqlParameter[] { };

                var DS = Common.GetDataSet("[sp_GetAllProducers]", System.Data.CommandType.StoredProcedure, sqlPar, dbConnectionStr);
                if (DS != null && DS.Tables.Count > 0)
                {
                    lstProducerViewModel = Common.ConvertDataTable<ProducerViewModel>(DS.Tables[0]).ToList();
                }
                return lstProducerViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid InsertActor(ActorsViewModel actorsViewModel)
        {
            try
            {
                var tblActor = new TblActor()
                {
                    ActorId = Guid.NewGuid(),
                    ActorName = actorsViewModel.ActorName,
                    ActorSex = actorsViewModel.ActorSex,
                    ActorDob = actorsViewModel.ActorDob,
                    ActorBio = actorsViewModel.ActorBio,
                    IsActive = true,
                    CreatedOn = DateTime.Now
                };
                _dBContext.Add(tblActor);
                _dBContext.SaveChanges();
                return tblActor.ActorId;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Guid InsertProducer(ProducerViewModel producerViewModel)
        {
            try
            {
                var tblProducer = new TblProducer()
                {
                    ProducerId = Guid.NewGuid(),
                    Name = producerViewModel.Name,
                    Sex = producerViewModel.Sex,
                    Dob = producerViewModel.Dob,
                    Bio = producerViewModel.Bio,
                    IsActive = true,
                    CreatedOn = DateTime.Now
                };
                _dBContext.Add(tblProducer);
                _dBContext.SaveChanges();
                return tblProducer.ProducerId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertMovie(MoviesViewModel moviesViewModel)
        {
            try
            {
                List<ActorsViewModel> lstActorsViewModels = new List<ActorsViewModel>();
                foreach (var id in moviesViewModel.ActorsId)
                {
                    ActorsViewModel actorsViewModels = new ActorsViewModel();
                    actorsViewModels.ActorId = Guid.Parse(id);
                    lstActorsViewModels.Add(actorsViewModels);
                }
                var entityActorToDataTable = TransferActorsViewModelToDataTable(lstActorsViewModels);
                var entityMovieToDataTable = TransferMovieViewModelToDataTable(moviesViewModel);
                bool result = false;
                
                if (entityMovieToDataTable != null && entityMovieToDataTable.Rows.Count > 0)
                {
                    SqlParameter[] SqlParams = new SqlParameter[2];
                    SqlParams[0] = new SqlParameter("@udtt_Movie", entityMovieToDataTable) { SqlDbType = SqlDbType.Structured };
                    SqlParams[1] = new SqlParameter("@udtt_Actors", entityActorToDataTable) { SqlDbType = SqlDbType.Structured };

                    string dbConnectionStr = _configuration.GetConnectionString("IMDBCon");
                    var DS = Common.GetDataSet("[sp_InsertMovie]", System.Data.CommandType.StoredProcedure, SqlParams, dbConnectionStr);
                    if (DS != null && DS.Tables.Count > 0)
                    {
                        result = Convert.ToBoolean(DS.Tables[0].Rows[0][0].ToString());
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateMovie(MoviesViewModel moviesViewModel)
        {
            try
            {
                List<ActorsViewModel> lstActorsViewModels = new List<ActorsViewModel>();
                foreach (var id in moviesViewModel.ActorsId)
                {
                    ActorsViewModel actorsViewModels = new ActorsViewModel();
                    actorsViewModels.ActorId = Guid.Parse(id);
                    lstActorsViewModels.Add(actorsViewModels);
                }
                var entityActorToDataTable = TransferActorsViewModelToDataTable(lstActorsViewModels);
                var entityMovieToDataTable = TransferMovieViewModelToDataTable(moviesViewModel);
                bool result = false;

                if (entityMovieToDataTable != null && entityMovieToDataTable.Rows.Count > 0)
                {
                    SqlParameter[] SqlParams = new SqlParameter[2];
                    SqlParams[0] = new SqlParameter("@udtt_Movie", entityMovieToDataTable) { SqlDbType = SqlDbType.Structured };
                    SqlParams[1] = new SqlParameter("@udtt_Actors", entityActorToDataTable) { SqlDbType = SqlDbType.Structured };

                    string dbConnectionStr = _configuration.GetConnectionString("IMDBCon");
                    var DS = Common.GetDataSet("[sp_UpdateMovie]", System.Data.CommandType.StoredProcedure, SqlParams, dbConnectionStr);
                    if (DS != null && DS.Tables.Count > 0)
                    {
                        result = Convert.ToBoolean(DS.Tables[0].Rows[0][0].ToString());
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMovie(Guid movieId)
        {
            try
            {
                bool result = false;
                if(movieId != Guid.Empty)
                {
                    var movie = _dBContext.TblMovies.Where(m => m.MovieId == movieId && m.IsActive == true).FirstOrDefault();
                    if (movie != null)
                    {
                        movie.IsActive = false;
                        result = true;
                    }
                }
                _dBContext.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable TransferActorsViewModelToDataTable(List<ActorsViewModel> lstActorsViewModels)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Actor_Id", typeof(Guid)));

            foreach (var item in lstActorsViewModels)
            {
                DataRow DrItem = dt.NewRow();
                DrItem["Actor_Id"] = item.ActorId;
                dt.Rows.Add(DrItem);
            }
            return dt;
        }

        private DataTable TransferMovieViewModelToDataTable(MoviesViewModel moviesViewModel)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Movie_Id", typeof(Guid)));
            dt.Columns.Add(new DataColumn("Producer_Id", typeof(Guid)));
            dt.Columns.Add(new DataColumn("MovieName", typeof(string)));
            dt.Columns.Add(new DataColumn("MovieReleaseYear", typeof(string)));
            dt.Columns.Add(new DataColumn("MoviePlot", typeof(string)));
            dt.Columns.Add(new DataColumn("MoviePoster", typeof(string)));
            dt.Columns.Add(new DataColumn("IsActive", typeof(bool)));
            dt.Columns.Add(new DataColumn("CreatedOn", typeof(DateTime)));

            DataRow DrItem = dt.NewRow();
            if (moviesViewModel.MovieId != Guid.Empty)
            {
                DrItem["Movie_Id"] = moviesViewModel.MovieId;
            }
            else
            {
                DrItem["Movie_Id"] = Guid.NewGuid();
            }
            DrItem["Producer_Id"] = moviesViewModel.ProducerId;
            DrItem["MovieName"] = moviesViewModel.MovieName;
            DrItem["MovieReleaseYear"] = moviesViewModel.MovieReleaseYear;
            DrItem["MoviePlot"] = moviesViewModel.MoviePlot;
            DrItem["MoviePoster"] = moviesViewModel.MoviePoster;
            DrItem["IsActive"] = false;
            DrItem["CreatedOn"] = DateTime.Now;
            dt.Rows.Add(DrItem);
            return dt;
        }
    }
}
