using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteCatLib.Response.Object
{
    public class Item
    {
        public int iId { get; set; }

        public string name { get; set; }

        public int rar { get; set; }

        public int price { get; set; }

        public string text { get; set; }

        public int excludeFlag { get; set; }

        public int itemType { get; set; }

        public int viewType { get; set; }

        public int dEffect { get; set; }

        public int cId { get; set; }

        public string cName { get; set; }

        public int cGroup { get; set; }

        public int cP { get; set; }

        public int num { get; set; }
    }
}
