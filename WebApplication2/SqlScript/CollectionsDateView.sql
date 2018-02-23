alter PROCEDURE collectionDateView
AS
Begin
DECLARE @cols AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX)

select @cols = STUFF((SELECT distinct ',' + QUOTENAME(AccountMasterId) 
                    from dbo.AccountMaster am
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')


		select @cols = STUFF((SELECT distinct ',' + QUOTENAME(collectionDate) 
                  from  [Collection]
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')
set @query = 'SELECT AccountMasterId, ' + @cols + ' from 
             (
                select 
				 collectionDate,
				
				AccountMasterId,
                Amount
                from  Collection
            ) x
            pivot 
            (
                sum(Amount)
                for collectionDate in (' + @cols + ')
            ) p
			order by AccountMasterId asc
			 '

execute(@query)
END


