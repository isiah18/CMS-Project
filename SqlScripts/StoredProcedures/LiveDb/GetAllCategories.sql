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

