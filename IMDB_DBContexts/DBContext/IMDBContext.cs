using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IMDB_DBContexts.DBContext
{
    public partial class IMDBContext : DbContext
    {
        public IMDBContext()
        {
        }

        public IMDBContext(DbContextOptions<IMDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblActor> TblActors { get; set; }
        public virtual DbSet<TblMovie> TblMovies { get; set; }
        public virtual DbSet<TblMovieActor> TblMovieActors { get; set; }
        public virtual DbSet<TblProducer> TblProducers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblActor>(entity =>
            {
                entity.HasKey(e => e.ActorId);

                entity.ToTable("tblActors");

                entity.Property(e => e.ActorId)
                    .ValueGeneratedNever()
                    .HasColumnName("Actor_Id");

                entity.Property(e => e.ActorDob)
                    .HasColumnType("datetime")
                    .HasColumnName("ActorDOB");

                entity.Property(e => e.ActorName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ActorSex)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMovie>(entity =>
            {
                entity.HasKey(e => e.MovieId);

                entity.ToTable("tblMovies");

                entity.Property(e => e.MovieId)
                    .ValueGeneratedNever()
                    .HasColumnName("Movie_Id");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.MovieReleaseYear)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProducerId).HasColumnName("Producer_Id");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMovieActor>(entity =>
            {
                entity.ToTable("tblMovieActors");

                entity.Property(e => e.ActorId).HasColumnName("Actor_Id");

                entity.Property(e => e.MovieId).HasColumnName("Movie_Id");
            });

            modelBuilder.Entity<TblProducer>(entity =>
            {
                entity.HasKey(e => e.ProducerId);

                entity.ToTable("tblProducers");

                entity.Property(e => e.ProducerId)
                    .ValueGeneratedNever()
                    .HasColumnName("Producer_Id");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
