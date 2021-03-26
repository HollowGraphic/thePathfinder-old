using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct GameCamera
    {
        //Data
        public float edgeBuffer;
        public float panSpeed;
        [LabelText("Rotation Speed")] public float rotateSpeed;

        public float zoomSpeed;
        public Plane Plane;
    }

    #region HELPERS

    public static partial class GameComponent
    {
        public const string GameCamera = "ThePathfinder.Components.GameCamera";

        internal static ref GameCamera GameCameraComponent(in this ent entity)
            => ref Storage<GameCamera>.components[entity.id];
    }

    internal sealed class StorageGameCamera : Storage<GameCamera>
    {
        public override GameCamera Create() => new GameCamera();

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