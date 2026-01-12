using _2D_MonoGame_Engine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Components;

public abstract class Component
{
    //Who owns this component
    public GameObject owner;
    
    public abstract void OnInitialize(GameObject newOwner);
    public abstract void OnDestroy();
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
    
    
}