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

