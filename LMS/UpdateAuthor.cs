using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMS

{
    public partial class UpdateAuthor : Form
    {
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database=Library; integrated security=True");
        
        public UpdateAuthor()
        {
            InitializeComponent();
            display();
        }

        int AID;
        private void btnMin_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;
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
                if (MessageBox.Show("Your unsaved data will be removed", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    Librarian lib = new Librarian();
                    this.Close();
                    lib.Show();
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
                if (txtName.Text != "" || txtAddress.Text != "")
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
            

        private void dataGridAuthor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    dataGridAuthor.Rows[e.RowIndex].Selected = true;
                    AID = Convert.ToInt16(dataGridAuthor.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtName.Text = dataGridAuthor.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtAddress.Text = dataGridAuthor.Rows[e.RowIndex].Cells[2].Value.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update Authors set AuthorName ='"+txtName.Text+"',AuthorAddress='"+txtAddress.Text+"' where AuthorID ="+ AID+"", con);
                cmd.ExecuteNonQuery();
                con.Close();
                display();
                txtAddress.Clear();
                txtName.Clear();
            }
            catch
            {
                throw;
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
            catch (Exception)
            {

                throw;
            }

        }
    }
}
