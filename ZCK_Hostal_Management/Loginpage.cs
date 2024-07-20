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
    public partial class frmLogin : Form
    {
        private readonly string connString = @"Data Source=AASHIQ\SQLEXPRESS;Initial Catalog=hostelDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public frmLogin()
        {
            InitializeComponent();
        }

        private void lblGreeting_Click(object sender, EventArgs e)
        {

        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Enter username and password...");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT COUNT(*) FROM userTbl WHERE username=@username AND password=@password";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        int result = (int)cmd.ExecuteScalar();
                        if (result == 1)
                        {
                            frmHome hm = new frmHome();
                            hm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong username or password");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }

        private void pbxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
