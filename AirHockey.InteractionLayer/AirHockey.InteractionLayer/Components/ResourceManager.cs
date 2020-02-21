namespace AirHockey.InteractionLayer.Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Xna.Framework.Graphics;
    using Resources;
    using Constants;

    /// <summary>
    /// The component that manages the dynamic resources/
    /// content in the game.
    /// </summary>
    public static class ResourceManager
    {
        public static bool LoadingCompleted
        {
            get;
            private set;
        }

        internal static int _currentIndex = 0;
        //internal static UsableResource _currentResource;

        /// <summary>
        /// A collection of resources that have been put into
        /// a usable state. These will be called by name when
        /// needed for sprites or sounds etc.
        /// </summary>
        private static readonly List<UsableResource> Resources = new List<UsableResource>();

        static ResourceManager()
        {
            LoadFontResources();
        }

        /// <summary>
        /// Releases resources that have been dynamically loaded (all
        /// images and audio).
        /// </summary>
        internal static void Release()
        {
            Resources.ForEach(x => x.Release());
        }

        /// <summary>
        /// Appends the given resource to the resource collection.
        /// </summary>
        /// <param name="resource">The resource to append.</param>
        public static void AppendResource(UsableResource resource)
        {
            var currentResource = Resources.FirstOrDefault(x => x.Type == resource.Type && x.Name == resource.Name);

            if (currentResource == null)
            {
                Resources.Add(resource);

                //if (GlobalSettings.PreloadResouraces == true)
                //{
                //    // immediately loads resource into memory
                //    if (resource.Type == UsableResourceType.Image)
                //    {
                //        resource.LoadTexture();
                //    }
                //    else if (resource.Type == UsableResourceType.Audio)
                //    {
                //        resource.LoadAudio();
                //    }
                //}
            }
        }

        /// <summary>
        /// Appends the given list of resources to the collection
        /// storing all usable resources.
        /// </summary>
        /// <param name="resources">The resources to be added.</param>
        public static void AppendResources(IEnumerable<UsableResource> resources)
        {
            foreach (var resource in resources)
            {
                AppendResource(resource);
            }
        }

        public static void PreLoadResource()
        {
            if (!LoadingCompleted)
            {
                UsableResource resource = Resources[_currentIndex];
                // immediately loads resource into memory
                if (resource.Type == UsableResourceType.Image)
                {
                    resource.LoadTexture();
                }
                else if (resource.Type == UsableResourceType.Audio)
                {
                    resource.LoadAudio();
                }

                _currentIndex += 1;

                if (_currentIndex >= Resources.Count)
                {
                    LoadingCompleted = true;
                }
            }
        }

        public static string GetCurrentResourceName()
        {
            string resourceProgress = "(" + _currentIndex.ToString() + " / " + Resources.Count.ToString() + ")";
            string[] seperators = {"Resources" , "Standard", "resources"};
            string[] splitFileNames = Resources[_currentIndex].FileName.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            string currentFileName = "...";
            
            currentFileName += splitFileNames[splitFileNames.Count() - 1];

            return Resources[_currentIndex].FileName == null ? "[Empty Resource]" : currentFileName + '\n' + resourceProgress;
        }

        /// <summary>
        /// Retrieves a resource by name.
        /// </summary>
        /// <param name="type">The type of the resource to be retrieved.</param>
        /// <param name="name">The name of the resource to be retrieved.</param>
        /// <returns>The retrieved resource.</returns>
        internal static UsableResource GetResource(UsableResourceType type, ResourceName name)
        {
            var result =
                Resources.FirstOrDefault(
                    x => String.Equals(x.Name, name.Name, StringComparison.CurrentCultureIgnoreCase) && x.Type == type);

            if (result == null)
            {
                throw new ArgumentException("An invalid Resource Name has been used to access a resource.");
            }

            return result;
        }

        /// <summary>
        /// Performs a check to see that the resource name exists in the pool
        /// of loaded resources and returns false if not.
        /// </summary>
        /// <param name="resource">The resource name to be validated.</param>
        /// <param name="type">The type of the resource that is expected.</param>
        /// <returns>Whether or not the resource name exists in the Resources collection.</returns>
        internal static bool ValidateResourceName(UsableResourceType type, ResourceName resource)
        {
            return
                Resources.Any(
                    x =>
                        String.Equals(x.Name, resource.Name, StringComparison.CurrentCultureIgnoreCase) &&
                        x.Type == type);
        }

        /// <summary>
        /// Loads all fonts in the Content project into memory.
        /// </summary>
        /// Note: this is a not as per the standard way of doing things.
        /// Due to limitations of XNA this is the only option short of
        /// writing our own Sprite Font system (which we may do later).
        private static void LoadFontResources()
        {
            var fullPathToContent = Path.GetFullPath(InternalComponents.Content.RootDirectory);

            foreach (
                var file in
                    Directory.GetFiles(
                        fullPathToContent,
                        "*.xnb",
                        SearchOption.AllDirectories).Select(x => MakeRelative(fullPathToContent, x)))
            {
                var name = GenerateResourceNameFromFile(file);
                var spriteFont =
                    InternalComponents.Content.Load<SpriteFont>(
                        file.Substring(file.IndexOf('/') + 1, file.LastIndexOf('.') - (file.IndexOf('/') + 1)));
                AppendResource(new UsableResource(name, spriteFont));
            }
        }

        /// <summary>
        /// Converts a file name (relative to the resource directory used to
        /// load it) into a resource name using Dot notation.
        /// </summary>
        /// <param name="relativeFilename">The relative pathed file name.</param>
        /// <returns>The name to be used as the resource name.</returns>
        private static string GenerateResourceNameFromFile(string relativeFilename)
        {
            return relativeFilename
                .Substring(0, relativeFilename.LastIndexOf('.'))    // remove extension
                .Replace(@"\", ".")                                 // convert directory structure to dot-notation
                .Replace("/", ".")
                .ToLower()                                          // resource names not case sensitive
                .Replace("content", "resources");
        }

        /// <summary>
        /// Converts a string representing an absolute path to be a relative
        /// path relative to the given directory.
        /// </summary>
        /// <param name="basePath">The base directory from which the relative path is calculated.</param>
        /// <param name="filename">The absolute filename to convert to being relative.</param>
        /// <returns>The relative path of the filename provided.</returns>
        private static string MakeRelative(string basePath, string filename)
        {
            var fileUri = new Uri(filename);
            var referenceUri = new Uri(basePath);
            var result = referenceUri.MakeRelativeUri(fileUri).ToString();
            return result;
        }
    }
}
