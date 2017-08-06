USE [master]
GO
/****** Object:  Database [Test_JIST_Capstone]    Script Date: 3/9/2016 5:17:00 PM ******/
CREATE DATABASE [Test_JIST_Capstone]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Test_JIST_Capstone', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014\MSSQL\DATA\Test_JIST_Capstone' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Test_JIST_Capstone_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014\MSSQL\DATA\Test_JIST_Capstone_log.ldf' , SIZE = 3072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Test_JIST_Capstone] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Test_JIST_Capstone].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Test_JIST_Capstone] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET ARITHABORT OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Test_JIST_Capstone] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Test_JIST_Capstone] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Test_JIST_Capstone] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Test_JIST_Capstone] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET RECOVERY FULL 
GO
ALTER DATABASE [Test_JIST_Capstone] SET  MULTI_USER 
GO
ALTER DATABASE [Test_JIST_Capstone] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Test_JIST_Capstone] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Test_JIST_Capstone] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Test_JIST_Capstone] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Test_JIST_Capstone] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Test_JIST_Capstone', N'ON'
GO
USE [Test_JIST_Capstone]
GO
/****** Object:  Table [dbo].[AboutBlog]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AboutBlog](
	[IsLive] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[ImagePath] [nvarchar](50) NULL,
	[Twitter] [nvarchar](50) NULL,
	[Facebook] [nvarchar](50) NULL,
	[Instagram] [nvarchar](50) NULL,
	[LinkedIn] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Blog]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[AdminId] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Tagline] [nvarchar](50) NULL,
	[ContactDescription] [nvarchar](2500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoriesOnPosts]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriesOnPosts](
	[PostId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_PostCategories] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Inquiries]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inquiries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InquiryStatusId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Inquiries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InquiryStatus]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InquiryStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_InquiryStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Posts]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[PostStatusId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[PublishDate] [datetime] NOT NULL,
	[ExpireDate] [datetime] NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostStatuses]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PostStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StaticPages]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaticPages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsLive] [bit] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Content] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](100) NULL,
 CONSTRAINT [PK_StaticPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tags]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TagsOnPosts]    Script Date: 3/9/2016 5:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagsOnPosts](
	[PostId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_PostTags] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[AboutBlog] ([IsLive], [Title], [Content], [ImagePath], [Twitter], [Facebook], [Instagram], [LinkedIn]) VALUES (1, N'about the JIST crew', N'we write code', NULL, N'twitter', N'facebook', N'instagram', N'linkedin')
INSERT [dbo].[Blog] ([AdminId], [Title], [Tagline], [ContactDescription]) VALUES (1, N'JIST blog', N'this is our blog', N'hey fill this out to contact me!')
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Turtles')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'.NET')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'jQuery')
SET IDENTITY_INSERT [dbo].[Categories] OFF
INSERT [dbo].[CategoriesOnPosts] ([PostId], [CategoryId]) VALUES (1, 2)
SET IDENTITY_INSERT [dbo].[Inquiries] ON 

INSERT [dbo].[Inquiries] ([Id], [InquiryStatusId], [Name], [Phone], [Email], [Message]) VALUES (1, 1, N'Janet Jackson', NULL, N'jj@gmail.com', N'Nice job on the site!')
SET IDENTITY_INSERT [dbo].[Inquiries] OFF
SET IDENTITY_INSERT [dbo].[InquiryStatus] ON 

INSERT [dbo].[InquiryStatus] ([Id], [Status]) VALUES (1, N'New')
INSERT [dbo].[InquiryStatus] ([Id], [Status]) VALUES (2, N'Reviewed')
SET IDENTITY_INSERT [dbo].[InquiryStatus] OFF
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([Id], [AuthorId], [PostStatusId], [CreatedDate], [PublishDate], [ExpireDate], [Title], [Content]) VALUES (1, 1, 4, CAST(N'2016-01-04 00:00:00.000' AS DateTime), CAST(N'2016-01-04 00:00:00.000' AS DateTime), NULL, N'Started Bootcamp', N'Learning C#.')
SET IDENTITY_INSERT [dbo].[Posts] OFF
SET IDENTITY_INSERT [dbo].[PostStatuses] ON 

INSERT [dbo].[PostStatuses] ([Id], [Status]) VALUES (1, N'Draft')
INSERT [dbo].[PostStatuses] ([Id], [Status]) VALUES (2, N'PendingReview')
INSERT [dbo].[PostStatuses] ([Id], [Status]) VALUES (3, N'NeedsRevised')
INSERT [dbo].[PostStatuses] ([Id], [Status]) VALUES (4, N'Approved')
INSERT [dbo].[PostStatuses] ([Id], [Status]) VALUES (5, N'FutureDate')
INSERT [dbo].[PostStatuses] ([Id], [Status]) VALUES (6, N'Expired')
SET IDENTITY_INSERT [dbo].[PostStatuses] OFF
SET IDENTITY_INSERT [dbo].[StaticPages] ON 

INSERT [dbo].[StaticPages] ([Id], [IsLive], [Title], [Content], [ImagePath]) VALUES (1, 1, N'Test Static Page', N'This is a test', NULL)
SET IDENTITY_INSERT [dbo].[StaticPages] OFF
SET IDENTITY_INSERT [dbo].[Tags] ON 

INSERT [dbo].[Tags] ([Id], [Name]) VALUES (1, N'NASA')
INSERT [dbo].[Tags] ([Id], [Name]) VALUES (2, N'Cleveland')
SET IDENTITY_INSERT [dbo].[Tags] OFF
INSERT [dbo].[TagsOnPosts] ([PostId], [TagId]) VALUES (1, 2)
ALTER TABLE [dbo].[CategoriesOnPosts]  WITH CHECK ADD  CONSTRAINT [FK_CategoriesOnPosts_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[CategoriesOnPosts] CHECK CONSTRAINT [FK_CategoriesOnPosts_Categories]
GO
ALTER TABLE [dbo].[CategoriesOnPosts]  WITH CHECK ADD  CONSTRAINT [FK_CategoriesOnPosts_Posts] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[CategoriesOnPosts] CHECK CONSTRAINT [FK_CategoriesOnPosts_Posts]
GO
ALTER TABLE [dbo].[Inquiries]  WITH CHECK ADD  CONSTRAINT [FK_Inquiries_InquiryStatus] FOREIGN KEY([InquiryStatusId])
REFERENCES [dbo].[InquiryStatus] ([Id])
GO
ALTER TABLE [dbo].[Inquiries] CHECK CONSTRAINT [FK_Inquiries_InquiryStatus]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_PostStatuses] FOREIGN KEY([PostStatusId])
REFERENCES [dbo].[PostStatuses] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_PostStatuses]
GO
ALTER TABLE [dbo].[TagsOnPosts]  WITH CHECK ADD  CONSTRAINT [FK_TagsOnPosts_Posts] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[TagsOnPosts] CHECK CONSTRAINT [FK_TagsOnPosts_Posts]
GO
ALTER TABLE [dbo].[TagsOnPosts]  WITH CHECK ADD  CONSTRAINT [FK_TagsOnPosts_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([Id])
GO
ALTER TABLE [dbo].[TagsOnPosts] CHECK CONSTRAINT [FK_TagsOnPosts_Tags]
GO
USE [master]
GO
ALTER DATABASE [Test_JIST_Capstone] SET  READ_WRITE 
GO
