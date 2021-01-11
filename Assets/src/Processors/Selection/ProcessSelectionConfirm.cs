using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Input;
using UnityEngine;
using Drawing;
namespace ThePathfinder.Processors
{
  public sealed class ProcessSelectionConfirm : ProcessorInput, ITick
  {
    //[GroupBy(Tags.SELECTABLE)]
    private readonly Group<Selectable, SelectionCandidate> _unselectedCandidates = default;
    private readonly Group<Selectable, Selected> _selected = default;
    private readonly Group<Selectable, Selected, SelectionCandidate> _selectedCandidate = default;
    private readonly Group<Commander> _commanders = default;
    public void Tick(float dt)
    {
      foreach (var commander in _commanders)
      {
        //TODO Handle multi player, it woul be nice to have something like: foreach commander if(commander.getButton) -> do something
        //INVESTIGATE multiplayer, do we need to distinguish between players here? Or can we just have one local player that reports to the server?
        if (player.GetButtonUp(ActionId.Selection_Confirm))
        {
          if (player.GetButton(ActionId.Selection_Additive))
          {
            foreach (var candidate in _unselectedCandidates)//add
            {
              SelectEntity(commander, candidate);
            }
          }
          else if (player.GetButton(ActionId.Selection_Subtractive))
          {
            foreach (var candidate in _selectedCandidate)//remove
            {
              DeSelectEntity(candidate);
            }
          }
          else
          {
            foreach (var selected in _selected)
            {
              selected.Remove<Selected>();
            }
            foreach (var candidate in _unselectedCandidates)
            {
              SelectEntity(commander, candidate);
            }
          }
        }
      }
    }

    private static void SelectEntity(ent commander, ent candidate)
    {
      candidate.Get<Selected>() = new Selected(commander);
      //clean up entities
      candidate.Remove<SelectionCandidate>();
    }
    private static void DeSelectEntity(ent candidate)
    {
      candidate.Remove<Selected>();
      //clean up entities
      candidate.Remove<SelectionCandidate>();
    }

      protected override int SetCategoryId()
      {
          return Category.Selection;
      }
  }
}
