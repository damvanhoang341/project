USE [master]
GO
/****** Object:  Database [QuizDB]    Script Date: 6/11/2025 6:54:16 PM ******/
CREATE DATABASE [QuizDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuizDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuizDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuizDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuizDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QuizDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuizDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuizDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuizDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuizDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuizDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuizDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuizDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuizDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuizDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuizDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuizDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuizDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuizDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuizDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuizDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuizDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuizDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuizDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuizDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuizDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuizDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuizDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuizDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuizDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuizDB] SET  MULTI_USER 
GO
ALTER DATABASE [QuizDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuizDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuizDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuizDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuizDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuizDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QuizDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [QuizDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200)
GO
USE [QuizDB]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 6/11/2025 6:54:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[id] [varchar](10) NOT NULL,
	[content] [text] NOT NULL,
	[questionid] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 6/11/2025 6:54:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[id] [varchar](10) NOT NULL,
	[content] [text] NOT NULL,
	[correctanswer] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question_Test]    Script Date: 6/11/2025 6:54:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question_Test](
	[testid] [varchar](10) NOT NULL,
	[questionid] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[testid] ASC,
	[questionid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 6/11/2025 6:54:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[id] [varchar](10) NOT NULL,
	[code] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/11/2025 6:54:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a1', N'3', N'q1')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a10', N'Hình c?u', N'q3')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a11', N'Ðúng', N'q4')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a12', N'Sai', N'q4')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a2', N'0', N'q1')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a3', N'4', N'q1')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a4', N'2', N'q1')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a5', N'Tây', N'q2')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a6', N'Nam', N'q2')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a7', N'Ðông', N'q2')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a8', N'Hình vuông', N'q3')
INSERT [dbo].[Answer] ([id], [content], [questionid]) VALUES (N'a9', N'Tam giác', N'q3')
GO
INSERT [dbo].[Question] ([id], [content], [correctanswer]) VALUES (N'q1', N'1 + 1 b?ng bao nhiêu?', N'a4')
INSERT [dbo].[Question] ([id], [content], [correctanswer]) VALUES (N'q2', N'M?t tr?i m?c ? hu?ng nào?', N'a7')
INSERT [dbo].[Question] ([id], [content], [correctanswer]) VALUES (N'q3', N'Trái d?t có hình gì?', N'a10')
INSERT [dbo].[Question] ([id], [content], [correctanswer]) VALUES (N'q4', N'Trái d?t quay quanh m?t tr?i là dúng?', N'a11')
GO
INSERT [dbo].[Question_Test] ([testid], [questionid]) VALUES (N't1', N'q1')
INSERT [dbo].[Question_Test] ([testid], [questionid]) VALUES (N't1', N'q2')
INSERT [dbo].[Question_Test] ([testid], [questionid]) VALUES (N't1', N'q4')
INSERT [dbo].[Question_Test] ([testid], [questionid]) VALUES (N't2', N'q2')
INSERT [dbo].[Question_Test] ([testid], [questionid]) VALUES (N't2', N'q3')
GO
INSERT [dbo].[Test] ([id], [code]) VALUES (N't1', N'PRN1838_Test1')
INSERT [dbo].[Test] ([id], [code]) VALUES (N't2', N'PRN1838_Test2')
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD FOREIGN KEY([questionid])
REFERENCES [dbo].[Question] ([id])
GO
ALTER TABLE [dbo].[Question_Test]  WITH CHECK ADD FOREIGN KEY([questionid])
REFERENCES [dbo].[Question] ([id])
GO
ALTER TABLE [dbo].[Question_Test]  WITH CHECK ADD FOREIGN KEY([testid])
REFERENCES [dbo].[Test] ([id])
GO
USE [master]
GO
ALTER DATABASE [QuizDB] SET  READ_WRITE 
GO
