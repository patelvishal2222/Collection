ALTER PROCEDURE MonthView
(
@AccountMasteId  int=NULL
)
AS
Begin
DECLARE @cols AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX)

select @cols = STUFF((SELECT distinct ',' + QUOTENAME(AccountMasterId) 
                    from dbo.AccountMaster am where  ( @AccountMasteId is NULL OR Am.AccountMasterId=@AccountMasteId)
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

set @query = 'Declare @AccountMasteId_  int='+Convert(varchar,ISNULL(@AccountMasteId,0))+';
SELECT MonthName, ' + @cols + ' from 
             (
                select 
				 convert(varchar(7), ReceiptDate, 126) as  MonthName,
				
				AccountMasterId,
                Amount
				
                from  Collection'+case when @AccountMasteId is null then '' else ' where AccountMasterId=@AccountMasteId_ ' END
				+ '
				 
            ) x
            pivot 
            (
                sum(Amount)
				
                for AccountMasterId in (' + @cols + ')
            ) p '
			print(@query)
execute(@query)
END