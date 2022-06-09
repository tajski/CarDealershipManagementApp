using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS
{
    public class Order
    {
        public int id, pstatus, ostatus;
        public KeyValuePair<int, string> client, dealer;
        public string car;
        public DateTime order_date;
        public double payment;

        public Order(int id, int client_id, string client_name, string car, int dealer_id, string dealer_name, DateTime order_date, double payment, int pstatus, int ostatus)
        {
            this.id = id;
            client = new KeyValuePair<int, string>(client_id, client_name);
            dealer = new KeyValuePair<int, string>(dealer_id, dealer_name);
            this.car = car;
            this.order_date = order_date;
            this.payment = payment;
            this.pstatus = pstatus;
            this.ostatus = ostatus;

        }

        public int getID()
        {
            return id;
        }
        public override string ToString()
        {
            return "[ID: " + id.ToString() + "] " + client.Value + ": " + car + " (Dealer: " + dealer.Value + ") Order status: " + ostatus; 
        }

    }
}
