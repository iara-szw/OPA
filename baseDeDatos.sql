USE [master]
GO
/****** Object:  Database [Opa]    Script Date: 2/12/2025 19:21:04 ******/
CREATE DATABASE [Opa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Opa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS01\MSSQL\DATA\Opa.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Opa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS01\MSSQL\DATA\Opa_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Opa] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Opa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Opa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Opa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Opa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Opa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Opa] SET ARITHABORT OFF 
GO
ALTER DATABASE [Opa] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Opa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Opa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Opa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Opa] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Opa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Opa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Opa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Opa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Opa] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Opa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Opa] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Opa] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Opa] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Opa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Opa] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Opa] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Opa] SET RECOVERY FULL 
GO
ALTER DATABASE [Opa] SET  MULTI_USER 
GO
ALTER DATABASE [Opa] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Opa] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Opa] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Opa] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Opa] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Opa] SET QUERY_STORE = OFF
GO
USE [Opa]
GO
/****** Object:  Table [dbo].[Administrador]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrador](
	[IdAdministrador] [int] IDENTITY(1,1) NOT NULL,
	[IdTienda] [int] NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Permisos] [bit] NOT NULL,
 CONSTRAINT [PK_Administrador] PRIMARY KEY CLUSTERED 
(
	[IdAdministrador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Color]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[idColor] [int] IDENTITY(1,1) NOT NULL,
	[codigoHexa] [varchar](7) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[idColor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ColorXComprador]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColorXComprador](
	[IdCC] [int] IDENTITY(1,1) NOT NULL,
	[IdColor] [int] NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ColorXComprador] PRIMARY KEY CLUSTERED 
(
	[IdCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comprador]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comprador](
	[Usuario] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Contraseña] [varchar](500) NOT NULL,
	[Telefono] [varchar](200) NOT NULL,
	[FotoDePerfil] [varchar](200) NULL,
	[Mail] [varchar](200) NOT NULL,
	[Genero] [int] NOT NULL,
	[MedioDePago] [int] NULL,
	[esVendedor] [bit] NOT NULL,
	[Talles] [int] NULL,
	[NombreTalle] [int] NULL,
 CONSTRAINT [PK_Comprador] PRIMARY KEY CLUSTERED 
(
	[Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deseado]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deseado](
	[IdDeseado] [int] IDENTITY(1,1) NOT NULL,
	[IdPrenda] [int] NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Deseado] PRIMARY KEY CLUSTERED 
(
	[IdDeseado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estilo]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estilo](
	[IdEstilo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Estilo] PRIMARY KEY CLUSTERED 
(
	[IdEstilo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estiloXComprador]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estiloXComprador](
	[IdEC] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](50) NOT NULL,
	[idEstilo] [int] NOT NULL,
 CONSTRAINT [PK_estiloXComprador] PRIMARY KEY CLUSTERED 
(
	[IdEC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstiloXPrenda]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstiloXPrenda](
	[idEP] [int] IDENTITY(1,1) NOT NULL,
	[IdPrenda] [int] NOT NULL,
	[IdEstilo] [int] NOT NULL,
 CONSTRAINT [PK_EstiloXPrenda] PRIMARY KEY CLUSTERED 
(
	[idEP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genero]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genero](
	[IdGenero] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Genero] PRIMARY KEY CLUSTERED 
(
	[IdGenero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedioDePago]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedioDePago](
	[IdMedioDePago] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MedioDePago] PRIMARY KEY CLUSTERED 
(
	[IdMedioDePago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Poseido]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Poseido](
	[IdPoseido] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[IdPrenda] [int] NOT NULL,
 CONSTRAINT [PK_Adquirido] PRIMARY KEY CLUSTERED 
(
	[IdPoseido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prenda]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prenda](
	[Tipo] [int] NOT NULL,
	[IdPrenda] [int] IDENTITY(1,1) NOT NULL,
	[Modelo] [varchar](100) NOT NULL,
	[IdTalle] [int] NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Precio] [decimal](15, 2) NOT NULL,
	[foto] [varchar](50) NOT NULL,
	[Color] [int] NOT NULL,
	[IdTienda] [int] NOT NULL,
	[mostrar] [bit] NOT NULL,
	[stock] [int] NOT NULL,
 CONSTRAINT [PK_Prenda] PRIMARY KEY CLUSTERED 
(
	[IdPrenda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Talle]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Talle](
	[IdTalle] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Talle] PRIMARY KEY CLUSTERED 
(
	[IdTalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TallesUsu]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TallesUsu](
	[idTalle] [int] IDENTITY(1,1) NOT NULL,
	[MedidaTorso] [decimal](6, 2) NULL,
	[MedidaCintura] [decimal](6, 2) NULL,
	[MedidaPierna] [decimal](6, 2) NULL,
	[MedidaHombros] [decimal](6, 2) NULL,
	[MedidaBrazos] [decimal](6, 2) NULL,
	[MedidaCadera] [decimal](6, 2) NULL,
 CONSTRAINT [PK_TallesUsu] PRIMARY KEY CLUSTERED 
(
	[idTalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Temporada]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Temporada](
	[idTemporada] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Temporada] PRIMARY KEY CLUSTERED 
(
	[idTemporada] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TemporadaXComprador]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemporadaXComprador](
	[IdTc] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[IdTemporada] [int] NOT NULL,
 CONSTRAINT [PK_TemporadaXComprador] PRIMARY KEY CLUSTERED 
(
	[IdTc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TemporadaXPrenda]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemporadaXPrenda](
	[IdTP] [int] IDENTITY(1,1) NOT NULL,
	[IdTemporada] [int] NOT NULL,
	[IdPrenda] [int] NOT NULL,
 CONSTRAINT [PK_TemporadaXPrenda] PRIMARY KEY CLUSTERED 
(
	[IdTP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tienda]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tienda](
	[IdTienda] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Ubicacion] [varchar](500) NOT NULL,
	[Mail] [varchar](200) NOT NULL,
	[Telefono] [varchar](200) NOT NULL,
	[Descripcion] [varchar](2000) NOT NULL,
	[FotoDePerfil] [varchar](200) NOT NULL,
	[Contacto] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Tienda] PRIMARY KEY CLUSTERED 
(
	[IdTienda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipos]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipos](
	[idTipo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tipos] PRIMARY KEY CLUSTERED 
(
	[idTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposXComprador]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposXComprador](
	[idTIC] [int] IDENTITY(1,1) NOT NULL,
	[idTipo] [int] NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TiposXComprador] PRIMARY KEY CLUSTERED 
(
	[idTIC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Administrador] ON 
GO
INSERT [dbo].[Administrador] ([IdAdministrador], [IdTienda], [Usuario], [Permisos]) VALUES (3, 3, N'admin', 1)
GO
INSERT [dbo].[Administrador] ([IdAdministrador], [IdTienda], [Usuario], [Permisos]) VALUES (4, 4, N'admin', 1)
GO
SET IDENTITY_INSERT [dbo].[Administrador] OFF
GO
SET IDENTITY_INSERT [dbo].[Color] ON 
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (1, N'#000000', N'Negro')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (2, N'#FFFFFF', N'Blanco')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (3, N'#808080', N'Gris')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (4, N'#D3D3D3', N'Gris Claro')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (5, N'#404040', N'Gris Oscuro')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (6, N'#FF0000', N'Rojo')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (7, N'#800020', N'Rojo Borgoña')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (8, N'#5E1914', N'Rojo Vino')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (9, N'#0000FF', N'Azul')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (10, N'#000080', N'Azul Marino')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (11, N'#4682B4', N'Azul Acero')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (12, N'#00FFFF', N'Cian')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (13, N'#ADD8E6', N'Celeste')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (14, N'#008000', N'Verde')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (15, N'#808000', N'Verde Oliva')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (16, N'#4B5320', N'Verde Militar')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (17, N'#77DD77', N'Verde Pastel')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (18, N'#FFFF00', N'Amarillo')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (19, N'#FDFD96', N'Amarillo Pastel')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (20, N'#D4A017', N'Mostaza')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (21, N'#FFA500', N'Naranja')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (22, N'#CC5500', N'Naranja Quemado')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (23, N'#FFC0CB', N'Rosa')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (24, N'#FFD1DC', N'Rosa Pastel')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (25, N'#FF00FF', N'Rosa Fucsia')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (26, N'#C8A2C8', N'Lila')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (27, N'#E6E6FA', N'Lavanda')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (28, N'#800080', N'Morado')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (29, N'#8B4513', N'Marrón')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (30, N'#F5F5DC', N'Beige')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (31, N'#FFFDD0', N'Crema')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (32, N'#C2B280', N'Arena')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (33, N'#C0C0C0', N'Plateado')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (34, N'#D4AF37', N'Dorado')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (35, N'#3B0A0A', N'Borgoña Oscuro')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (36, N'#004953', N'Petróleo')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (37, N'#98FF98', N'Verde Menta')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (38, N'#7DF9FF', N'Azul Eléctrico')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (39, N'#C4C3D0', N'Lavanda Gris')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (40, N'#C19A6B', N'Camel')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (41, N'#2B2B2B', N'Carbón')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (42, N'#4E2A1E', N'Chocolate')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (43, N'#6F2DA8', N'Uva')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (44, N'#E2725B', N'Terracota')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (45, N'#9CAF88', N'Salvia')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (46, N'#F2EDE4', N'Perla')
GO
INSERT [dbo].[Color] ([idColor], [codigoHexa], [Nombre]) VALUES (47, N'#0B0C10', N'Smoky Black')
GO
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[ColorXComprador] ON 
GO
INSERT [dbo].[ColorXComprador] ([IdCC], [IdColor], [Usuario]) VALUES (1, 11, N'admin')
GO
INSERT [dbo].[ColorXComprador] ([IdCC], [IdColor], [Usuario]) VALUES (2, 12, N'admin')
GO
INSERT [dbo].[ColorXComprador] ([IdCC], [IdColor], [Usuario]) VALUES (3, 13, N'admin')
GO
SET IDENTITY_INSERT [dbo].[ColorXComprador] OFF
GO
INSERT [dbo].[Comprador] ([Usuario], [Nombre], [Apellido], [Contraseña], [Telefono], [FotoDePerfil], [Mail], [Genero], [MedioDePago], [esVendedor], [Talles], [NombreTalle]) VALUES (N'admin', N'a', N'notengo', N'03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', N'28372983', NULL, N'sdfsdf@gmail.com', 1, NULL, 0, 1, NULL)
GO
INSERT [dbo].[Comprador] ([Usuario], [Nombre], [Apellido], [Contraseña], [Telefono], [FotoDePerfil], [Mail], [Genero], [MedioDePago], [esVendedor], [Talles], [NombreTalle]) VALUES (N'iara', N'iara', N'szw', N'03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', N'230230972', NULL, N'correco@com.com', 1, NULL, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Estilo] ON 
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (1, N'Streetwear', N'Moda urbana con influencia del skate, hip hop y cultura juvenil')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (2, N'Vintage', N'Ropa inspirada en décadas pasadas, clásica y retro')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (3, N'Minimalista', N'Diseños simples, limpios y funcionales con colores neutros')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (4, N'Boho', N'Estilo bohemio con telas sueltas, estampados y espíritu libre')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (5, N'Grunge', N'Ropa desgastada, oversize, inspiración rock 90s y tonos oscuros')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (6, N'Y2K', N'Estilo inspirado en los 2000s con colores llamativos y siluetas futuristas')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (7, N'Techwear', N'Ropa funcional, futurista y utilitaria, resistente e impermeable')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (8, N'Casual', N'Ropa diaria relajada y cómoda')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (9, N'Formal', N'Vestimenta elegante para eventos y trabajo')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (10, N'Skater', N'Ropa cómoda, oversize y resistente inspirada en el skate')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (11, N'Gótico', N'Estilo oscuro inspirado en cultura alternativa y música goth')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (12, N'Punk', N'Actitud rebelde con cuero, tachas y estampados fuertes')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (13, N'Athleisure', N'Combinación de ropa deportiva y casual para uso diario')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (14, N'Preppy', N'Estilo clásico universitario, prolijo y elegante')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (15, N'Romántico', N'Prendas suaves, femeninas, con encajes y telas livianas')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (16, N'Artsy', N'Ropa creativa, estampados únicos y estética artística')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (17, N'Indie', N'Estilo alternativo con prendas únicas y artesanales')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (18, N'Formal Urbano', N'Fusión de elegancia y ropa urbana moderna')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (19, N'Oversize', N'Prendas grandes, voluminosas y cómodas')
GO
INSERT [dbo].[Estilo] ([IdEstilo], [Nombre], [descripcion]) VALUES (20, N'Sustentable', N'Ropa hecha con materiales reciclados o ecológicos')
GO
SET IDENTITY_INSERT [dbo].[Estilo] OFF
GO
SET IDENTITY_INSERT [dbo].[estiloXComprador] ON 
GO
INSERT [dbo].[estiloXComprador] ([IdEC], [usuario], [idEstilo]) VALUES (1, N'admin', 8)
GO
INSERT [dbo].[estiloXComprador] ([IdEC], [usuario], [idEstilo]) VALUES (2, N'admin', 12)
GO
INSERT [dbo].[estiloXComprador] ([IdEC], [usuario], [idEstilo]) VALUES (3, N'admin', 15)
GO
SET IDENTITY_INSERT [dbo].[estiloXComprador] OFF
GO
SET IDENTITY_INSERT [dbo].[EstiloXPrenda] ON 
GO
INSERT [dbo].[EstiloXPrenda] ([idEP], [IdPrenda], [IdEstilo]) VALUES (82, 28, 19)
GO
INSERT [dbo].[EstiloXPrenda] ([idEP], [IdPrenda], [IdEstilo]) VALUES (83, 28, 20)
GO
INSERT [dbo].[EstiloXPrenda] ([idEP], [IdPrenda], [IdEstilo]) VALUES (87, 31, 8)
GO
INSERT [dbo].[EstiloXPrenda] ([idEP], [IdPrenda], [IdEstilo]) VALUES (88, 32, 8)
GO
INSERT [dbo].[EstiloXPrenda] ([idEP], [IdPrenda], [IdEstilo]) VALUES (89, 30, 8)
GO
INSERT [dbo].[EstiloXPrenda] ([idEP], [IdPrenda], [IdEstilo]) VALUES (93, 33, 2)
GO
INSERT [dbo].[EstiloXPrenda] ([idEP], [IdPrenda], [IdEstilo]) VALUES (94, 33, 15)
GO
INSERT [dbo].[EstiloXPrenda] ([idEP], [IdPrenda], [IdEstilo]) VALUES (95, 33, 20)
GO
SET IDENTITY_INSERT [dbo].[EstiloXPrenda] OFF
GO
INSERT [dbo].[Genero] ([IdGenero], [Nombre]) VALUES (1, N'Mujer')
GO
INSERT [dbo].[Genero] ([IdGenero], [Nombre]) VALUES (2, N'Hombre')
GO
INSERT [dbo].[Genero] ([IdGenero], [Nombre]) VALUES (3, N'Otro')
GO
SET IDENTITY_INSERT [dbo].[Prenda] ON 
GO
INSERT [dbo].[Prenda] ([Tipo], [IdPrenda], [Modelo], [IdTalle], [Descripcion], [Precio], [foto], [Color], [IdTienda], [mostrar], [stock]) VALUES (4, 28, N'Campera aire', 1, N'Campera para aire aireada', CAST(75000.00 AS Decimal(15, 2)), N'OIP (1).webp', 1, 3, 1, 27)
GO
INSERT [dbo].[Prenda] ([Tipo], [IdPrenda], [Modelo], [IdTalle], [Descripcion], [Precio], [foto], [Color], [IdTienda], [mostrar], [stock]) VALUES (3, 30, N'Jogging Ton', 1, N'Jogging comodo de algodon. NO APTO PARA CORRER.NO APTO PARA LAVAROPAS.NO APTO PARA TAREAS DOMESTICAS PESADAS/QUE MANCHAN.', CAST(23000.00 AS Decimal(15, 2)), N'OIP (2).webp', 1, 4, 1, 20)
GO
INSERT [dbo].[Prenda] ([Tipo], [IdPrenda], [Modelo], [IdTalle], [Descripcion], [Precio], [foto], [Color], [IdTienda], [mostrar], [stock]) VALUES (3, 31, N'Jogging Ton', 2, N'Jogging comodo de algodon. NO APTO PARA CORRER.NO APTO PARA LAVAROPAS.NO APTO PARA TAREAS DOMESTICAS PESADAS/QUE MANCHAN.', CAST(23000.00 AS Decimal(15, 2)), N'OIP (2).webp', 1, 4, 0, 20)
GO
INSERT [dbo].[Prenda] ([Tipo], [IdPrenda], [Modelo], [IdTalle], [Descripcion], [Precio], [foto], [Color], [IdTienda], [mostrar], [stock]) VALUES (3, 32, N'Jogging Ton', 3, N'Jogging comodo de algodon. NO APTO PARA CORRER.NO APTO PARA LAVAROPAS.NO APTO PARA TAREAS DOMESTICAS PESADAS/QUE MANCHAN.', CAST(23000.00 AS Decimal(15, 2)), N'OIP (2).webp', 1, 4, 0, 20)
GO
INSERT [dbo].[Prenda] ([Tipo], [IdPrenda], [Modelo], [IdTalle], [Descripcion], [Precio], [foto], [Color], [IdTienda], [mostrar], [stock]) VALUES (4, 33, N'Campera de cuero', 1, N'Campera de cuero azul marino', CAST(55000.00 AS Decimal(15, 2)), N'fc93c05f5823892b2b690c3d74fdd864.jpg', 10, 4, 1, 25)
GO
SET IDENTITY_INSERT [dbo].[Prenda] OFF
GO
SET IDENTITY_INSERT [dbo].[Talle] ON 
GO
INSERT [dbo].[Talle] ([IdTalle], [Nombre]) VALUES (1, N'S')
GO
INSERT [dbo].[Talle] ([IdTalle], [Nombre]) VALUES (2, N'M')
GO
INSERT [dbo].[Talle] ([IdTalle], [Nombre]) VALUES (3, N'L')
GO
SET IDENTITY_INSERT [dbo].[Talle] OFF
GO
SET IDENTITY_INSERT [dbo].[TallesUsu] ON 
GO
INSERT [dbo].[TallesUsu] ([idTalle], [MedidaTorso], [MedidaCintura], [MedidaPierna], [MedidaHombros], [MedidaBrazos], [MedidaCadera]) VALUES (1, CAST(12.00 AS Decimal(6, 2)), CAST(12.00 AS Decimal(6, 2)), CAST(12.00 AS Decimal(6, 2)), CAST(12.00 AS Decimal(6, 2)), CAST(12.00 AS Decimal(6, 2)), CAST(12.00 AS Decimal(6, 2)))
GO
SET IDENTITY_INSERT [dbo].[TallesUsu] OFF
GO
SET IDENTITY_INSERT [dbo].[Temporada] ON 
GO
INSERT [dbo].[Temporada] ([idTemporada], [Nombre]) VALUES (1, N'Verano')
GO
INSERT [dbo].[Temporada] ([idTemporada], [Nombre]) VALUES (2, N'Invierno')
GO
INSERT [dbo].[Temporada] ([idTemporada], [Nombre]) VALUES (3, N'Otoño')
GO
INSERT [dbo].[Temporada] ([idTemporada], [Nombre]) VALUES (4, N'Primavera')
GO
INSERT [dbo].[Temporada] ([idTemporada], [Nombre]) VALUES (5, N'Neutro')
GO
SET IDENTITY_INSERT [dbo].[Temporada] OFF
GO
SET IDENTITY_INSERT [dbo].[TemporadaXPrenda] ON 
GO
INSERT [dbo].[TemporadaXPrenda] ([IdTP], [IdTemporada], [IdPrenda]) VALUES (28, 2, 28)
GO
INSERT [dbo].[TemporadaXPrenda] ([IdTP], [IdTemporada], [IdPrenda]) VALUES (31, 5, 31)
GO
INSERT [dbo].[TemporadaXPrenda] ([IdTP], [IdTemporada], [IdPrenda]) VALUES (32, 5, 32)
GO
INSERT [dbo].[TemporadaXPrenda] ([IdTP], [IdTemporada], [IdPrenda]) VALUES (33, 5, 30)
GO
INSERT [dbo].[TemporadaXPrenda] ([IdTP], [IdTemporada], [IdPrenda]) VALUES (35, 5, 33)
GO
SET IDENTITY_INSERT [dbo].[TemporadaXPrenda] OFF
GO
SET IDENTITY_INSERT [dbo].[Tienda] ON 
GO
INSERT [dbo].[Tienda] ([IdTienda], [Nombre], [Ubicacion], [Mail], [Telefono], [Descripcion], [FotoDePerfil], [Contacto]) VALUES (3, N'Air', N'Buenos aires', N'buenosair@gm.com', N'25472458', N'Ropa para aire', N'OIP.webp', N'Visita nuestro tiktok @air')
GO
INSERT [dbo].[Tienda] ([IdTienda], [Nombre], [Ubicacion], [Mail], [Telefono], [Descripcion], [FotoDePerfil], [Contacto]) VALUES (4, N'Tonica', N'Chubut', N'Fash@com.com', N'28493749', N'Moda fashion de parte de una boutique, tonica', N'logo.png', N'@FashBou')
GO
SET IDENTITY_INSERT [dbo].[Tienda] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipos] ON 
GO
INSERT [dbo].[Tipos] ([idTipo], [Nombre]) VALUES (1, N'Remera')
GO
INSERT [dbo].[Tipos] ([idTipo], [Nombre]) VALUES (2, N'Pantalon')
GO
INSERT [dbo].[Tipos] ([idTipo], [Nombre]) VALUES (3, N'Jogging')
GO
INSERT [dbo].[Tipos] ([idTipo], [Nombre]) VALUES (4, N'Campera')
GO
INSERT [dbo].[Tipos] ([idTipo], [Nombre]) VALUES (5, N'Buzo')
GO
INSERT [dbo].[Tipos] ([idTipo], [Nombre]) VALUES (6, N'Musculosa')
GO
INSERT [dbo].[Tipos] ([idTipo], [Nombre]) VALUES (7, N'Sweater')
GO
SET IDENTITY_INSERT [dbo].[Tipos] OFF
GO
ALTER TABLE [dbo].[Administrador]  WITH CHECK ADD  CONSTRAINT [FK_Administrador_Comprador] FOREIGN KEY([Usuario])
REFERENCES [dbo].[Comprador] ([Usuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Administrador] CHECK CONSTRAINT [FK_Administrador_Comprador]
GO
ALTER TABLE [dbo].[Administrador]  WITH CHECK ADD  CONSTRAINT [FK_Administrador_Tienda] FOREIGN KEY([IdTienda])
REFERENCES [dbo].[Tienda] ([IdTienda])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Administrador] CHECK CONSTRAINT [FK_Administrador_Tienda]
GO
ALTER TABLE [dbo].[ColorXComprador]  WITH CHECK ADD  CONSTRAINT [FK_ColorXComprador_Color] FOREIGN KEY([IdColor])
REFERENCES [dbo].[Color] ([idColor])
GO
ALTER TABLE [dbo].[ColorXComprador] CHECK CONSTRAINT [FK_ColorXComprador_Color]
GO
ALTER TABLE [dbo].[ColorXComprador]  WITH CHECK ADD  CONSTRAINT [FK_ColorXComprador_Comprador] FOREIGN KEY([Usuario])
REFERENCES [dbo].[Comprador] ([Usuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ColorXComprador] CHECK CONSTRAINT [FK_ColorXComprador_Comprador]
GO
ALTER TABLE [dbo].[Comprador]  WITH CHECK ADD  CONSTRAINT [FK_Comprador_Genero] FOREIGN KEY([Genero])
REFERENCES [dbo].[Genero] ([IdGenero])
GO
ALTER TABLE [dbo].[Comprador] CHECK CONSTRAINT [FK_Comprador_Genero]
GO
ALTER TABLE [dbo].[Comprador]  WITH CHECK ADD  CONSTRAINT [FK_Comprador_MedioDePago] FOREIGN KEY([MedioDePago])
REFERENCES [dbo].[MedioDePago] ([IdMedioDePago])
GO
ALTER TABLE [dbo].[Comprador] CHECK CONSTRAINT [FK_Comprador_MedioDePago]
GO
ALTER TABLE [dbo].[Comprador]  WITH CHECK ADD  CONSTRAINT [FK_Comprador_Talle] FOREIGN KEY([NombreTalle])
REFERENCES [dbo].[Talle] ([IdTalle])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comprador] CHECK CONSTRAINT [FK_Comprador_Talle]
GO
ALTER TABLE [dbo].[Comprador]  WITH CHECK ADD  CONSTRAINT [FK_Comprador_TallesUsu] FOREIGN KEY([Talles])
REFERENCES [dbo].[TallesUsu] ([idTalle])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comprador] CHECK CONSTRAINT [FK_Comprador_TallesUsu]
GO
ALTER TABLE [dbo].[Deseado]  WITH CHECK ADD  CONSTRAINT [FK_Deseado_Comprador1] FOREIGN KEY([Usuario])
REFERENCES [dbo].[Comprador] ([Usuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deseado] CHECK CONSTRAINT [FK_Deseado_Comprador1]
GO
ALTER TABLE [dbo].[Deseado]  WITH CHECK ADD  CONSTRAINT [FK_Deseado_Prenda] FOREIGN KEY([IdPrenda])
REFERENCES [dbo].[Prenda] ([IdPrenda])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deseado] CHECK CONSTRAINT [FK_Deseado_Prenda]
GO
ALTER TABLE [dbo].[estiloXComprador]  WITH CHECK ADD  CONSTRAINT [FK_estiloXComprador_Comprador1] FOREIGN KEY([usuario])
REFERENCES [dbo].[Comprador] ([Usuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[estiloXComprador] CHECK CONSTRAINT [FK_estiloXComprador_Comprador1]
GO
ALTER TABLE [dbo].[estiloXComprador]  WITH CHECK ADD  CONSTRAINT [FK_estiloXComprador_Estilo] FOREIGN KEY([idEstilo])
REFERENCES [dbo].[Estilo] ([IdEstilo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[estiloXComprador] CHECK CONSTRAINT [FK_estiloXComprador_Estilo]
GO
ALTER TABLE [dbo].[EstiloXPrenda]  WITH CHECK ADD  CONSTRAINT [FK_EstiloXPrenda_Estilo] FOREIGN KEY([IdEstilo])
REFERENCES [dbo].[Estilo] ([IdEstilo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EstiloXPrenda] CHECK CONSTRAINT [FK_EstiloXPrenda_Estilo]
GO
ALTER TABLE [dbo].[EstiloXPrenda]  WITH CHECK ADD  CONSTRAINT [FK_EstiloXPrenda_Prenda] FOREIGN KEY([IdPrenda])
REFERENCES [dbo].[Prenda] ([IdPrenda])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EstiloXPrenda] CHECK CONSTRAINT [FK_EstiloXPrenda_Prenda]
GO
ALTER TABLE [dbo].[Poseido]  WITH CHECK ADD  CONSTRAINT [FK_Poseido_Comprador1] FOREIGN KEY([Usuario])
REFERENCES [dbo].[Comprador] ([Usuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Poseido] CHECK CONSTRAINT [FK_Poseido_Comprador1]
GO
ALTER TABLE [dbo].[Poseido]  WITH CHECK ADD  CONSTRAINT [FK_Poseido_Prenda] FOREIGN KEY([IdPrenda])
REFERENCES [dbo].[Prenda] ([IdPrenda])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Poseido] CHECK CONSTRAINT [FK_Poseido_Prenda]
GO
ALTER TABLE [dbo].[Prenda]  WITH CHECK ADD  CONSTRAINT [FK_Prenda_Talle] FOREIGN KEY([IdTalle])
REFERENCES [dbo].[Talle] ([IdTalle])
GO
ALTER TABLE [dbo].[Prenda] CHECK CONSTRAINT [FK_Prenda_Talle]
GO
ALTER TABLE [dbo].[Prenda]  WITH CHECK ADD  CONSTRAINT [FK_Prenda_Tienda] FOREIGN KEY([IdTienda])
REFERENCES [dbo].[Tienda] ([IdTienda])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prenda] CHECK CONSTRAINT [FK_Prenda_Tienda]
GO
ALTER TABLE [dbo].[TemporadaXComprador]  WITH CHECK ADD  CONSTRAINT [FK_TemporadaXComprador_Comprador] FOREIGN KEY([Usuario])
REFERENCES [dbo].[Comprador] ([Usuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TemporadaXComprador] CHECK CONSTRAINT [FK_TemporadaXComprador_Comprador]
GO
ALTER TABLE [dbo].[TemporadaXComprador]  WITH CHECK ADD  CONSTRAINT [FK_TemporadaXComprador_Temporada] FOREIGN KEY([IdTemporada])
REFERENCES [dbo].[Temporada] ([idTemporada])
GO
ALTER TABLE [dbo].[TemporadaXComprador] CHECK CONSTRAINT [FK_TemporadaXComprador_Temporada]
GO
ALTER TABLE [dbo].[TemporadaXPrenda]  WITH CHECK ADD  CONSTRAINT [FK_TemporadaXPrenda_Prenda] FOREIGN KEY([IdPrenda])
REFERENCES [dbo].[Prenda] ([IdPrenda])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TemporadaXPrenda] CHECK CONSTRAINT [FK_TemporadaXPrenda_Prenda]
GO
ALTER TABLE [dbo].[TemporadaXPrenda]  WITH CHECK ADD  CONSTRAINT [FK_TemporadaXPrenda_Temporada] FOREIGN KEY([IdTemporada])
REFERENCES [dbo].[Temporada] ([idTemporada])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TemporadaXPrenda] CHECK CONSTRAINT [FK_TemporadaXPrenda_Temporada]
GO
ALTER TABLE [dbo].[TiposXComprador]  WITH CHECK ADD  CONSTRAINT [FK_TiposXComprador_Comprador] FOREIGN KEY([Usuario])
REFERENCES [dbo].[Comprador] ([Usuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TiposXComprador] CHECK CONSTRAINT [FK_TiposXComprador_Comprador]
GO
ALTER TABLE [dbo].[TiposXComprador]  WITH CHECK ADD  CONSTRAINT [FK_TiposXComprador_Tipos] FOREIGN KEY([idTipo])
REFERENCES [dbo].[Tipos] ([idTipo])
GO
ALTER TABLE [dbo].[TiposXComprador] CHECK CONSTRAINT [FK_TiposXComprador_Tipos]
GO
/****** Object:  StoredProcedure [dbo].[agregarPrenda]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[agregarPrenda]
    @IdTienda INT,
    @Tipo int,
    @Modelo NVARCHAR(100),
    @IdTalle INT,
    @Descripcion NVARCHAR(255),
    @Precio DECIMAL(10,2),
    @Estilo1 INT,
    @Estilo2 INT,
    @Estilo3 INT,
    @Color INT,
    @Temporada INT,
    @Foto NVARCHAR(255),
    @stock int
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdPrenda INT;

    -- 1️⃣ Insertar la prenda
    If EXISTS (SELECT 1 FROM PRENDA where Modelo=@Modelo AND IdTienda=@IdTienda)
    BEGIN
    INSERT INTO Prenda (Tipo, Modelo, IdTalle, Descripcion, Precio, Foto, Color, IdTienda, mostrar,stock)
    VALUES (@Tipo, @Modelo, @IdTalle, @Descripcion, @Precio, @Foto, @Color, @IdTienda, 0,@stock);
    END
    ELSE
    BEGIN 
      INSERT INTO Prenda (Tipo, Modelo, IdTalle, Descripcion, Precio, Foto, Color, IdTienda, mostrar,stock)
    VALUES (@Tipo, @Modelo, @IdTalle, @Descripcion, @Precio, @Foto, @Color, @IdTienda, 1,@stock);
    END 
    SET @IdPrenda = SCOPE_IDENTITY();
    -- 4️⃣ Relacionar con temporada
    INSERT INTO TemporadaXPrenda (IdTemporada, IdPrenda)
    VALUES (@Temporada, @IdPrenda);
       
    -- 5️⃣ Relacionar con estilos
    INSERT INTO EstiloXPrenda (IdEstilo, IdPrenda)
    VALUES (@Estilo1, @IdPrenda);
     if (@Estilo2!=-1)
        BEGIN

    INSERT INTO EstiloXPrenda (IdEstilo, IdPrenda)
    VALUES (@Estilo2, @IdPrenda);
    IF(@Estilo3!=-1)
    BEGIN
    INSERT INTO EstiloXPrenda (IdEstilo, IdPrenda)
    VALUES (@Estilo3, @IdPrenda);
    END
    END

END;
GO
/****** Object:  StoredProcedure [dbo].[cargarMedidas]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cargarMedidas]
 @idUsuario varchar(50),
 @pMedidaTorso decimal(6,3),
 @pMedidaCintura decimal(6,3),
 @pMedidaPierna decimal(6,3),
 @pMedidaHombros decimal(6,3),
 @pMedidaBrazos decimal(6,3),
 @pMedidaCadera decimal(6,3)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;

        DECLARE @idTalle INT;
        SELECT @idTalle = Talles FROM Comprador WHERE Usuario = @idUsuario;

        IF @idTalle IS NOT NULL
        BEGIN
            UPDATE TallesUsu
            SET MedidaBrazos = @pMedidaBrazos,
                MedidaCadera = @pMedidaCadera,
                MedidaCintura = @pMedidaCintura,
                MedidaHombros = @pMedidaHombros,
                MedidaPierna = @pMedidaPierna,
                MedidaTorso = @pMedidaTorso
            WHERE idTalle = @idTalle;
        END
        ELSE
        BEGIN
            INSERT INTO TallesUsu (MedidaTorso, MedidaCintura, MedidaPierna, MedidaHombros, MedidaBrazos, MedidaCadera)
            VALUES (@pMedidaTorso, @pMedidaCintura, @pMedidaPierna, @pMedidaHombros, @pMedidaBrazos, @pMedidaCadera);

            DECLARE @newId INT = SCOPE_IDENTITY();
            UPDATE Comprador SET Talles = @newId WHERE Usuario = @idUsuario;
        END

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[crearTienda]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[crearTienda]
    @Nombre          VARCHAR(200),
    @Ubicacion       VARCHAR(500),	
    @Mail            VARCHAR(200),
    @Telefono        VARCHAR(200),
    @Descripcion     VARCHAR(2000),
    @FotoDePerfil    VARCHAR(200),
    @Contacto        VARCHAR(200),
    @Usuario         VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert tienda
    INSERT INTO Tienda (Nombre, Ubicacion, Mail, Telefono, Descripcion, FotoDePerfil, Contacto)
    VALUES (@Nombre, @Ubicacion, @Mail, @Telefono, @Descripcion, @FotoDePerfil, @Contacto);

    -- Obtener IdTienda recién creada
    DECLARE @NuevoIdTienda INT = SCOPE_IDENTITY();

    -- Insert administrador
    INSERT INTO Administrador (IdTienda, Usuario, Permisos)
    VALUES (@NuevoIdTienda, @Usuario, 1);

    -- Devolver IdTienda creada
    SELECT @NuevoIdTienda AS IdTiendaCreada;
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarPrenda]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EliminarPrenda] 
	-- Add the parameters for the stored procedure here
	@idPrenda int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @modelo varchar(50)
	DECLARE @tienda int
	DECLARE @nuevoId int
    -- Insert statements for procedure here
	SET @tienda = (SELECT idTienda from Prenda where idPrenda=@idPrenda)
	SET @modelo = (SELECT Modelo from Prenda where idPrenda=@idPrenda)

DELETE FROM Prenda WHERE IdPrenda=@idPrenda
If ((SELECT COUNT(IdPrenda) FROM Prenda where Modelo=@modelo AND IdTienda=@tienda AND mostrar=1)=0)
BEGIN

IF EXISTS(SELECT 1 FROM Prenda where Modelo=@modelo AND IdTienda=@tienda)
BEGIN
SET @nuevoId=(SELECT TOP 1 IdPrenda FROM PRENDA where Modelo=@modelo AND IdTienda=@tienda)
UPDATE Prenda SET mostrar=1 WHERE Modelo=@modelo AND IdTienda=@tienda AND IdPrenda=@nuevoId
END
END
END
GO
/****** Object:  StoredProcedure [dbo].[levantarRecomendados]    Script Date: 2/12/2025 19:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[levantarRecomendados]
	-- Add the parameters for the stored procedure here
@id varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF @id='default'
	BEGIN
	SELECT TOP 20 * from prenda where mostrar=1
	RETURN 
	END
	DECLARE @estilo1 int
	DECLARE @color1 int
	DECLARE @temporada1 int
	SELECT TOP 1 @estilo1 = idEstilo FROM estiloXComprador where usuario=@id
	SELECT TOP 1 @color1 = idColor FROM ColorXComprador where usuario=@id
	SELECT TOP 1 @temporada1 = idTemporada FROM TemporadaXComprador where usuario=@id

	SELECT * FROM Prenda 
	INNER JOIN  EstiloXPrenda as EP ON EP.IdPrenda=Prenda.IdPrenda
	inner join Color as CP ON CP.idColor=PRENDA.Color
	inner join TemporadaXPrenda as TP ON TP.IdPrenda=PRENDA.IdPrenda
	WHERE EP.IdEstilo = @estilo1 AND CP.IdColor=@color1 AND TP.IdTemporada=@temporada1 AND Prenda.mostrar=1
END
GO
USE [master]
GO
ALTER DATABASE [Opa] SET  READ_WRITE 
GO
