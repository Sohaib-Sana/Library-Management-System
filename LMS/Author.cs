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
    public partial class Author : Form
    {
        int AID;
        String aName, aAddress;
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database = Library; integrated security=true");
        public Author()
        {
            InitializeComponent();
            display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Librarian lib = new Librarian();
            lib.Show();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Close();
            AddAuthor aA = new AddAuthor();
            aA.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.Close();
            UpdateAuthor uA = new UpdateAuthor();
            uA.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("You want to remove Following Record? \nName : " + aName + "\tAddress : " + aAddress + "", "Are You sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From Authors where AuthorID =" + AID + "", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    display();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        public void display()
        {
            con.Open();
            SqlDataAdapter dA = new SqlDataAdapter("exec sp_AuthorsDisplay", con);
            DataTable dT = new DataTable();
            dA.Fill(dT);
            dataGridAuthor.DataSource = dT;
            con.Close();
        }

        private void dataGridAuthor_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {
                    dataGridAuthor.Rows[e.RowIndex].Selected = true;
                    AID = Convert.ToInt16(dataGridAuthor.Rows[e.RowIndex].Cells[0].Value.ToString());
                    aName = dataGridAuthor.Rows[e.RowIndex].Cells[1].Value.ToString();
                    aAddress = dataGridAuthor.Rows[e.RowIndex].Cells[2].Value.ToString();
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
