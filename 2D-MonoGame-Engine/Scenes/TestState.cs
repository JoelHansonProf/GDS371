using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.Input;
using _2D_MonoGame_Engine.Objects;
using _2D_MonoGame_Engine.Objects.Base;
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

    public override void HandleInput(GameTime gameTime)
    {
        if (InputWrapper.Instance != null)
        {
            InputWrapper.Instance.CheckForKeyBoardInput(Keyboard.GetState());
        }
    }
}