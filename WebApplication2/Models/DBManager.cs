using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class DBManager : DbContext
    {
        public DBManager()
            : base("test")
        {
           
        }

        //public static ApplicationDbContext Create()
        //{
        //    return new ApplicationDbContext();
        //}

        
        // public DbSet<Test> Tests { get; set; }
         public DbSet<AccountMaster> AccountMaster { get; set; }
         public DbSet<Collection> Collection { get; set; }
    }

    public class DBConnection
    {

        SqlConnection con = new SqlConnection();
        public DBConnection()
        {
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["test"].ConnectionString;

            con = new SqlConnection(strcon);
        }
        public List<T> GetDataList<T>(string sql)
        {
            DataTable dt = new DataTable();
            List<T> Entity = new List<T>();
            dt = GetData(sql);
            Entity = ConvertDataTable<T>(dt);
            return Entity;

        }

        public DataTable GetData(String sql)
        {

            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter(sql, con);
                ad.Fill(dt);


            }
            catch (Exception e)
            {

            }
            finally
            {
                //con.Close();
            }
            return dt;
        }

        public object GetScalerValue(String sql)
        {

            
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                con.Open();

                return cmd.ExecuteScalar();

            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }

        public SqlDataReader GetDataReder(String sql)
        {

            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                con.Open();

                reader = cmd.ExecuteReader();

            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }
            return reader;
        }
        public bool excuteCommand(string sql)
        {

            SqlCommand cmd = new SqlCommand();
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = sql;
               // cmd.ExecuteScalar();

                cmd.CommandType = CommandType.Text;

                if (cmd.ExecuteNonQuery() > 0)

                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public string InsertUpdateQuery<T>(T obj)
        {
            string sql=string.Empty;
            string InsertSql=string.Empty;
            string UpdateSql=string.Empty;
            string InsertField=string.Empty;
            string InsertValue=string.Empty;
            string UpdateFieldValue=string.Empty ;
            string TableName = string.Empty;
            string KeyName = string.Empty;
            int KeyValue = 0;
            bool NoQuery = false;
             Type TypeObject = typeof(T);
             TableName = TypeObject.Name;

             foreach (PropertyInfo pro in TypeObject.GetProperties())
                {
                     if(pro.GetGetMethod().IsVirtual ==true  )
                     {

                     }
                     else if(pro.GetCustomAttributes().Count()>0)
                     {
                         NoQuery = false;
                         foreach(Attribute Attribute in pro.GetCustomAttributes() )
                         {
                             if (Attribute is KeyAttribute)
                             {
                                 KeyName = pro.Name;
                                 KeyValue = Convert.ToInt32(pro.GetValue(obj).ToString());
                                 NoQuery = true;
                             }
                             else if (Attribute is NotSqlEffect)
                             {
                                 NoQuery = true;
                             }
                            
                             
                         }

                          if ( NoQuery==false )
                             {
                                
                                 DataTypeQuery<T>(pro, obj, ref InsertField, ref InsertValue, ref UpdateFieldValue);
                             }
                     }
                     else
                     {

                         DataTypeQuery<T>(pro, obj, ref InsertField, ref InsertValue, ref UpdateFieldValue);
                        
                       

                     }
                 }
             InsertField = RemoveLastComm(InsertField);
             InsertValue = RemoveLastComm(InsertValue);
             UpdateFieldValue = RemoveLastComm(UpdateFieldValue);
             InsertSql = "Insert Into " + TableName + "(" + InsertField + ")values(" + InsertValue + ")";
             UpdateSql = "Update " + TableName + " Set " + UpdateFieldValue + " WHERE " + KeyName + "=" + KeyValue.ToString();

                 if(KeyValue==0)
                 {
                     sql=InsertSql;
                 }
                 else
                 {
                     sql = UpdateSql;
                 }

           return sql;

        }
        public void DataTypeQuery<T>(PropertyInfo pro,T obj,ref  string  InsertField,ref string  InsertValue,ref string UpdateFieldValue)
        {
         InsertField = InsertField + pro.Name + ",";
                         if (pro.PropertyType == typeof(string))
                         {

                             InsertValue = InsertValue + "'" + pro.GetValue(obj) + "',";
                             UpdateFieldValue = UpdateFieldValue + pro.Name + "='" + pro.GetValue(obj) + "',";
                         }
                         else if (pro.PropertyType == typeof(Int32) ||  pro.PropertyType == typeof(System.Decimal))
                         {
                             InsertValue = InsertValue + "" + pro.GetValue(obj) + ",";
                             UpdateFieldValue = UpdateFieldValue + pro.Name + "=" + pro.GetValue(obj) + ",";


                         }
                         else if (pro.PropertyType == typeof(bool))
                         {
                             InsertValue = InsertValue + "" + ( Convert.ToBoolean( pro.GetValue(obj)) ==true?1:0 )+ ",";
                             UpdateFieldValue = UpdateFieldValue + pro.Name + "=" + (Convert.ToBoolean( pro.GetValue(obj))==true?1:0) + ",";

                             }
                         else
                         {
                             InsertValue = InsertValue + "'" + pro.GetValue(obj) + "',";
                             UpdateFieldValue = UpdateFieldValue + pro.Name + "='" + pro.GetValue(obj) + "',";
                         }
        }


        public string RemoveLastComm(string str)
        {
            if(str.LastIndexOf(",")>0)
            {
                str = str.Substring(0, str.LastIndexOf(","));

            }
            return str;
        }
        protected  List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        protected static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (! ( dr[column.ColumnName] is  DBNull) )
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else if(pro.GetGetMethod().IsVirtual==true)
                    {
                       
                    }
                    else 
                        continue;
                }
            }
            return obj;
        }

        public DataTable ToDataTables<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prp = props[i];
                table.Columns.Add(prp.Name, prp.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}