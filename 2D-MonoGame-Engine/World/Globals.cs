using _2D_MonoGame_Engine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _2D_MonoGame_Engine.World;

public static class Globals
{
    public static Point windowSize { get; set; }
    public static bool Paused { get; set; }
    public static GraphicsDeviceManager Graphics { get; set; }
    public static ContentManager ContentManager { get; set; }
    public static Camera.Camera Camera { get; set; }
    
    public static GameState CurrentState { get; set; }
}