USE [Test_JIST_Capstone]
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

