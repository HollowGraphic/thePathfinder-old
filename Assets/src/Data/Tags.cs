using System;

#if UNITY_EDITOR

#endif
namespace ThePathfinder
{
    [Serializable]
    public static class Tags
    {
        #region COMMON 0 - 100

        public const int None = 0;

        /// <summary>
        /// Common Tags
        /// </summary>
        public const int Active = 1, Selectable = 2, Selected = 3, PRESelected = 4, Dead = 5, Moveable = 6;

        #endregion

        #region COMMANDER |101 - 200|

        /// <summary>
        /// Commander Tags
        /// </summary>
        public const int Commander = 101, BoxSelecting = 102, Selection = 103;

        #endregion

        #region NPCs |201-400|

        //[TagField("NPCs")]
        /// <summary>
        /// NPC pathing |201-300|
        /// </summary>
        public const int Navigator = 201, Pathing = 202;

        /// <summary>
        /// NPC factions |301 - 320|
        /// </summary>
        public const int Team1 = 301, Team2 = 302, Team3 = 303, Team4 = 304, Team5 = 305, Team6 = 306;

        #endregion

        /// <summary>
        /// Commander Commands |321 - 401|
        /// </summary>
       // public const int Combatant = 321, AttackMove = 322, Stop = 323;

        #region Input |401-500|

        public const int Dragging = 401;

        #endregion

        #region Buildables 500 - 1000

        //[TagField(categoryName = BUILDABLES)]
        public const int Blocked = 15, Ghost = 16;

        #endregion

#if UNITY_EDITOR
        //public static ValueDropdownList<int> GetTags()
       // {
           // return tags;
       // }

        //private static readonly ValueDropdownList<int> tags = new ValueDropdownList<int>()
        //{
            // {"Active", ACTIVE},
            // {"Selectable", SELECTABLE},
            // {"Selected", SELECTED},
            // {"Pre-Selected", PRE_SELECTED},
            // {"Navigator", NAVIGATOR},
            // {"Team 1", TEAM_1},
            // {"Combatant", COMBATANT},
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
        //};

#endif
    }
}