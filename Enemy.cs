using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Enemy
    {

            public Rectangle Rectangle;
            private Vector2 position;
            public Color Color { get; set; }
            public bool IsAlive { get; set; } = false;
            public bool Remove { get; set; } = false;

            public Enemy(int x, int y, int w, int h, Color color)
            {
                position.X = x;
                position.Y = y;
                Rectangle = new Rectangle(x, y, w, h);
                Color = color;
            }
            

            public void Move(int vx, int vy)
            {
                Rectangle = new Rectangle(Rectangle.X + vx, Rectangle.Y + vy, Rectangle.Width, Rectangle.Height);
            }
            
            internal void MoveDown() => Move(0, 1);


            internal static Enemy CreateEnemy(int x, int y, int w, int h)
            {
                var block = new Enemy(x, y, w, h, Color.OldLace);
                block.IsAlive = true;
                return block;
            }
            
           
            public void Draw(SpriteBatch spriteBatch, Texture2D texture)
            {
                if(IsAlive)spriteBatch.Draw(texture, Rectangle, Color);
            }

            }
    }

