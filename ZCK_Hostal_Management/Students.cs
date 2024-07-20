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

namespace ZCK_Hostal_Management
{
    public partial class frmStudents : Form
    {
        //database connection
        private readonly string connString = @"Data Source=AASHIQ\SQLEXPRESS;Initial Catalog=hostelDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public frmStudents()
        {
            InitializeComponent();
            showStudent();
        }

        //create  function for show student details
        private void showStudent()
        {
            // Using block ensures the connection is properly disposed of
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM studentTbl";
                    SqlDataAdapter udt = new SqlDataAdapter(query, conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(udt);
                    DataSet ds = new DataSet();
                    udt.Fill(ds);
                    stuDGV.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        //reset function
        private void Reset()
        {
            txtStname.Text = "";
            txtSphn.Text = "";
            txtSaddress.Text = "";
            cbxGrade.SelectedIndex = -1;
            key = 0;
        }

        private void Students_Load(object sender, EventArgs e)
        {

        }
 
        private void pbxClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbxUsers_Click(object sender, EventArgs e)
        {
            
            frmUsers usr = new frmUsers();
            usr.Show();
            this.Hide();
        }

        private void pbxLogout_Click(object sender, EventArgs e)
        {
            
            frmLogin lg = new frmLogin();
            lg.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtStname.Text == "" || txtSphn.Text == "" || txtSaddress.Text == "" || cbxGrade.SelectedIndex == -1 )
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
                        string query = "INSERT INTO studentTbl(stu_name,parents_phn, stu_address, stu_grade, create_date) VALUES(@stn, @stphn, @stadrs, @stgrd,@sdate)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@stn", txtStname.Text);
                            cmd.Parameters.AddWithValue("@stphn", txtSphn.Text);
                            cmd.Parameters.AddWithValue("@stadrs", txtSaddress.Text);
                            cmd.Parameters.AddWithValue("@stgrd", cbxGrade.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@sdate", JoinDt.Value.Date);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("student Added...");
                        }
                        showStudent();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void pbxDashbrd_Click_1(object sender, EventArgs e)
        {
            frmHome hm = new frmHome();
            hm.Show();
            this.Hide();
        }

        int key = 0;
        private void stuDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStname.Text = stuDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtSphn.Text = stuDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtSaddress.Text = stuDGV.SelectedRows[0].Cells[3].Value.ToString();
            cbxGrade.SelectedItem = stuDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (txtStname.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(stuDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtStname.Text == "" || txtSphn.Text == "" || txtSaddress.Text == "" || cbxGrade.SelectedIndex == -1)
            {
                MessageBox.Show("Select Student....");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "update studentTbl set stu_name=@stn, parents_phn=@stphn, stu_address=@stadrs, stu_grade=@stgrd, create_date=@sdate where sId=@skey";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@stn", txtStname.Text);
                            cmd.Parameters.AddWithValue("@stphn", txtSphn.Text);
                            cmd.Parameters.AddWithValue("@stadrs", txtSaddress.Text);
                            cmd.Parameters.AddWithValue("@stgrd", cbxGrade.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@sdate", JoinDt.Value.Date);
                            cmd.Parameters.AddWithValue("@skey", key);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("student updated...");
                        }
                        showStudent();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select Student....");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "delete from studentTbl where  sId=@skey";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@skey", key);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Student Deleted...");
                        }
                        showStudent();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void pbxBooking_Click(object sender, EventArgs e)
        {
            
            Bookings bs = new Bookings();
            bs.Show();
            this.Hide();
        }
    }
}
