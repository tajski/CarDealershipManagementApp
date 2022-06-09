using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FMS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = FMS.Properties.Resources.connectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT user_id FROM [users] WHERE user_username=@username AND user_password=@password";
            command.Parameters.AddWithValue("@username", textBox1.Text);
            command.Parameters.AddWithValue("@password", Utilities.hashPassword(textBox2.Text));
            con.Open();
            var result = command.ExecuteScalar();
            con.Close();

            if(result != null)
            {
                //Authenticated
                if(textBox1.Text == "admin")
                {
                    //Admin
                    Hide();
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.ShowDialog();
                    Show();
                }
                else
                {
                    con.Open();
                    command.CommandText = "SELECT account_id, account_type FROM accounts WHERE account_user_id=@user_id";
                    command.Parameters.AddWithValue("@user_id", result.ToString());
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.Read())
                    {
                        int account_id = reader.GetInt32(0);
                        int account_type = reader.GetInt32(1);

                        con.Close();

                        if(account_type == 0)
                        {
                            Hide();
                            ManagerPanel managerPanel = new ManagerPanel(account_id);
                            managerPanel.ShowDialog();
                            Show();
                        }
                        else if (account_type == 1)
                        {
                            Hide();
                            DealerPanel dealerPanel = new DealerPanel(account_id);
                            dealerPanel.ShowDialog();
                            Show();
                        }
                    }
                }
            }
            else
            {
                //Authentication Error
                MessageBox.Show("Authentiaction Failed");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Utilities.createAdmin("123");
        }
    }
}
