using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteCatLib
{
    public class CryptoDectectResult
    {
        public bool isGzipped { get; }
        public bool isEncrypted { get; }
        public bool isDefultKey { get; }
        public CryptoDectectResult(bool gzipped, bool crypted, bool defaultKey)
        {
            this.isGzipped = gzipped;
            this.isEncrypted = crypted;
            this.isDefultKey = defaultKey;
        }
    }

    public class CryptoDectect
    {
        public static CryptoDectectResult isRequestCrypted(string method)
        {
            switch(method)
            {
                case "regist/checkregister":
                    return new CryptoDectectResult(false, false, true);
                case "assetbundle/version":
                    return new CryptoDectectResult(false, false, false);
                case "regist/create":
                    return new CryptoDectectResult(false, true, true);
                case "deck/defaultweapon":
                    return new CryptoDectectResult(false, true, false);
                case "quest/generate":
                    return new CryptoDectectResult(false, true, false);

            }

            return new CryptoDectectResult(false, true, false);
        }

        public static CryptoDectectResult isResponseCrypted(string method)
        {
            switch (method)
            {
                case "regist/checkregister":
                    return new CryptoDectectResult(true, true, false);
                case "assetbundle/version":
                    return new CryptoDectectResult(true, true, false);
            }

            return new CryptoDectectResult(true, true, false);
        }
    }
}
