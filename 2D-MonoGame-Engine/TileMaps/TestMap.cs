using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.TileMaps;

public class TestMap : Map
{
    private ContentManager manager;
    
    public TestMap(ContentManager manager, Point mapSize) : base(mapSize)
    {
        _mapSize = mapSize;
        _tiles = new Tile[mapSize.X, mapSize.Y];
        this.manager = manager;
    }


    public override void OnInitialize()
    {
        Texture2D tileTexture = manager.Load<Texture2D>("DirtTile");
        
        for (int y = 0; y < _mapSize.Y; y++)
        for (int x = 0; x < _mapSize.X; x++)
        {
            if (y == _mapSize.Y - 1)
            {
                _tiles[x, y] = new Tile(tileTexture, new Vector2(x * tileTexture.Width, y * tileTexture.Height));
            }else 
                _tiles[x, y] = new AirTile();
        }
    }
}