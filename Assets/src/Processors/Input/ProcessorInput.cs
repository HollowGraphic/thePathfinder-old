using Pixeye.Actors;
using Rewired;
using ThePathfinder.Input;

namespace ThePathfinder.Processors
{
    public abstract class ProcessorInput : Processor
    {
        protected static Mouse mouse;
        protected static Player player;
        private int categoryId;
        
        public ProcessorInput()
        {
            mouse = ReInput.controllers.Mouse;
            player = ReInput.players.GetPlayer(Players.Player0);
            categoryId = SetCategoryId();
        }

        /// <summary>
        /// Set the category id that will be used to determine which inputs to disable
        /// </summary>
        /// <param name="id"></param>
        protected abstract int SetCategoryId();
        
        /// <summary>
        /// Disables maps that conflict whith this <see cref="ProcessorInput"/> 
        /// </summary>
        protected void DisableConflictingInputs()
        {
        
            EnableMaps(false);
        }
        /// <summary>
        /// Enables maps that conflict whith this <see cref="ProcessorInput"/>
        /// </summary>
        protected void EnableConflictingInputs()
        {
            EnableMaps(true);
        }
        private void EnableMaps( bool enable)
        {
            //flip flag
            
            switch (categoryId)
            {
                case Category.Abiltity_Map:
                    player.controllers.maps.SetMapsEnabled(enable, Category.Selection);
                    player.controllers.maps.SetMapsEnabled(enable, Category.Orders);
                    break;
                case Category.Orders:
                    player.controllers.maps.SetMapsEnabled(enable, Category.Selection);
                    break;
                default:
                    break;
            }
        }
    }
}