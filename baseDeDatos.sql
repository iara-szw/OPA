USE [master]
GO
/****** Object:  Database [Opa]    Script Date: 26/9/2025 15:46:30 ******/
CREATE DATABASE [Opa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Opa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Opa.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Opa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Opa_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
EXEC sys.sp_db_vardecimal_storage_format N'Opa', N'ON'
GO
ALTER DATABASE [Opa] SET QUERY_STORE = OFF
GO
USE [Opa]
GO
/****** Object:  User [alumno]    Script Date: 26/9/2025 15:46:30 ******/
CREATE USER [alumno] FOR LOGIN [alumno] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Administrador]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrador](
	[IdAdministrador] [int] NOT NULL,
	[IdTienda] [int] NOT NULL,
	[IdVendedor] [int] NOT NULL,
	[Permisos] [bit] NOT NULL,
 CONSTRAINT [PK_Administrador] PRIMARY KEY CLUSTERED 
(
	[IdAdministrador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Adquirido]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adquirido](
	[IdAdquirido] [int] NOT NULL,
 CONSTRAINT [PK_Adquirido] PRIMARY KEY CLUSTERED 
(
	[IdAdquirido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Calificacion]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calificacion](
	[IdCalificacion] [int] NOT NULL,
	[Tipo] [varchar](20) NOT NULL,
	[IdPrenda] [int] NULL,
	[IdTienda] [int] NULL,
	[Puntaje] [decimal](1, 0) NOT NULL,
	[Comentario] [varchar](2000) NULL,
	[IdComprador] [int] NOT NULL,
 CONSTRAINT [PK_Calificacion] PRIMARY KEY CLUSTERED 
(
	[IdCalificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carrito]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrito](
	[IdCarrito] [int] NOT NULL,
	[IdComprador] [int] NOT NULL,
	[IdPrenda] [int] NOT NULL,
 CONSTRAINT [PK_Carrito] PRIMARY KEY CLUSTERED 
(
	[IdCarrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comprador]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comprador](
	[IdComprador] [int] NOT NULL,
	[Usuario] [varchar](100) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Contraseña] [varchar](500) NOT NULL,
	[Telefono] [varchar](200) NOT NULL,
	[FotoDePerfil] [varchar](200) NULL,
	[Mail] [varchar](200) NOT NULL,
	[Genero] [int] NOT NULL,
	[MedidaTorso] [decimal](3, 2) NULL,
	[MedioDePago] [int] NULL,
 CONSTRAINT [PK_Comprador] PRIMARY KEY CLUSTERED 
(
	[IdComprador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deseado]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deseado](
	[IdDeseado] [int] NOT NULL,
	[IdPrenda] [int] NOT NULL,
	[IdComprador] [int] NOT NULL,
 CONSTRAINT [PK_Deseado] PRIMARY KEY CLUSTERED 
(
	[IdDeseado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genero]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genero](
	[IdGenero] [int] NOT NULL,
 CONSTRAINT [PK_Genero] PRIMARY KEY CLUSTERED 
(
	[IdGenero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imagen]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imagen](
	[IdImagen] [int] NOT NULL,
	[IdTienda] [int] NOT NULL,
 CONSTRAINT [PK_Imagen] PRIMARY KEY CLUSTERED 
(
	[IdImagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedioDePago]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedioDePago](
	[IdMedioDePago] [int] NOT NULL,
 CONSTRAINT [PK_MedioDePago] PRIMARY KEY CLUSTERED 
(
	[IdMedioDePago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Outfit]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Outfit](
	[IdOutfit] [int] NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[IdPrenda] [int] NOT NULL,
	[Creador] [varchar](200) NOT NULL,
	[Likes] [int] NOT NULL,
 CONSTRAINT [PK_Outfit] PRIMARY KEY CLUSTERED 
(
	[IdOutfit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preferencia]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preferencia](
	[IdPreferencia] [int] NOT NULL,
 CONSTRAINT [PK_Preferencia] PRIMARY KEY CLUSTERED 
(
	[IdPreferencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prenda]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prenda](
	[Tipo] [varchar](100) NOT NULL,
	[IdPrenda] [int] NOT NULL,
	[Modelo] [varchar](100) NOT NULL,
	[IdTalle] [int] NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Precio] [decimal](15, 2) NOT NULL,
 CONSTRAINT [PK_Prenda] PRIMARY KEY CLUSTERED 
(
	[IdPrenda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrendaTienda]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrendaTienda](
	[IdPrendaTienda] [int] NOT NULL,
	[IdPrenda] [int] NOT NULL,
	[IdTienda] [int] NOT NULL,
 CONSTRAINT [PK_PrendaTienda] PRIMARY KEY CLUSTERED 
(
	[IdPrendaTienda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Talle]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Talle](
	[IdTalle] [int] NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Talle] PRIMARY KEY CLUSTERED 
(
	[IdTalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tienda]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tienda](
	[IdTienda] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Vendedor]    Script Date: 26/9/2025 15:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendedor](
	[IdVendedor] [int] NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Apellido] [varchar](200) NOT NULL,
	[Telefono] [varchar](200) NOT NULL,
	[Mail] [varchar](200) NOT NULL,
	[FotoDePerfil] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Vendedor] PRIMARY KEY CLUSTERED 
(
	[IdVendedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Administrador]  WITH CHECK ADD  CONSTRAINT [FK_Administrador_Tienda] FOREIGN KEY([IdTienda])
REFERENCES [dbo].[Tienda] ([IdTienda])
GO
ALTER TABLE [dbo].[Administrador] CHECK CONSTRAINT [FK_Administrador_Tienda]
GO
ALTER TABLE [dbo].[Administrador]  WITH CHECK ADD  CONSTRAINT [FK_Administrador_Vendedor] FOREIGN KEY([IdVendedor])
REFERENCES [dbo].[Vendedor] ([IdVendedor])
GO
ALTER TABLE [dbo].[Administrador] CHECK CONSTRAINT [FK_Administrador_Vendedor]
GO
ALTER TABLE [dbo].[Calificacion]  WITH CHECK ADD  CONSTRAINT [FK_Calificacion_Comprador] FOREIGN KEY([IdComprador])
REFERENCES [dbo].[Comprador] ([IdComprador])
GO
ALTER TABLE [dbo].[Calificacion] CHECK CONSTRAINT [FK_Calificacion_Comprador]
GO
ALTER TABLE [dbo].[Calificacion]  WITH CHECK ADD  CONSTRAINT [FK_Calificacion_Prenda] FOREIGN KEY([IdPrenda])
REFERENCES [dbo].[Prenda] ([IdPrenda])
GO
ALTER TABLE [dbo].[Calificacion] CHECK CONSTRAINT [FK_Calificacion_Prenda]
GO
ALTER TABLE [dbo].[Calificacion]  WITH CHECK ADD  CONSTRAINT [FK_Calificacion_Tienda] FOREIGN KEY([IdTienda])
REFERENCES [dbo].[Tienda] ([IdTienda])
GO
ALTER TABLE [dbo].[Calificacion] CHECK CONSTRAINT [FK_Calificacion_Tienda]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Comprador] FOREIGN KEY([IdComprador])
REFERENCES [dbo].[Comprador] ([IdComprador])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Comprador]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Prenda] FOREIGN KEY([IdPrenda])
REFERENCES [dbo].[Prenda] ([IdPrenda])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Prenda]
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
ALTER TABLE [dbo].[Deseado]  WITH CHECK ADD  CONSTRAINT [FK_Deseado_Comprador] FOREIGN KEY([IdComprador])
REFERENCES [dbo].[Comprador] ([IdComprador])
GO
ALTER TABLE [dbo].[Deseado] CHECK CONSTRAINT [FK_Deseado_Comprador]
GO
ALTER TABLE [dbo].[Deseado]  WITH CHECK ADD  CONSTRAINT [FK_Deseado_Prenda] FOREIGN KEY([IdPrenda])
REFERENCES [dbo].[Prenda] ([IdPrenda])
GO
ALTER TABLE [dbo].[Deseado] CHECK CONSTRAINT [FK_Deseado_Prenda]
GO
ALTER TABLE [dbo].[Imagen]  WITH CHECK ADD  CONSTRAINT [FK_Imagen_Tienda] FOREIGN KEY([IdTienda])
REFERENCES [dbo].[Tienda] ([IdTienda])
GO
ALTER TABLE [dbo].[Imagen] CHECK CONSTRAINT [FK_Imagen_Tienda]
GO
ALTER TABLE [dbo].[Outfit]  WITH CHECK ADD  CONSTRAINT [FK_Outfit_Prenda] FOREIGN KEY([IdPrenda])
REFERENCES [dbo].[Prenda] ([IdPrenda])
GO
ALTER TABLE [dbo].[Outfit] CHECK CONSTRAINT [FK_Outfit_Prenda]
GO
ALTER TABLE [dbo].[Prenda]  WITH CHECK ADD  CONSTRAINT [FK_Prenda_Talle] FOREIGN KEY([IdTalle])
REFERENCES [dbo].[Talle] ([IdTalle])
GO
ALTER TABLE [dbo].[Prenda] CHECK CONSTRAINT [FK_Prenda_Talle]
GO
ALTER TABLE [dbo].[PrendaTienda]  WITH CHECK ADD  CONSTRAINT [FK_PrendaTienda_Prenda] FOREIGN KEY([IdPrenda])
REFERENCES [dbo].[Prenda] ([IdPrenda])
GO
ALTER TABLE [dbo].[PrendaTienda] CHECK CONSTRAINT [FK_PrendaTienda_Prenda]
GO
ALTER TABLE [dbo].[PrendaTienda]  WITH CHECK ADD  CONSTRAINT [FK_PrendaTienda_Tienda] FOREIGN KEY([IdTienda])
REFERENCES [dbo].[Tienda] ([IdTienda])
GO
ALTER TABLE [dbo].[PrendaTienda] CHECK CONSTRAINT [FK_PrendaTienda_Tienda]
GO
USE [master]
GO
ALTER DATABASE [Opa] SET  READ_WRITE 
GO