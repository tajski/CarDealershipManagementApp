using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMS
{
    public partial class DealerPanel : Form
    {
        int account_id;
        public DealerPanel(int id)
        {
            InitializeComponent();
            account_id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            EditProfile editProfile = new EditProfile(account_id);
            editProfile.ShowDialog();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            ClientProfiles clientProfiles = new ClientProfiles();
            clientProfiles.ShowDialog();
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            CreateOrder createOrder = new CreateOrder(account_id);
            createOrder.ShowDialog();
            Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            ViewOrders viewOrders = new ViewOrders(account_id);
            viewOrders.ShowDialog();
            Show();
        }
    }
}
