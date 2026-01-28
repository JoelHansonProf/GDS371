using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Debug;

public static class DebugGfx
{
    // A single white pixel used to draw lines and shapes
    private static Texture2D _pixel;

    /// <summary>
    /// Draws a hollow rectangle outline.
    /// </summary>
    /// <param name="spriteBatch">The active SpriteBatch.</param>
    /// <param name="rect">The rectangle to visualize.</param>
    /// <param name="color">The color of the outline.</param>
    /// <param name="thickness">How thick the border lines should be.</param>
    public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rect, Color color, int thickness = 1)
    {
        // Create the 1x1 pixel texture if it doesn't exist yet
        if (_pixel == null)
        {
            _pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }

        // Draw Top Line
        spriteBatch.Draw(_pixel, new Rectangle(rect.X, rect.Y, rect.Width, thickness), color);

        // Draw Bottom Line
        spriteBatch.Draw(_pixel, new Rectangle(rect.X, rect.Bottom - thickness, rect.Width, thickness), color);

        // Draw Left Line
        spriteBatch.Draw(_pixel, new Rectangle(rect.X, rect.Y, thickness, rect.Height), color);

        // Draw Right Line
        spriteBatch.Draw(_pixel, new Rectangle(rect.Right - thickness, rect.Y, thickness, rect.Height), color);
    }
}
