using System.Collections.Generic;
using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.Managers;
using _2D_MonoGame_Engine.Objects;
using _2D_MonoGame_Engine.Objects.Base;
using _2D_MonoGame_Engine.Utilities;
using _2D_MonoGame_Engine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2D_MonoGame_Engine;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private ServiceLocator _serviceLocator = new ServiceLocator();
    private HashSet<GameObject> _gameObjects = new HashSet<GameObject>();

    private GameObject testObject;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Globals.windowSize = new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        Globals.ContentManager = Content;
        
        GameManager manager = new GameManager();
        
            
        TestObject newTest = new TestObject();
        newTest.transform.Position = new Vector2(100, 100);
        _gameObjects.Add(newTest);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here


        foreach (var gameObject in _gameObjects)
            gameObject.Update(gameTime);


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        foreach (var gameObject in _gameObjects)
            gameObject.Draw(_spriteBatch);
        
        
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}