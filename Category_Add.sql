USE [OnlineShop]
GO
/****** Object:  StoredProcedure [dbo].[Category_Add]    Script Date: 06/11/2017 16:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Category_Add]
	@name nvarchar(50),
	@alias varchar(50),
	@parentId int,
	@order int,
	@status bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    insert into Category(Name, Alias, ParentId, CreatedDate, [Order], Status)
    values(@name, @alias, @parentId, getdate(), @order, @status)
END
