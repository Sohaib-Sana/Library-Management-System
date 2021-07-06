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
    public partial class DeleteLibrarian : Form
    {
        Form prevWindow=null;
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database=Library; integrated security= True");
        public DeleteLibrarian(Form frm)
        {
            InitializeComponent();
            display();
            prevWindow = frm;
        }
        int LID;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                dataGridView1.Rows[e.RowIndex].Selected = true;
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
                dataGridView1.DataSource = dT;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
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

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
            prevWindow.Show();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Librarian will be deleted Permanantly?", "Are You sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
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
    }
}
