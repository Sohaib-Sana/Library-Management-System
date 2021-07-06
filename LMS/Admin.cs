using System;
using System.Windows.Forms;

namespace LMS
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure, You want to exit Admin Form.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnAddLibrarian_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddLibrarian aL = new AddLibrarian(this);
            aL.Show();
        }

        private void btnUpdateLibrarian_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateLibrarian uL = new UpdateLibrarian(this);
            uL.Show();
        }

        private void btnViewLibrarian_Click(object sender, EventArgs e)
        {
            this.Close();
            ViewLibrarian vL = new ViewLibrarian();
            vL.Show();
        }

        private void btnDeleteLibrarian_Click(object sender, EventArgs e)
        {
            this.Hide();
            DeleteLibrarian dL = new DeleteLibrarian(this);
            dL.Show();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure, You want to exit Admin Form.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }      
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure, You want to Quit Application.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       
    }
}
