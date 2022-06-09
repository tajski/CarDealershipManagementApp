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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void updateList(string query)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            con.Open();
            command.CommandText = "SELECT account_id, account_name, account_type FROM accounts WHERE account_type in (0, 1) and (account_name LIKE @query) ORDER BY account_type";
            command.Parameters.AddWithValue("@query", query + "%");

            SqlDataReader reader = command.ExecuteReader();

            listBox1.Items.Clear();
            while (reader.Read())
                listBox1.Items.Add(new Account(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));

            con.Close();
        }   

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            updateList(textBox6.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            updateList("");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int account_id;
            try
            {
                account_id = ((Account)listBox1.SelectedItem).getID();
            }
            catch(Exception)
            {
                return;
            }

            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT user_username, account_name, account_create_date, account_phone, account_mail, account_type, account_comments FROM [users], accounts WHERE user_id = account_user_id AND account_id = @id";
            command.Parameters.AddWithValue("@id", account_id);
            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                textBox7.Text = account_id.ToString();
                textBox8.Text = reader.GetValue(0).ToString();
                textBox9.Text = reader.GetValue(1).ToString();
                textBox10.Text = reader.GetValue(2).ToString();
                textBox11.Text = reader.GetValue(3).ToString();
                textBox12.Text = reader.GetValue(4).ToString();

                if (reader.GetInt32(5) == 0)
                {
                    textBox13.Text = "Manager";
                }
                else textBox13.Text = "Dealer";

                textBox14.Text = reader.GetValue(6).ToString();

            }

            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!validateInputs())
            {
                MessageBox.Show("Values put are invalid.");
                return;
            }

            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "INSERT INTO [users] (user_username, user_password) VALUES(@username, @password)";
            command.Parameters.AddWithValue("@username", textBox1.Text);
            command.Parameters.AddWithValue("@password", Utilities.hashPassword(textBox2.Text));
            con.Open();

            if(command.ExecuteNonQuery() > 0)
            {
                command.CommandText = "SELECT user_id FROM [users] WHERE user_username = @username";
                int user_id = (int)command.ExecuteScalar();

                command.CommandText = "INSERT INTO accounts (account_user_id, account_name, account_type, account_comments, account_create_date) VALUES (@user_id, @name, @type, @comments, @date)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@name", textBox3.Text);
                command.Parameters.AddWithValue("@type", comboBox1.SelectedIndex);
                command.Parameters.AddWithValue("@comments", textBox5.Text);
                command.Parameters.AddWithValue("@date", DateTime.Now);

                if(command.ExecuteNonQuery() > 0)
                {
                    //Account created
                    MessageBox.Show("Account created");
                }
                else MessageBox.Show("Account creation failed");

            }
            else MessageBox.Show("Account creation failed");
            con.Close();
            updateList("");
        }

        private bool validateInputs()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                return false;
            if (comboBox1.SelectedIndex < 0)
                return false;

            return true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
                return;

            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "DELETE FROM [users] WHERE user_username=@username";
            command.Parameters.AddWithValue("@username", textBox8.Text);
            con.Open();
            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account deleted.");
            else MessageBox.Show("Error deleting an account.");
            con.Close();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            updateList("");


        }
    }
}
