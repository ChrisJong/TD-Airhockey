namespace AirHockey.InteractionLayer.Components
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Resources;

    /// <summary>
    /// Manages and provides controls for debug-specific
    /// functionality.
    /// </summary>
    public static class DebugManager
    {
#if DEBUG

        private static Color textColor = Color.White;
        /// <summary>
        /// The maximum length for a debug text line.
        /// </summary>
        private const int MaxLineLength = 80;

        /// <summary>
        /// Stores the debug text messages.
        /// </summary>
        private static readonly Queue<string> DebugQueue = new Queue<string>();
#endif

        /// <summary>
        /// Adds text to the debug text message queue.
        /// </summary>
        /// <param name="debugText">The debug text to add.</param>
        public static void Write(string debugText, Color debugColor)
        {
#if DEBUG
            DebugManager.textColor = debugColor;

            var rows = SplitIntoRows(debugText);

            foreach (var debugTextLine in rows)
            {
                DebugQueue.Enqueue(debugTextLine);

                if (DebugQueue.Count > 15)
                {
                    DebugQueue.Dequeue();
                }
            }
#endif
        }

        public static void Write(string debugText)
        {
            DebugManager.Write(debugText, Color.White);
        }

        /// <summary>
        /// Draws the debug text to the screen.
        /// </summary>
        public static void DrawDebugText()
        {
#if DEBUG
            var message = string.Join("\r\n", DebugQueue.ToList());

            DrawingManager.DrawText(new ResourceName("resources.global.debugging"), message, 10, 10, DebugManager.textColor);
#endif
        }

#if DEBUG
        /// <summary>
        /// Splits a debug message into separate entries in
        /// the Queue based on new line characters and lines
        /// exceeding the MaxLineLength in length.
        /// </summary>
        /// <param name="debugText">The debug text to split.</param>
        /// <returns>The split debug text.</returns>
        private static IEnumerable<string> SplitIntoRows(string debugText)
        {
            var result = debugText.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries).ToList();

            for (var i = 0; i < result.Count; i++)
            {
                if (result[i].Length > MaxLineLength)
                {
                    var extraPart = result[i].Substring(MaxLineLength);
                    result[i] = result[i].Substring(0, MaxLineLength);
                    result.Insert(i + 1, extraPart);
                }
            }

            return result;
        }
#endif
    }
}
