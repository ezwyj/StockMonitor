using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorCore.Entity
{
    public class Market
    {
        public HashSet<string> SZ { get; set; }
        public HashSet<string> SH { get; set; }
    }
}
