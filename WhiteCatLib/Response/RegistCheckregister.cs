using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WhiteCatLib.Response.Object;
using WhiteCatLib.Response.Result;

namespace WhiteCatLib.Response
{
    public class RegistCheckregister
    {
        public int error { get; set; }

        public RegistCheckregisterResult result { get; set; }

        public bool useSplitBundle { get; set; }
        
    }
}
