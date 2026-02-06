
using _2D_MonoGame_Engine.DataStructures;
using Microsoft.Xna.Framework;

namespace _2D_MonoGame_Engine.Interfaces;

//Since we are updating the quad tree to be able to have anything we need to be able to get the position and bounds of the object
public interface IQuadTreeObject<T> where T : IQuadTreeObject<T>
{
    public QuadTree<T> CurrentNode { get; set; }
    
    public Vector2 GetPosition();
    
    public Rectangle GetBounds();
}
