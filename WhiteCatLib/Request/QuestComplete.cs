using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteCatLib.Response.Object;

namespace WhiteCatLib.Request
{
    public class QuestComplete
    {
        public string qt { get; set; }

        public int gold { get; set; }

        public int soul { get; set; }

        public IList<int> cardIds { get; set; }

        public IList<int> weaponIds { get; set; }

        public IList<int> ornamentIds { get; set; }

        public IList<int> itemIds { get; set; }

        public IList<int> destroyEnemyIds { get; set; }

        public IList<int> destroyObjectIds { get; set; }

        public IList<int> openTreasureIds { get; set; }

        public int totalDamageCount { get; set; }

        public int totalDamageAmount { get; set; }

        public int totalDamageCountFromPlacementObject { get; set; }

        public int totalDeadCount { get; set; }

        public int totalHelperDeadCount { get; set; }

        public int totalBadStatusCount { get; set; }

        public int totalActionSkillUseCount { get; set; }

        public int totalAllAttackUseCount { get; set; }

        public int totalUseActionSkillNum { get; set; }

        public int totalUseHealActionSkillNum { get; set; }

        public bool isDestroyBossAtActionSkill { get; set; }

        public bool isDestroyBossAtAllAttack { get; set; }

        public int maxChainNum { get; set; }

        public int routeId { get; set; }

        public int playingTime { get; set; }

        public int restTime { get; set; }

        public int questGameType { get; set; }

        public int clearFloor { get; set; }

        public int gt { get; set; }

        public int lsId { get; set; }

        public int lscId { get; set; }
    }
}
