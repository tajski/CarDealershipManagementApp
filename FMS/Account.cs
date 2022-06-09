using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS
{
    public class Account
    {
        int id, type;
        string name;

        public Account(int id, string name, int type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
        }

        public override string ToString()
        {
            string account_type;
            if (type == 0)
                account_type = "Manager";
            else if (type == 1)
                account_type = "Dealer";
            else
                account_type = "Client";


            return account_type + " [ID " + id.ToString() + "] - " + name;

        }

        public int getID()
        {
            return id;
        }
    }
}
