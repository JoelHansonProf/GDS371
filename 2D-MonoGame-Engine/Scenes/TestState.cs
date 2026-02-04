using System;
using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.DataStructures;
using _2D_MonoGame_Engine.Input;
using _2D_MonoGame_Engine.Objects;
using _2D_MonoGame_Engine.Objects.Base;
using _2D_MonoGame_Engine.TileMaps;
using _2D_MonoGame_Engine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2D_MonoGame_Engine.Scenes;

public class TestState : GameState
{

    private TestMap _testMap;
    
    public TestState()
    {
        
    }
    
    public override void LoadContent(ContentManager content)
    {
        
        
        var testGameObject = new TestObject();
        testGameObject.transform.Position = new Vector2(100, 100);
      
        AddGameObject(testGameObject);

        
        
        LevelQuadTree = new QuadTree<GameObject>(new Rectangle(0, 0, 2000, 2000));
        LevelQuadTree.debug = true;
        
        _testMap = new TestMap(content, new Point(Globals.windowSize.X / 32, Globals.windowSize.Y /32 ));
        _testMap.OnInitialize();
        
        
        Globals.Camera = new Camera.FollowCamera(Globals.Graphics.GraphicsDevice.Viewport, testGameObject.transform);
        
        Globals.Camera.SetPosition(new Vector2(Globals.windowSize.X / 2f,Globals.windowSize.Y / 2f));
    
        for (int i = 0; i < 1000; i++)
        {
            var random = new Random();
           // var testSprite = new TestCollision();
            //AddGameObject(testSprite,true);
        }
    
    
    
        var moveLeftAction = InputWrapper.Instance.AddKeyBind(Keys.A, "Move Left");
        var moveRightAction = InputWrapper.Instance.AddKeyBind(Keys.D, "Move Right");

        //Setting up keybind
        moveLeftAction.pressed += testGameObject.MoveLeft;
        moveRightAction.pressed += testGameObject.MoveRight;
        
        moveRightAction.released += testGameObject.StopMoving;
        moveLeftAction.released += testGameObject.StopMoving;
    }

    public override void UnloadContent(ContentManager content)
    {
      
    }


    public override void Update(GameTime gameTime)
    { 
        //BUG:: Globals.Camera.Update was here
        
        base.Update(gameTime);
        
        //TODO:: Cache camera
        Globals.Camera.Update(gameTime);
        
      
      
  
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        
        _testMap.Draw(spriteBatch);
        
        base.Draw(spriteBatch);
        
        if (LevelQuadTree != null && LevelQuadTree.debug)
            LevelQuadTree.DebugDraw(spriteBatch);
    }

    public override void HandleInput(GameTime gameTime)
    {
        if (InputWrapper.Instance != null)
        {
            InputWrapper.Instance.CheckForKeyBoardInput(Keyboard.GetState());
        }
    }
}