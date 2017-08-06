USE [JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[AddInquiries]    Script Date: 3/11/2016 2:44:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddInquiries]
(
	@InquiryStatusId int,
    	@Name nvarchar(100),
	@Phone nvarchar(20) ,
	@Email nvarchar(50) ,
	@Message nvarchar(MAX)

) AS
BEGIN

INSERT INTO Inquiries(InquiryStatusId, Name, Phone, Email, [Message])
VALUES (@InquiryStatusId,@Name, @Phone, @Email, @Message)

END
GO

