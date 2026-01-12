using _2D_MonoGame_Engine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Components;

public class Transform : Component
{
    private Vector2 _position;
    public Vector2 Position { get => _position; set => _position = value; }
    
    private float _rotation;
    public float Rotation { get => _rotation;
        set
        {
            if (value > 360)
            {
                _rotation = 0;
            }
        }
    }
    
    private Vector2 _scale;
    public Vector2 Scale { get => _scale; set => _scale = value; }
    
    public override void OnInitialize(GameObject newOwner)
    {
        owner = newOwner;
    }

    public override void OnDestroy()
    {
      
    }

    public override void Update(GameTime gameTime)
    {
       
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      
    }
}