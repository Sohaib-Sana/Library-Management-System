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
    public partial class Users : Form
    {
        int UID;
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database = Library; integrated security=True");
        Librarian prevWindow = new Librarian();
        public Users()
        {
            try
            {
                InitializeComponent();
                display();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void display()
        {
            con.Open();
            SqlDataAdapter dA = new SqlDataAdapter("exec sp_UsersDisplay",con);
            DataTable dT = new DataTable();
            dA.Fill(dT);
            dataGridUser.DataSource = dT;
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Sure, You want to exit Application.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                prevWindow.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Selected record will be deleted permanantly.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Users where UserID LIKE '" + UID + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    display();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                UpdateUser uU = new UpdateUser();
                uU.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                AddUser aU = new AddUser();
                aU.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void dataGridUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    UID = int.Parse(dataGridUser.Rows[e.RowIndex].Cells[0].Value.ToString());
                    dataGridUser.Rows[e.RowIndex].Selected = true;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
