using System;
using System.Collections.Generic;
using _2D_MonoGame_Engine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2D_MonoGame_Engine;

public class Editor : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    //TileSet references
    private Texture2D _tileSet;
    private Texture2D _editorBG;
    private List<Rectangle> _tileRectangles;

    //Grid values
    private int[,] _grid;
    private int _gridWidth = 200;
    private int _gridHeight = 200;
    private int _gridOffestX = 0;
    private int _gridOffestY = 0;
    
    
    //TWO Folder
    // Size of the tiles themselves, and the sizes that each sprite should be
    private int _tileSize = 32;

    //Update Variables
    private int _selectedTile;
    private Point _lastMousePosition = Point.Zero;

    //Panning
    private bool _isPanning = false;
    
    //Pallete tool
    private int _paletteOffSetY;
    
    //Tools Panel
    private int _toolPaletteX;
    private int _toolPaletteWidth;
    private int _toolPaletteY;
    private int _toolPaletteHeight;
    
    
    private int _toolPalettePadding = 10;

    private int _paddingBetweenTiles;
    //Layering
    private string[] _layers = { "Background", "Foreground", "Collision" };
    private string _activeLayer = "Background";
    
    //Our new layers
    private int[,] _backgroundLayer;
    private int[,] _foregroundLayer;
    private int[,] _collisionLayer;
    
    public Editor()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 800,
            PreferredBackBufferHeight = 600
        };


        _toolPaletteWidth = 200;
        _toolPaletteX = _graphics.PreferredBackBufferWidth - _toolPaletteWidth;
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _grid = new int[_gridWidth, _gridHeight];

        
        //Create the 3 separate layers
        _backgroundLayer = new int[_gridWidth, _gridHeight];
        _foregroundLayer = new int[_gridWidth, _gridHeight];
        _collisionLayer = new int[_gridWidth, _gridHeight];
        
        for(int y = 0; y < _gridHeight; y++)
        for (int x = 0; x < _gridWidth; x++)
        {
            //Default to -1 for empty tiles
            _backgroundLayer[x,y] = -1;
            _foregroundLayer[x,y] = -1;
            _collisionLayer[x,y] = -1;
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        try
        {
            _tileSet = Content.Load<Texture2D>("tiles");
            _editorBG = Content.Load<Texture2D>("stockphoto");
            _paletteOffSetY = _tileSet.Height;
            Console.WriteLine("Loaded TileSet");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading TileSet: " + e);
        }

        GenerateTileRectangles();
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        //Get mouse state
        var mouseState = Mouse.GetState();
        var mousePosition = new Point(mouseState.X, mouseState.Y);
        
        HandlePanning(ref mouseState);

        HandleLayerSwapping(ref mouseState);
        //Place Tiles
        if (mouseState.LeftButton == ButtonState.Pressed && mousePosition.Y < _gridHeight * _tileSize)
        {
            PlaceTile(mouseState);
        }
        
        HandleTilePaletteSelection(ref mouseState);
     
    }

    private void HandlePanning(ref MouseState mouseState)
    {
        var mousePosition = new Point(mouseState.X, mouseState.Y);

        if (mouseState.MiddleButton == ButtonState.Pressed)
        {
            if (!_isPanning)
            {
                _isPanning = true;
                _lastMousePosition = mouseState.Position;
            }
            else
            {
                //Get the directional movement
                Point delta = mousePosition - _lastMousePosition;
                
                //Update last Position
                _lastMousePosition = mousePosition;
                
                
                //subtract the offset
                _gridOffestX -= (int)Math.Round((float)delta.X / (_tileSize));
                _gridOffestY -= (int)Math.Round((float)delta.Y / (_tileSize));
            }
        }
        else
        {
            _isPanning = false;
        }
    }

    private void HandleLayerSwapping(ref MouseState mouseState)
    {
        int buttonX = _toolPaletteX;
        int buttonY = _toolPalettePadding + 250;

        //Loop through the available layers
        for (int i = 0; i < _layers.Length; i++)
        {
            //If mouse is pressed and within bounds of the button
            if (mouseState.LeftButton == ButtonState.Pressed && mouseState.X >= buttonX
                                                             && mouseState.X < buttonX + 160
                                                             && mouseState.Y >= buttonY
                                                             && mouseState.Y < buttonY + 30)
            {
                //set the active layer to i
                _activeLayer = _layers[i];
            }
            
            //Move to the next button
            buttonY += 30;
        }
    }

    private void HandleTilePaletteSelection(ref MouseState mouseState)
    {
        var mousePosition = new Point(mouseState.X, mouseState.Y);
        
        //Tile Palette Selection section

        int paddedTileSize = _tileSize + _paddingBetweenTiles;
        
        int maxColumns = GraphicsDevice.Viewport.Width / paddedTileSize;
        
        
        if (mouseState.LeftButton == ButtonState.Pressed && mousePosition.Y >= _paletteOffSetY &&
            mousePosition.Y < _paletteOffSetY + (_tileRectangles.Count / maxColumns + 1) * _tileSize)
        {
            int column = mousePosition.X / paddedTileSize;
            int row = (mousePosition.Y - _paletteOffSetY) / paddedTileSize;
            
            //Get the index of the tile
            int tileIndex = row * maxColumns + column;

            if (tileIndex >= 0 && tileIndex < _tileRectangles.Count)
            {
                //Set the selected tile
                _selectedTile = tileIndex;
                
                Console.WriteLine("Selected Tile: " + _selectedTile);
            }
        }
    }

    private void PlaceTile(MouseState mouseState)
    {
        int mouseX = mouseState.X;
        int mouseY = mouseState.Y;
        
        //Check to see if mouse is within grid bounds
        if (mouseX >= _toolPaletteX && mouseX < _toolPaletteX + _toolPaletteWidth) return;
        //If mouse is greater than the grid, don't let us draw tiles
        if (mouseY >= _paletteOffSetY ) return;

        //
        int x = (mouseState.X / _tileSize) + _gridOffestX;
        int y = (mouseState.Y / _tileSize) + _gridOffestY;

        //Check to see if the tile is within the grid bounds
        if (x >= 0 && x < _grid.GetLength(0) && y >= 0 && y < _grid.GetLength(1) && _selectedTile != -1)
        {
            switch (_activeLayer)
            {
                case "Background":
                    _backgroundLayer[x, y] = _selectedTile;
                    break;
                case "Foreground":
                    _foregroundLayer[x, y] = _selectedTile;
                    break;
                case "Collision":
                    _collisionLayer[x, y] = _selectedTile;
                    break;
            }
        }
    }

    private void GenerateTileRectangles()
    {
        _tileRectangles = new List<Rectangle>();

        Color[] textureData = new Color[_tileSet.Width * _tileSet.Height];
        _tileSet.GetData(textureData);
        
        for (int y = 0; y < _tileSet.Height / _tileSize; y++)
        {
            for (int x = 0; x < _tileSet.Width / _tileSize; x++)
            {
                var sourceRectangle = new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize);
                
                if(sourceRectangle.IsTileEmpty(textureData,_tileSet.Width))
                    _tileRectangles.Add(sourceRectangle);
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.BlanchedAlmond);
        _spriteBatch.Begin();

        //DrawGrid();
        DrawLayer(_backgroundLayer, "Background");
        DrawLayer(_foregroundLayer, "Foreground");
        DrawLayer(_collisionLayer, "Collision");
        
        DrawToolsPanel();
        DrawTilePalette();
      
     
        _spriteBatch.End();
        base.Draw(gameTime);
    }


    public void DrawLayer(int[,] layer, string layerName)
    {
                            //SHort hand if statement    True           False
        Color alphaColor = (layerName == _activeLayer) ? Color.White : new Color(255, 255, 255, 128);
        
        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                int gridX = x + _gridOffestX;
                int gridY = y + _gridOffestY;
                
                if(gridX < 0 || gridY < 0 || gridX >= _grid.GetLength(0) || gridY >= _grid.GetLength(1)) continue;
                
                int tileIndex = layer[gridX, gridY];
              
                if (tileIndex != -1)
                {
                    var sourceRectangle = _tileRectangles[tileIndex];
                    
                    //Draw the tile
                    _spriteBatch.Draw(_tileSet, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), 
                        sourceRectangle, alphaColor);
                }
            }
        }
    }

    public void DrawGrid()
    {
        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                int gridX = x + _gridOffestX;
                int gridY = y + _gridOffestY;
                
                if(gridX < 0 || gridY < 0 || gridX >= _grid.GetLength(0) || gridY >= _grid.GetLength(1)) continue;
                
                int tileIndex = _grid[gridX, gridY];
                _spriteBatch.DrawString(Content.Load<SpriteFont>("gameFont"), $"{tileIndex}", new Vector2(x * _tileSize + _tileSize/2f,y * _tileSize + _tileSize/2f),Color.Black);
                if (tileIndex != -1)
                {
                    var sourceRectangle = _tileRectangles[tileIndex];
                    
                    
                    
                    //Draw the tile
                    _spriteBatch.Draw(_tileSet, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), 
                        sourceRectangle, Color.White);
                }
            }
        }
    }

    public void DrawTilePalette()
    {
        //Draw the tile pallete background
        _spriteBatch.Draw(_editorBG, new Rectangle(0,_paletteOffSetY,GraphicsDevice.Viewport.Width,_paletteOffSetY),Color.White);
        
        //Max columns is the number of tiles that fit on the screen width wise
        int paddedTileSize = _tileSize + _paddingBetweenTiles;
        int maxColumns = GraphicsDevice.Viewport.Width / paddedTileSize;

        for (int i = 0; i < _tileRectangles.Count; i++)
        {
            //Calculate row and column
            int row = i / maxColumns;
            int column = i % maxColumns;
            
            //Set position of each tile
            int x = column * paddedTileSize;
            int y = _paletteOffSetY + row * paddedTileSize;
            
            //Get the source rectangle
            var sourceRectangle = _tileRectangles[i];
            
            
            _spriteBatch.Draw(_tileSet, new Rectangle(x,y,_tileSize,_tileSize), sourceRectangle,Color.White);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("gameFont"), $"{i}", new Vector2(x + _tileSize/2f,y + _tileSize/2f),Color.White);
        }
    }

    public void DrawToolsPanel()
    {
        //Draw the background
        _spriteBatch.Draw(_editorBG, new Rectangle(_toolPaletteX,0,_toolPaletteWidth,400),Color.White);
        
        _spriteBatch.DrawString(Content.Load<SpriteFont>("gameFont"), "Tools Panel", new Vector2(_toolPaletteX+ _toolPaletteWidth/2f - 7,10),Color.White);

        if (_selectedTile != -1)
        {
            
            //Create a rectangle
            var selectedTileRectangle = new Rectangle(_toolPaletteX + 20, 400 /2, _tileSize * 2, _tileSize * 2) ;
            //Draw the currently selected tile
            _spriteBatch.Draw(_tileSet, selectedTileRectangle, _tileRectangles[_selectedTile],Color.White);
            //OPtional
            _spriteBatch.DrawString(Content.Load<SpriteFont>("gameFont"), 
                $"Tile:{_selectedTile}", new Vector2(_toolPaletteX + 20 + _tileSize * 2 + 10,  400 /2f +10),Color.White);
        }

        DrawLayerSwitching();
    }

    private void DrawLayerSwitching()
    {
        int buttonX = _toolPaletteX;
        int buttonY = _toolPalettePadding + 250;


        for (int i = 0; i < _layers.Length; i++)
        {
            var layerName = _layers[i];
            //active color
            Color buttonColor = (_activeLayer == layerName) ? Color.Red : Color.White;
            
            //Draw the button Background
            _spriteBatch.Draw(Content.Load<Texture2D>("square"), new Rectangle(buttonX, buttonY, 100, 30), buttonColor);
            
            //Draw the text
            _spriteBatch.DrawString(Content.Load<SpriteFont>("gameFont"), layerName, new Vector2(buttonX, buttonY ), Color.White);
            
            //Move to the next button
            buttonY += 30;
        }
    }
}