using Econtent.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtent
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get the value from the input fields.
           
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.contactNumber = textBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //Inserting data into datbase using the method created
            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("Success");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                clear();

            }
            else
            {
                MessageBox.Show("Try again");
            }

         
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            
            
        }

        private void Econtact_Load(object sender, EventArgs e)
        {

            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        public void clear()
        {
            txtboxContactID.Text = "";
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtBoxAddress.Text = "";
            textBoxContactNumber.Text = "";
            cmbGender.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the data from textboxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.contactNumber = textBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact has been successfully Updated.");
                clear();

            }
            else
            {
                MessageBox.Show("Failed to Update Content");
            }
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get data from data grid view and load it to the textboxes respectively
            // Identify the row on which mouse is clicked

            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the data from the contact list box

            int rowIndex = int.Parse(txtboxContactID.Text);
            c.ContactID = rowIndex;
            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("Selected row data deleted successfully");
                clear();
            }
            else
            {
                MessageBox.Show("Attempt Failed!");
            }
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the value from text: box
            string keyword = cmbSearch.Text;
            string value = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            //MessageBox.Show("SELECT * FROM tbl_contact WHERE " + keyword + " LIKE '%" + value + "%' ");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE " + keyword + " LIKE '%" + value + "%' ", conn);           
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvContactList.DataSource = dt;
        
        }


    }
}
