USE [Test_JIST_Capstone]
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

