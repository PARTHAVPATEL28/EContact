using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Econtent.econtactClasses
{
    class contactClass
    {
        //Getter Setter properties

        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string contactNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Selecting Data from Database

        public  DataTable Select()
        {
            //Step1 : Database Connection
            SqlConnection conn = new SqlConnection(myconstrng);
            DataTable dt = new DataTable();
            try
            {
                //Step 2: Writing SQL Query
                string sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL Dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Inserting data into the Database

        public bool Insert(contactClass c)
        {
            // Creating a default return type and setting its value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconstrng);
            try
            {
                // Creaate a SQL query to insert data
                string sql = "INSERT INTO tbl_contact (FirstName,LastName,ContactNo,Address,Gender) VALUES (@FirstName,@LastName,@ContactNo,@Address,@Gender)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.contactNumber);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        // Method to update data in database from our application
        public bool Update(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconstrng);
            try
            {
                //SQl to update data in our database
                string sql = "UPDATE tbl_contact SET FirstName = @FirstName, LastName = @LastName, ContactNo = @ContactNo, Address = @Address, Gender = @Gender WHERE ContactID = @ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.contactNumber);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                conn.Open();
                int row = cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool Delete(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconstrng);
            try
            {
                string sql = "DELETE FROM tbl_contact WHERE ContactID = @ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID",c.ContactID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
            return isSuccess;
        }
    }
}
