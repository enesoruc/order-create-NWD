using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Connection_UI
{
    public class Connection
    {
        public string Server { get; set; }
        public string AuthenticationType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Connstr { get; set; }
        public int MaxSize { get; set; } = 0;
        public int MinSize { get; set; } = 0;
        public bool Mars { get; set; } = false;
    }
}
