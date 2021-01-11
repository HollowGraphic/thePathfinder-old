using System.Collections.Generic;
using Pixeye.Actors;
using ThePathfinder.Input;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    /// <summary>
    /// Units follow orders
    /// </summary>
    public sealed class UnitComponent : AuthoringComponent
    {
        public override void Set(ref ent entity)
        {
            entity.Set<Unit>();

        }
    }
}