USE [Test_JIST_Capstone]
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

