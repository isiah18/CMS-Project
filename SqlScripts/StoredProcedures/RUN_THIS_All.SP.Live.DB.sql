USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[AddTag]    Script Date: 3/14/2016 12:10:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddTag]
(
	@Name nvarchar(200),
	@Id int OUTPUT
) AS

INSERT INTO Tags (Name)
VALUES (@Name)

SET @Id = SCOPE_IDENTITY()
GO



USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetTagByName]    Script Date: 3/14/2016 11:50:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTagByName]
(
	@tagName nvarchar(200)
)
AS
SELECT *
FROM Tags
Where Name = @tagName

GO



USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetPostStatusByName]    Script Date: 3/14/2016 11:50:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPostStatusByName]
(
	@status nvarchar(30)
)
AS
SELECT * 
FROM PostStatuses
WHERE [Status] = @status

GO



USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetCategoryByName]    Script Date: 3/14/2016 11:50:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCategoryByName]
(
	@categoryName nvarchar(200)
)
AS
SELECT * 
FROM Categories
WHERE Name = @categoryName

GO



USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetAllPostStatuses]    Script Date: 3/14/2016 11:49:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllPostStatuses]
AS
SELECT *
FROM PostStatuses


GO



USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetAllCategories]    Script Date: 3/14/2016 11:48:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllCategories]
AS
SELECT *
FROM Categories


GO



USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[CreateTagOnPost]    Script Date: 3/14/2016 11:48:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateTagOnPost]
(
	@tagId int,
	@postId int
)
AS
INSERT INTO TagsOnPosts(PostId, TagId)
VALUES(@postId, @tagId)

GO



USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[CreateTag]    Script Date: 3/14/2016 11:48:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateTag]
(
	@tagId int OUTPUT,
	@tagName nvarchar(200)
)
AS
INSERT INTO Tags(Name)
VALUES(@tagName)
SET @tagId = SCOPE_IDENTITY()
GO



USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[CreatePost]    Script Date: 3/14/2016 11:48:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreatePost]
(
	@postID int OUTPUT,
	@AuthorID nvarchar(128), 
	@PostStatusID int, 
	@CreatedDate datetime, 
	@PublishDate datetime, 
	@ExpireDate datetime, 
	@Title nvarchar(200), 
	@Content nvarchar(MAX)
)
AS
INSERT INTO Posts(AuthorId, PostStatusId, CreatedDate, PublishDate, [ExpireDate], Title, Content)
VALUES(@AuthorID, @PostStatusID, @CreatedDate, @PublishDate, @ExpireDate, @Title, @Content)

SET @postID = SCOPE_IDENTITY()

GO



USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[CreateCategoryOnPost]    Script Date: 3/14/2016 11:47:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateCategoryOnPost]
(
	@categoryId int,
	@postId int
)
AS
INSERT INTO CategoriesOnPosts(PostId, CategoryId)
VALUES(@postId, @categoryId)

GO

USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[AddInquiries]    Script Date: 3/11/2016 2:44:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddInquiries]
(
	@InquiryStatusId int,
    @Name nvarchar(100),
	@Phone nvarchar(20) ,
	@Email nvarchar(50) ,
	@Message nvarchar(MAX)

) AS
BEGIN

INSERT INTO Inquiries(InquiryStatusId, Name, Phone, Email, [Message])
VALUES (@InquiryStatusId,@Name, @Phone, @Email, @Message)

END
GO

USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetAllInquiries]    Script Date: 3/9/2016 5:40:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllInquiries]
AS
SELECT * 
FROM Inquiries


GO

USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetAllPosts]    Script Date: 3/9/2016 5:40:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllPosts]
AS
SELECT *
FROM Posts


GO

USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetBlogData]    Script Date: 3/9/2016 5:53:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetBlogData] AS

SELECT *
FROM Blog
GO

USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetStatusForSinglePost]    Script Date: 3/11/2016 11:53:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetStatusForSinglePost]
(
	@postID int
)
AS

SELECT PostStatuses.Id, [Status]
From Posts 
INNER JOIN PostStatuses 
ON Posts.PostStatusId = PostStatuses.Id
WHERE Posts.Id = @postID


GO

USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[DeletePost]    Script Date: 3/11/2016 2:47:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeletePost]
(
	@postID int
)
AS 

DELETE 
FROM Posts
WHERE Posts.Id = @postID

DELETE 
FROM TagsOnPosts
WHERE TagsOnPosts.PostId = @postID

DELETE
FROM CategoriesOnPosts
WHERE CategoriesOnPosts.PostId = @postID


GO

