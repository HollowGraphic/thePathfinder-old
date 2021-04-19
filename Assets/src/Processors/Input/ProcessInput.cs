using Pixeye.Actors;
using Rewired;
using ThePathfinder.Input;

namespace ThePathfinder.Processors.Input
{
    public abstract class ProcessInput : Processor
    {
        protected readonly Mouse Mouse;
        protected readonly Player Player;
        protected abstract int CategoryId { get;}

        protected ProcessInput()
        {
            Mouse = ReInput.controllers.Mouse;
            Player = ReInput.players.GetPlayer(Players.Player0);
            
        }

        /// <summary>
        ///Disables maps that conflict with this <see cref="ProcessInput" />
        /// </summary>
        protected void DisableConflictingInputs()
        {
            EnableMaps(false);
        }

        /// <summary>
        ///     Enables maps that conflict with this <see cref="ProcessInput" />
        /// </summary>
        protected void EnableConflictingInputs()
        {
            EnableMaps(true);
        }

        private void EnableMaps(bool enable)
        {
            
            switch (CategoryId)
            {
                case Category.Abiltity_Map:
                    Player.controllers.maps.SetMapsEnabled(enable, Category.Selection);
                    Player.controllers.maps.SetMapsEnabled(enable, Category.Orders);
                    break;
                case Category.Orders:
                    Player.controllers.maps.SetMapsEnabled(enable, Category.Selection);
                    break;
            }
        }
    }
}