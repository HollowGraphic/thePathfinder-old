using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    /// <summary>
    /// Defines an entity that can rotate
    /// </summary>
    [Serializable, HideLabel]
    public struct Rotator
    {
        public bool canRotate;
         /// <summary>
         /// The speed that an entity can rotate 
         /// </summary>
        [InfoBox("All axis have been constraint, this means entity will not rotate. Is this is intended?", InfoMessageType.Warning), ShowIf("RotateInfoBox")]
        [LabelText("Rotation Speed"),TitleGroup("Rotation Settings"), ShowIf("CanRotate")] public float speed;
        
        /// <summary>
        /// Defines what axis an entity cannot rotate
        /// </summary>
        [BoxGroup("Constraints"),ShowIf("CanRotate")]public AxisConstraints constraints;
#if UNITY_EDITOR
        bool RotateInfoBox()
        {
            return constraints.x && constraints.y && constraints.z;
        }
        
        bool CanRotate()
        {
            return canRotate;
        }
#endif
    }
    [Serializable, HideLabel]
    public struct AxisConstraints
    {
        public bool x, y, z;
    }
    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string RotationSpeed = "ThePathfinder.Components.RotationSpeed";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Rotator RotatorComponent(in this ent entity) =>
            ref Storage<Rotator>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageRotationSpeed : Storage<Rotator>
    {
        public override Rotator Create() => new Rotator();

        // Use for cleaning components that were removed at the current frame.
        public override void Dispose(indexes disposed)
        {
            foreach (var id in disposed)
            {
                ref var component = ref components[id];
            }
        }
    }

    #endregion
}