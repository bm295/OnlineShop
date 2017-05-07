USE [OnlineShop]
GO
/****** Object:  StoredProcedure [dbo].[Account_Login]    Script Date: 05/07/2017 19:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Account_Login]
	-- Add the parameters for the stored procedure here
	@userName varchar(20),
	@password varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @count int
	declare @result bit
	select @count = count(1) from Account with(nolock)
	where UserName = @userName and Password = @password
	
	if @count > 0
		set @result = 1
	else
		set @result = 0
		
	select @result
END
