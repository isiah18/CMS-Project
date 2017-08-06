USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[CreateTag]    Script Date: 3/14/2016 11:48:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateTag]
(
	@tagId int OUTPUT,
	@tagName nvarchar(200)
)
AS
INSERT INTO Tags(Name)
VALUES(@tagName)
SET @tagId = SCOPE_IDENTITY()
GO

