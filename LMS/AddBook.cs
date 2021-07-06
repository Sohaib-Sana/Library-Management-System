using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMS
{
    public partial class AddBook : Form
    {
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database = Library; integrated security = True");
        

        public AddBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you Sure, You want to exit Application.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                try
                {
                    Application.Exit();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
           

        

        private void btnMin_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Books prevWindow = new Books();
            try
            {
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int bQuantity = Int16.Parse(txtQuantity.Text);
                int bCategory = Int16.Parse(cbxCat.SelectedValue.ToString());
                int bAuthor = Int16.Parse(cbxAuthor.SelectedValue.ToString());
                int bPublisher = Int16.Parse(cbxPublisher.SelectedValue.ToString());
                
                //Select CategoryID From Category Where CategoryID = bCategory

                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Books values ('" + txtName.Text + "'," + bCategory + "," +bAuthor+ ","+bPublisher+","+bQuantity+")", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("New Book is added.");
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();   
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

        private void AddBook_Load(object sender, EventArgs e)
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
    }
}
