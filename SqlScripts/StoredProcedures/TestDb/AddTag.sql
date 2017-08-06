USE [Test_JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[AddTag]    Script Date: 3/14/2016 12:10:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddTag]
(
	@Name nvarchar(200),
	@Id int OUTPUT
) AS

INSERT INTO Tags (Name)
VALUES (@Name)

SET @Id = SCOPE_IDENTITY()
GO

