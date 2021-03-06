USE [master]
GO

/****** Object:  Database [RocketEnglishDB]    Script Date: 21/05/2015 23:23:54 ******/
CREATE DATABASE [RocketEnglishDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RocketEnglishDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RocketEnglishDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RocketEnglishDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RocketEnglishDB_log.ldf' , SIZE = 1024KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO

ALTER DATABASE [RocketEnglishDB] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RocketEnglishDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [RocketEnglishDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [RocketEnglishDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [RocketEnglishDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [RocketEnglishDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [RocketEnglishDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [RocketEnglishDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [RocketEnglishDB] SET  MULTI_USER 
GO

ALTER DATABASE [RocketEnglishDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [RocketEnglishDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [RocketEnglishDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [RocketEnglishDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [RocketEnglishDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [RocketEnglishDB] SET  READ_WRITE 
GO

