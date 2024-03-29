// <auto-generated>
// Rewired Constants
// This list was generated on 3/31/2021 12:53:42 AM
// The list applies to only the Rewired Input Manager from which it was generated.
// If you use a different Rewired Input Manager, you will have to generate a new list.
// If you make changes to the exported items in the Rewired Input Manager, you will
// need to regenerate this list.
// </auto-generated>

namespace ThePathfinder.Input {
    public static partial class ActionId {
        // Default
        // Camera
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Camera Controls", friendlyName = "Pan Horizontal")]
        public const int Camera_PanHorizontal = 0;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Camera Controls", friendlyName = "Pan Vertical")]
        public const int Camera_PanVertical = 1;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Camera Controls", friendlyName = "Zoom")]
        public const int Camera_Zoom = 2;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Camera Controls", friendlyName = "Rotate")]
        public const int Camera_Rotate = 3;
        // Selection
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Selection", friendlyName = "Confirm")]
        public const int Selection_Confirm = 4;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Selection", friendlyName = "Cancel")]
        public const int Selection_Cancel = 5;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Selection", friendlyName = "Add Mod")]
        public const int Selection_Additive = 6;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Selection", friendlyName = "Remove Mod")]
        public const int Selection_Subtractive = 7;
        // Order
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Basic Orders", friendlyName = "Force Move")]
        public const int Order_Move = 8;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Basic Orders", friendlyName = "Stop")]
        public const int Order_Stop = 9;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Basic Orders", friendlyName = "Patrol")]
        public const int Order_Patrol = 11;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Basic Orders", friendlyName = "Queue Order")]
        public const int Order_Queue = 12;
        // Ability
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Cast")]
        public const int Ability_Cast = 32;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Abort")]
        public const int Ability_Abort = 21;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_0 = 22;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_9 = 31;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_8 = 30;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_7 = 29;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_6 = 28;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_5 = 27;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_4 = 26;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_3 = 25;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_2 = 24;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Action0")]
        public const int Ability_1 = 23;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Abilities", friendlyName = "Attack")]
        public const int Ability_Attack = 33;
        // Common
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Common", friendlyName = "Repeat Last")]
        public const int Common_RepeatLast = 34;
    }
    public static partial class Category {
        public const int Default = 0;
        public const int Camera = 1;
        public const int Selection = 2;
        public const int Orders = 3;
        public const int Abiltity_Map = 4;
        public const int Common = 5;
    }
    public static partial class Layout {
        public static partial class Joystick {
            public const int Default = 0;
        }
        public static partial class Keyboard {
            public const int Default = 0;
        }
        public static partial class Mouse {
            public const int Default = 0;
        }
        public static partial class CustomController {
            public const int Default = 0;
        }
    }
    public static partial class Players {
        [Rewired.Dev.PlayerIdFieldInfo(friendlyName = "System")]
        public const int System = 9999999;
        [Rewired.Dev.PlayerIdFieldInfo(friendlyName = "Player")]
        public const int Player0 = 0;
    }
    public static partial class CustomController {
    }
    public static partial class LayoutManagerRuleSet {
    }
    public static partial class MapRule {
        public const int AbilityPrimed = 1;
    }
}
