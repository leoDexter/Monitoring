-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE CityPaging
	-- Add the parameters for the stored procedure here
	@TotalRecords BIGINT = NULL OUTPUT,
	@PageIndex INT,
	@PageSize INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Preenchendo o parâmetro de saída com o valor total de registros existentes
	SET @TotalRecords = (SELECT COUNT(C.Id) FROM City C);

	-- Executa a query de paginação
	SELECT pagedResults.Id, pagedResults.Name FROM 
	(
		SELECT ROW_NUMBER() OVER(ORDER BY C.Id) AS NUMBER, 
			C.Id,
			C.Name, 
			MAX(T.Date) as 'LastUpdate' 
		FROM City C 
			INNER JOIN Temperatures T ON C.Id = T.CityId					
		GROUP BY 
			C.Id, C.Name 

    ) AS pagedResults
	WHERE 
		NUMBER BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND (@PageIndex * @PageSize)
	ORDER BY 
		pagedResults.LastUpdate DESC

END