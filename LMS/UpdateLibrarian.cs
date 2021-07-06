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
    public partial class UpdateLibrarian : Form
    {
        Form prevWindow = null;
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database=Library; integrated security= True");
        int LID;
        String LGender;
        public UpdateLibrarian(Form frm)
        {
            InitializeComponent();
            display();
            prevWindow = frm;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                    LID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    DOBPicker.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    String Gen = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (Gen == "Male") {
                        RBMale.Checked = true;
                    }
                    else { RBFemale.Checked = true; }
                    txtSalary.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error); ;
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update Librarian set LibName='" + txtName.Text + "' ,LibPass='" + txtPassword.Text + "',LibDOB='" + DOBPicker.Text + "',LibGender='" + LGender + "',LibSalary='" + txtSalary.Text + "',LibAdress='" + txtAddress.Text + "' where LibID=" + LID + "", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Librarian Updated Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                display();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You want to Quit Application", "Are you sure.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text != "" || txtName.Text != "" || txtPassword.Text != "" || txtSalary.Text != "")
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

        public void Clear() {
            txtAddress.Clear();
            txtName.Clear();
            if (RBMale.Checked != false)
            {
                RBMale.Checked = false;
            }
            else
            {
                RBMale.Checked = false;
            }
            txtSalary.Clear();
            txtPassword.Clear();
        }

        public void display()
        {
            try
            {
                con.Open();
                SqlDataAdapter dA = new SqlDataAdapter("select * from Librarian", con);
                DataTable dT = new DataTable();
                dA.Fill(dT);
                dataGridView1.DataSource = dT;
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure, You want to exit Application.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }


        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13) {
                btnUpdate.PerformClick();
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
