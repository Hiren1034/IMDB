USE [master]
GO
/****** Object:  Database [IMDB_DB]    Script Date: 06-02-2022 01:15:50 AM ******/
CREATE DATABASE [IMDB_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IMDB_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\IMDB_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IMDB_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\IMDB_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [IMDB_DB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IMDB_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IMDB_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IMDB_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IMDB_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IMDB_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IMDB_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [IMDB_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IMDB_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IMDB_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IMDB_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IMDB_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IMDB_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IMDB_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IMDB_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IMDB_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IMDB_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IMDB_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IMDB_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IMDB_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IMDB_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IMDB_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IMDB_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IMDB_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IMDB_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [IMDB_DB] SET  MULTI_USER 
GO
ALTER DATABASE [IMDB_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IMDB_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IMDB_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IMDB_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IMDB_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IMDB_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'IMDB_DB', N'ON'
GO
ALTER DATABASE [IMDB_DB] SET QUERY_STORE = OFF
GO
USE [IMDB_DB]
GO
/****** Object:  UserDefinedTableType [dbo].[udtt_Actors]    Script Date: 06-02-2022 01:15:50 AM ******/
CREATE TYPE [dbo].[udtt_Actors] AS TABLE(
	[Actor_Id] [uniqueidentifier] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[udtt_Movie]    Script Date: 06-02-2022 01:15:50 AM ******/
CREATE TYPE [dbo].[udtt_Movie] AS TABLE(
	[Movie_Id] [uniqueidentifier] NOT NULL,
	[Producer_Id] [uniqueidentifier] NOT NULL,
	[MovieName] [nvarchar](max) NULL,
	[MovieReleaseYear] [varchar](50) NULL,
	[MoviePlot] [nvarchar](max) NULL,
	[MoviePoster] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL
)
GO
/****** Object:  Table [dbo].[tblActors]    Script Date: 06-02-2022 01:15:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblActors](
	[Actor_Id] [uniqueidentifier] NOT NULL,
	[ActorName] [varchar](500) NULL,
	[ActorSex] [varchar](50) NULL,
	[ActorDOB] [datetime] NULL,
	[ActorBio] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_tblActors] PRIMARY KEY CLUSTERED 
(
	[Actor_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMovieActors]    Script Date: 06-02-2022 01:15:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMovieActors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Movie_Id] [uniqueidentifier] NULL,
	[Actor_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_tblMovieActors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMovies]    Script Date: 06-02-2022 01:15:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMovies](
	[Movie_Id] [uniqueidentifier] NOT NULL,
	[Producer_Id] [uniqueidentifier] NOT NULL,
	[MovieName] [nvarchar](max) NULL,
	[MovieReleaseYear] [varchar](50) NULL,
	[MoviePlot] [nvarchar](max) NULL,
	[MoviePoster] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_tblMovies] PRIMARY KEY CLUSTERED 
(
	[Movie_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProducers]    Script Date: 06-02-2022 01:15:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProducers](
	[Producer_Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](500) NULL,
	[Sex] [varchar](50) NULL,
	[DOB] [datetime] NULL,
	[Bio] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_tblProducers] PRIMARY KEY CLUSTERED 
(
	[Producer_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'9d46d43d-b2d3-4629-a823-1cb4b8f8aab3', N'Anand Babu', N'Male', CAST(N'1963-08-30T00:00:00.000' AS DateTime), N'Anand Babu is an Indian film actor who is working in Tamil and Telugu films. He is the son of legendary comedy actor Nagesh.', 1, CAST(N'2022-02-03T09:49:24.287' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'638d559b-6e31-4c60-8623-444dbaa017a5', N'Dhanush', N'Male', CAST(N'1983-02-25T00:00:00.000' AS DateTime), N'Venkatesh Prabhu known by his stage name Dhanush, is an Indian film actor, producer, lyricist and playback singer who has worked predominantly in Tamil cinema. In 2011, he won the National Film Award for Best Actor for the Tamil film "Aadukalam" and in the same year, he received international attention for his song "Why This Kolaveri Di", which went on to became the most viewed Indian song in YouTube.As of 2014, he has won five Filmfare Awards.', 1, CAST(N'2022-02-03T09:10:47.727' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'a0b46281-098d-4332-ae24-5ccfe564a4c5', N'Seema Biswas', N'Female', CAST(N'1965-01-14T00:00:00.000' AS DateTime), N'Seema Biswas was born on January 14, 1965 in Nalbari, Assam, India. She is an actress, known for Bandit Queen (1994), Water (2005) and Khamoshi the Musical (1996).', 1, CAST(N'2022-02-03T09:49:24.287' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'bdb02e39-a8cd-4f7f-a292-67afbd002137', N'Rashmika Mandanna', N'Female', CAST(N'1996-04-05T00:00:00.000' AS DateTime), N'Rashmika Mandanna (Born on 5th April 1996) is an Indian actress who has and is working in Kannada, Tamil, Telugu & Hindi films and is rightly been called, ''Pan-India Actress''. She enjoys a huge fanbase across the nation and has been bestowed with the title, ''National Crush of India''.', 1, CAST(N'2022-02-05T20:07:15.197' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'2d95c392-e955-49ab-92a2-7f45c638a5df', N'Fahadh Faasil', N'Male', CAST(N'1982-08-08T00:00:00.000' AS DateTime), N'Fahadh is the son of Malayalam film director Fazil, and his wife Rozina. He has two sisters, Ahameda and Fatima, and a brother, Farhaan Faasil. He completed his schooling from SDV Central School Alleppey, Lawrence School Ooty and Choice School Thripunithara. He went on to pursue his degree from Sanatana Dharma College Alleppey and M.A. in philosophy from the University of Miami.', 1, CAST(N'2022-02-05T20:08:23.317' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'4e5f9ca4-8840-41a3-99f3-808f1ec0dd3f', N'Vijay Kumar', N'Male', CAST(N'1962-04-12T00:00:00.000' AS DateTime), N'Vijay Kumar is an actor in Indian cinema and theater. He is originally from Patna district of Bihar but lives in Mumbai. He is an acting trained from National School of Drama (NSD), New Delhi & Film and Television Institute of India (FTII), Pune. He has also studied in Central School of Speech & Drama, London granted by Charles Wallace India Trust. Mr. Kumar is equally active in theater and cinema genres.', 1, CAST(N'2022-02-03T09:49:24.287' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'708ce3f6-0935-4561-b9e5-99cd164ef3c4', N'Dimple Hayathi', N'Female', CAST(N'1998-08-21T00:00:00.000' AS DateTime), N'Dimple Hayathi (born as Dimple) is an Indian actress who predominantly works in Telugu and Tamil films.', 1, CAST(N'2022-02-03T09:49:24.287' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'1575b3dd-d3ef-4da3-87fe-ad5494166f48', N'Akshay Kumar', N'Male', CAST(N'1967-09-09T00:00:00.000' AS DateTime), N'Rajiv Hari "Akshay Kumar" Om Bhatia was born on September 09, 1967, in Amritsar, Punjab to Aruna Bhatia and Hari Om Bhatia. He is an Indian actor, film producer, former model, and television personality. He went to Bangkok to learn how to use a sword and also worked as a waiter in a restaurant. He studied martial arts in Hong Kong. It was a student who suggested that he should try modeling. Because of his success as model, he was offered films. Along with his good looks and excellent martial art skills, he was always the first choice to do adventurous movies. He does his own stunts in his films.', 1, CAST(N'2022-02-03T09:49:24.287' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'3e99d81f-71b7-49a5-aa58-b4d7ab20af7a', N'Brahmaji', N'Male', CAST(N'1965-04-25T00:00:00.000' AS DateTime), N'Brahmaji is an Indian film actor. He was born in April 25, 1965 Film Nagar, Hyderabad, India. He is best known for his work in the Telugu cinema industry. He appears mostly in Telugu movies. He is a regular actor in director Krishna Vamsi''s films. Brahmaji appeared in an important role in Krishna Vamsi''s debut film as a director, Gulabi', 1, CAST(N'2022-02-05T20:32:32.717' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'b1e04cca-8bf4-45ef-9584-d292d7c9d009', N'Sara Ali Khan', N'Female', CAST(N'1995-08-12T00:00:00.000' AS DateTime), N'Sara Ali Khan was born on 12 August 1995 in Mumbai to Saif Ali Khan, son of Mansoor Ali Khan Pataudi and Sharmila Tagore, and Amrita Singh; both actors of the Hindi film industry. A member of the Pataudi family, she is also the maternal granddaughter of Rukhsana Sultana and Shivinder Singh Virk. She has a younger brother, Ibrahim Ali Khan. Her half-brother, Taimur Ali Khan, is Saif''s son from his second marriage to Kareena Kapoor. Khan is of predominantly Bengali and Pathan descent on her father''s side, and of Punjabi descent on her mother''s side.', 1, CAST(N'2022-02-03T09:49:24.287' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'ed64bb6f-2dcf-4ed7-8f82-d9497118f6e0', N'Allu Arjun', N'Male', CAST(N'1980-04-08T00:00:00.000' AS DateTime), N'Allu Arjun nicknamed Bunny was born on April 8, 1982. His father, Allu Aravind, is a famous producer and his uncle, Chiranjeevi is one of the top actors in the Telugu industry. Also, he is the grandson of late comedian Padmashree Allu Ramalingaiah. He is well-known as stylish star for his unique way of acting and dancing. He is also known for changing his hair and body for each movie that he does. He has starred in 5 movies so far and all of them are super hits. Besides being a illustrious actor though, Allu Arjun gives back to his community and shows to be a successful role-model. Every year on his birthday, April 8th, he donates blood and attends functions for physically and mentally challenged kids.', 1, CAST(N'2022-02-05T20:02:14.627' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'5fff6e23-69d5-4135-b779-ddf4d166ddb0', N'G. Marimuthu', N'Male', CAST(N'1967-07-12T00:00:00.000' AS DateTime), N'G. Marimuthu is an Indian film director and actor, who has worked in the Tamil film industry. After making his debut as a director with Kannum Kannum, he has gone on to make other ventures including Pulivaal and feature in supporting roles as an actor', 1, CAST(N'2022-02-03T09:49:24.287' AS DateTime), NULL)
INSERT [dbo].[tblActors] ([Actor_Id], [ActorName], [ActorSex], [ActorDOB], [ActorBio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'3fc64dff-53a1-428f-9250-f8941d191221', N'Raavu Ramesh Rao', N'Male', CAST(N'1968-04-21T00:00:00.000' AS DateTime), N'Rao Ramesh was born on April 21, 1968 in Srikakulam, Andhra Pradesh, India as Raavu Ramesh Rao. He is an actor, known for Pushpa: The Rise - Part 1 (2021), Jai Bhim (2021) and A Aa (2016).', 1, CAST(N'2022-02-05T20:30:06.147' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[tblMovieActors] ON 

INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (1, N'57d7b696-b048-466e-8dac-a1697ca850d0', N'638d559b-6e31-4c60-8623-444dbaa017a5')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (2, N'57d7b696-b048-466e-8dac-a1697ca850d0', N'b1e04cca-8bf4-45ef-9584-d292d7c9d009')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (3, N'57d7b696-b048-466e-8dac-a1697ca850d0', N'1575b3dd-d3ef-4da3-87fe-ad5494166f48')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (4, N'57d7b696-b048-466e-8dac-a1697ca850d0', N'a0b46281-098d-4332-ae24-5ccfe564a4c5')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (5, N'57d7b696-b048-466e-8dac-a1697ca850d0', N'708ce3f6-0935-4561-b9e5-99cd164ef3c4')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (6, N'57d7b696-b048-466e-8dac-a1697ca850d0', N'4e5f9ca4-8840-41a3-99f3-808f1ec0dd3f')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (7, N'57d7b696-b048-466e-8dac-a1697ca850d0', N'9d46d43d-b2d3-4629-a823-1cb4b8f8aab3')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (8, N'57d7b696-b048-466e-8dac-a1697ca850d0', N'5fff6e23-69d5-4135-b779-ddf4d166ddb0')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (39, N'76c736d5-67de-45ed-8ed3-c16d7db62df2', N'ed64bb6f-2dcf-4ed7-8f82-d9497118f6e0')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (40, N'76c736d5-67de-45ed-8ed3-c16d7db62df2', N'3e99d81f-71b7-49a5-aa58-b4d7ab20af7a')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (41, N'76c736d5-67de-45ed-8ed3-c16d7db62df2', N'2d95c392-e955-49ab-92a2-7f45c638a5df')
INSERT [dbo].[tblMovieActors] ([Id], [Movie_Id], [Actor_Id]) VALUES (42, N'76c736d5-67de-45ed-8ed3-c16d7db62df2', N'bdb02e39-a8cd-4f7f-a292-67afbd002137')
SET IDENTITY_INSERT [dbo].[tblMovieActors] OFF
GO
INSERT [dbo].[tblMovies] ([Movie_Id], [Producer_Id], [MovieName], [MovieReleaseYear], [MoviePlot], [MoviePoster], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'57d7b696-b048-466e-8dac-a1697ca850d0', N'd9f8fb03-cf5f-4861-825c-ff2e89eee778', N'Atrangi Re', N'2021', N'A Tamil boy meets a girl from Bihar, what follows is a love story for the ages. A non-linear narrative of two romances running in parallel from different timelines.', N'', 1, CAST(N'2022-02-03T09:49:13.257' AS DateTime), NULL)
INSERT [dbo].[tblMovies] ([Movie_Id], [Producer_Id], [MovieName], [MovieReleaseYear], [MoviePlot], [MoviePoster], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'76c736d5-67de-45ed-8ed3-c16d7db62df2', N'0b82b854-a218-40ab-af6d-c0cf05f72d7f', N'Pushpa: The Rise - Part 1', N'2021', N'Story of Pushpa Raj, a lorry driver in Seshachalam forests of South India, set in the backdrop of red sandalwood smuggling. Red Sandalwood is endemic to South-Eastern Ghats (mountain range) of India.', NULL, 1, CAST(N'2022-02-05T22:21:28.253' AS DateTime), CAST(N'2022-02-06T00:51:40.217' AS DateTime))
GO
INSERT [dbo].[tblProducers] ([Producer_Id], [Name], [Sex], [DOB], [Bio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'0b82b854-a218-40ab-af6d-c0cf05f72d7f', N'Sukumar Bandreddi', N'Male', CAST(N'1970-01-11T00:00:00.000' AS DateTime), N'Sukumar Bandreddi (born 11 January 1970) is an Indian film director, producer, and screenwriter who predominantly works in Telugu cinema. Born in Mattaparru in the East Godavari district of Andhra Pradesh, Sukumar worked as a mathematics and physics lecturer at the Aditya Junior college, Kakinada for nearly six years before pursuing a career in films. He began working as a writer and assisted Mohan and V. V. Vinayak.', 1, CAST(N'2022-02-05T20:04:30.800' AS DateTime), NULL)
INSERT [dbo].[tblProducers] ([Producer_Id], [Name], [Sex], [DOB], [Bio], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'd9f8fb03-cf5f-4861-825c-ff2e89eee778', N'Aanand L. Rai', N'Male', CAST(N'1971-06-28T00:00:00.000' AS DateTime), N'Aanand L. Rai is a Hindi film director and producer known for romantic-comedy movies Tanu Weds Manu (2011), Raanjhanaa (2013), Tanu Weds Manu: Returns (2015) and Zero (2018). Rai started assisting his elder brother television director Ravi Rai in television series. Eventually he made his directorial debut with the psychological thriller Strangers, followed by Thodi Life Thoda Magic (2008).', 1, CAST(N'2022-02-03T09:40:54.150' AS DateTime), NULL)
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllActors]    Script Date: 06-02-2022 01:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllActors] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Actor_Id As ActorId, ActorName,ActorSex,ActorDOB,ActorBio,IsActive,CreatedOn,UpdatedOn
	FROM tblActors 
	WHERE IsActive = 1 
	ORDER BY ActorName ASC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllMovies]    Script Date: 06-02-2022 01:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllMovies]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT m.Movie_Id AS MovieId, m.Producer_Id AS ProducerId, m.MovieName, m.MovieReleaseYear, 
	m.MoviePlot, m.MoviePoster, p.Name AS ProducerName,Actors = STUFF
		(
			(SELECT ', ' + a.ActorName FROM tblActors a
			LEFT JOIN tblMovieActors ma ON m.Movie_Id = ma.Movie_Id
			WHERE a.IsActive = 1 AND a.Actor_Id = ma.Actor_Id
			FOR XML PATH ('')), 1, 1, ''
		), m.IsActive, m.CreatedOn, m.UpdatedOn
	FROM tblMovies m
	LEFT JOIN tblProducers p ON m.Producer_Id = p.Producer_Id
	WHERE m.IsActive = 1 AND p.IsActive = 1 ORDER BY m.MovieName
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllProducers]    Script Date: 06-02-2022 01:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllProducers]
AS
BEGIN
	SELECT Producer_Id AS ProducerId,Name,Sex,DOB,IsActive,CreatedOn,UpdatedOn 
	FROM tblProducers 
	WHERE IsActive = 1
	ORDER BY Name
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertMovie]    Script Date: 06-02-2022 01:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertMovie] 
	@udtt_Movie udtt_Movie READONLY,
	@udtt_Actors udtt_Actors READONLY
AS
BEGIN

BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @CurrentMovieId UNIQUEIDENTIFIER = null

    -- Insert statements for procedure here
	INSERT INTO tblMovies (Movie_Id,MovieName,MoviePlot,MoviePoster,MovieReleaseYear,Producer_Id,IsActive,CreatedOn)
	SELECT Movie_Id,MovieName,MoviePlot,MoviePoster,MovieReleaseYear,Producer_Id,1,GETDATE() FROM @udtt_Movie

	INSERT INTO tblMovieActors (Movie_Id,Actor_Id)
	SELECT (SELECT Movie_Id FROM @udtt_Movie),Actor_Id FROM @udtt_Actors

	SELECT CAST(1 as bit)

END TRY  
BEGIN CATCH  
	--SELECT CAST(0 as bit)
	SELECT ERROR_NUMBER() AS ErrorNumber,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_STATE() AS ErrorState,
		ERROR_LINE () AS ErrorLine,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_MESSAGE() AS ErrorMessage; 
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateMovie]    Script Date: 06-02-2022 01:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateMovie] 
	@udtt_Movie udtt_Movie READONLY,
	@udtt_Actors udtt_Actors READONLY
AS
BEGIN

BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @CurrentMovieId UNIQUEIDENTIFIER = null

    -- Insert statements for procedure here
	UPDATE m SET 
	m.MovieName = um.MovieName,
	m.MoviePlot = um.MoviePlot,
	m.MoviePoster = um.MoviePoster,
	m.MovieReleaseYear = um.MovieReleaseYear,
	m.Producer_Id = um.Producer_Id,
	m.IsActive = 1,
	m.UpdatedOn = GETDATE()
	FROM tblMovies m 
	INNER JOIN @udtt_Movie um ON um.Movie_Id = m.Movie_Id

	DELETE FROM tblMovieActors WHERE Movie_Id = (SELECT Movie_Id FROM @udtt_Movie)

	INSERT INTO tblMovieActors (Movie_Id,Actor_Id)
	SELECT (SELECT Movie_Id FROM @udtt_Movie),Actor_Id FROM @udtt_Actors

	--UPDATE tblMovieActors SET 
	--Actor_Id = (SELECT Actor_Id FROM @udtt_Actors)
	--WHERE Movie_Id = (SELECT Movie_Id FROM @udtt_Movies)
	
	SELECT CAST(1 as bit)

END TRY  
BEGIN CATCH  
	SELECT CAST(0 as bit)
	--SELECT ERROR_NUMBER() AS ErrorNumber,
	--	ERROR_SEVERITY() AS ErrorSeverity,
	--	ERROR_STATE() AS ErrorState,
	--	ERROR_LINE () AS ErrorLine,
	--	ERROR_PROCEDURE() AS ErrorProcedure,
	--	ERROR_MESSAGE() AS ErrorMessage; 
END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [IMDB_DB] SET  READ_WRITE 
GO
