namespace AirHockey.Constants
{
    /// <summary>
    /// Contains global settings for the game as a whole.
    /// </summary>
    public static class GlobalSettings
    {
        //Uncomment whicever view you want to start the game in.
        public const string StartingView = "LoadingScreenView";

        public static readonly string[] ResourceDirectories = {@"Resources"};
        public const bool KeepResourcesInMemory = true;
        public const string DefaultSkin = "standard";
#if DEBUG
        public static bool PreloadResources = false;
        public static bool FullScreenMode = false;
#else
        public static bool PreloadResources = true;
        public static bool FullScreenMode = true;
#endif
    }
}
