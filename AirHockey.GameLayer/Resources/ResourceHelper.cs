namespace AirHockey.GameLayer.Resources
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Utility.Extensions;

    static class ResourceHelper
    {
        /// <summary>
        /// Lists the image file extensions that we can open.
        /// </summary>
        private static readonly string[] ImageExtensions =
        {
            ".gif",
            ".png",
            ".jpg"
        };

        /// <summary>
        /// Lists the audio file extensions that we can open.
        /// </summary>
        /// Note: Must be PCM wave file, Mono or Stereo only,
        /// 8 or 16 bit, Sample Rate b/w 8Khz and 48Khz.
        private static readonly string[] AudioExtensions =
        {
            ".wav"
        };

        /// <summary>
        /// Appends the resources from a given path to the Resources collection.
        /// </summary>
        /// <param name="currentResources">A collection of the resources that have been accumilated so far.</param>
        /// <param name="resourcePath">The path to use when loading the resources.</param>
        private static void GenerateResource(List<GameResource> currentResources, string resourcePath)
        {
            var files = Directory.GetFiles(resourcePath, "*.*", SearchOption.AllDirectories)
                .Where(
                    x =>
                        x.EndsWithAny(
                            ImageExtensions
                                .Concat(AudioExtensions)
                                .ToArray(),
                            StringComparison.OrdinalIgnoreCase));

            foreach (var file in files)
            {
                if (file.EndsWithAny(ImageExtensions, StringComparison.OrdinalIgnoreCase))
                {
                    AddResource(
                        currentResources,
                        new GameResource(
                            GenerateResourceNameFromFile(MakeRelative(resourcePath, file)),
                            file,
                            GameResourceType.Image));
                }
                else if (file.EndsWithAny(AudioExtensions, StringComparison.OrdinalIgnoreCase))
                {
                    AddResource(
                        currentResources,
                        new GameResource(
                            GenerateResourceNameFromFile(MakeRelative(resourcePath, file)),
                            file,
                            GameResourceType.Audio));
                }
            }
        }

        /// <summary>
        /// Appends the resources from a given set of paths to the Resources
        /// collection.
        /// </summary>
        /// <param name="resourcePaths">The paths to use when loading the resources.</param>
        public static List<GameResource> GenerateResources(string[] resourcePaths)
        {
            var result = new List<GameResource>();
            resourcePaths = resourcePaths.Select(Path.GetFullPath).ToArray();
            resourcePaths.ToList().ForEach(x => GenerateResource(result, x));
            return result;
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
                .ToLower();                                         // resource names not case sensitive
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

        /// <summary>
        /// Adds a resource to the Resources collection. This function MUST be called
        /// in place of calling Resources.Add() since it performs additional and
        /// essential checks.
        /// </summary>
        /// <param name="currentResources">A collection of the resources that have been accumilated so far.</param>
        /// <param name="resource">The resource to be added.</param>
        public static void AddResource(List<GameResource> currentResources, GameResource resource)
        {
            if (resource == null ||
                currentResources.Any(
                    x => (x.Name == resource.Name && x.Type == resource.Type) || x.FileName == resource.FileName))
            {
                return;
            }

            currentResources.Add(resource);
        }
    }
}
