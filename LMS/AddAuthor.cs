using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LMS
{
    public partial class AddAuthor : Form
    {
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database=Library; integrated security=True");
        
        public AddAuthor()
        {
            InitializeComponent();
            display();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Minimized;
            }
            
            catch (Exception)
            {
                throw;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                Author prevWindow = new Author();
                if (txtAddress.Text != "" || txtName.Text != "")
                {
                    if (MessageBox.Show("Your unsaved data will be removed", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        
                        this.Close();
                        prevWindow.Show();
                    }
                }
                else
                {
                    this.Close();
                    prevWindow.Show();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Author prevWindow = new Author();
            try
            {
                if (txtAddress.Text != "" || txtName.Text != "")
                {
                    if (MessageBox.Show("Your unsaved data will be removed", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        this.Close();
                        prevWindow.Show();
                    }
                }
                else
                {
                    this.Close();
                    prevWindow.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Authors values ('" + txtName.Text + "','" + txtAddress.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Author has been successfully added","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtAddress.Clear();
                txtName.Clear();
                display();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        public void display()
        {
            try
            {
                con.Open();
                SqlDataAdapter dA = new SqlDataAdapter("exec sp_AuthorsDisplay", con);
                DataTable dT = new DataTable();
                dA.Fill(dT);
                dataGridAuthor.DataSource = dT;
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            
        }
    }
}
