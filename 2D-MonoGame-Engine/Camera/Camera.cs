using _2D_MonoGame_Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Camera;

public class Camera
{
    protected Vector2 _position;
    
    //TODO:: Refactor Transform -> FollowCamera child
    // We don't want Base Camera to be coupled to Transform
    
    //TODO:: Would be nice to have an offset vector
    
    public float Zoom { get; set; } = 1;
    public float Rotation { get; set; } = 0;
    protected int ViewportWidth { get; set; }
    protected int ViewportHeight { get; set; }
    
    public Camera(Viewport viewport)
    {
        ViewportWidth = viewport.Width;
        ViewportHeight = viewport.Height;
    }

    public void SetPosition(Vector2 position)
    {
        _position = position;
    }

    public Matrix GetViewMatrix()
    {
        return Matrix.CreateTranslation(new Vector3(-_position,0.0f)) *
               Matrix.CreateRotationZ(Rotation) *
               Matrix.CreateScale(Zoom, Zoom, 1.0f) *
               Matrix.CreateTranslation(new Vector3(ViewportWidth / 2.0f, ViewportHeight / 2.0f, 0.0f));
    }
    
    public virtual void Update(GameTime gameTime){}
}