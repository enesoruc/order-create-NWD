using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_UI
{
    public class Order
    {
        public string CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public string ShipName { get; set; }
        public string ShipAdress { get; set; }
        public string ShipCity { get; set; }
        public int ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string productName { get; set; }
        public int productCount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Freight { get; set; }

        public override string ToString()
        {
            return productName + " " + productCount + " " + TotalPrice;
        }
    }
}
