using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WhiteCatLib.Response.Object;
using WhiteCatLib.Response.Result;


namespace WhiteCatLib.Response
{
    public class AssetbundleVersion
    {
        public IList<AssetVersion> assetVersion { get; set; }

        public AssetbundleVersionResult result { get; set; }

    }
}
