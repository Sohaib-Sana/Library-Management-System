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
    public partial class AddLibrarian : Form
    {
        Form prevWindow = null;
        SqlConnection con = new SqlConnection("data source=DESKTOP-IEU1F6P; database=Library; integrated security = True");
        String LGender;
        public AddLibrarian(Form frm)
        {
            InitializeComponent();
            prevWindow = frm;
        }

        public void Clear() {
            txtName.Clear();
            txtPassword.Clear();
            if (RBMale.Checked != false)
            {
                RBMale.Checked = false;
            }
            else
            {
                RBMale.Checked = false;
            }
            txtSalary.Clear();
            txtAddress.Clear();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure, You want to exit Application.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text != "" && txtName.Text != "" &&  txtPassword.Text != "" && txtSalary.Text != "")
            {
                String Lname = txtName.Text;
                String LPass = txtPassword.Text;
                //Int64 LAge = Int64.Parse(txtAge.Text);
                Int64 LSalary = Int64.Parse(txtSalary.Text);
                String LAddress = txtAddress.Text;
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Librarian values ('" + Lname + "','" + LPass + "','" + DOBPicker.Text + "','" + LGender + "'," + LSalary + ",'" + LAddress + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfuly", "Succes", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                con.Close();
                Clear();
                prevWindow.Refresh();

            }
            else
            {
                MessageBox.Show("Empty Fields are Not Allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text != "" || txtName.Text != "" || txtPassword.Text != "" || txtSalary.Text != "")
            {
                if (MessageBox.Show("Your unsaved data will be removed", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Close();
                    prevWindow.Refresh();
                    prevWindow.Show();
                }
            }
            else
            {
                this.Close();
                prevWindow.Show();
            }
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (Char)13)
            {
                btnAdd.PerformClick();
            }
        }

        private void RBMale_CheckedChanged(object sender, EventArgs e)
        {
            LGender = "Male";
        }

        private void RBFemale_CheckedChanged(object sender, EventArgs e)
        {
            LGender = "Female";
        }
    }
}
