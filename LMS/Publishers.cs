using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMS
{
    public partial class Publishers : Form
    {
        int PID;
        String pName, pAddress,pEmail;
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database=Library; integrated security=True");
        Librarian prevWindow = new Librarian();
        public Publishers()
        {
            InitializeComponent();
            display();
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


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Sure, You want to exit Application.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                prevWindow.Show();
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Selected Record will be deleted Permanantly.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Publishers where PubID = " + PID + "", con);
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
                UpdatePublisher uP = new UpdatePublisher();
                this.Close();
                uP.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                AddPublisher aP = new AddPublisher();
                aP.Show();
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Publishers_Load(object sender, EventArgs e)
        {

        }

        public void display()
        {
            try
            {
                con.Open();
                SqlDataAdapter dA = new SqlDataAdapter("exec sp_PublishersDisplay", con);
                DataTable dT = new DataTable();
                dA.Fill(dT);
                dataGridPublisher.DataSource = dT;
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridPublisher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    dataGridPublisher.Rows[e.RowIndex].Selected = true;
                    if (dataGridPublisher.Rows[e.RowIndex].Cells[0].Value.ToString() != null)
                    {
                        PID = Convert.ToInt16(dataGridPublisher.Rows[e.RowIndex].Cells[0].Value.ToString());
                        pName = dataGridPublisher.Rows[e.RowIndex].Cells[1].Value.ToString();
                        pAddress = dataGridPublisher.Rows[e.RowIndex].Cells[2].Value.ToString();
                        pEmail = dataGridPublisher.Rows[e.RowIndex].Cells[3].Value.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
