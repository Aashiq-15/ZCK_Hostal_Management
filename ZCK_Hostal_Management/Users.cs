using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ZCK_Hostal_Management
{
    public partial class frmUsers : Form
    {
        // database Connection string
        private readonly string connString = @"Data Source=AASHIQ\SQLEXPRESS;Initial Catalog=hostelDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public frmUsers()
        {
            InitializeComponent();
            showUsers();
        }


        //show user details
        private void showUsers()
        {
            // Using block ensures the connection is properly disposed of
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM userTbl";
                    SqlDataAdapter udt = new SqlDataAdapter(query, conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(udt);
                    DataSet ds = new DataSet();
                    udt.Fill(ds);
                    userDGV.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void pbxDashbrd_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHome hm = new frmHome();
            hm.Show();
        }

        private void pbxStudents_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStudents st = new frmStudents();
            st.Show();
        }

        private void pbxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbxLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin lg = new frmLogin();
            lg.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtPhone.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "INSERT INTO UserTbl(username, password, phone, address) VALUES(@un, @upas, @uphn, @uadrs)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@un", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@upas", txtPassword.Text);
                            cmd.Parameters.AddWithValue("@uphn", txtPhone.Text);
                            cmd.Parameters.AddWithValue("@uadrs", txtAddress.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("User Created...");
                        }
                        showUsers();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        int key = 0;
        private void userDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUsername.Text = userDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtPassword.Text = userDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtPhone.Text = userDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = userDGV.SelectedRows[0].Cells[4].Value.ToString();

            if(txtUsername.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(userDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtPhone.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Select User....");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "update UserTbl set username=@un, password=@upas, phone=@uphn, address=@uadrs where Id=@ukey";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@un", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@upas", txtPassword.Text);
                            cmd.Parameters.AddWithValue("@uphn", txtPhone.Text);
                            cmd.Parameters.AddWithValue("@uadrs", txtAddress.Text);
                            cmd.Parameters.AddWithValue("@ukey", key);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("User Update...");
                        }
                        showUsers();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        //reset function
        private void Reset()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            key = 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select User....");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "delete from UserTbl where  Id=@ukey";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ukey", key);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("User Deleted...");
                        }
                        showUsers();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {

        }

        private void pbxBooking_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bookings bs = new Bookings();
            bs.Show();
        }
    }
}
