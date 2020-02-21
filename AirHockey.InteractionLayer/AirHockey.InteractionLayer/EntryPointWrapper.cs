namespace AirHockey.InteractionLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EntryPointWrapper
    {
        private static EntryPoint _myEntryPoint;

        public static void SetFullScreen(bool isFullScreen)
        {
            if (AirHockey.Constants.GlobalSettings.FullScreenMode == true)
            {
                _myEntryPoint = EntryPoint.Instance;
                _myEntryPoint.SetFullScreen(isFullScreen);
            }
        }
    }
}
