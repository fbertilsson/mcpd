-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
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
-- Author:		Fredrik Bertilsson
-- Create date: 
-- Description:	
-- =============================================
CREATE FUNCTION dbo.count_dogs 
(
	-- Add the parameters for the function here
	@p1 int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = COUNT(*) from dbo.AnimalSet_Dog

	-- Return the result of the function
	RETURN @Result

END
GO

