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
    public partial class AddOrder_UI : Form
    {
        public event RefreshList Guncelle;
        Helper helper;
        Order order;
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        #region Degiskenler
        int shipViaForInsert = 0;
        int employeeIDForInsert = 0;
        int productIDForInsert = 0;
        decimal freight;
        decimal unitPrice = 0;
        string customerIDForInsert = string.Empty;
        DateTime orderDate = DateTime.Now;
        DateTime requiredDate = DateTime.Today.AddDays(5); 
        #endregion

        public AddOrder_UI()
        {
            InitializeComponent();
            conn = new SqlConnection(Properties.Settings.Default.NWD);
            helper = new Helper();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int affecredRows = 0;
            cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Orders(CustomerID, EmployeeID,ShipVia, Freight, Shipname, ShipAddress, ShipCity, ShipPostalCode, ShipCountry) VALUES(@customerID,@employeeID,@shipVia,@freight,@shipName,@shipAdress,@shipCity,@postaCode,@shipCountry)";
            
            cmd.Connection = conn;
            conn.Open();
            foreach (Order item in helper.orders)
            {
                cmd.Parameters.AddWithValue("@customerID", item.CustomerID);
                cmd.Parameters.AddWithValue("@employeeID", item.EmployeeID);
                cmd.Parameters.AddWithValue("@shipVia", item.ShipVia);
                cmd.Parameters.AddWithValue("@freight", item.Freight);
                cmd.Parameters.AddWithValue("@shipName", item.ShipName);
                cmd.Parameters.AddWithValue("@shipAdress", item.ShipAdress);
                cmd.Parameters.AddWithValue("@shipCity", item.ShipCity);
                cmd.Parameters.AddWithValue("@postaCode", item.ShipPostalCode);
                cmd.Parameters.AddWithValue("@shipCountry", item.ShipCountry);
                affecredRows += cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            
            if (affecredRows > 0)
            {
                MessageBox.Show("İşlem başarılı");
            }
            else
            {
                MessageBox.Show("HATA");
            }
            conn.Close();
            Guncelle();
            this.Close();
        }

        private void AddOrder_UI_Load(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SELECT * FROM Customers;SELECT * FROM Products;SELECT FirstName+' '+LastName AS AdSoyad FROM Employees;SELECT * FROM Shippers",conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmbCustomers.Items.Add(reader[1]);
            }
            reader.NextResult();
            while (reader.Read())
            {
                cmbProducts.Items.Add(reader[1]);
            }
            reader.NextResult();
            while (reader.Read())
            {
                cmbEmployees.Items.Add(reader["AdSoyad"]);
            }
            reader.NextResult();
            while (reader.Read())
            {
                cmbShippers.Items.Add(reader[1]);
            }
            conn.Close();
        }

        private void cmbCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd = new SqlCommand(string.Format("SELECT CustomerID From Customers WHERE CompanyName='{0}'",
                cmbCustomers.SelectedItem.ToString()), conn);
            customerIDForInsert = (string)cmd.ExecuteScalar();
            conn.Close();
        }

        
        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd = new SqlCommand($"SELECT ProductID,UnitPrice From Products WHERE ProductName='{cmbProducts.SelectedItem.ToString()}'", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productIDForInsert = (int)reader[0];
                txtUnitPrice.Clear();
                unitPrice= (decimal)reader[1];
            }
            txtUnitPrice.Text = unitPrice.ToString();
            conn.Close();
        }

        private void cmbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd = new SqlCommand(string.Format("SELECT EmployeeID From Employees WHERE FirstName+' '+LastName='{0}'",
                cmbEmployees.SelectedItem.ToString()), conn);
            employeeIDForInsert = (int)cmd.ExecuteScalar();
            conn.Close();
        }

        private void cmbShippers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd = new SqlCommand(string.Format("SELECT ShipperID From Shippers WHERE CompanyName='{0}'",
                cmbShippers.SelectedItem.ToString()), conn);
            shipViaForInsert = (int)cmd.ExecuteScalar();
            conn.Close();
        }

        private void btnCalculater_Click(object sender, EventArgs e)
        {
            freight =int.Parse(txtFreight.Text);
            decimal productCount = (decimal)nmProductCount.Value;
            total = productCount * unitPrice+freight;
            txtTotalPrice.Text = total.ToString();
        }

        decimal total;
        private void btnAddCart_Click(object sender, EventArgs e)
        {
            lstOrders.Items.Clear();
            AddOrderList();//yeni sipariş oluşturur
            foreach (Order item in helper.orders)
            {
                lstOrders.Items.Add(item);
            }
        }

        void AddOrderList()
        {
            order = new Order();
            order.CustomerID = customerIDForInsert;
            order.EmployeeID = employeeIDForInsert;
            order.ShipVia = shipViaForInsert;
            order.ShipName = txtShipName.Text;
            order.ShipAdress = txtAdress.Text;
            order.ShipCity = txtShipCity.Text;
            order.ShipCountry = txtShipCountry.Text;
            order.ShipPostalCode = int.Parse(txtShipPostaCode.Text);
            order.productName = cmbProducts.SelectedItem.ToString();
            order.productCount = (int)nmProductCount.Value;
            order.TotalPrice = total;
            order.Freight = freight;
            helper.orders.Add(order);
        }
    }
}
