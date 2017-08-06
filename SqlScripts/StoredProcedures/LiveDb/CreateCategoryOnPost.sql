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

