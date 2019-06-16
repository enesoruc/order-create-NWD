using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Order_UI
{
    public partial class Orders : Form
    {
        SqlConnection conn;
        public Orders()
        {
            InitializeComponent();
            conn = new SqlConnection(Properties.Settings.Default.NWD);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillList();            
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            AddOrder_UI addOrder = new AddOrder_UI();
            addOrder.Guncelle += FillList;
            addOrder.ShowDialog();
        }
        
        void FillList()
        {
            SqlCommand cmd = new SqlCommand(@"select o.OrderID ,o.OrderDate,o.RequiredDate,c.CompanyName,e.FirstName+' '+e.LastName AS AdSoyad,o.ShipCity+'/'+o.ShipCountry AS SiparisAdress,o.Freight from Orders o 
                                            JOIN Employees e
                                                ON e.EmployeeID = o.EmployeeID
                                            JOIN Customers c
                                                ON c.CustomerID = o.CustomerID", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ListViewItem lvi;
            while (reader.Read())
            {
                lvi = new ListViewItem(reader["OrderID"].ToString());
                lvi.SubItems.Add(reader["OrderDate"].ToString());
                lvi.SubItems.Add(reader["RequiredDate"].ToString());
                lvi.SubItems.Add(reader["CompanyName"].ToString());
                lvi.SubItems.Add(reader["AdSoyad"].ToString());
                lvi.SubItems.Add(reader["SiparisAdress"].ToString());
                lvi.SubItems.Add(reader["Freight"].ToString());
                lvi.Tag = reader["OrderID"];
                lstOrders.Items.Add(lvi);
            }
            lstOrders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstOrders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            reader.Close();
            conn.Close();
        }
    }
}
