using System;
using System.Collections.Generic;
using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.DataStructures;
using _2D_MonoGame_Engine.Objects.Base;
using _2D_MonoGame_Engine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Scenes;

public abstract class GameState
{
   
    //HashSet just wants to Update/Draw ticks
    private readonly HashSet<GameObject> _gameObjects = new HashSet<GameObject>();

    //Going to be handled for collision detection
    //UPDATED This to handle BoxColliders
    public QuadTree<BoxCollider> LevelQuadTree { get; set; }
    
    public abstract void LoadContent(ContentManager content);

    public abstract void UnloadContent(ContentManager content);

    //For future use
    public abstract void HandleInput(GameTime gameTime);
 
    //Small observer pattern to inform that state has changed
    
    public event EventHandler<GameState> onStateChanged;

    
    
    protected void SwitchState(GameState newState)
    {
        onStateChanged?.Invoke(this, newState);
    }

    public virtual bool AddGameObject(GameObject gameObject, bool addToQuadTree = false)
    {
        _gameObjects.Add(gameObject);

        
        if(addToQuadTree)
            LevelQuadTree.Insert(gameObject.GetComponent<BoxCollider>());
        
        return true;
    }

    public bool AddToQuadTree(BoxCollider gameObject)
    {
        return LevelQuadTree.Insert(gameObject);
    }

    public virtual bool RemoveGameObject(GameObject gameObject)
    {
        return _gameObjects.Remove(gameObject);
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach (var gameObject in _gameObjects)
        {
            gameObject.Update(gameTime);
        }

        if (LevelQuadTree is { DynamicallyResize: true })
        {
            LevelQuadTree.DynamicallyUpdate();
        }
        
        Globals.Camera.Update(gameTime);
    }
    
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (var gameObject in _gameObjects)
        {
            gameObject.Draw(spriteBatch);
        }
    }
    
}