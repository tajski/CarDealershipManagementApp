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
    public partial class ClientProfiles : Form
    {
        public ClientProfiles()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;

        private void updateList(string query)
        {
            command = con.CreateCommand();
            command.CommandText = "SELECT account_id, account_name, account_type FROM accounts WHERE account_type = 2 AND (account_name LIKE @query)";
            command.Parameters.AddWithValue("@query", query + "%");
            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while(reader.Read())
            {
                listBox1.Items.Add(new Account(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }

            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ClientProfiles_Load(object sender, EventArgs e)
        {
            updateList("");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            updateList(textBox5.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Inputs not valid.");
                    return;
            }


            command = con.CreateCommand();
            command.CommandText = "INSERT INTO accounts (account_name, account_phone, account_mail, account_comments, account_type, account_create_date) VALUES (@name, @phone, @mail, @comments, 2, @date)";
            command.Parameters.AddWithValue("@name", textBox1.Text);
            command.Parameters.AddWithValue("@phone", textBox2.Text);
            command.Parameters.AddWithValue("@mail", textBox3.Text);
            command.Parameters.AddWithValue("@comments", textBox4.Text);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            con.Open();
            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Client account created.");
            else
                MessageBox.Show("Error creating account.");
            con.Close();
            updateList("");

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
                return;
            int account_id = ((Account)listBox1.SelectedItem).getID();
            command = con.CreateCommand();
            command.CommandText = "SELECT account_name, account_phone, account_mail, account_comments, account_create_date FROM accounts WHERE account_id=@id";
            command.Parameters.AddWithValue("@id", account_id);

            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                textBox6.Text = account_id.ToString();
                textBox7.Text = reader.GetString(0);
                textBox8.Text = reader.GetString(1);
                textBox9.Text = reader.GetString(2);
                textBox10.Text = reader.GetString(3);
                textBox11.Text = reader.GetValue(4).ToString();

            }

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox7.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("Wrong input.");
                return;
            }

            command = con.CreateCommand();
            command.CommandText = "UPDATE accounts SET account_name=@name, account_phone=@phone, account_mail=@mail, account_comments=@comments WHERE account_id=@id";
            command.Parameters.AddWithValue("@name", textBox7.Text);
            command.Parameters.AddWithValue("@phone", textBox8.Text);
            command.Parameters.AddWithValue("@mail", textBox9.Text);
            command.Parameters.AddWithValue("@comments", textBox10.Text);
            command.Parameters.AddWithValue("@id", textBox6.Text);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Client account updated.");
            else
                MessageBox.Show("Error updating client account.");

            con.Close();
        }
    }
}
