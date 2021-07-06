using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMS
{
    public partial class UpdateBook : Form
    {
        int BookID,BookQuantity;
        String BookName,BookCat,BookAuthor,BookPub;
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database = Library; integrated security = true");
        
        public UpdateBook()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure, You want to exit Application.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Books prevWindow = new Books();
            if (txtName.Text != "" || txtQuantity.Text != "")
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int bQuantity = Int16.Parse(txtQuantity.Text);
            int bCategory = Int16.Parse(cbxCat.SelectedValue.ToString());
            int bAuthor = Int16.Parse(cbxAuthor.SelectedValue.ToString());
            int bPub = Int16.Parse(cbxPublisher.SelectedValue.ToString());
            

            con.Open();
            SqlCommand cmd = new SqlCommand("Update Books set BookName'" + txtName.Text + "', BookCategory =" + bCategory + ",BookAuthor =" + bAuthor + ",BookPublisher =" + bPub + ",BookQuantity" + bQuantity + "", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record is Updated.","Success!");
            Clear();
        }

        private void dataGridBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbxAuthor.Text = null;
            cbxPublisher.Text = null;
            if (e.RowIndex >= 0)
            {
                BookID = Convert.ToInt16(dataGridBook.Rows[e.RowIndex].Cells[0].Value.ToString());
                BookName = dataGridBook.Rows[e.RowIndex].Cells[1].Value.ToString();
                BookCat = dataGridBook.Rows[e.RowIndex].Cells[2].Value.ToString();
                BookAuthor = dataGridBook.Rows[e.RowIndex].Cells[3].Value.ToString();
                BookPub = dataGridBook.Rows[e.RowIndex].Cells[4].Value.ToString();
                BookQuantity = Convert.ToInt16(dataGridBook.Rows[e.RowIndex].Cells[5].Value.ToString());




                dataGridBook.Rows[e.RowIndex].Selected = true;
                txtName.Text = BookName;
                txtQuantity.Text = BookQuantity.ToString();
                cbxCat.SelectedIndex = cbxCat.FindStringExact(BookCat);
                cbxAuthor.SelectedText = BookAuthor;
                cbxPublisher.SelectedText = BookPub;
            }
 
        }

        private void UpdateBook_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataAdapter dA = new SqlDataAdapter("Select * From Category", con);
                SqlDataAdapter dA2 = new SqlDataAdapter("Select AuthorID,AuthorName From Authors", con);
                SqlDataAdapter dA3 = new SqlDataAdapter("Select  PubID,PubName From Publishers", con);
                DataSet dS = new DataSet();

                dA.Fill(dS, "Category");
                dA2.Fill(dS, "Author");
                dA3.Fill(dS, "Publisher");

                cbxCat.DataSource = dS.Tables[0];
                cbxCat.DisplayMember = "CatName";
                cbxCat.ValueMember = "CatID";
                cbxCat.SelectedIndex = -1;

                cbxAuthor.DataSource = dS.Tables[1];
                cbxAuthor.DisplayMember = "AuthorName";
                cbxAuthor.ValueMember = "AuthorID";
                cbxAuthor.SelectedIndex = -1;

                cbxPublisher.DataSource = dS.Tables[2];
                cbxPublisher.DisplayMember = "PubName";
                cbxPublisher.ValueMember = "PubID";
                cbxPublisher.SelectedIndex = -1;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Clear()
        {
            try
            {
                txtName.Clear();
                cbxCat.SelectedIndex = -1;
                cbxAuthor.SelectedIndex = -1;
                cbxPublisher.SelectedIndex = -1;
                txtQuantity.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
