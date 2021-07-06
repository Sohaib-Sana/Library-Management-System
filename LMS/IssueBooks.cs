using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMS
{
    public partial class IssueBooks : Form
    {
        Librarian lb = new Librarian();
        int ID;
        String Status, Message1 = null;
        SqlConnection con = new SqlConnection("data source = DESKTOP-IEU1F6P; database=Library; integrated security=True");
        public IssueBooks()
        {
            InitializeComponent();
            display();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You Want to Quit Application?", "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataAdapter dA = new SqlDataAdapter("Select * From Books", con);
                DataSet dS = new DataSet();
               SqlDataAdapter dA2 = new SqlDataAdapter("Select * From Users", con);
                dA.Fill(dS, "Table1");
                dA2.Fill(dS, "Table2");
                con.Close();

                cbxBook.DataSource = dS.Tables[0];
                cbxBook.DisplayMember = "BookName";
                cbxBook.ValueMember = "BookID";
                cbxBook.SelectedIndex = -1;
                cbxUser.DataSource = dS.Tables[1];
                cbxUser.DisplayMember = "UserName";
                cbxUser.ValueMember = "UserID";
                cbxUser.SelectedIndex = -1;
            }
            catch (Exception)
            {

            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (cbxUser.SelectedIndex != -1 || cbxBook.SelectedIndex != -1)
            {
                if (MessageBox.Show("You have unsaved data. Do you really want to go back?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Close();
                    lb.Show();
                }
            }
            else
            {
                this.Close();
                lb.Show();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (Status == "issued" || Status == "Issued")
                {
                    SqlCommand cmd = new SqlCommand("sp_Returned ", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 50);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    Message1 = Convert.ToString(cmd.Parameters["@Message"].Value);
                    MessageBox.Show(Message1);

                }
            }
            catch (Exception)
            {
                
            }
            finally {
                con.Close();
                display();
            }

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            try
            {
                int UserID = Int16.Parse(cbxUser.SelectedValue.ToString());
                int BookID = Int16.Parse(cbxBook.SelectedValue.ToString());
                int Due = Int16.Parse(txtDue.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("exec sp_Issue " + UserID + ", " + BookID + "," + Due + "", con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }
            finally {
                con.Close();
                MessageBox.Show("Book: '" + cbxBook.Text + "' Issued to '" + cbxUser.Text + "'");
                cbxBook.SelectedIndex = -1;
                cbxUser.SelectedIndex = -1;
                txtDue.Clear();
                display();
            }
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SearchRecords ", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", txtSearch.Text);
                cmd.Parameters.AddWithValue("@BookName", txtSearch.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter dA = new SqlDataAdapter(cmd);
                DataTable dT = new DataTable();
                dA.Fill(dT);
                dGIssueBooks.DataSource = dT;
                lblTotalRecords.Text = $"Total Records: {dGIssueBooks.RowCount}";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
        }

        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSearch.Text == "Search Record")
            {
                txtSearch.Clear();
                txtSearch.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search Record")
            {
                txtSearch.Clear();
                txtSearch.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtDue_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtDue.Text == "No of Days [1,2,3...]") {
                txtDue.Clear();
                txtDue.ForeColor = System.Drawing.Color.Black;
            }

        }

        private void txtDue_Enter(object sender, EventArgs e)
        {
            if (txtDue.Text == "No of Days [1,2,3...]")
            {
                txtDue.Clear();
                txtDue.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void dGIssueBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dGIssueBooks.Rows[e.RowIndex].Selected = true;
                if (dGIssueBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = Convert.ToInt16(dGIssueBooks.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Status = dGIssueBooks.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
            }
            catch (Exception)
            {

              
            }
        }

        public void display() {
            try
            {
                con.Open();
                SqlDataAdapter dA = new SqlDataAdapter("exec sp_Display ", con);
                DataTable dT = new DataTable();
                dA.Fill(dT);
                dGIssueBooks.DataSource = dT;
                con.Close();
                lblTotalRecords.Text = $"Total Records: {dGIssueBooks.RowCount}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}
