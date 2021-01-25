using ThePathfinder.Components;
using Pixeye.Actors;
using System.Runtime.ExceptionServices;


namespace ThePathfinder.Processors
{
  /// <summary>
  /// Handles what happens when a unit has been selected
  /// </summary>
  public sealed class ProcessUnitSelected : Processor
  {
    private readonly Group<Unit, Selected> _selectedUnits = default;

    public override void HandleEcsEvents()
    {
      foreach (var unit in _selectedUnits.added)
      {
        //assign the newly selected unit to it's commander
        //unit.SelectedComponent().commander.Get<Selection>().value.Add(unit);
      }
      foreach (var unit in _selectedUnits.removed)
      {
        //remove unit to it's commander
        //unit.SelectedComponent().commander.Get<Selection>().value.Remove(unit);
      }
    }
  }
}