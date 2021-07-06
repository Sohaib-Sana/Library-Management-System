using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMS
{
    public partial class UpdatePublisher : Form
    {
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database=Library; integrated security=True");
        int PID;
        String pName, pAddress, pEmail;
        public UpdatePublisher()
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

        private void button1_Click(object sender, EventArgs e)
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
                Publishers pub = new Publishers();
                pub.Show();
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dataGridPublisher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    PID = Convert.ToInt16(dataGridPublisher.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtName.Text = dataGridPublisher.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtAddress.Text = dataGridPublisher.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtEmail.Text = dataGridPublisher.Rows[e.RowIndex].Cells[3].Value.ToString();
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
                con.Open();
                SqlCommand cmd = new SqlCommand("Update Publishers set PubName ='" + txtName.Text + "',PubAddress='" + txtAddress.Text + "',PubEmail = '"+txtEmail.Text+"' where PubID =" + PID + "", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Successfully Updated.");
                display();
                txtAddress.Clear();
                txtName.Clear();
                txtEmail.Clear();
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
                SqlDataAdapter dA = new SqlDataAdapter("exec sp_PublishersDisplay", con);
                DataTable dT = new DataTable();
                dA.Fill(dT);
                dataGridPublisher.DataSource = dT;
                con.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
