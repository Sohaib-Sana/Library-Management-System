using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMS
{
    public partial class Books : Form
    {
        SqlConnection con = new SqlConnection("data Source = DESKTOP-IEU1F6P; database = Library; integrated security = True");
        Librarian prevWindow = new Librarian();
        int BookID,BookAuthor,BookCategory,BookPublisher;
        String BookName;
        public Books()
        {
            InitializeComponent();
            display();
        }

        public void display()
        {
            try
            {
                con.Open();
                SqlDataAdapter dA = new SqlDataAdapter("exec sp_BooksDisplay", con);
                DataTable dT = new DataTable();
                dA.Fill(dT);
                dataGridBook.DataSource = dT;
                con.Close();
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
                if (MessageBox.Show("This Record will be deleted Permanantly.", "Warning", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)==DialogResult.OK)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From Books where ID=" + BookID + " And  BookName='" + BookName + "'", con);
                    con.Close();
                    this.Refresh(); 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                UpdateBook uB = new UpdateBook();
                uB.Show();
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
                this.Close();
                AddBook aB = new AddBook();
                aB.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dataGridBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BookID = Convert.ToInt16(dataGridBook.Rows[e.RowIndex].Cells[0].Value.ToString());
                BookName = dataGridBook.Rows[e.RowIndex].Cells[1].Value.ToString();

                dataGridBook.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
