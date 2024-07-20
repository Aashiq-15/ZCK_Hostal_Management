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
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
            showBookings();
        }

        private readonly string connString = @"Data Source=AASHIQ\SQLEXPRESS;Initial Catalog=hostelDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        private void showBookings()
        {
            // Using block ensures the connection is properly disposed of
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM bookingTbl";
                    SqlDataAdapter udt = new SqlDataAdapter(query, conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(udt);
                    DataSet ds = new DataSet();
                    udt.Fill(ds);
                    bookingDGV.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void pbxDashbrd_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHome hm= new frmHome();
            hm.Show();
        }

        private void pbxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbxStudents_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStudents st = new frmStudents();
            st.Show();
        }

        private void pbxUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmUsers us = new frmUsers();
            us.Show();
        }

        private void pbxLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin lg = new frmLogin();
            lg.Show();
        }

        private void Bookings_Load(object sender, EventArgs e)
        {

        }
    }
}
