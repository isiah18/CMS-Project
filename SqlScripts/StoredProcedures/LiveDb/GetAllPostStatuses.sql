USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[GetAllPostStatuses]    Script Date: 3/14/2016 11:49:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllPostStatuses]
AS
SELECT *
FROM PostStatuses


GO

