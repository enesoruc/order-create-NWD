using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerilestirLib;
using System.Windows.Forms;

namespace Sql_Connection_UI
{
    public partial class SQL_Connection_UI : Form
    {
        SqlConnection con;
        Connection connection;
        Advenced advenced;
        public SQL_Connection_UI()
        {
            InitializeComponent();
            con = new SqlConnection(Properties.Settings.Default.NWD);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM sys.databases";
            cmd.Connection = con;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmbDatabase.Items.Add(reader[0]);
            }
            con.Close();


        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            if (txtServerName.Text.Length>0)
            {
                cmbAType.Enabled = true;
                cmbDatabase.Enabled = true;
            }
            else
            {
                cmbAType.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                cmbDatabase.Enabled = false;
            }
        }

        private void cmbAType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAType.SelectedIndex==0)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
            }
            else if (cmbAType.SelectedIndex==1)
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void cmbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDatabase.SelectedIndex<0)
            {
                btnSave.Enabled = false;
                btnTestConnection.Enabled = false;
                btnAdvenced.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                btnTestConnection.Enabled = true;
                btnAdvenced.Enabled = true;
            }
        }

        private void btnAdvenced_Click(object sender, EventArgs e)
        {
            advenced = new Advenced();
            advenced.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            connection = new Connection();
            connection.Server = txtServerName.Text;
            connection.AuthenticationType = cmbAType.SelectedItem.ToString();
            connection.Database = cmbDatabase.SelectedItem.ToString();
            connection.Mars = advenced.GetMars();
            connection.MaxSize = advenced.GetMaxSize();
            connection.MinSize = advenced.GetMinSize();
            if (cmbAType.SelectedIndex==0)
            {
                connection.Connstr = "Server=" + txtServerName.Text + ";Database=" + cmbDatabase.SelectedItem.ToString() + ";Trusted_Connection=True";
            }
            else if (cmbAType.SelectedIndex==1)
            {
                connection.UserName = txtUserName.Text;
                connection.Password = txtPassword.Text;
                connection.Connstr = "Server=" + connection.Server + ";Database=" + connection.Database + ";User ID="+connection.UserName+";Password="+connection.Password;
            }
            SerilestirXml serilestir = new SerilestirXml();
            serilestir.XmlSerialize(connection, "MyAppConfig.xml ");
        }
    }
}
