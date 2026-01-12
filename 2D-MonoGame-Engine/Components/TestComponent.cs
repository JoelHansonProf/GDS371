using _2D_MonoGame_Engine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Components;

public class TestComponent : Component
{
    public override void OnInitialize(GameObject newOwner)
    {
        owner = newOwner;
    }

    public override void OnDestroy()
    {
      
    }

    public override void Update(GameTime gameTime)
    {
        owner.transform.Position += new Vector2(1, 0);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
    }
}