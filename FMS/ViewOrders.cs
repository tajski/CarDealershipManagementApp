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
    public partial class ViewOrders : Form
    {
        int account_id, account_type;
        public ViewOrders(int id)
        {
            InitializeComponent();
            account_id = id;
        }


        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;

        private void updateList()
        {
            command = con.CreateCommand();
            command.CommandText = "SELECT order_id, order_client_id, client.account_name, order_dealer_id, dealer.account_name, order_order_date, order_car, order_payment, order_payment_status, order_status FROM orders, accounts AS client, accounts AS dealer WHERE order_client_id = client.account_id AND order_dealer_id = dealer.account_id AND (client.account_name LIKE @query OR client.account_phone LIKE @query OR order_id LIKE @query)";
            command.Parameters.AddWithValue("@query", textBox1.Text + "%");

            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int client_id = reader.GetInt32(1);
                string client_name = reader.GetString(2);
                int dealer_id = reader.GetInt32(3);
                string dealer_name = reader.GetString(4);
                DateTime order_date = new DateTime();
                DateTime.TryParse(reader.GetValue(5).ToString(), out order_date);
                string car = reader.GetString(6);
                double payment = reader.GetDouble(7);
                int pstatus = reader.GetInt32(8);
                int ostatus = reader.GetInt32(9);

                listBox1.Items.Add(new Order(id, client_id, client_name, car, dealer_id, dealer_name, order_date, payment, pstatus, ostatus));

            }

            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateForm();
        }

        private void updateForm()
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Problem selecting an order");
                return;
            }

            Order order = (Order)listBox1.SelectedItem;
            textBox2.Text = order.id.ToString();
            textBox3.Text = order.client.ToString();
            textBox4.Text = order.dealer.ToString();
            textBox5.Text = order.car.ToString();
            textBox6.Text = order.payment.ToString();
            textBox7.Text = order.order_date.Date.ToString();
            textBox8.Text = order.pstatus.ToString();
            textBox9.Text = order.ostatus.ToString();

            if (account_type == 0 || account_type == 1)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Problem selecting an order");
                return;
            }
            Order order = (Order)listBox1.SelectedItem;

            Hide();
            EditOrders editOrders = new EditOrders(order);
            editOrders.ShowDialog();
            updateList();
            updateForm();
            Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            updateList();
            command = con.CreateCommand();
            command.CommandText = "SELECT account_type FROM accounts WHERE account_id = @id";
            command.Parameters.AddWithValue("@id", account_id);
            con.Open();
            account_type = (int)command.ExecuteScalar();
            con.Close();
        }
    }
}
