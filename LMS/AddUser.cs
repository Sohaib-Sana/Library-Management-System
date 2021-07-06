using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMS
{
    public partial class AddUser : Form
    {
        SqlConnection con = new SqlConnection("data Source = DESKTOP-IEU1F6P; database = Library; integrated security=True");
        
        public AddUser()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            Users uR = new Users();
            try
            {
                if ( txtName.Text != "" || txtDepartment.Text != "" || txtContact.Text!= ""|| txtAddress.Text != "" || txtEmail.Text!= "")
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text == "")
                {
                    MessageBox.Show("Contact Field could not be Empty.");
                }
                else if (txtAddress.Text == "")
                {
                    MessageBox.Show("Address Field could not be Empty.");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Users values('" + txtName.Text + "','" + txtDepartment.Text + "','" + txtContact.Text + "', '" + txtAddress.Text + "', '" + txtEmail.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New Record has been added.");
                    Clear();
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

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (Char)13)
            {
                btnAdd.PerformClick();
            }
        }
    }
}
