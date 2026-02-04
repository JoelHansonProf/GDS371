using System;
using _2D_MonoGame_Engine.Components;
using _2D_MonoGame_Engine.Objects.Base;
using _2D_MonoGame_Engine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_MonoGame_Engine.Objects;

public class TestCollision : GameObject
{
   public TestCollision()
   {
       var random = new Random();
       
       AddComponent<SpriteRenderer>(Globals.ContentManager.Load<Texture2D>("tyranitar"),transform);
       AddComponent<BoxCollider>(false, new Rectangle(transform.Position.ToPoint(), GetComponent<SpriteRenderer>().size.ToPoint()));
       transform.Position = new Vector2(random.Next(0,2000), random.Next(0, 2000));
       GetComponent<BoxCollider>().debugDraw = true;
   }
}