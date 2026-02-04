
using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.TileMaps;

public class Tile : GameObject
{
    
    //Just the constructor
    public Tile(Texture2D texture,Vector2 basePosition)
    {
        AddComponent<SpriteRenderer>(texture, transform);
        transform.Position = basePosition;
    }

    protected Tile()
    {
    }
}