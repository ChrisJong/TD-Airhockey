namespace AirHockey.InteractionLayer.Components
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Input;

    /// <summary>
    /// A manager that handles the input to the current
    /// device.
    /// </summary>
    public static class InputManager
    {
        public delegate void FingerPressedHandler(TouchPoint point);
        public delegate void FingerMovedHandler(TouchPoint originPoint, TouchPoint currentPoint);
        public delegate void FingerReleasedHandler(TouchPoint point);

        public delegate void TagPlacedHandler(TagPoint point);
        public delegate void TagRemovedHandler(TagPoint point);

        /// <summary>
        /// Triggered when a finger is first registered.
        /// </summary>
        public static event FingerPressedHandler OnFingerPressed;
        /// <summary>
        /// Triggered when a finger is moved from its position in the
        /// previous frame.
        /// </summary>
        public static event FingerMovedHandler OnFingerMoved;
        /// <summary>
        /// Triggered when a finger of a particular id is no longer
        /// registered.
        /// </summary>
        public static event FingerReleasedHandler OnFingerReleased;

        /// <summary>
        /// Triggered when a tag is first placed.
        /// </summary>
        public static event TagPlacedHandler OnTagPlaced;
        /// <summary>
        /// Triggered when a tag of a particular id is no longer
        /// registered.
        /// </summary>
        public static event TagRemovedHandler OnTagRemoved;

        /// <summary>
        /// The mapping between tag values and their corresponding
        /// index in the TagPoints array.
        /// </summary>
        /// Note: Tag value 0 is reserved for testing using mouse input.
        internal static readonly Dictionary<int, int> TagValueMapping = new Dictionary<int, int>
        {
            {0, 0},
            {1, 1},
            {2, 2},
            {3, 3},
            {4, 4},
            {5, 5},
            {6, 6},
            {100, 100},
            {101, 101},
            {102, 102},
            {103, 103},
            {104, 104},
            {105, 105},
            {106, 106}
        };

        private static List<TouchPoint> _oldTouchPoints = new List<TouchPoint>();
        private static List<TagPoint> _oldTagPoints = new List<TagPoint>(); 

        /// <summary>
        /// The points that are recorded as touch input.
        /// </summary>
        private static List<TouchPoint> _touchPoints = new List<TouchPoint>();

        /// <summary>
        /// The points that are recorded as Tag Inputs for each
        /// player.
        /// </summary>
        private static List<TagPoint> _tagPoints = new List<TagPoint>();

        /// <summary>
        /// Moves all current input to old input and clears lists.
        /// </summary>
        public static void StartFrameInput()
        {
            _oldTouchPoints = _touchPoints;
            _oldTagPoints = _tagPoints;

            _touchPoints = new List<TouchPoint>();
            _tagPoints = new List<TagPoint>();
        }

        /// <summary>
        /// Handles TagRemoved and FingerReleased events.
        /// </summary>
        public static void EndFrameInput()
        {
            if (OnFingerReleased != null)
            {
                _oldTouchPoints.Where(x => !_touchPoints.Select(y => y.Id).Contains(x.Id))
                    .ToList()
                    .ForEach(z => OnFingerReleased.Invoke(z));
            }

            if (OnTagRemoved != null)
            {
                _oldTagPoints.Where(x => !_tagPoints.Select(y => y.Id).Contains(x.Id))
                    .ToList()
                    .ForEach(z => OnTagRemoved.Invoke(z));
            }
        }

        /// <summary>
        /// Adds a touch point to the list for the current frame.
        /// </summary>
        /// <param name="id">The ID of the touch input.</param>
        /// <param name="x">The X position of the touch.</param>
        /// <param name="y">The Y position of the touch.</param>
        internal static void RegisterTouchPoint(int id, int x, int y)
        {
            var newTouchPoint = new TouchPoint
            {
                Id = id,
                Location = new Point(x, y)
            };

            var previousPoint = _oldTouchPoints.FirstOrDefault(previous => previous.Id == id);

            if (previousPoint != null)
            {
                if (OnFingerMoved != null)
                {
                    if (previousPoint.Location.X != x || previousPoint.Location.Y != y)
                    {
                        OnFingerMoved.Invoke(previousPoint, newTouchPoint);
                    }
                }
            }
            else
            {
                if (OnFingerPressed != null)
                {
                    OnFingerPressed.Invoke(newTouchPoint);
                }
            }

            _touchPoints.Add(newTouchPoint);
        }

        /// <summary>
        /// Sets the point for the Tag Point denoted by the given
        /// tag value.
        /// </summary>
        /// <param name="id">The ID of the tag input.</param>
        /// <param name="tagValue">The tag value.</param>
        /// <param name="x">The detected X position.</param>
        /// <param name="y">The detected Y position.</param>
        /// <param name="angle">The detected angle.</param>
        internal static void RegisterTagInput(int id, int tagValue, int x, int y, float angle = 0.0f)
        {
            if (TagValueMapping.ContainsKey(tagValue))
            {
                var newTagPoint = new TagPoint
                {
                    Id = id,
                    Location = new Point(x, y),
                    Type = TagValueMapping[tagValue],
                    Angle = angle
                };

                var previousPoint = _oldTagPoints.FirstOrDefault(previous => previous.Id == id);

                if (previousPoint == null)
                {
                    if (OnTagPlaced != null)
                    {
                        OnTagPlaced.Invoke(newTagPoint);
                    }
                }

                _tagPoints.Add(newTagPoint);
            }
        }

        /// <summary>
        /// Retrieves the position of a given tag type/value.
        /// </summary>
        /// <param name="type">The type of the Tag.</param>
        /// <param name="removeFromList">Whether or not to remove a point after it is retrieved (stop double handling of input).</param>
        /// <returns>The point at which the tag was detected or null if the tag was not detected.</returns>
        public static TagPoint GetTagPoint(int type, bool removeFromList = false)
        {
            var point = _tagPoints.FirstOrDefault(x => x.Type == type);

            if (removeFromList && point != null)
            {
                _tagPoints.Remove(point);
            }

            return point;
        }

        /// <summary>
        /// Retrieves a list of touch points.
        /// </summary>
        /// <returns>A list of touch points.</returns>
        public static List<TouchPoint> GetTouchPoints()
        {
            return _touchPoints;
        }

        /// <summary>
        /// Retrieves a list of touch points near a location.
        /// </summary>
        /// <param name="x">The X position to be near.</param>
        /// <param name="y">The Y position to be near.</param>
        /// <param name="distance">The distance threshold for touch points to be included.</param>
        /// <returns>The included touch points.</returns>
        public static List<TouchPoint> GetTouchPointsNear(float x, float y, float distance)
        {
            return
                _touchPoints.Where(
                    point =>
                        (point.Location.X - x)*(point.Location.X - x) + (point.Location.Y - y)*(point.Location.Y - y) <=
                        distance*distance).ToList();
        }
    }
}
