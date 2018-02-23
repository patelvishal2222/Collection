alter Procedure GenrateCollection
(
@startDate AS datetime,
@EndDate AS datetime=NULL,
@AccountMasterId as Int=NULL
)
as
BEGIN
IF @EndDate IS NULL
SET @EndDate=@startDate

 ;WITH  DateMaster AS
 (
 SELECT  @startDate AS curDate 
 UNION all
 
 SELECT  DATEADD(d,1,curDate) FROM DateMaster WHERE curDate<@EndDate
 )

 INSERT INTO [Collection]
 (AccountMasterId,CollectionDate,ReceiptDate,Amount,IsAuto,IsAutoDate)
 SELECT 
 AM.AccountMasterId,
 DM.curDate AS CollectionDate,
 DM.curDate AS ReceiptDate,
 AM.Amount, 
 1 AS IsAuto,
 GETUTCDATE() As IsAutoDate 
 FROM  AccountMaster AM,DateMaster DM
 WHERE 
 AM.StartDate<=DM.curDate  
 AND ISNULL(AM.EndDate,DATEADD(d,1,DM.curDate))>=DM.curDate
 AND (@AccountMasterId IS NULL   OR AM.AccountMasterId=@AccountMasterId)
  AND Not Exists (SELECT  *  FROM [Collection] C WHERE
  c.AccountMasterId=AM.AccountMasterId
  AND C.CollectionDate= DM.curDate)
  OPTION (MAXRECURSION 0)


 
 END
