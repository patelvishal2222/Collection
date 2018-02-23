using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{


    
    public class Test
    {
        public int id { get; set; }
       [Required]
        public string name { get; set; }
        [Required]
       public string address { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [ValidBirthDate(ErrorMessage = "Birth Date can not be greater than current date")]
        [ValidLessDate]
        public DateTime BirthDate { get; set; }
        [Required]
        
        public int Age { get; set; }
        
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
        
        [DisplayName("Commform")]
        public string com { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [ValidLessDate(ErrorMessage = "Join date can not be less than 1-jan-1900")]
        public DateTime JoinDate { get; set; }
        [Required]



        
        public float Salary { get; set; }


        [Column]
        public string user_image { get; set; }

        //It provide access to individual files that have been uploaded by a client.
        public HttpPostedFileBase user_image_data { get; set; }
       // public class test:DbContext
      //  {
           
            //protected override void OnModelCreating(DbModelBuilder modelBuilder)
            //{
            //    modelBuilder.Entity<Test>().ToTable("Grievance", schemaName: "LRAT");

            //    base.OnModelCreating(modelBuilder);
            //}
       // }
    }
    //Database.SetInitializer<CustomerDBContext>(null);

    public class Test_BLayer
    {

        SqlConnection con=new SqlConnection();
        public Test_BLayer()
        {
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["test"].ConnectionString;

            con = new SqlConnection(strcon);
        }


        public bool insertUpdate(Test Test)
        {

            try
            {
            //    string sql=string.Empty;
               
            //    if(Test.id==0)
            //  sql= "Insert into Test  (name,address) values ('" + Test.name + "','"+Test.address+"')";
            //    else
            //        sql = "Update Test set   name='" + Test.name + "',address='"+Test.address+"'  where id="+Test.id.ToString();
                string procedure = "SP_InsertUpdate_test"; 
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = procedure;
                

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("Id", Test.id);
                cmd.Parameters.Add("name", Test.name);
                cmd.Parameters.Add("address", Test.address);
                cmd.Parameters.Add("BirthDate", Test.BirthDate);
                cmd.Parameters.Add("Gender", Test.Gender);
                cmd.Parameters.Add("Age", Test.Age);
                cmd.Parameters.Add("Email", Test.Email);
                cmd.Parameters.Add("Pass", Test.Pass);
                cmd.Parameters.Add("JoinDate", Test.JoinDate);
                cmd.Parameters.Add("Salary", Test.Salary);
                if (cmd.ExecuteNonQuery() > 0)

                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {

                return false;
            }
            finally
            {

                con.Close();
            }
            
        }
        public Test Get(int ? Id)
        {
            Test test = new Test();
            DataTable dt = new DataTable();
           // string sql = "select *  from test  where id="+Id.ToString() ;
            try
            {
                string procedure = "sp_Get_Test";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = procedure;
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                // SqlDataAdapter ad = new SqlDataAdapter(sql, con);
                //ad.Fill(dt);
                cmd.Parameters.Add("Id", Id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    test = Get(reader);

                }
            }
            catch (Exception e)
            {

            }
             finally
            {
                con.Close();
            }
            return test;
        }
        public Test Get(SqlDataReader reader)
        {
            Test test = new Test();
            test.id = Convert.ToInt32(reader["Id"]);
            test.name = reader["Name"].ToString();
            test.address = reader["address"].ToString();
            test.Gender = reader["Gender"] is DBNull ? false : Convert.ToBoolean(reader["Gender"]);
            test.BirthDate = reader["BirthDate"] is DBNull ? new DateTime() : Convert.ToDateTime(reader["BirthDate"]);
            test.Age = reader["Age"] is DBNull ? 0 : Convert.ToInt32(reader["Age"]);
            test.Email = reader["Email"] is DBNull ? string.Empty : Convert.ToString  (reader["Email"]);
            test.Pass = reader["Pass"] is DBNull ? string.Empty : Convert.ToString(reader["Pass"]);
            test.JoinDate = reader["JoinDate"] is DBNull ? new DateTime() : Convert.ToDateTime(reader["JoinDate"]);
            test.Salary = reader["Salary"] is DBNull ? 0.0f : float.Parse(reader["Salary"].ToString());
            return test;
        }

        public List<Test> ToList()
        {
            List<Test> t=new List<Test>();
            DataTable dt = new DataTable();
            string sql = "select *  from test";
            string procedure = "sp_GetALL_Test";
            try
            {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = procedure;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            //ad.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Test test = new Test();
                test = Get(reader);
                //test.id = Convert.ToInt32(reader["Id"]);
                //test.name = reader["Name"].ToString();
                //test.address = reader["address"].ToString();
                //test.Gender = reader["Gender"] is DBNull ? false : Convert.ToBoolean(reader["Gender"]);
                t.Add(test);

            }

            }
            catch(Exception e)
            {

            }
            finally
            {
                con.Close();
            }
            return t;


        }
        public bool Delete(int Id)
        {
         try
            {

              //  string sql = "Delete From Test where id=" + Id.ToString();
                string procedure = "sp_delete_Test";
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Id", Id);
                if (cmd.ExecuteNonQuery() > 0)

                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {

                return false;
            }
            finally
            {

                con.Close();
            }
        }
    }

}