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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source=DESKTOP-IEU1F6P; database=Library; Integrated security = True";
            SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandText = "select * From Admins where Username='"+txtUsername.Text+"'and Pass='"+txtPassword.Text+"'";
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlDataAdapter da = new SqlDataAdapter("select * From Admins where Username LIKE '" + txtUsername.Text + "'and Pass LIKE '" + txtPassword.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                this.Hide();
                Admin ad = new Admin();
                ad.Show();

            }
            else {
                MessageBox.Show("Invalid UserName Or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_MouseClick(object sender, MouseEventArgs e)
        {
            if(txtUsername.Text == "Username")
            {
                txtUsername.Clear();
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Clear();
            }
            txtPassword.PasswordChar = '*';
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void lblShowMore_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Only Admins in our Database can access Admin Page", "InFormation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure, You want to exit Application.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (Char)13)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
