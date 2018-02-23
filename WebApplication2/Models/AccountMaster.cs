using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication2.Models
{
   public  class NotSqlEffect :Attribute
    {

    }
    public class AccountMaster
    {
        CollectionDataLayer CollectionDataLayer = new CollectionDataLayer();
        [Key]
        public int AccountMasterId { get; set; }
        [Required]
        public string AccountNo { get; set; }
        [Required]
        public string AccountName { get; set; }
        public string PanNo { get; set; }
        [Required]
        [DisplayName("Amount")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime ? EndDate { get; set; }
        public string AccountImagePath { get; set; }
        [NotSqlEffect]
        public HttpPostedFileBase AccountImage { get; set; }
        public virtual List<Collection>Collection { 
            get
        
            {
                List<Collection> Collection = new List<Collection>();
                
                Collection = CollectionDataLayer.ToList().Where(t => t.AccountMasterId == this.AccountMasterId).ToList();
                return  Collection;
            }

        }
        [DisplayName("Amount")]
        public virtual decimal AmountCollection
        {
            get
            {
                return this.Collection.Sum(x => x.Amount);
            }

        }
    }


    public class AccountDataLayer : DBConnection
    {

        public IList<AccountMaster>ToList()
        {
            IList<AccountMaster> AccountMasters = new List<AccountMaster>();
            DataTable dt = new DataTable();
            AccountMasters = GetDataList<AccountMaster>("Select *  from AccountMaster");
             
            return AccountMasters;

        }

        public  AccountMaster Find (int ? id)
        {
            AccountMaster AccountMaster = new AccountMaster();
            AccountMaster = GetDataList<AccountMaster>("Select *  from AccountMaster where AccountMasterId=" + id).SingleOrDefault();
            return AccountMaster;

        }

        public DataTable GetProcedure (string ProcedureName)
        {
            return GetData(ProcedureName);
        }
        
        public bool InsertUpdate(AccountMaster AccountMaster)
        {

          

               try
               {
                      string sql=string.Empty;
                   sql = InsertUpdateQuery(AccountMaster);
                   excuteCommand(sql);
                   sql = "SELECT IDENT_CURRENT('AccountMaster' ) ";
                   if (AccountMaster.AccountMasterId==0 )
                AccountMaster.AccountMasterId= Convert.ToInt32(  GetScalerValue(sql));

 
               }
               catch (Exception ex)
               {

                   return false;
               }
               finally
               {

                   
               }
           
            
           return true;
        }

        public bool Deleted(int Id)
        {

            try
            {
                string sql = string.Empty;
                sql = "Update AccountMaster set IsActive=0 where AccountMasterId=" + Id.ToString();
                excuteCommand(sql);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
    }
}