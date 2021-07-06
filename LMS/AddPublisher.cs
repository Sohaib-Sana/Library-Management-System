using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMS
{
    public partial class AddPublisher : Form
    {
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database=Library; integrated security=True");
        Librarian lib = new Librarian();
        
        public AddPublisher()
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
                if (txtAddress.Text != "" || txtName.Text != "")
                {
                    if (MessageBox.Show("Your unsaved data will be removed by closing the App", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    this.Close();
                    lib.Show();
                }
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Publishers pub = new Publishers();
            try
            {
                if (txtAddress.Text != "" || txtName.Text != "")
                {

                    if (MessageBox.Show("Your unsaved data will be removed", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        this.Close();
                        pub.Show();
                    }
                }
                else
                {
                    this.Close();
                    pub.Show();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Publishers values ('"+txtName.Text+"','"+txtAddress+ "','" + txtEmail.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New Publisher is added.");
                txtAddress.Clear();
                txtEmail.Clear();
                txtName.Clear();
                display();
            }
            catch (Exception)
            {

                throw;
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
