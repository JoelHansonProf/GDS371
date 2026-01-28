using System;
using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.DataStructures;
using _2D_MonoGame_Engine.Input;
using _2D_MonoGame_Engine.Objects;
using _2D_MonoGame_Engine.Objects.Base;
using _2D_MonoGame_Engine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2D_MonoGame_Engine.Scenes;

public class TestState : GameState
{
    
    public TestState()
    {
        
    }
    
    public override void LoadContent(ContentManager content)
    {
        var testGameObject = new TestObject();
        testGameObject.transform.Position = new Vector2(100, 100);
      
        AddGameObject(testGameObject);

        LevelQuadTree = new QuadTree<GameObject>(new Rectangle(0, 0, Globals.windowSize.X, Globals.windowSize.Y));
        LevelQuadTree.debug = true;


        for (int i = 0; i < 100; i++)
        {
            var random = new Random();
            var testSprite = new LevelSprite(content.Load<Texture2D>("smallShuckle"), new Vector2(random.Next(0,Globals.windowSize.X), random.Next(0,Globals.windowSize.Y)));
            AddGameObject(testSprite,true);
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

    public override void Draw(SpriteBatch spriteBatch)
    {
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