using _2D_MonoGame_Engine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Components;

public class SpriteRenderer : Component
{
    private Transform _transform;
    
    private Texture2D _texture;
    private Vector2 _origin;
    public Vector2 Origin { get; }

    public Color color { get; set; } = Color.White;


    public SpriteRenderer(Texture2D texture, Transform transform)
    {
        _texture = texture;
        _transform = transform;
    }

    public SpriteRenderer(Texture2D texture, Transform transform, Color color)
    {
        _texture = texture;
        _transform = transform;
        this.color = color;
    }


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
        
        Rectangle sourceRectangle = new Rectangle((int)_transform.Position.X,
            (int)_transform.Position.Y, _texture.Width, _texture.Height);
        
       spriteBatch.Draw(_texture, sourceRectangle, color);
    }
}