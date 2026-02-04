using _2D_MonoGame_Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Camera;

public class FollowCamera : Camera
{
    public Transform followTransform { get; set; }

    public FollowCamera(Viewport viewport, Transform lockOnTransform) : base(viewport)
    {
        ViewportWidth = viewport.Width;
        ViewportHeight = viewport.Height;
        followTransform = lockOnTransform;
    }

    public override void Update(GameTime gameTime)
    {
        //TODO:: Add smoothing
        // (Lerping)

        _position = followTransform.Position;
    }
}