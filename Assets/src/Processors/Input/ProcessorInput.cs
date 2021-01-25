using Pixeye.Actors;
using Rewired;
using ThePathfinder.Input;

namespace ThePathfinder.Processors.Input
{
    public abstract class ProcessorInput : Processor
    {
        protected static Mouse Mouse;
        protected static Player Player;
        private readonly int _categoryId;

        public ProcessorInput()
        {
            Mouse = ReInput.controllers.Mouse;
            Player = ReInput.players.GetPlayer(Players.Player0);
            // ReSharper disable once VirtualMemberCallInConstructor
            _categoryId = SetCategoryId();
        }

        /// <summary>
        ///     Set the category id that will be used to determine which inputs to disable
        /// </summary>
        /// <param name="id"></param>
        protected abstract int SetCategoryId();

        /// <summary>
        ///     Disables maps that conflict whith this <see cref="ProcessorInput" />
        /// </summary>
        protected void DisableConflictingInputs()
        {
            EnableMaps(false);
        }

        /// <summary>
        ///     Enables maps that conflict whith this <see cref="ProcessorInput" />
        /// </summary>
        protected void EnableConflictingInputs()
        {
            EnableMaps(true);
        }

        private void EnableMaps(bool enable)
        {
            //flip flag

            switch (_categoryId)
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