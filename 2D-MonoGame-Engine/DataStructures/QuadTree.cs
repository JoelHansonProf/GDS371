using System;
using System.Collections.Generic;
using System.Linq;
using _2D_MonoGame_Engine.Debug;
using _2D_MonoGame_Engine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.DataStructures;

public class QuadTree<T> where T : GameObject
{
    private readonly int NODE_CAPACITY = 4;

    private HashSet<T> _objects = new HashSet<T>();


    //The initial bound of our quad tree
    private Rectangle _bounds;

    //Children nodes
    private QuadTree<T> _northEast;
    private QuadTree<T> _northWest;
    private QuadTree<T> _southEast;
    private QuadTree<T> _southWest;


    private bool _subdivided = false;

    public bool debug = false;

    public QuadTree(Rectangle bounds, int capacity = 4)
    {
        _bounds = bounds;
        NODE_CAPACITY = capacity;
    }

    public bool Insert(T item)
    {
        try
        {
            Point itemPoint = item.transform.Position.ToPoint();

            Console.WriteLine("Attempting to insert: " + item + "");

            if (!_bounds.Contains(itemPoint))
            {
                //Return an error if want
                return false;
            }

            //If not subdivided, add to objects
            if (_objects.Count < NODE_CAPACITY && !_subdivided)
            {
                Console.WriteLine("Added to objects");
                _objects.Add(item);
                return true;
            }


            if (!_subdivided)
                Subdivide();
            
            
            if(_northEast.Insert(item)) return true;
            if(_northWest.Insert(item)) return true;
            if(_southEast.Insert(item)) return true;
            if(_southWest.Insert(item)) return true;
            
            //We should NEVER beable to get here
            Console.WriteLine("We got to the point of no return, check code.");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine("Quad Tree Error: " + e);
        }

        return false;
    }

    private void Subdivide()
    {
        //Get the x and y of the bounds
        int x = _bounds.X;
        int y = _bounds.Y;
        
        //Get half the width and height
        int halfWidth = _bounds.Size.X / 2;
        int halfHeight = _bounds.Size.Y / 2;
        
        //Create the children
        _northWest = new QuadTree<T>(new Rectangle(x, y, halfWidth, halfHeight));
        _northEast = new QuadTree<T>(new Rectangle(x + halfWidth, y, halfWidth, halfHeight));
        _southWest = new QuadTree<T>(new Rectangle(x, y+halfHeight, halfWidth, halfHeight));
        _southEast = new QuadTree<T>(new Rectangle(x + halfWidth, y+halfHeight, halfWidth, halfHeight));
        
        //Set the same settings as the parent
        _northWest.debug = debug;
        _northEast.debug = debug;
        _southWest.debug = debug;
        _southEast.debug = debug;

        
        _subdivided = true;

        //Add all objects to the children
        foreach (var obj in _objects)
        {
            bool added = false || (
                _northWest.Insert((obj)) || 
                _northEast.Insert((obj)) || 
                _southWest.Insert((obj)) ||
                _southEast.Insert((obj)));
            if (added)
            {
                //Do a thing
            }
        }
        
        //This is NO LONGER HOLDING ON TO ANY OBJECTS
        _objects.Clear();
    }

    public List<T> QueryRange(Rectangle range)
    {
        List<T> pointsQueried = new List<T>();
        
        //If bounds isn't in the range, return false
        // This is most likely a children check
        if(!_bounds.Intersects(range)) 
            return pointsQueried;


        for (int i = 0; i < _objects.Count; i++)
        {
            //Get the position of the object
            var objectPostion = _objects.ElementAt(i).transform.Position;
            //Convert Vector2 to Point
            Point pointOfObject = objectPostion.ToPoint();
            
            //If the point is in the range, add it to the list
            if (range.Contains(pointOfObject))
            {
                pointsQueried.Add(_objects.ElementAt(i));
            }
        }
        
        //If not subdivided, return the list
        if(!_subdivided) return pointsQueried;
        
        //If we are
        pointsQueried.AddRange(_northEast.QueryRange(range));
        pointsQueried.AddRange(_northWest.QueryRange(range));
        pointsQueried.AddRange(_southEast.QueryRange(range));
        pointsQueried.AddRange(_southWest.QueryRange(range));
        
        return pointsQueried;
    }

    /// <summary>
    /// Clears all objects in the quad tree
    /// </summary>
    public void Clear()
    {
        _objects.Clear();

        if (_subdivided)
        {
            _northEast.Clear();
            _northWest.Clear();
            _southEast.Clear();
            _southWest.Clear();

            _subdivided = false;
        }
    }

    public void DebugDraw(SpriteBatch spriteBatch)
    {
        if (_subdivided)
        {
            _northEast.DebugDraw(spriteBatch);
            _northWest.DebugDraw(spriteBatch);
            _southEast.DebugDraw(spriteBatch);
            _southWest.DebugDraw(spriteBatch);
        }
        
        DebugGfx.DrawRectangle(spriteBatch, _bounds, _subdivided ? Color.Red : Color.Green);
    }
}