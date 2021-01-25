using Pixeye.Actors;
using Sirenix.OdinInspector;
using Drawing;
using System;
using ThePathfinder.Components;

namespace ThePathfinder
{
    public class GameActor : Actor
#if UNITY_EDITOR
   ,IDrawGizmos
#endif
    {
        protected override void Setup()
        {
            foreach (var component in GetComponents<IComponent>())
            {
                component.Set(ref entity);
                component.RegisterObservers(entity);
            }
        }

#if UNITY_EDITOR
#pragma warning disable
public GameActor()
    {
      DrawingManager.Register(this);
    }
    private void OnDrawGizmos()
    {

    }
    private ValueDropdownList<int> GetTags()
    {
      //Taggs tags;
      return Tags.GetTags();
    }
    public virtual void DrawGizmos(){}

#endif
    }
}