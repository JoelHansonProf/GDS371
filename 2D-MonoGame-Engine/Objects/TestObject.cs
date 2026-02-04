using System;
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


    public event EventHandler testEvent;
    
    public Vector2 velocity;
    public float moveSpeed = 1000;
    
    
    public TestObject()
    {
        AddComponent<SpriteRenderer>(Globals.ContentManager.Load<Texture2D>("smallShuckle"), transform);
        
        var SpriteRenderer = GetComponent<SpriteRenderer>();
        //AddComponent<TestComponent>();
        
        ServiceLocator.Instance.GetService(typeof(GameManager), out _gameManager);

        AddComponent<BoxCollider>(false, new Rectangle(transform.Position.ToPoint(), SpriteRenderer.size.ToPoint()));
        
        var boxCollider = GetComponent<BoxCollider>();
        boxCollider.debugDraw = true;
        boxCollider.queryRange = 20;
        boxCollider.onCollisionEnter += (otherCollider) =>
        {
            Console.WriteLine($"Collision with {otherCollider.owner.GetType().Name}");
        };
    }

    public override void Update(GameTime gameTime)
    {
        //BUG:: base.Update(gameTime) was here
        transform.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * moveSpeed;
        //Round position to nearest pixel
        transform.Position = new Vector2(MathF.Round(transform.Position.X), MathF.Round(transform.Position.Y));
        base.Update(gameTime);
    }
    
    //Just moving
    public void MoveLeft()
    {
        velocity = new Vector2(-1, 0);
    }
    public void MoveRight()
    {
        velocity = new Vector2(1, 0);
    }

    public void StopMoving()
    {
        velocity = Vector2.Zero;
    }
}