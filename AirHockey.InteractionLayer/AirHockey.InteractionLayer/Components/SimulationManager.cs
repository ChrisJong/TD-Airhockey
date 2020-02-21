namespace AirHockey.InteractionLayer.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Input;
    using Microsoft.Xna.Framework.Input;
    using Utility.Extensions;

    /// <summary>
    /// Handles the conversion of input from Moues and Keyboard
    /// to a simulation of Touch and Tag inputs.
    /// </summary>
    internal static class SimulationManager
    {
        /// <summary>
        /// Maps an XNA key (that is pressed) to a Tag value that will
        /// be toggled whenever the key is pressed.
        /// </summary>
        private static readonly Dictionary<Keys, int> KeyToTagValueMapping = new Dictionary<Keys, int>
        {
            {Keys.S, TagType.PlayerOne.SlingshotTower},     // Slingshot Tower
            {Keys.D, TagType.PlayerOne.ForcefieldTower},
            {Keys.F, TagType.PlayerOne.BlackholeTower},
            {Keys.G, TagType.PlayerOne.PulsarTower},
            {Keys.H, TagType.PlayerOne.SlowTower},
            {Keys.J, TagType.PlayerOne.StasisTower},

            {Keys.W, TagType.PlayerTwo.SlingshotTower},
            {Keys.E, TagType.PlayerTwo.ForcefieldTower},
            {Keys.R, TagType.PlayerTwo.BlackholeTower},
            {Keys.T, TagType.PlayerTwo.PulsarTower},
            {Keys.Y, TagType.PlayerTwo.SlowTower},
            {Keys.U, TagType.PlayerTwo.StasisTower}
        };

        /// <summary>
        /// To avoid deriving this each frame, the keys that are used are stored
        /// in list form at start up.
        /// </summary>
        private static readonly List<Keys> KeysToUseInSimulation = KeyToTagValueMapping.GetKeysList();

        /// <summary>
        /// Stores a list of keys that were pressed down last frame. This
        /// is used to detect when a key is released (and then triggers the
        /// toggling).
        /// </summary>
        private static List<Keys> _previouslyPressedKeys = new List<Keys>();

        /// <summary>
        /// Stores which tags (as far as the simulator is concerned) is down.
        /// Note that the simulator ignores actual tag input when it is run
        /// and may clash on occasion.
        /// </summary>
        private static readonly Dictionary<int, SimulatedTagInput> TagActive = CreateTagActiveDictionary();

        private static Dictionary<int, SimulatedTagInput> CreateTagActiveDictionary()
        {
            var result = new Dictionary<int, SimulatedTagInput>();

            foreach (var pair in KeyToTagValueMapping)
            {
                if (!result.ContainsKey(pair.Value))
                {
                    result.Add(pair.Value, null);
                }
            }

            return result;
        }

        /// <summary>
        /// Runs simulations of Touch and Tag inputs from Mouse and
        /// Keyboard states.
        /// </summary>
        public static void PerformSimulations()
        {
            var mouseState = Mouse.GetState();
            var keyState = Keyboard.GetState();

            SimulateTouchInput(mouseState);
            SimulateMouseAsTagInput(mouseState);
            SimulateKeysAsTagInput(keyState, mouseState.X, mouseState.Y);
        }

        /// <summary>
        /// Simulates touch input from left mouse button.
        /// </summary>
        /// <param name="state">The Mouse State.</param>
        private static void SimulateTouchInput(MouseState state)
        {
            if (state.LeftButton == ButtonState.Pressed)
            {
                InputManager.RegisterTouchPoint(0, state.X, state.Y);
            }
        }

        /// <summary>
        /// Simulates tag input from the right mouse button.
        /// </summary>
        /// <param name="state">The Mouse State.</param>
        private static void SimulateMouseAsTagInput(MouseState state)
        {
            if (state.RightButton == ButtonState.Pressed)
            {
                InputManager.RegisterTagInput(0, 0, state.X, state.Y);
            }
        }

        /// <summary>
        /// Simulates tag input based on keys being used to toggle
        /// the tag ON and OFF.
        /// </summary>
        /// <param name="state">The state of the keyboard.</param>
        /// <param name="x">The X position of the mouse (for inital tag placement).</param>
        /// <param name="y">The Y position of the mouse (for inital tag placement).</param>
        private static void SimulateKeysAsTagInput(KeyboardState state, int x, int y)
        {
            var keysPressed = state.GetPressedKeys().Where(a => KeysToUseInSimulation.Contains(a)).ToList();

            foreach (var previousKey in _previouslyPressedKeys.Where(a => !keysPressed.Contains(a)))
            {
                var tagValue = KeyToTagValueMapping[previousKey];

                // pressed and then released. toggle value.
                if (TagActive[tagValue] == null)
                {
                    TagActive[tagValue] = new SimulatedTagInput
                    {
                        Tag = tagValue,
                        X = x,
                        Y = y
                    };
                }
                else
                {
                    TagActive[KeyToTagValueMapping[previousKey]] = null;
                }
            }

            _previouslyPressedKeys = keysPressed;

            foreach (var simulatedInput in TagActive.Where(a => a.Value != null).Select(activeTag => activeTag.Value))
            {
                InputManager.RegisterTagInput(0, simulatedInput.Tag, simulatedInput.X, simulatedInput.Y);
            }
        }
    }
}
