USE [Test_JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetBlogData]    Script Date: 3/9/2016 5:53:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetBlogData] AS

SELECT *
FROM Blog
GO

