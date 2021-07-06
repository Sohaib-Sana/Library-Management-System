using System;
using System.Windows.Forms;

namespace LMS
{
    public partial class Librarian : Form
    {
        public Librarian()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Sure, You want to exit.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Users usr = new Users();
                usr.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Books bk = new Books();
                bk.Show();

            }
            catch (Exception)
            {

                throw;
            }        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Author ar = new Author();
                ar.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Want to Leave Librarian Dashboard?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }

            }

    catch (Exception)
            {

                throw;
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnPublishers_Click(object sender, EventArgs e)
        {
            try
            {
                Publishers pb = new Publishers();
                pb.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnIssueReturn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                IssueBooks iB = new IssueBooks();
                iB.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You want to log out From Librarian", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void lblLogout_MouseEnter(object sender, EventArgs e)
        {
            lblLogout.ForeColor = System.Drawing.Color.Blue;
        }

        private void lblLogout_MouseLeave(object sender, EventArgs e)
        {
            lblLogout.ForeColor = System.Drawing.Color.Black ;
        }
    }
}
