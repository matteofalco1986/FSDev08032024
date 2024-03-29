USE [master]
GO
/****** Object:  Database [Hotel]    Script Date: 3/9/2024 11:46:17 AM ******/
CREATE DATABASE [Hotel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hotel', FILENAME = N'/var/opt/mssql/data/Hotel.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Hotel_log', FILENAME = N'/var/opt/mssql/data/Hotel_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Hotel] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Hotel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Hotel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Hotel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Hotel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Hotel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Hotel] SET ARITHABORT OFF 
GO
ALTER DATABASE [Hotel] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Hotel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Hotel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Hotel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Hotel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Hotel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Hotel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Hotel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Hotel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Hotel] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Hotel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Hotel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Hotel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Hotel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Hotel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Hotel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Hotel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Hotel] SET RECOVERY FULL 
GO
ALTER DATABASE [Hotel] SET  MULTI_USER 
GO
ALTER DATABASE [Hotel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Hotel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Hotel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Hotel] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Hotel] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Hotel] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Hotel', N'ON'
GO
ALTER DATABASE [Hotel] SET QUERY_STORE = OFF
GO
USE [Hotel]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 3/9/2024 11:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[AdminId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NULL,
	[Password] [nvarchar](20) NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Amenities]    Script Date: 3/9/2024 11:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Amenities](
	[AmenityId] [int] IDENTITY(1,1) NOT NULL,
	[PrenotazioneId] [int] NULL,
	[ServizioId] [int] NULL,
	[Data] [date] NULL,
	[Quantita] [int] NULL,
 CONSTRAINT [PK_Amenities] PRIMARY KEY CLUSTERED 
(
	[AmenityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Camere]    Script Date: 3/9/2024 11:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Camere](
	[CameraId] [int] NOT NULL,
	[TipologiaCameraId] [int] NULL,
	[Descrizione] [nvarchar](2500) NULL,
 CONSTRAINT [PK_Camere] PRIMARY KEY CLUSTERED 
(
	[CameraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clienti]    Script Date: 3/9/2024 11:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clienti](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](20) NULL,
	[Cognome] [nvarchar](20) NULL,
	[CodiceFiscale] [nvarchar](16) NULL,
	[Citta] [nvarchar](20) NULL,
	[Provincia] [nvarchar](2) NULL,
	[Email] [nvarchar](50) NULL,
	[Telefono] [nvarchar](15) NULL,
	[Mobile] [nvarchar](15) NULL,
 CONSTRAINT [PK_Clienti] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListaServiziAggiuntivi]    Script Date: 3/9/2024 11:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListaServiziAggiuntivi](
	[ServizioId] [int] NOT NULL,
	[TipoServizio] [nvarchar](50) NULL,
	[Prezzo] [money] NULL,
 CONSTRAINT [PK_ServiziAggiuntivi] PRIMARY KEY CLUSTERED 
(
	[ServizioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prenotazioni]    Script Date: 3/9/2024 11:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prenotazioni](
	[PrenotazioneId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[CameraId] [int] NULL,
	[TipoPrenotazioneId] [int] NULL,
	[Data] [date] NULL,
	[Anno] [int] NULL,
	[InizioSoggiorno] [date] NULL,
	[FineSoggiorno] [date] NULL,
	[NumeroNotti] [int] NULL,
	[Caparra] [money] NULL,
	[Tariffa] [money] NULL,
 CONSTRAINT [PK_Prenotazioni] PRIMARY KEY CLUSTERED 
(
	[PrenotazioneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipiPrenotazione]    Script Date: 3/9/2024 11:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipiPrenotazione](
	[TipoPrenotazioneId] [int] NOT NULL,
	[TipoPrenotazione] [nvarchar](30) NULL,
 CONSTRAINT [PK_TipiPrenotazione] PRIMARY KEY CLUSTERED 
(
	[TipoPrenotazioneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipologieCamera]    Script Date: 3/9/2024 11:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipologieCamera](
	[TipologiaCameraId] [int] NOT NULL,
	[TipologiaCamera] [nvarchar](50) NULL,
 CONSTRAINT [PK_TipologieCamera] PRIMARY KEY CLUSTERED 
(
	[TipologiaCameraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admins] ON 

INSERT [dbo].[Admins] ([AdminId], [Username], [Password]) VALUES (1, N'admin', N'admin')
INSERT [dbo].[Admins] ([AdminId], [Username], [Password]) VALUES (2, N'pinco', N'pallino')
INSERT [dbo].[Admins] ([AdminId], [Username], [Password]) VALUES (3, N'carlo', N'cracco')
INSERT [dbo].[Admins] ([AdminId], [Username], [Password]) VALUES (4, N'bellofigo', N'ionopagoafito')
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[Amenities] ON 

INSERT [dbo].[Amenities] ([AmenityId], [PrenotazioneId], [ServizioId], [Data], [Quantita]) VALUES (1, 1, 2, CAST(N'2024-03-14' AS Date), 2)
INSERT [dbo].[Amenities] ([AmenityId], [PrenotazioneId], [ServizioId], [Data], [Quantita]) VALUES (2, 3, 1, CAST(N'2024-03-13' AS Date), 2)
INSERT [dbo].[Amenities] ([AmenityId], [PrenotazioneId], [ServizioId], [Data], [Quantita]) VALUES (3, 2, 3, CAST(N'2024-03-14' AS Date), 2)
SET IDENTITY_INSERT [dbo].[Amenities] OFF
GO
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (1, 1, N'Una stanza raffinata e confortevole per il viaggiatore solitario, completa di arredi moderni, letto king-size e una vista mozzafiato sulla città.')
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (2, 2, N'Per una coppia in cerca di lusso e romanticismo, questa camera offre un ampio spazio, un comodo letto matrimoniale e un balcone privato con vista panoramica sul mare.')
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (3, 1, N'Ideale per il viaggiatore in cerca di tranquillità e comfort, questa stanza offre un ambiente accogliente con un letto singolo, arredi caldi e una scrivania per lavorare o rilassarsi.')
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (4, 2, N'Perfetta per una piccola famiglia o gruppo di amici, questa camera dispone di due letti singoli e tutti i comfort necessari per un soggiorno confortevole e piacevole.')
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (5, 1, N'Una stanza chic e moderna con vista sul verde lussureggiante del giardino dell''hotel, perfetta per il viaggiatore che cerca un rifugio tranquillo e sereno.')
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (6, 2, N'Una camera dal design classico e sobrio, con un letto matrimoniale, ideale per una coppia che cerca un ambiente accogliente e familiare.')
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (7, 1, N'Per il viaggiatore che ama godersi un po'' di aria fresca, questa stanza dispone di un balcone privato con vista sulla città o sul cortile interno dell''hotel.')
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (8, 2, N'Una stanza luminosa e spaziosa con due letti singoli, perfetta per amici o colleghi in viaggio insieme, completa di scrivania e zona relax.')
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (9, 1, N'Ideale per soggiorni prolungati o per chi preferisce prepararsi i pasti in autonomia, questa camera dispone di un pratico angolo cottura e tutti i comfort necessari.')
INSERT [dbo].[Camere] ([CameraId], [TipologiaCameraId], [Descrizione]) VALUES (10, 2, N'Per gli amanti della natura e delle escursioni, questa camera offre una vista mozzafiato sulle montagne circostanti, un ampio spazio e un letto matrimoniale per il massimo comfort durante il soggiorno.')
GO
SET IDENTITY_INSERT [dbo].[Clienti] ON 

INSERT [dbo].[Clienti] ([ClienteId], [Nome], [Cognome], [CodiceFiscale], [Citta], [Provincia], [Email], [Telefono], [Mobile]) VALUES (1, N'Carlo', N'Cracco', N'RSSGNN85A15H501S', N'Mantova', N'MN', N'carlo.cracco@gmail.com', N'0123456789', N'3283625456')
INSERT [dbo].[Clienti] ([ClienteId], [Nome], [Cognome], [CodiceFiscale], [Citta], [Provincia], [Email], [Telefono], [Mobile]) VALUES (2, N'Pinco', N'Pallino', N'CRLCRO12P32E453S', N'Macerata', N'MC', N'pinco@pallino.com', N'0123456789', N'3685848442')
SET IDENTITY_INSERT [dbo].[Clienti] OFF
GO
INSERT [dbo].[ListaServiziAggiuntivi] ([ServizioId], [TipoServizio], [Prezzo]) VALUES (1, N'Colazione in camera', 8.0000)
INSERT [dbo].[ListaServiziAggiuntivi] ([ServizioId], [TipoServizio], [Prezzo]) VALUES (2, N'Minibar', 15.0000)
INSERT [dbo].[ListaServiziAggiuntivi] ([ServizioId], [TipoServizio], [Prezzo]) VALUES (3, N'Internet', 10.0000)
INSERT [dbo].[ListaServiziAggiuntivi] ([ServizioId], [TipoServizio], [Prezzo]) VALUES (4, N'Letto aggiuntivo', 20.0000)
INSERT [dbo].[ListaServiziAggiuntivi] ([ServizioId], [TipoServizio], [Prezzo]) VALUES (5, N'Culla', 15.0000)
GO
SET IDENTITY_INSERT [dbo].[Prenotazioni] ON 

INSERT [dbo].[Prenotazioni] ([PrenotazioneId], [ClienteId], [CameraId], [TipoPrenotazioneId], [Data], [Anno], [InizioSoggiorno], [FineSoggiorno], [NumeroNotti], [Caparra], [Tariffa]) VALUES (1, 1, 4, 2, CAST(N'2024-03-06' AS Date), 2023, CAST(N'2024-03-15' AS Date), CAST(N'2024-03-17' AS Date), 2, 20.5000, 152.5000)
INSERT [dbo].[Prenotazioni] ([PrenotazioneId], [ClienteId], [CameraId], [TipoPrenotazioneId], [Data], [Anno], [InizioSoggiorno], [FineSoggiorno], [NumeroNotti], [Caparra], [Tariffa]) VALUES (2, 1, 2, 3, CAST(N'2024-03-08' AS Date), 2300, CAST(N'2024-03-13' AS Date), CAST(N'2024-03-22' AS Date), 9, 32.5000, 150.5000)
INSERT [dbo].[Prenotazioni] ([PrenotazioneId], [ClienteId], [CameraId], [TipoPrenotazioneId], [Data], [Anno], [InizioSoggiorno], [FineSoggiorno], [NumeroNotti], [Caparra], [Tariffa]) VALUES (3, 1, 5, 1, CAST(N'2024-03-08' AS Date), 2024, CAST(N'2024-03-11' AS Date), CAST(N'2024-03-14' AS Date), 3, 20.0000, 55.0000)
INSERT [dbo].[Prenotazioni] ([PrenotazioneId], [ClienteId], [CameraId], [TipoPrenotazioneId], [Data], [Anno], [InizioSoggiorno], [FineSoggiorno], [NumeroNotti], [Caparra], [Tariffa]) VALUES (4, 2, 9, 1, CAST(N'2024-03-08' AS Date), 2022, CAST(N'2022-03-09' AS Date), CAST(N'2022-03-16' AS Date), 7, 50.0000, 55.0000)
SET IDENTITY_INSERT [dbo].[Prenotazioni] OFF
GO
INSERT [dbo].[TipiPrenotazione] ([TipoPrenotazioneId], [TipoPrenotazione]) VALUES (1, N'Mezza pensione')
INSERT [dbo].[TipiPrenotazione] ([TipoPrenotazioneId], [TipoPrenotazione]) VALUES (2, N'Pensione completa')
INSERT [dbo].[TipiPrenotazione] ([TipoPrenotazioneId], [TipoPrenotazione]) VALUES (3, N'Colazione inclusa')
GO
INSERT [dbo].[TipologieCamera] ([TipologiaCameraId], [TipologiaCamera]) VALUES (1, N'Singola')
INSERT [dbo].[TipologieCamera] ([TipologiaCameraId], [TipologiaCamera]) VALUES (2, N'Doppia')
GO
ALTER TABLE [dbo].[Amenities]  WITH CHECK ADD  CONSTRAINT [FK_Amenities_ListaServiziAggiuntivi] FOREIGN KEY([ServizioId])
REFERENCES [dbo].[ListaServiziAggiuntivi] ([ServizioId])
GO
ALTER TABLE [dbo].[Amenities] CHECK CONSTRAINT [FK_Amenities_ListaServiziAggiuntivi]
GO
ALTER TABLE [dbo].[Amenities]  WITH CHECK ADD  CONSTRAINT [FK_Amenities_Prenotazioni] FOREIGN KEY([PrenotazioneId])
REFERENCES [dbo].[Prenotazioni] ([PrenotazioneId])
GO
ALTER TABLE [dbo].[Amenities] CHECK CONSTRAINT [FK_Amenities_Prenotazioni]
GO
ALTER TABLE [dbo].[Camere]  WITH CHECK ADD  CONSTRAINT [FK_Camere_TipologieCamera] FOREIGN KEY([TipologiaCameraId])
REFERENCES [dbo].[TipologieCamera] ([TipologiaCameraId])
GO
ALTER TABLE [dbo].[Camere] CHECK CONSTRAINT [FK_Camere_TipologieCamera]
GO
ALTER TABLE [dbo].[Prenotazioni]  WITH CHECK ADD  CONSTRAINT [FK_Prenotazioni_Camere] FOREIGN KEY([CameraId])
REFERENCES [dbo].[Camere] ([CameraId])
GO
ALTER TABLE [dbo].[Prenotazioni] CHECK CONSTRAINT [FK_Prenotazioni_Camere]
GO
ALTER TABLE [dbo].[Prenotazioni]  WITH CHECK ADD  CONSTRAINT [FK_Prenotazioni_Clienti] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clienti] ([ClienteId])
GO
ALTER TABLE [dbo].[Prenotazioni] CHECK CONSTRAINT [FK_Prenotazioni_Clienti]
GO
ALTER TABLE [dbo].[Prenotazioni]  WITH CHECK ADD  CONSTRAINT [FK_Prenotazioni_TipiPrenotazione] FOREIGN KEY([TipoPrenotazioneId])
REFERENCES [dbo].[TipiPrenotazione] ([TipoPrenotazioneId])
GO
ALTER TABLE [dbo].[Prenotazioni] CHECK CONSTRAINT [FK_Prenotazioni_TipiPrenotazione]
GO
USE [master]
GO
ALTER DATABASE [Hotel] SET  READ_WRITE 
GO
