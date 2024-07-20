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
using static Guna.UI2.Native.WinApi;

namespace ZCK_Hostal_Management
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
            countBooked();
            countStudents();
            countBookings();
            getStudent();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {

        }
        //database connection
        private readonly string connString = @"Data Source=AASHIQ\SQLEXPRESS;Initial Catalog=hostelDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        int free, booked;
        int bPer, FreePer;
        private void countBooked()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string Status = "reserved";
                
               conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) from bedTbl where bStatus='"+Status+"'",conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                free = 30 - Convert.ToInt32(dt.Rows[0][0].ToString());
                booked = Convert.ToInt32(dt.Rows[0][0].ToString());
                bPer = (booked / 30)*100;
                FreePer = (free / 30)*100;
                lblBooked.Text = dt.Rows[0][0].ToString()+ " Reserved Beds";
                lblAvlbl.Text = free + " Free Beds";
                bedslbl.Text = free + "";
                AProgress.Value = FreePer;
                avlBedPrgrs.Value = FreePer;
                BProgress.Value = bPer;
                conn.Close();
            }

        }

        private void countStudents()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) from studentTbl ", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lblStnum.Text = dt.Rows[0][0].ToString() + "  Students";
                conn.Close();
            }

        }

        private void countBookings()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) from  bookingTbl", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lblBknum.Text = dt.Rows[0][0].ToString() + "  Bookings";
                conn.Close();
            }

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pnlCenter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStudents st = new frmStudents();
            st.Show();
        }

        private void pbxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void getStudent()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("select sId from studentTbl", conn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("sId",typeof(int));
                dt.Load(rdr);
                cbxSid.ValueMember = "sId";
                cbxSid.DataSource = dt;
                conn.Close();
            }
        }

        int bednum = 0;
        private void getStudentName()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();
                string query = "select * from studentTbl where sId=" + cbxSid.SelectedValue.ToString() + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                DataTable dt=new DataTable();
                SqlDataAdapter sda =new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    txtStbl.Text = dr["stu_name"].ToString();
                }
                conn.Close();
            }
        }

        private void Reset()
        {
            bednum = 0;
        }
        private void updateBed()
        {
            string status = "Reserved";
            using (SqlConnection conn = new SqlConnection(connString))
            {

                try
                {
                    conn.Open();
                    string query = "update bedTbl set bStatus=@bst where bId=@rkey";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@bst", status);
                        cmd.Parameters.AddWithValue("@rkey", bednum);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bed updated...");
                        conn.Close();
                        Reset();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnRsrvd_Click(object sender, EventArgs e)
        {
            if(txtStbl.Text == "" || bednum == 0)
            {
                MessageBox.Show("Select Bed and Studnets...");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "INSERT INTO bookingTbl(sId, stu_name, bId, bNumber) VALUES(@sid, @stn, @bid, @btnum)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@sid", cbxSid.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@stn", txtStbl.Text);
                            cmd.Parameters.AddWithValue("@bid", bednum);
                            cmd.Parameters.AddWithValue("@btnum", bednum);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Bed Reserved...");
                            Reset();
                            updateBed();
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void cbxSid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getStudentName();
        }


        private void B1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void B3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B4_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void B5_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B7_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void B8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B9_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B10_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B11_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B12_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B13_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void B14_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B15_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B16_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B17_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B18_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B19_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B20_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B21_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B22_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void B23_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void B24_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B25_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void B26_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B27_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B28_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void B29_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void B30_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bookings bs = new Bookings();
            bs.Show();
        }

        private void B1_Click(object sender, EventArgs e)
        {
            bednum = 1;
        }

        private void B2_Click(object sender, EventArgs e)
        {
            bednum = 2;
        }

        private void B3_Click(object sender, EventArgs e)
        {
            bednum = 3;
        }

        private void B4_Click(object sender, EventArgs e)
        {
            bednum = 4;
        }

        private void B5_Click(object sender, EventArgs e)
        {
            bednum = 5;
        }

        private void B6_Click(object sender, EventArgs e)
        {
            bednum = 6;
        }

        private void B7_Click(object sender, EventArgs e)
        {
            bednum = 7;
        }

        private void B8_Click(object sender, EventArgs e)
        {
            bednum = 8;
        }

        private void B9_Click(object sender, EventArgs e)
        {
            bednum = 9;
        }

        private void B10_Click(object sender, EventArgs e)
        {
            bednum = 10;
        }

        private void B11_Click(object sender, EventArgs e)
        {
            bednum = 11;
        }

        private void B12_Click(object sender, EventArgs e)
        {
            bednum = 12;
        }

        private void B13_Click(object sender, EventArgs e)
        {
            bednum = 13;
        }

        private void B14_Click(object sender, EventArgs e)
        {
            bednum = 14;
        }

        private void B15_Click(object sender, EventArgs e)
        {
            bednum = 15;
        }

        private void B16_Click(object sender, EventArgs e)
        {
            bednum = 17;
        }

        private void B17_Click(object sender, EventArgs e)
        {
            bednum = 17;
        }

        private void B18_Click(object sender, EventArgs e)
        {
            bednum = 18;
        }

        private void B19_Click(object sender, EventArgs e)
        {
            bednum = 19;
        }

        private void B20_Click(object sender, EventArgs e)
        {
            bednum = 20;
        }

        private void B21_Click(object sender, EventArgs e)
        {
            bednum = 21;
        }

        private void B22_Click(object sender, EventArgs e)
        {
            bednum = 22;
        }

        private void B23_Click(object sender, EventArgs e)
        {
            bednum = 23;
        }

        private void B24_Click(object sender, EventArgs e)
        {
            bednum = 24;
        }

        private void B25_Click(object sender, EventArgs e)
        {
            bednum = 25;
        }

        private void B26_Click(object sender, EventArgs e)
        {
            bednum = 26;
        }

        private void B27_Click(object sender, EventArgs e)
        {
            bednum = 27;
        }

        private void B28_Click(object sender, EventArgs e)
        {
            bednum = 28;
        }

        private void B29_Click(object sender, EventArgs e)
        {
            bednum = 29;
        }

        private void B30_Click(object sender, EventArgs e)
        {
            bednum = 30;
        }

        private void pbxUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmUsers usr= new frmUsers();
            usr.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin lg = new frmLogin();
            lg.Show();
        }


    }
}
