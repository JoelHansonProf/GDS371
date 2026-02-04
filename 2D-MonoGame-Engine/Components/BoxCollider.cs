using System;
using System.Collections.Generic;
using _2D_MonoGame_Engine.Debug;
using _2D_MonoGame_Engine.Objects.Base;
using _2D_MonoGame_Engine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Components;

public class BoxCollider : Component
{

    public Rectangle Bounds;

    public bool isStatic;
    public bool isTrigger { get; set; }
    
    public float boundsScale { get; set; } = 1;
    public float queryRange { get; set; } = 1;
    public bool debugDraw { get; set; } = false;

    private bool _isColliding = false;

    public event Action<BoxCollider> onCollision;
    public event Action<BoxCollider> onCollisionEnter;

    public List<BoxCollider> currentlyCollidingWith;
    
    public BoxCollider(bool isStatic, Rectangle bounds)
    {
        currentlyCollidingWith = new List<BoxCollider>();
        this.isStatic = isStatic;
        Bounds = bounds;
    }
    

    public override void OnInitialize(GameObject newOwner)
    {
        owner = newOwner;
    }

    public override void OnDestroy()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        //So if we are NOT static we need to update the bounds
        if (!isStatic)
        {
            Bounds.X = (int)owner.transform.Position.X;
            Bounds.Y = (int)owner.transform.Position.Y;
        }

        CheckForCollision();

    }

    public void CheckForCollision()
    {
        _isColliding = false;

        //
        Rectangle queryBox = Bounds;

        //Inflate the box by the query range
        queryBox.Inflate(queryRange, queryRange);

        List<GameObject> objectsNearOwner = Globals.CurrentState.LevelQuadTree.QueryRange(queryBox);

        //Check for objects entering collision
        for (int i = 0; i < objectsNearOwner.Count; i++)
        {
            BoxCollider otherCollider = objectsNearOwner[i].GetComponent<BoxCollider>();

            //If we picked up the same object, ignore it
            if (owner == otherCollider.owner)
                continue;

            if (Bounds.Intersects(otherCollider.Bounds))
            {
                //If we are not already colliding with this object
                if (!currentlyCollidingWith.Contains(otherCollider))
                {
                    currentlyCollidingWith.Add(otherCollider);
                    onCollisionEnter?.Invoke(otherCollider);
                }

                _isColliding = true;
                onCollision?.Invoke(otherCollider);
            }
        }
        
        //Check for objects leaving
        for(int i = 0; i < currentlyCollidingWith.Count; i++)
        {
            if (!objectsNearOwner.Contains(currentlyCollidingWith[i].owner))
            {
                currentlyCollidingWith.RemoveAt(i);
                i--;
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (debugDraw)
        {
            DebugGfx.DrawRectangle(spriteBatch,Bounds, _isColliding ? Color.Red : Color.Green, thickness:3);
        }
    }
}