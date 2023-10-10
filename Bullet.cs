using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceShooter
{
    public class Bullet
    {
        public bool IsAlive { get; set; } = true;
        public Color Color { get; set; }
        public Rectangle Rectangle;
        int x;
        int y;
        int speed;
        string mode;
        public Bullet(float x, float y, int w, int h, string mode)
        {
            this.x = (int)x;
            this.y = (int)y;
            this.mode = mode;
            Rectangle = new Rectangle((int)x, (int)y, w, h);
        }

        public bool IsVisible() => y > 0;

        public void Speed(int v)
        {
           speed = v;
        }

        public void Update()
        {
            if(mode == "enemy") { y += (speed*2); Color = Color.Red; }
            else { y -= speed; Color = Color.Blue; }

         
           Rectangle = new Rectangle(x, y, 10, 10);
        }

       
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (IsAlive) spriteBatch.Draw(texture, Rectangle, Color);
        }
    }
}