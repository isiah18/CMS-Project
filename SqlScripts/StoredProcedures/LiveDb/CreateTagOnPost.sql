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

