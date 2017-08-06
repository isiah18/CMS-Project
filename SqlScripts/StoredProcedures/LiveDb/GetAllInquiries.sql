USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetAllInquiries]    Script Date: 3/9/2016 5:40:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllInquiries]
AS
SELECT * 
FROM Inquiries


GO

