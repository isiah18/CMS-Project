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

