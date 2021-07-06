using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMS
{
    public partial class ViewLibrarian : Form
    {
        SqlConnection con = new SqlConnection("data source=DESKTOP-IEU1F6P; database=Library; integrated security = True");
        public ViewLibrarian()
        {
            InitializeComponent();
            display();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddLibrarian aL = new AddLibrarian(this);
            aL.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Admin a = new Admin();
            a.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateLibrarian uL = new UpdateLibrarian(this);
            uL.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You want to delete Librarian Permanantly?", "Are You sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From Librarian where LibID = '" + LID + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    display();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                this.Refresh();
            }
        }

        public void display()
        {
            try
            {
                con.Open();
                SqlDataAdapter dA = new SqlDataAdapter("select * from Librarian", con);
                DataTable dT = new DataTable();
                dA.Fill(dT);
                dataGridLibrarian.DataSource = dT;
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
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

        private void ViewLibrarian_Load(object sender, EventArgs e)
        {
            display();
        }

        int LID;
        private void dataGridLibrarian_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridLibrarian.Rows[e.RowIndex].Selected = true;
                LID = int.Parse(dataGridLibrarian.Rows[e.RowIndex].Cells[0].Value.ToString());
                dataGridLibrarian.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}