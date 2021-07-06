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
    public partial class UpdateUser : Form
    {
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database=Library; integrated security = true");
       
        String UID;// uName, uDept, uCon, uAddress, uEmail;
        public UpdateUser()
        {
            InitializeComponent();
            display();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text == null)
                {
                    MessageBox.Show("Contact Field could not be Empty.");
                }
                else if (txtAddress.Text == null)
                {
                    MessageBox.Show("Address Field could not be Empty.");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Users set UserName = '" + txtName.Text + "',UserDept = '" + txtDepartment.Text + "',UserContact = '" + txtContact.Text + "',UserAddress = '" + txtAddress.Text + "',UserEmail = '" + txtEmail.Text + "' where UserID = "+UID+"", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record is updated Successfully", "Success");
                    display();
                    Clear();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void display()
        {
            try
            {
                con.Open();
                SqlDataAdapter dA = new SqlDataAdapter("exec sp_UsersDisplay", con);
                DataTable dT = new DataTable();
                dA.Fill(dT);
                dataGridUser.DataSource = dT;
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Sure, You want to exit Application.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    this.Close();
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
            Users uR = new Users();
            try
            {
                if ( txtName.Text != null || txtDepartment.Text != null || txtContact.Text != null || txtAddress.Text != null || txtEmail.Text != null)
                {
                    if (MessageBox.Show("Your unsaved data will be removed", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        this.Close();
                        uR.Show();
                    }
                }
                else
                {
                    this.Close();
                    uR.Refresh();
                    uR.Show();
                }
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
                    UID = dataGridUser.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtName.Text = dataGridUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtDepartment.Text = dataGridUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtContact.Text = dataGridUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtAddress.Text = dataGridUser.Rows[e.RowIndex].Cells[4].Value.ToString();
                    String Email = dataGridUser.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtEmail.Text = Email;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Clear()
        {
            try
            {
                txtName.Clear();
                txtDepartment.Clear();
                txtContact.Clear();
                txtAddress.Clear();
                txtEmail.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
