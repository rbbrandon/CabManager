//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Robs_Taxi_Manager
{
    public class Expense
    {
        public string Category { get; set; }
        public decimal Cost { get; set; }
        public decimal GST { get; set; }

        public Expense(string category, decimal cost, decimal gst)
        {
            Category = category;
            Cost = cost;
            GST = gst;
        }
    }
}
