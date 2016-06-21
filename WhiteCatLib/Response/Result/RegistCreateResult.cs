using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteCatLib.Response.Object;

namespace WhiteCatLib.Response.Result
{
    public class RegistCreateResult
    {
        public string uh { get; set; }

        public int userId { get; set; }

        public UnknownType userInfo { get; set; }

        public UnknownType userStatus { get; set; }

        public AddParameter addParameter { get; set; }

        public IList<Deck> decks { get; set; }

        public IList<Card> cards { get; set; }

        public IList<UnknownType> weapons { get; set; }

        public IList<Item> items { get; set; }

        public bool recommendUpdate { get; set; }

        public int isTutorialStream { get; set; }

        public int newsId { get; set; }

        public IList<UnknownType> lKeys { get; set; }

        public int enableOnlineMatch { get; set; }

        public int enableFollowMatch { get; set; }
    }
}
