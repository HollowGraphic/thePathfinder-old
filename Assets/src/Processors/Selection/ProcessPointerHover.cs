using BigBiteStudios;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Input;
using ThePathfinder.Processors.Input;
#if UNITY_EDITOR
using Drawing;
#endif
namespace ThePathfinder.Processors.Selection
{
    /// <summary>
    ///     Handles the pointer hovering over entities
    /// </summary>
    public sealed class ProcessPointerHover : ProcessorInput, ITick
    {
        private readonly Group<PointerBounds> _bounds = default;

        private readonly Group<Commander> _commanders = default;

        public void Tick(float delta)
        {
            foreach (var commander in _commanders)
            {
                if (Player.GetButton(ActionId.Selection_Confirm)
                    || Player.GetButtonDown(ActionId.Selection_Confirm)
                    || Player.GetButtonUp(ActionId.Selection_Confirm))
                    return;

                var r = Mouse.GetMouseRay();
#if UNITY_EDITOR
                Draw.Ray(r, 1000f);
#endif
                foreach (var bounds in _bounds)
                {
                    var cPointerBounds = bounds.PointerBoundsComponent();
                    //cPointerBounds.value.center = bounds.transform.position;

                    if (cPointerBounds.value.bounds.IntersectRay(r))
                    {
                        if (!bounds.Has<PointerHover>()) //hover if not already
                            bounds.Get<PointerHover>() = new PointerHover(commander);
                        if (bounds.Has<Selectable>() && !bounds.Has<SelectionCandidate>()
                        ) //if ent can be selected but currently isn't
                            bounds.Get<SelectionCandidate>() = new SelectionCandidate(commander);
                    }
                    else
                    {
                        if (bounds.Has<PointerHover>()) bounds.Remove<PointerHover>();
                        if (bounds.Has<Selectable>() && bounds.Has<SelectionCandidate>())
                            bounds.Remove<SelectionCandidate>();
                    }
                }
            }
        }

        protected override int SetCategoryId()
        {
            return -100;
        }
    }
}