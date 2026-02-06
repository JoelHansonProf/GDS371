
using _2D_MonoGame_Engine.DataStructures;
using Microsoft.Xna.Framework;

namespace _2D_MonoGame_Engine.Interfaces;

public interface IQuadTreeObject<T> where T : IQuadTreeObject<T>
{
    public QuadTree<T> QuadTreeNode { get; set; }
    
    public Vector2 GetPosition();
    
    public Rectangle GetBounds();
}
