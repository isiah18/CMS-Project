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

