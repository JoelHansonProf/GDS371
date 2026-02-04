using System;
using System.Collections.Generic;
using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.Input;
using _2D_MonoGame_Engine.Managers;
using _2D_MonoGame_Engine.Objects;
using _2D_MonoGame_Engine.Objects.Base;
using _2D_MonoGame_Engine.Scenes;
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
    private InputWrapper _inputWrapper = new InputWrapper();
    
    private GameState _currentState;
    
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
        Globals.Graphics = _graphics;
        GameManager manager = new GameManager();
        Globals.Camera = new Camera.Camera(GraphicsDevice.Viewport);
        //Forcing a state change to get the game going
        //This is THE ONLY TIME YOU WILL BE DIRECTLY CALLING THIS METHOD
        //Every other time is in the state itself
        SwitchGameState(new TestState());
        
        
        
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }


    private void OnStateSwitch(object sender, GameState newState)
    {
        Console.WriteLine($"{sender.GetType().Name} wants to switch to {newState.GetType().Name}");
        SwitchGameState(newState);
    }

    private void SwitchGameState(GameState newState)
    {
        //NUll check to make sure we can do an Unload and other things
        if (_currentState != null)
        {
            _currentState.onStateChanged -= OnStateSwitch;
            _currentState.UnloadContent(Content);
        }

        //Set the new state
        _currentState = newState;
        //Subscribe to the state change event
        _currentState.onStateChanged += OnStateSwitch;
        //Load the new state
        _currentState.LoadContent(Content);
        
        //Set the Globals.CurrentState
        Globals.CurrentState = newState;
    }
    
    //State -> State Change REquest -> main game "Oh time to change states" -> Actual state change

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _currentState.HandleInput(gameTime);
        _currentState.Update(gameTime);
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(transformMatrix: Globals.Camera.GetViewMatrix());
        
        _currentState.Draw(_spriteBatch);
        
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}