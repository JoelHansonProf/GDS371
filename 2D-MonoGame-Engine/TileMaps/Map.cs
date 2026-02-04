using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.TileMaps;

public class Map
{
    protected Point _mapSize;
    
    protected Tile[,] _tiles;


    public Map(Point mapSize)
    {
        _mapSize = mapSize;
    }

    public virtual void OnInitialize()
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int y = 0; y < _mapSize.Y; y++)
        {
            for (int x = 0; x < _mapSize.X; x++)
            {
                 _tiles[x, y].Draw(spriteBatch);
            }
        }
    }
}