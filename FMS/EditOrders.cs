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
    public partial class EditOrders : Form
    {

        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;
        public EditOrders(Order order)
        {
            InitializeComponent();
            textBox1.Text = order.id.ToString();
            textBox2.Text = order.client.ToString();
            textBox3.Text = order.dealer.ToString();
            textBox4.Text = order.car.ToString();
            textBox5.Text = order.payment.ToString();
            textBox6.Text = order.order_date.Date.ToString();
            textBox7.Text = order.pstatus.ToString();
            textBox8.Text = order.ostatus.ToString();
        }

        private void EditOrders_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fMSDataSet.statuses' table. You can move, or remove it, as needed.
            this.statusesTableAdapter.Fill(this.fMSDataSet.statuses);
        }

        public void button1_Click(object sender, EventArgs e)
        {

            command = con.CreateCommand();
            command.CommandText = "UPDATE [orders] SET order_car = @car, order_payment = @price, order_payment_status = @pstatus, order_status = @ostatus WHERE order_id = @id";
            command.Parameters.AddWithValue("@id", textBox1.Text);
            command.Parameters.AddWithValue("@car", textBox4.Text);
            command.Parameters.AddWithValue("@price", textBox5.Text);
            command.Parameters.AddWithValue("@pstatus", textBox7.Text);
            command.Parameters.AddWithValue("@ostatus", textBox8.Text);


            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Changes applied successfully.");
            else
                MessageBox.Show("Unable to update.");

            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            command = con.CreateCommand();
            command.CommandText = "DELETE FROM [orders] WHERE order_id = @id";
            command.Parameters.AddWithValue("@id", textBox1.Text);
            con.Open();
            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Order successfully deleted.");
            else
                MessageBox.Show("Unable to delete order.");
            con.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
