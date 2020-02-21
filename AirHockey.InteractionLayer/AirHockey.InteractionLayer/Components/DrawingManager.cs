namespace AirHockey.InteractionLayer.Components
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Resources;
    using Utility.Classes;
    using Color = System.Drawing.Color;
    using Point = System.Drawing.Point;
    using Rectangle = System.Drawing.Rectangle;

    /// <summary>
    /// Abstracts all drawing functionality for simple and
    /// portable access to drawing images and shapes on to
    /// the screen.
    /// </summary>
    public static class DrawingManager
    {
        private static readonly RasterizerState RasterizerState = new RasterizerState();

        /// <summary>
        /// Returns the dimensions of the screen in use.
        /// </summary>
        /// <returns>The width and the hight of the screen in use.</returns>
        public static Point GetScreenDimensions()
        {
            return new Point(
                InternalComponents.GraphicsDevice.Viewport.Width,
                InternalComponents.GraphicsDevice.Viewport.Height);
        }

        /// <summary>
        /// Gets the dimensions of an image. Duplicate calls should
        /// be avoided due to a potential performance hit.
        /// </summary>
        /// <param name="resourceName">The resource of an image to retrieve values for.</param>
        /// <returns>The dimensions of the image.</returns>
        public static Point GetImageDimensions(ResourceName resourceName)
        {
            if (!ResourceManager.ValidateResourceName(UsableResourceType.Image, resourceName))
            {
                throw new ArgumentException("Invalid resouce name provided for GetImageDimensions.");
            }

            var resource = ResourceManager.GetResource(UsableResourceType.Image, resourceName);
            var texture = resource.LoadTexture();

            var result = new Point(texture.Width, texture.Height);

            resource.Unload();

            return result;
        }

        /// <summary>
        /// Draws a circle to the screen.
        /// </summary>
        /// <param name="position">The position of the circle's origin.</param>
        /// <param name="origin">The relative origin of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="colour">The colour of the circle.</param>
        /// <param name="depth">At what depth to draw the circle.</param>
        public static void DrawEllipse(Point position, DrawingOrigin origin, float radius, Color colour, float depth)
        {
            DrawEllipse(position.X, position.Y, origin, radius, colour, depth);
        }

        /// <summary>
        /// Draws a circle to the screen.
        /// </summary>
        /// <param name="x">The X position of the circle's origin.</param>
        /// <param name="y">The Y position of the circle's origin.</param>
        /// <param name="origin">The relative origin of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="colour">The colour of the circle.</param>
        /// <param name="depth">At what depth to draw the circle.</param>
        public static void DrawEllipse(
            int x,
            int y,
            DrawingOrigin origin,
            float radius,
            Color colour,
            float depth)
        {
            DrawEllipse(x, y, origin, radius, radius, colour, 0.0f, depth);
        }

        /// <summary>
        /// Draws an ellipse to the screen.
        /// </summary>
        /// <param name="position">The position of the ellipse's origin.</param>
        /// <param name="origin">The relative origin of the ellipse.</param>
        /// <param name="shortRadius">The radius of the short side of the ellipse.</param>
        /// <param name="longRadius">The radius of the long side of the ellipse.</param>
        /// <param name="angle">The angle at which to draw the ellipse.</param>
        /// <param name="colour">The colour of the ellipse.</param>
        /// <param name="depth">At what depth to draw the ellipse.</param>
        public static void DrawEllipse(
            Point position,
            DrawingOrigin origin,
            float shortRadius,
            float longRadius,
            Color colour,
            float angle,
            float depth)
        {
            DrawEllipse(position.X, position.Y, origin, shortRadius, longRadius, colour, angle, depth);
        }

        /// <summary>
        /// Draws an ellipse to the screen.
        /// </summary>
        /// <param name="x">The X position of the ellipse's origin.</param>
        /// <param name="y">The Y position of the ellipse's origin.</param>
        /// <param name="origin">The relative origin of the ellipse.</param>
        /// <param name="shortRadius">The radius of the short side of the ellipse.</param>
        /// <param name="longRadius">The radius of the long side of the ellipse.</param>
        /// <param name="angle">The angle at which to draw the ellipse.</param>
        /// <param name="colour">The colour of the ellipse.</param>
        /// <param name="depth">At what depth to draw the ellipse.</param>
        public static void DrawEllipse(
            int x,
            int y,
            DrawingOrigin origin,
            float shortRadius,
            float longRadius,
            Color colour,
            float angle,
            float depth)
        {
            {
                var originPoint = GetOriginPosition(x, y, (int) longRadius*2, (int) shortRadius*2, origin);
                x = originPoint.X;
                y = originPoint.Y;
            }

            var texture = new Texture2D(InternalComponents.GraphicsDevice, (int) longRadius*2, (int) shortRadius*2);
            var textureData = new Microsoft.Xna.Framework.Color[((int) longRadius*2)*((int) shortRadius*2)];

            for (var yPoint = 0; yPoint < shortRadius*2; yPoint++)
            {
                var dY = yPoint - shortRadius;

                for (var xPoint = 0; xPoint < longRadius*2; xPoint++)
                {
                    var index = yPoint*(int) longRadius*2 + xPoint;

                    var dX = (xPoint - longRadius)/(longRadius/shortRadius);

                    if (dX*dX + dY*dY <= shortRadius*shortRadius)
                    {
                        textureData[index] = ConvertColor(colour);
                    }
                    else
                    {
                        textureData[index] = Microsoft.Xna.Framework.Color.Transparent;
                    }
                }
            }

            texture.SetData(textureData);

            InternalComponents.SpriteBatch.Draw(
                texture,
                new Vector2(x, y),
                null,
                Microsoft.Xna.Framework.Color.White,
                angle,
                new Vector2((float) texture.Width/2, (float) texture.Height/2),
                1.0f,
                SpriteEffects.None,
                depth);
        }

        /// <summary>
        /// Draws a rectangle of a given colour to the screen.
        /// </summary>
        /// <param name="rectangle">The rectangle's dimensions.</param>
        /// <param name="colour">The colour of the rectangle.</param>
        /// <param name="depth">The depth at which to draw the rectangle.</param>
        public static void DrawRectangle(
            Rectangle rectangle,
            Color colour,
            float depth = 0.0f)
        {
            DrawRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, colour);
        }

        /// <summary>
        /// Draws a rectangle of a given colour to the screen.
        /// </summary>
        /// <param name="x">The X position of the rectangle.</param>
        /// <param name="y">The Y position of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="colour">The colour of the rectangle.</param>
        /// <param name="depth">The depth at which to draw the rectangle.</param>
        public static void DrawRectangle(
            int x,
            int y,
            int width,
            int height,
            Color colour,
            float depth = 0.0f)
        {
            var color = ConvertColor(colour);
            var clearingTexture = new Texture2D(InternalComponents.GraphicsDevice, width, height);

            clearingTexture.SetData(Enumerable.Repeat(color, width*height).ToArray());

            InternalComponents.SpriteBatch.Draw(
                clearingTexture,
                new Vector2(x, y),
                null,
                Microsoft.Xna.Framework.Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                depth);
        }

        /// <summary>
        /// Draws an image onto the screen at the given position and angle.
        /// </summary>
        /// <param name="resourceName">The image to draw onto the screen.</param>
        /// <param name="position">The position at which to draw the image.</param>
        /// <param name="origin">The desired origin point for the image.</param>
        /// <param name="angle">The angle at which to draw the image.</param>
        /// <param name="depth">How deep the image is drawn (ie Layering/Z Ordering).</param>
        /// <param name="alpha">The opacity at which to draw. This can be a value between 0.0f and 1.0f.</param>
        public static void DrawImage(
            ResourceName resourceName,
            Point position,
            DrawingOrigin origin = DrawingOrigin.TopLeft,
            float angle = 0.0f,
            float depth = 0.0f,
            float alpha = 1.0f)
        {
            DrawImage(resourceName, position.X, position.Y, new Vector(1, 1), origin, angle, depth, alpha);
        }

        /// <summary>
        /// Draws an image onto the screen at the given position and angle.
        /// </summary>
        /// <param name="resourceName">The image to draw onto the screen.</param>
        /// <param name="x">The X position at which to draw the image.</param>
        /// <param name="y">The Y position at which to draw the image.</param>
        /// <param name="origin">The desired origin point for the image.</param>
        /// <param name="angle">The angle at which to draw the image.</param>
        /// <param name="depth">How deep the image is drawn (ie Layering/Z Ordering).</param>
        /// <param name="alpha">The opacity at which to draw. This can be a value between 0.0f and 1.0f.</param>
        public static void DrawImage(
            ResourceName resourceName,
            float x,
            float y,
            DrawingOrigin origin = DrawingOrigin.TopLeft,
            float angle = 0.0f,
            float depth = 0.0f,
            float alpha = 1.0f)
        {
            DrawImage(resourceName, x, y, new Vector(1, 1), origin, angle, depth, alpha);
        }

        /// <summary>
        /// Draws an image onto the screen at the given position with the given
        /// scale and angle.
        /// </summary>
        /// <param name="resourceName">The image to draw onto the screen.</param>
        /// <param name="position">The position at which to draw the image.</param>
        /// <param name="scale">The scale to apply to the image.</param>
        /// <param name="origin">The desired origin point for the image.</param>
        /// <param name="angle">The angle at which to draw the image.</param>
        /// <param name="depth">How deep the image is drawn (ie Layering/Z Ordering).</param>
        public static void DrawImage(
            ResourceName resourceName,
            Point position,
            Vector scale,
            DrawingOrigin origin = DrawingOrigin.TopLeft,
            float angle = 0.0f,
            float depth = 0.0f)
        {
            DrawImage(resourceName, position.X, position.Y, scale, origin, angle, depth);
        }

        /// <summary>
        /// Draws an image onto the screen at the given position with the given
        /// scale and angle.
        /// </summary>
        /// <param name="resourceName">The image to draw onto the screen.</param>
        /// <param name="position">The position at which to draw the image.</param>
        /// <param name="scale">The scale to apply to the image.</param>
        /// <param name="origin">The desired origin point for the image.</param>
        /// <param name="angle">The angle at which to draw the image.</param>
        /// <param name="depth">How deep the image is drawn (ie Layering/Z Ordering).</param>
        /// <param name="alpha">The opacity at which to draw. This can be a value between 0.0f and 1.0f.</param>
        public static void DrawImage(
            ResourceName resourceName,
            Point position,
            Vector scale,
            DrawingOrigin origin,
            float angle,
            float depth,
            float alpha)
        {
            DrawImage(resourceName, position.X, position.Y, scale, origin, angle, depth, alpha);
        }

        /// <summary>
        /// Draws an image onto the screen at the given position with the given
        /// scale and angle.
        /// </summary>
        /// <param name="resourceName">The image to draw onto the screen.</param>
        /// <param name="x">The X position at which to draw the image.</param>
        /// <param name="y">The Y position at which to draw the image.</param>
        /// <param name="scale">The scale to apply to the image.</param>
        /// <param name="origin">The desired origin point for the image.</param>
        /// <param name="angle">The angle at which to draw the image.</param>
        /// <param name="depth">How deep the image is drawn (ie Layering/Z Ordering).</param>
        /// <param name="alpha">The opacity at which to draw. This can be a value between 0.0f and 1.0f.</param>
        public static void DrawImage(
            ResourceName resourceName,
            float x,
            float y,
            Vector scale,
            DrawingOrigin origin = DrawingOrigin.TopLeft,
            float angle = 0.0f,
            float depth = 0.0f,
            float alpha = 1.0f)
        {
            // This is being commented as it causes a 20% drop in performance. 20%!
            // Current fps differences when 4 towers are placed:
            //          30fps uncommented.
            //          55fps commented.
            // Consider that a game doesn't dyanmically load images when it's deployed.
            // Using reflections for such an expensive task is just wasteful.
            // if (!ResourceManager.ValidateResourceName(UsableResourceType.Image, resourceName))
            // {
            //     throw new ArgumentException("Invalid resouce name provided for DrawImage.");
            // }

            var resource = ResourceManager.GetResource(UsableResourceType.Image, resourceName);
            var texture = resource.LoadTexture();

            InternalComponents.SpriteBatch.Draw(
                texture,
                new Vector2(x, y),
                null,
                Microsoft.Xna.Framework.Color.White * alpha,
                angle,
                GenerateOriginVector(origin, texture),
                ConvertVector(scale),
                SpriteEffects.None,
                depth);

            // Who loads and unloads hundreds of times a frame?
            // Really poor game structure - even if it's a regular count check.
            // This should only be done AFTER a game or level finishes.
            resource.Unload();
        }

        /// <summary>
        /// Initialises all the InterfaceLayer requirements for drawing in a
        /// particular context. This overload simply sets for full screen
        /// drawing. This is for Views.
        /// </summary>
        public static void InitialiseDrawingContext(Color backgroundColor)
        {
            InternalComponents.SpriteBatch.End();

            InternalComponents.SpriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.Additive,
                null,
                null,
                new RasterizerState());

            if (backgroundColor != Color.Transparent)
            {
                InternalComponents.GraphicsDevice.Clear(ConvertColor(backgroundColor));
            }
        }

        /// <summary>
        /// Initialises all the InterfaceLayer requirements for drawing in a
        /// particular context. This overload sets for drawing within a
        /// particular area of the screen. This is for dialogs.
        /// </summary>
        public static void InitialiseDrawingContext(int x, int y, int width, int height, Color backgroundColor)
        {
            InternalComponents.SpriteBatch.End();

            RasterizerState.ScissorTestEnable = true;

            InternalComponents.SpriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.NonPremultiplied,
                null,
                null,
                new RasterizerState {ScissorTestEnable = true});

            InternalComponents.GraphicsDevice.ScissorRectangle = new Microsoft.Xna.Framework.Rectangle(x, y, width, height);

            if (backgroundColor != Color.Transparent)
            {
                var clearingTexture = new Texture2D(InternalComponents.GraphicsDevice, width, height);
                clearingTexture.SetData(Enumerable.Repeat(ConvertColor(backgroundColor), width*height).ToArray());
                InternalComponents.SpriteBatch.Draw(clearingTexture, new Vector2(x, y), Microsoft.Xna.Framework.Color.White);
            }
        }

        /// <summary>
        /// Draws text to the screen.
        /// </summary>
        /// <param name="font">The font resource to use when drawing.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="point">The position at which to draw the text.</param>
        /// <param name="colour">The colour for the text.</param>
        /// <param name="depth">The depth at which to draw the text.</param>
        public static void DrawText(ResourceName font, string text, Point point, Color colour, float depth = 0.0f)
        {
            DrawText(font, text, point.X, point.Y, colour, depth);
        }

        /// <summary>
        /// Draws text to the screen.
        /// </summary>
        /// <param name="font">The font resource to use when drawing.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="x">The X position at which to draw the text.</param>
        /// <param name="y">The Y position at which to draw the text.</param>
        /// <param name="colour">The colour for the text.</param>
        /// <param name="depth">The depth at which to draw the text.</param>
        public static void DrawText(ResourceName font, string text, int x, int y, Color colour, float depth = 0.0f)
        {
            var resource = ResourceManager.GetResource(UsableResourceType.Font, font);
            var spriteFont = resource.LoadFont();

            InternalComponents.SpriteBatch.DrawString(
                spriteFont,
                text,
                new Vector2(x, y),
                ConvertColor(colour),
                0.0f,
                new Vector2(),
                1.0f,
                SpriteEffects.None,
                depth);

            resource.Unload();
        }

        /// <summary>
        /// Draws text in the given bound space (using line wrapping and
        /// cutting off lines beyond the hight limit).
        /// </summary>
        /// <param name="font">The font resource to use when drawing.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="boundsForText">The position and width and height limits to use when drawing.</param>
        /// <param name="centreTextInBounds">Whether or not to centre the text within the bounds of the rectangle.</param>
        /// <param name="colour">The colour for the text.</param>
        /// <param name="depth">The depth at which to draw the text.</param>
        public static void DrawText(
            ResourceName font,
            string text,
            Rectangle boundsForText,
            bool centreTextInBounds,
            Color colour,
            float depth = 0.0f)
        {
            var resource = ResourceManager.GetResource(UsableResourceType.Font, font);
            var spriteFont = resource.LoadFont();

            if (boundsForText.Width != 0)
            {
                text = WrapTextWidth(spriteFont, text, boundsForText.Width);

                if (centreTextInBounds)
                {
                    boundsForText.X += (boundsForText.Width - (int) spriteFont.MeasureString(text).X)/2;
                }
            }

            if (boundsForText.Height != 0)
            {
                text = WrapTextHeight(spriteFont, text, boundsForText.Height);

                if (centreTextInBounds)
                {
                    boundsForText.Y += (boundsForText.Height - (int) spriteFont.MeasureString(text).Y)/2;
                }
            }

            InternalComponents.SpriteBatch.DrawString(
                spriteFont,
                text,
                new Vector2(boundsForText.X, boundsForText.Y),
                ConvertColor(colour),
                0.0f,
                new Vector2(),
                1.0f,
                SpriteEffects.None,
                depth);

            resource.Unload();
        }

        public static void DrawText(
            ResourceName font,
            string text,
            Rectangle boundsForText,
            bool centreTextInBounds,
            Color colour,
            float rotation,
            float depth = 0.0f)
        {
            var resource = ResourceManager.GetResource(UsableResourceType.Font, font);
            var spriteFont = resource.LoadFont();

            if (boundsForText.Width != 0)
            {
                text = WrapTextWidth(spriteFont, text, boundsForText.Width);

                if (centreTextInBounds)
                {
                    boundsForText.X += (boundsForText.Width - (int)spriteFont.MeasureString(text).X) / 2;
                }
            }

            if (boundsForText.Height != 0)
            {
                text = WrapTextHeight(spriteFont, text, boundsForText.Height);

                if (centreTextInBounds)
                {
                    boundsForText.Y += (boundsForText.Height - (int)spriteFont.MeasureString(text).Y) / 2;
                }
            }

            InternalComponents.SpriteBatch.DrawString(
                spriteFont,
                text,
                new Vector2(boundsForText.X, boundsForText.Y),
                ConvertColor(colour),
                rotation,
                new Vector2(),
                1.0f,
                SpriteEffects.None,
                depth);

            resource.Unload();
        }

        /// <summary>
        /// Returns a string with new line characters that facilitate
        /// wrapping with a given max line width.
        /// </summary>
        /// <param name="spriteFont">The font being used.</param>
        /// <param name="text">The text to write.</param>
        /// <param name="maxLineWidth">The maximum line width.</param>
        /// <returns>The wrapped text.</returns>
        private static string WrapTextWidth(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            var positionOfPreviousSpace = -1;
            var result = string.Empty;
            var width = 0.0f;

            for (var index = 0; index < text.Length; index++)
            {
                var character = text[index];

                if (character == '\r' || character == '\n')
                {
                    positionOfPreviousSpace = -1;
                    width = 0.0f;
                }
                else if (character == ' ' || character == '\t')
                {
                    positionOfPreviousSpace = index;
                }

                if (character != '\r' && character != '\n')
                {
                    var additionalWidth = spriteFont.MeasureString(character.ToString(CultureInfo.InvariantCulture)).X;

                    if (width + additionalWidth > maxLineWidth)
                    {
                        if (positionOfPreviousSpace != -1)
                        {
                            result = result.Insert(positionOfPreviousSpace, "\r\n");
                        }
                        else
                        {
                            // one very long line will simply break at the long point.
                            result += "\r\n";
                        }

                        width = 0.0f;

                        if (character == ' ' || character == '\t')
                        {
                            continue; // Do not put leading spaces and tabs just because they couldn't fit on previous line.
                        }
                    }

                    width += additionalWidth;
                }

                result += character;
            }

            return result;
        }

        /// <summary>
        /// Returns a string whose lines have been trimmed until
        /// the height of the text is below the limit.
        /// </summary>
        /// <param name="spriteFont">The font being used.</param>
        /// <param name="text">The text to write.</param>
        /// <param name="maxLineHeight">The maximum text heigth.</param>
        /// <returns>The trimmed text.</returns>
        private static string WrapTextHeight(SpriteFont spriteFont, string text, float maxLineHeight)
        {
            var result = text.Replace("\r\n", "\n");

            while (spriteFont.MeasureString(result).Y > maxLineHeight && result.Length > 0)
            {
                result = result.Contains('\n')
                    ? result.Substring(0, result.LastIndexOf('\n'))
                    : string.Empty;
            }

            return result.Replace("\n", "\r\n");
        }

        /// <summary>
        /// Converts a native dot net color value to an XNA color value.
        /// </summary>
        /// <param name="nativeColor">The dot net color to convert.</param>
        /// <returns>The XNA color equivalent of the dot net color.</returns>
        private static Microsoft.Xna.Framework.Color ConvertColor(Color nativeColor)
        {
            return new Microsoft.Xna.Framework.Color(nativeColor.R, nativeColor.G, nativeColor.B, nativeColor.A);
        }

        /// <summary>
        /// Converts a Vector to the XNA class Vector2.
        /// </summary>
        /// <param name="vector">The original value.</param>
        /// <returns>The Vector2 that XNA uses.</returns>
        private static Vector2 ConvertVector(Vector vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        /// <summary>
        /// Creates the Vector2 used by the Draw method to determine
        /// the texture's origin point. This converts from a simple enum
        /// to the required Vector2.
        /// </summary>
        /// <param name="origin">The desired origin position.</param>
        /// <param name="texture">The texture to use when calculating Vector2 values.</param>
        /// <returns>The Vector2 with the correct values for the desired origin.</returns>
        private static Vector2 GenerateOriginVector(DrawingOrigin origin, Texture2D texture)
        {
            switch (origin)
            {
                case DrawingOrigin.BottomCenter:
                    return new Vector2(texture.Width/2.0f, texture.Height);
                case DrawingOrigin.BottomLeft:
                    return new Vector2(0, texture.Height);
                case DrawingOrigin.BottomRight:
                    return new Vector2(texture.Width, texture.Height);
                case DrawingOrigin.Center:
                    return new Vector2(texture.Width/2.0f, texture.Height/2.0f);
                case DrawingOrigin.MiddleLeft:
                    return new Vector2(0, texture.Height/2.0f);
                case DrawingOrigin.MiddleRight:
                    return new Vector2(texture.Width, texture.Height/2.0f);
                case DrawingOrigin.TopCenter:
                    return new Vector2(texture.Width/2.0f, 0);
                case DrawingOrigin.TopLeft:
                    return new Vector2(0, 0);
                case DrawingOrigin.TopRight:
                    return new Vector2(texture.Width, 0);
            }

            throw new NotImplementedException("DrawingOrigin value not implemented in GenerateOriginVector.");
        }

        /// <summary>
        /// Get the absolute origin position of an object.
        /// </summary>
        /// <param name="x">The X position of the object.</param>
        /// <param name="y">The Y position of the object.</param>
        /// <param name="width">The width of the object.</param>
        /// <param name="height">The height of the object.</param>
        /// <param name="origin">The origin of the object.</param>
        /// <returns>The position of the origin of the object.</returns>
        private static Point GetOriginPosition(int x, int y, int width, int height, DrawingOrigin origin)
        {
            switch (origin)
            {
                case DrawingOrigin.BottomCenter:
                    return new Point(x + width/2, y + height);
                case DrawingOrigin.BottomLeft:
                    return new Point(x, y + height);
                case DrawingOrigin.BottomRight:
                    return new Point(x + width, y + height);
                case DrawingOrigin.Center:
                    return new Point(x + width/2, y + height/2);
                case DrawingOrigin.MiddleLeft:
                    return new Point(x, y + height/2);
                case DrawingOrigin.MiddleRight:
                    return new Point(width, height / 2);
                case DrawingOrigin.TopCenter:
                    return new Point(width / 2, 0);
                case DrawingOrigin.TopLeft:
                    return new Point(0, 0);
                case DrawingOrigin.TopRight:
                    return new Point(width, 0);
            }

            throw new NotImplementedException("DrawingOrigin value not implemented in GenerateOriginVector.");
        }
    }
}
