namespace AirHockey.GameLayer.Views.Core.Base
{
    using System.Collections.Generic;
    using ComponentModel;
    using Utility.Attributes;
    using Utility.Classes;
    using System;

    /// <summary>
    /// A common base class for Views and DialogViews.
    /// </summary>
    abstract class GameObjectContainerBase : UpdateAndDrawableBase, IMessageHandler
    {
        private readonly List<GameObjectBase> _gameObjects = new List<GameObjectBase>();
        //private static int instanceCount = 0;

        //public GameObjectContainerBase()
        //{
        //    instanceCount++;
        //    InteractionLayer.Components.DebugManager.Write(instanceCount.ToString() + " ++ NEW: Object Container " + this.ToString());
        //}

        //~GameObjectContainerBase()
        //{
        //    instanceCount--;
        //    InteractionLayer.Components.DebugManager.Write(instanceCount.ToString() + "-- DELETED: Object Container " + this.ToString());
        //}

        /// <summary>
        /// The objects in this view dialog. Objects contained
        /// in this list are updated and rendered.
        /// </summary>
        [NeverNull]
        public List<GameObjectBase> GameObjects
        {
            get { return this._gameObjects; }
        }

        /// <summary>
        /// Adds the given game object to the GameObjects collection
        /// while adding this GameObjectConrtainerBase's reference to
        /// its MessageHandlers.
        /// </summary>
        /// <param name="gameObject">The game object to add.</param>
        public void AddGameObject(GameObjectBase gameObject)
        {
            if (gameObject != null)
            {
                this.GameObjects.Add(gameObject);
            }
        }

        /// <summary>
        /// Handles a message that is passed in.
        /// </summary>
        /// <param name="message">The message to be handled.</param>
        /// <param name="parameters">The parameters for that message.</param>
        /// <returns>The result of the message.</returns>
        public abstract object AcceptMessage(string message, params object[] parameters);

        public override void Release()
        {
            foreach (GameObjectBase g in GameObjects)
            {
                g.Release();
            }

            // After calling release on all objects, clear the list.
            this.GameObjects.Clear();
        }
    }
}
