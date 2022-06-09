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
    public partial class CreateOrder : Form
    {
        int dealer_id;
        public CreateOrder(int id)
        {
            InitializeComponent();
            dealer_id = id;
        }

        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;


        private void updateList(string query)
        {
            command = con.CreateCommand();
            command.CommandText = "SELECT account_id, account_name, account_type FROM accounts WHERE account_type = 2 AND (account_name LIKE @query)";
            command.Parameters.AddWithValue("@query", query + "%");

            con.Open();

            listBox1.Items.Clear();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                listBox1.Items.Add(new Account(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));


            con.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CreateOrder_Load(object sender, EventArgs e)
        {
            updateList("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateList(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Wrong input.");
                return;
            }

            int client_id = ((Account)listBox1.SelectedItem).getID();

            command = con.CreateCommand();
            command.CommandText = "INSERT INTO orders (order_client_id, order_dealer_id, order_car, order_order_date, order_payment) VALUES (@client, @dealer, @car, @date, @price)";
            command.Parameters.AddWithValue("@client", client_id);
            command.Parameters.AddWithValue("@dealer", dealer_id);
            command.Parameters.AddWithValue("@car", textBox2.Text);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.Parameters.AddWithValue("@price", textBox3.Text);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("New order created.");
            else
                MessageBox.Show("Error creating an order");

            con.Close();

        }

    }
}
