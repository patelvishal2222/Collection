using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Collection
    {
        [Key]
        public int CollectionId { get; set; }
        [Required]
        public int AccountMasterId { get; set; }
        [Required]
        public DateTime CollectionDate { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public bool IsDeleted { get; set; }
        
    }

    public class CollectionDataLayer : DBConnection
    {

        public IList<Collection> ToList()
        {
            IList<Collection> Collections = new List<Collection>();
            
            DataTable dt = new DataTable();
            dt = GetData("Select *  from Collection");
            Collections = ConvertDataTable<Collection>(dt);
            
            return Collections;

        }
        public Collection Find(int? id)
        {
            Collection Collection = new Collection();

            Collection = GetDataList<Collection>("Select *  from Collection where CollectionId=" + id).SingleOrDefault();
            

            return Collection;

        }

        public bool InsertUpdate(Collection Collection)
        {

            try
            {
                    string sql=string.Empty;
                   sql = InsertUpdateQuery(      Collection);
                   excuteCommand(sql);
               
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public bool changeReceiptDate(int? Id, DateTime  receiptDate)
        {

            try
            {
                string sql = string.Empty;
                sql = "Update Collection set ReceiptDate='" + receiptDate.ToString("dd-MMM-yyy") + "' where CollectionId=" + Id.ToString();
                excuteCommand(sql);

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool TransferReceiptDate( DateTime receiptDate,DateTime transferDate)
        {

            try
            {
                string sql = string.Empty;
                sql = "Update Collection set ReceiptDate='" + transferDate.ToString("dd-MMM-yyyy") + "' where ISNULL(IsDeleted,0)=0 and receiptDate='" + receiptDate.ToString("dd-MMM-yyyy") + "'";
                excuteCommand(sql);

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public bool Deleted(int ? Id,int value)
        {

            try
            {
                 string sql = string.Empty;
                 sql = "Update Collection set ISDeleted=" + value +" where CollectionId=" + Id.ToString();
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