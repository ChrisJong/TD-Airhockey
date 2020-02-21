namespace AirHockey.InteractionLayer.Components.Resources
{
    using System;
    using System.IO;
    using Constants;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using Utility.Attributes;

    /// <summary>
    /// Stores a useable resource based on the GameResource
    /// (which is copied). This can be loaded and unloaded
    /// for active usage.
    /// </summary>
    public class UsableResource
    {
        /// <summary>
        /// The name/identifier that is used when picking a
        /// resource to be used. This should be unique.
        /// </summary>
        [NeverNull]
        public readonly string Name;

        /// <summary>
        /// The name of the file to which the resource points. Or
        /// an Image resource's name.
        /// </summary>
        [NeverNull]
        public readonly string FileName;

        /// <summary>
        /// The type of the resource. This is mostly for internal
        /// information and initial validation when loading the
        /// resource being pointed to.
        /// </summary>
        public readonly UsableResourceType Type;

        /// <summary>
        /// Stores the XNA asset object (such as Texture2D or SoundEffect).
        /// </summary>
        private object _asset;

        /// <summary>
        /// Stores a 'reference count' for the number of instances in which
        /// this resource was Loaded. Whenever the reference count is zero,
        /// the resource is released.
        /// </summary>
        private uint _referenceCount;

        /// <summary>
        /// A flag indicating whether or not the resource should be unloaded
        /// from memory when the reference count reaches zero.
        /// </summary>
        private readonly bool _keepInMemory;

        public UsableResource(
            string name,
            string fileName,
            UsableResourceType type,
            bool keepInMemory = GlobalSettings.KeepResourcesInMemory)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Name and FileName for new resources must not be null.");
            }

            this.Name = name;
            this.FileName = fileName;
            this.Type = type;

            this._keepInMemory = keepInMemory;
        }

        internal UsableResource(
            string name,
            SpriteFont asset)
        {
            this.Name = name;
            this.FileName = name;
            this._asset = asset;
            this.Type = UsableResourceType.Font;
            this._keepInMemory = GlobalSettings.KeepResourcesInMemory;
        }

        /// <summary>
        /// Releases the resource if it is in memory.
        /// </summary>
        internal void Release()
        {
            if (this._asset != null)
            {
                if (this.Type != UsableResourceType.Font) // currently fonts are only loaded via the content manager.
                {
                    ((IDisposable) this._asset).Dispose();
                }

                this._asset = null;
            }
        }

        /// <summary>
        /// Increments the reference count and returns the Texture2D asset.
        /// </summary>
        /// <returns>The texture to use.</returns>
        internal Texture2D LoadTexture()
        {
            if (this.Type == UsableResourceType.Image)
            {
                if (this._referenceCount == 0 && this._asset == null)
                {
                    this._asset = LoadTexture(this.FileName);
                }

                this._referenceCount++;
                return (Texture2D) this._asset;
            }

            throw new InvalidOperationException("Cannot load a texture from a non-texture resource.");
        }

        /// <summary>
        /// Increments the reference count and returns the SoundEffect asset.
        /// </summary>
        /// <returns>The sound effect asset to use.</returns>
        internal SoundEffect LoadAudio()
        {
            if (this.Type == UsableResourceType.Audio)
            {
                if (this._referenceCount == 0 && this._asset == null)
                {
                    this._asset = LoadSound(this.FileName);
                }

                this._referenceCount++;
                return (SoundEffect) this._asset;
            }

            throw new InvalidOperationException("Cannot load audio from a non-audio resource.");
        }

        /// <summary>
        /// Increments the reference count and returns the SoundEffect asset.
        /// </summary>
        /// <returns>The sound effect asset to use.</returns>
        internal SpriteFont LoadFont()
        {
            if (this.Type == UsableResourceType.Font)
            {
                if (this._asset != null)
                {
                    this._referenceCount++;
                    return (SpriteFont) this._asset;
                }
                // no dynamic loading for fonts at the moment.
                throw new InvalidOperationException("Cannot load font from an uninitialised font resource.");
            }

            throw new InvalidOperationException("Cannot load font from a non-font resource.");
        }

        /// <summary>
        /// Decrements the reference count and releases a resource if
        /// the flag for Keep In Memory is false.
        /// </summary>
        internal void Unload()
        {
            this._referenceCount--;

            if (this._referenceCount < 1)
            {
                this._referenceCount = 0;

                if (this._asset != null && !this._keepInMemory)
                {
                    this.Release();
                }
            }
        }

        /// <summary>
        /// Loads a texture with the given file name into memory.
        /// </summary>
        /// <param name="fileName">The filename of the texture.</param>
        /// <returns>The texture that has been loaded.</returns>
        internal static Texture2D LoadTexture(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open);
            var result = Texture2D.FromStream(InternalComponents.GraphicsDevice, stream);
            stream.Close();
            return result;
        }

        /// <summary>
        /// Loads a sound with the given file name into memory.
        /// </summary>
        /// <param name="fileName">The given file name.</param>
        /// <returns>The sound that has been loaded.</returns>
        internal static SoundEffect LoadSound(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open);
            var result = SoundEffect.FromStream(stream);
            stream.Close();
            return result;
        }
    }
}
