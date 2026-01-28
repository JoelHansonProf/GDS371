
using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Objects;

public class LevelSprite : GameObject
{
    public LevelSprite(Texture2D texture, Vector2 basePosition)
    {
        transform.Position = basePosition;
        AddComponent<SpriteRenderer>(texture, transform);
    }

}