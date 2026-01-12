using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.Managers;
using _2D_MonoGame_Engine.Objects.Base;
using _2D_MonoGame_Engine.Utilities;
using _2D_MonoGame_Engine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Objects;

public class TestObject : GameObject
{
    private GameManager _gameManager;
    public TestObject()
    {
        AddComponent<SpriteRenderer>(Globals.ContentManager.Load<Texture2D>("smallShuckle"), transform);
        AddComponent<TestComponent>();
        
        ServiceLocator.Instance.GetService(typeof(GameManager), out _gameManager);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _gameManager.IncrementScore();
    }
}