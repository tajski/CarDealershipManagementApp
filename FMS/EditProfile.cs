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
    public partial class EditProfile : Form
    {
        int account_id;
        public EditProfile(int account_id)
        {
            InitializeComponent();
            this.account_id = account_id;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Name not entered.");
                return;
            }

            SqlCommand command = con.CreateCommand();
            command.CommandText = "UPDATE accounts SET account_name=@name, account_phone=@phone, account_mail=@mail, account_comments=@comments WHERE account_id=@account_id";
            command.Parameters.AddWithValue("@name", textBox3.Text);
            command.Parameters.AddWithValue("@phone", textBox4.Text);
            command.Parameters.AddWithValue("@mail", textBox5.Text);
            command.Parameters.AddWithValue("@comments", textBox7.Text);
            command.Parameters.AddWithValue("@account_id", account_id);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account updated.");
            else
                MessageBox.Show("Error updating account.");

            con.Close();
        }


        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        private void EditProfile_Load(object sender, EventArgs e)
        {
            SqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT user_username, account_name, account_phone, account_mail, account_type, account_comments, account_create_date FROM [users], accounts WHERE account_user_id = user_id AND account_id=@account_id";
            command.Parameters.AddWithValue("@account_id", account_id);
            con.Open();

            SqlDataReader reader = command.ExecuteReader();

            if(reader.Read())
            {
                textBox1.Text = account_id.ToString();
                textBox2.Text = reader.GetValue(0).ToString();
                textBox3.Text = reader.GetValue(1).ToString();
                textBox4.Text = reader.GetValue(2).ToString();
                textBox5.Text = reader.GetValue(3).ToString();
                if (reader.GetInt32(4) == 0)
                    textBox6.Text = "Manager";
                else if (reader.GetInt32(4) == 1)
                    textBox6.Text = "Dealer";
                else if (reader.GetInt32(4) == 2)
                    textBox6.Text = "Client";
                textBox7.Text = reader.GetValue(5).ToString();
                textBox8.Text = reader.GetValue(6).ToString();

            }

            con.Close();
        }
    }
}
