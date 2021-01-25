using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder
{
    [Serializable]
    public static class Tags
    {
        #region COMMON 0 - 100

        public const int NONE = 0;

        /// <summary>
        /// Common Tags
        /// </summary>
        public const int ACTIVE = 1, SELECTABLE = 2, SELECTED = 3, PRE_SELECTED = 4, DEAD = 5, MOVEABLE = 6;

        #endregion

        #region COMMANDER |101 - 200|

        /// <summary>
        /// Commander Tags
        /// </summary>
        public const int COMMANDER = 101, BOX_SELECTING = 102, SELECTION = 103;

        #endregion

        #region NPCs |201-400|

        //[TagField("NPCs")]
        /// <summary>
        /// NPC pathing |201-300|
        /// </summary>
        public const int NAVIGATOR = 201, PATHING = 202;

        /// <summary>
        /// NPC factions |301 - 320|
        /// </summary>
        public const int TEAM_1 = 301, TEAM_2 = 302, TEAM_3 = 303, TEAM_4 = 304, TEAM_5 = 305, TEAM_6 = 306;

        #endregion

        /// <summary>
        /// Commander Commands |321 - 401|
        /// </summary>
        public const int COMBATANT = 321, ATTACK_MOVE = 322, STOP = 323;

        #region Input |401-500|

        public const int Dragging = 401;

        #endregion

        #region Buildables 500 - 1000

        //[TagField(categoryName = BUILDABLES)]
        public const int Blocked = 15, Ghost = 16;

        #endregion

#if UNITY_EDITOR
    public static ValueDropdownList<int> GetTags()
    {
      return tags;
    }

    private static readonly ValueDropdownList<int> tags = new ValueDropdownList<int>()
        {
            {"Active", ACTIVE},
            {"Selectable", SELECTABLE},
            {"Selected", SELECTED},
            {"Pre-Selected", PRE_SELECTED},
            {"Navigator", NAVIGATOR},
            {"Team 1", TEAM_1},
            {"Combatant", COMBATANT},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
            // {"Active", ACTIVE},
        };

#endif
    }
}