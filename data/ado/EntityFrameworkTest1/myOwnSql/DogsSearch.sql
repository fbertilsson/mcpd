-- ================================================
-- Template generated from Template Explorer using:
-- Create Multi-Statement Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fredrik
-- Create date: 
-- Description:	
-- =============================================
CREATE FUNCTION DogsSearch 
(
	-- Add the parameters for the function here
	@searchstring nvarchar(200)
)
RETURNS 
TABLE 
AS
	RETURN select * from dbo.AnimalSet_Dog d where d.PetName like @searchstring;
GO