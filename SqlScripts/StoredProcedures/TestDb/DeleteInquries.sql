USE [Test_JIST_Capstone]
GO

/****** Object:  StoredProcedure [dbo].[DeleteInquiry]    Script Date: 3/14/2016 11:35:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteInquiry]
(
	@InquiryId int
)
AS

DELETE FROM Inquiries
WHERE Inquiries.Id = @InquiryId
GO

