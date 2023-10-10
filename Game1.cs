using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D pixelTexture;
        Texture2D airplaneTexture;
        Texture2D enemyTexture;
        KeyboardState oldKeyState = Keyboard.GetState();
        List<Bullet> bullets = new List<Bullet>();
        Airplane airplane;
        List<Enemy> enemies = new List<Enemy>();
        Random rnd = new Random();
        double lastShot = 0;
        double lastEnemyShot = 0;

        int speed = 1;
        int lives = 3;
        int enemiesAmount;
        int score;
        int level = 1;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            enemies.Add(Enemy.CreateEnemy(rnd.Next(10,100), rnd.Next(200), enemyTexture.Width, enemyTexture.Height));
            enemies.Add(Enemy.CreateEnemy(rnd.Next(120,300), rnd.Next(200), enemyTexture.Width, enemyTexture.Height));
            enemies.Add(Enemy.CreateEnemy(rnd.Next(320,500), rnd.Next(200), enemyTexture.Width, enemyTexture.Height));
            enemies.Add(Enemy.CreateEnemy(rnd.Next(520,690), rnd.Next(200), enemyTexture.Width, enemyTexture.Height));

            airplane = new Airplane(350, 650, airplaneTexture.Width, airplaneTexture.Height);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pixelTexture = Content.Load<Texture2D>("pixel");
            airplaneTexture = Content.Load<Texture2D>("Airplane");
            enemyTexture = Content.Load<Texture2D>("Enemy");
        }

        protected override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            enemiesAmount = enemies.Count();

            if (keyState.IsKeyDown(Keys.Left)) airplane.MoveLeft();
            if (keyState.IsKeyDown(Keys.Right)) airplane.MoveRight();
            if (airplane.position.X < 0) airplane.position.X = 0;
            if (airplane.position.X > 700) airplane.position.X = 700;

            airplane.Speed(speed);


            if (lastEnemyShot < gameTime.TotalGameTime.TotalMilliseconds - 2500)
            {
                lastEnemyShot = gameTime.TotalGameTime.TotalMilliseconds;
                foreach (var enemy in enemies)
                {
                    bullets.Add(new Bullet((enemy.Rectangle.X + 35), enemy.Rectangle.Y + 80, 20, 20, "enemy"));
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //Ett skott varje sekund
                if (lastShot < gameTime.TotalGameTime.TotalMilliseconds - 200)
                {
                    lastShot = gameTime.TotalGameTime.TotalMilliseconds;
                    bullets.Add(new Bullet((airplane.position.X + 45), airplane.position.Y - 50, 20, 20, "off"));
                }


            }

            foreach (var enemy in enemies)
            {
                enemy.MoveDown();
                if (airplane.GetRectangle().Intersects(enemy.Rectangle) || enemy.Rectangle.Y > 800)
                {
                    lives--;
                    enemiesAmount--;
                    enemy.IsAlive = false;
                }
                 
                
            }

            foreach(var bullet in bullets)
            {
                foreach(var enemy in enemies)
                {
                    if (bullet.Rectangle.Intersects(enemy.Rectangle))
                    {
                        bullet.IsAlive = false;
                        enemy.IsAlive = false;
                        enemiesAmount--;
                        score++;
                    }
                }

                if (bullet.Rectangle.Intersects(airplane.GetRectangle()))
                {
                    bullet.IsAlive = false;
                    lives--;
                }

                bullet.Speed(speed);
                
                bullet.Update();
            }

            bullets.RemoveAll(bullet => !bullet.IsVisible());
            bullets.RemoveAll(bullet => !bullet.IsAlive);
            enemies.RemoveAll(block => !block.IsAlive);

            if(enemiesAmount == 0)
            {
                //bullets.Clear(); //optional
                enemies.Add(Enemy.CreateEnemy(rnd.Next(10, 100), rnd.Next(200), enemyTexture.Width, enemyTexture.Height));
                enemies.Add(Enemy.CreateEnemy(rnd.Next(120, 300), rnd.Next(200), enemyTexture.Width, enemyTexture.Height));
                enemies.Add(Enemy.CreateEnemy(rnd.Next(320, 500), rnd.Next(200), enemyTexture.Width, enemyTexture.Height));
                enemies.Add(Enemy.CreateEnemy(rnd.Next(520, 690), rnd.Next(200), enemyTexture.Width, enemyTexture.Height));
                enemiesAmount = 4;
                speed++;
                level++;
            }

            // Starts again
            if (lives == 0)
            {
                enemies.Clear();
                bullets.Clear();
                lives = 3;
                score = 0;
                speed = 0;
                level = 0;
                airplane.position.X = 350;
            }


            oldKeyState = Keyboard.GetState();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (var bullet in bullets)
            {
                bullet.Draw(_spriteBatch,pixelTexture);
            }
            enemies.ForEach(block => block.Draw(_spriteBatch, enemyTexture));
            airplane.Draw(_spriteBatch, airplaneTexture);
            _spriteBatch.End();

            Window.Title = "Lives: " + lives +"/3 Score: " + score + " Level: " + level;

            base.Draw(gameTime);
        }
    }
}
