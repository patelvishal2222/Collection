alter PROCEDURE DateView
AS
Begin
DECLARE @cols AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX)

select @cols = STUFF((SELECT distinct ',' + QUOTENAME(AccountMasterId) 
                    from dbo.AccountMaster am
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

set @query = 'SELECT CollectionDate, ' + @cols + ' from 
             (
                select 
				 collectionDate,
				ReceiptDate,
				AccountMasterId,
                Amount
                from  Collection
            ) x
            pivot 
            (
                sum(Amount)
                for AccountMasterId in (' + @cols + ')
            ) p
			order by CollectionDate desc
			 '

execute(@query)
END