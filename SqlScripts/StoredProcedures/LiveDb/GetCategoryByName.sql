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

