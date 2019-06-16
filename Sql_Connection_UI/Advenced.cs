using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sql_Connection_UI
{
    public partial class Advenced : Form
    {
        public Advenced()
        {
            InitializeComponent();
        }
        bool rdTrueFalse;
        int Min;
        int Max;
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (rdTrue.Checked)
            {
                rdTrueFalse = true;
            }
            else if (rdFalse.Checked)
            {
                rdTrueFalse = false;
            }
            SQL_Connection_UI form = new SQL_Connection_UI();
            this.Close();
        }

        private void nmMin_ValueChanged(object sender, EventArgs e)
        {
            Min =(int)nmMin.Value;
        }

        private void nmMax_ValueChanged(object sender, EventArgs e)
        {
            Max = (int)nmMax.Value;
        }

        public bool Mars
        {
            get
            {
                return rdTrueFalse;
            }
            set
            {
                rdTrueFalse = value;
            }
        }
        public int MinSize
        {
            get
            {
                return Min;
            }
            set
            {
                Min = value;
            }
        }
        public int MaxSize
        {
            get
            {
                return Max;
            }
            set
            {
                Max = value;
            }
        }
        public bool GetMars()
        {
            return Mars;
        }
        public int GetMaxSize()
        {
            return MaxSize;
        }
        public int GetMinSize()
        {
            return MinSize;
        }
    }
}
