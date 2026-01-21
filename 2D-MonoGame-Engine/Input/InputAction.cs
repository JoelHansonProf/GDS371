using System;
using Microsoft.Xna.Framework.Input;

namespace _2D_MonoGame_Engine.Input;

public class InputAction
{
    //Monogame Variable
    //What key to press
    private Keys _keybind;
    //Previous keyboard state
    private KeyboardState _previousState;

    //Continous press down
    public event Action performed;
    //1 frame If key goes up
    public event Action released;
    // 1 frame if key goes down
    public event Action pressed;
    
    public InputAction(Keys keybind)
    {
        _keybind = keybind;
    }


    public void CheckForInput(KeyboardState keyboardState)
    {
        if (IsDownThisFrame(keyboardState, _keybind))
        {
            pressed?.Invoke();
        }

        if (IsUpThisFrame(keyboardState, _keybind))
        {
            released?.Invoke();
        }

        if (keyboardState.IsKeyDown(_keybind))
        {
            performed?.Invoke();
        }
        
        _previousState = keyboardState;
    }

    /// <summary>
    /// Checks to see if the key is down this frame
    /// </summary>
    /// <param name="state"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsDownThisFrame(KeyboardState state, Keys key)
    {
        return state.IsKeyDown(key) && _previousState.IsKeyUp(key);
    }

    public bool IsUpThisFrame(KeyboardState state, Keys key)
    {
        return state.IsKeyUp(key) && _previousState.IsKeyDown(key);
    }

}