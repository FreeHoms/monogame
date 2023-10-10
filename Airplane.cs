using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Airplane
    {
            public Vector2 position;
            private Rectangle rectangle;
            int v;
            public Airplane(int x, int y, int w, int h)
            {
                position.X = x;
                position.Y = y;
                rectangle = new Rectangle(x, y, w, h);
            }
             public void Position(int x, int y)
              {
                position.X = x;
                position.Y = y;
              }

            public void Move(float dx, float dy)
            {
                position.X += dx;
                position.Y += dy;
            }
            public void Draw(SpriteBatch spriteBatch, Texture2D texture)
            {
                spriteBatch.Draw(texture, position, Color.Silver);
            }

            public void Speed(int speed)
            {
                v = 8 + speed; 
            }
            public Rectangle GetRectangle()
            {
                rectangle.X = (int)position.X;
                rectangle.Y = (int)position.Y;
                return rectangle;
            }
            internal void MoveLeft() => Move(-v, 0);
            internal void MoveRight() => Move(v, 0);
        }
    }




