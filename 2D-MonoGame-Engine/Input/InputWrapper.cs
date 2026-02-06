using System;
using System.Collections.Generic;
using _2D_MonoGame_Engine.Utilities;
using Microsoft.Xna.Framework.Input;

namespace _2D_MonoGame_Engine.Input;

public class InputWrapper : SingletonBehaviour<InputWrapper>
{
    private readonly Dictionary<string, InputAction> actions;
    
    public InputWrapper()
    {
        actions = new Dictionary<string, InputAction>();
        
        OnIntialize();
    }

    public InputAction AddKeyBind(Keys keybind, string actionName = "New KeyBind")
    {
        if (actionName == "New KeyBind")
        {
            throw new ArgumentException("Action name cannot be 'New KeyBind'");
        }

        if (actions.ContainsKey(actionName))
        {
            throw new ArgumentException("Action name already exists");
            
            
            //TODO:: add keybind to existing action
        }
        else
        {
            actions.Add(actionName, new InputAction(keybind));
        }
        
        return actions[actionName];
    }

    public void ResetActions()
    {
        actions.Clear();
    }

    public InputAction GetAction(string actionName)
    {
        return actions[actionName];
    }

    public void CheckForKeyBoardInput(KeyboardState keyboardState)
    {
        foreach (var action in actions.Values)
        {
            action.CheckForInput(keyboardState);
        }
    }
}