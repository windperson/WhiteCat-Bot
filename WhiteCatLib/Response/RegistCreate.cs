using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteCatLib.Response.Object;
using WhiteCatLib.Response.Result;

namespace WhiteCatLib.Response
{
    public class RegistCreate
    {

        public IList<AssetVersion> assetVersion { get; set; }

        public int error { get; set; }

        public RegistCreateResult result { get; set; }

        public int tutorial { get; set; }

        
    }
}
