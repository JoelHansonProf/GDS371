using System;
using System.Collections.Generic;
using _2D_MonoGame_Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Objects.Base;

public class GameObject
{
    private uint id;
    public uint ID => id;

    public Transform transform = new Transform();
    //arrays
    //List - 
    //HashSet - is an unordered list of pointers
    //Dictionary - mapped list - key is the pointer value is the reference

    //Dictionary
    //List/HashSet
    private List<Component> _components = new List<Component>();

    public GameObject()
    {

        var random = new Random();

        //Generates a random Unique ID
        id = (uint)random.Next(0, Int32.MaxValue);
    }


    public virtual void Update(GameTime gameTime)
    {
        foreach (var component in _components)
        {
            component.Update(gameTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in _components)
        {
            component.Draw(spriteBatch);
        }
    }
    //gameObject.AddComponent<Transform>();
    public GameObject AddComponent<T>(params object[] args) where T : Component
    {
        //Transform newObject = new Transform();
        T component = (T)Activator.CreateInstance(typeof(T), args);

        if (component != null)
        {
            component.OnInitialize(this);
            _components.Add(component);
        }
        else
        {
            throw new ArgumentException("Component cannot be null");
        }

        return this;
    }


    //Good to note that maybe having a Dictionary in the backend is the way to go

    public T GetComponent<T>() where T : Component
    {

        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i] is T)
            {
                return (T)_components[i];
            }
        }

        return default(T);
    }

    public bool RemoveComponent<T>() where T : Component
    {
        T component = GetComponent<T>();
        if (component != null)
        {
            _components.Remove(component);
            return true;
        }

        return false;
    }
}