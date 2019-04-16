using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace xwingGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D xwingTexture;
        Texture2D tiefighterTexture;
        Texture2D shotTexture;
        Texture2D enemyShotTexture;
        Player xwing;
        int playerScore = 0;
        KeyboardState kstate = new KeyboardState();
        List<Shot> shots = new List<Shot>();
        List<Shot> enemyShots = new List<Shot>();
        List<Enemy> tieFighterList;
        SpriteFont gameFont;
        SoundEffect xWingFireSound;
        Song battleMusic;
        Random rand;
        
        public Game1()
        {
            rand = new Random();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            xwingTexture = Content.Load<Texture2D>("xwing");
            xwing = new Player(xwingTexture, new Vector2(
                                                    Window.ClientBounds.Width/2-xwingTexture.Width, 
                                                    Window.ClientBounds.Height-xwingTexture.Height), 3);

            shotTexture = Content.Load<Texture2D>("shot");
            enemyShotTexture = Content.Load<Texture2D>("enemyShot");

            tiefighterTexture = Content.Load<Texture2D>("tiefighter");
            tieFighterList = new List<Enemy>();
            
            //Add some enemies (TIE Fighters)
            for (int i=0;i<10;i++)
            {
                tieFighterList.Add(new Enemy(tiefighterTexture, new Vector2(70*i+10, 50), 3));
                tieFighterList[i].FireRate = rand.Next(500, 2000); //Set random fire rate for each enemy
            }

            gameFont = Content.Load <SpriteFont> ("gameFont");
            xWingFireSound = Content.Load<SoundEffect>("XWingFire");
            battleMusic = Content.Load<Song>("BattleMusic");
            MediaPlayer.Play(battleMusic);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left)) {
                xwing.MoveX(-1);
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                xwing.MoveX(1);
            }

            //If space is pressed, fire a shot through each laser cannon if enough time has elapsed since last shot
            //if (kstate.IsKeyDown(Keys.Space) && System.Math.Abs(xwingLastShot-gameTime.TotalGameTime.Milliseconds)>xwing.FireRate)
            if (kstate.IsKeyDown(Keys.Space) && System.Math.Abs(xwing.LastShot - gameTime.TotalGameTime.Milliseconds) > xwing.FireRate)
            {
                xwing.LastShot = gameTime.TotalGameTime.Milliseconds;
                shots.Add(new Shot(shotTexture, new Vector2(xwing.Position.X + 5, xwing.Position.Y + 8)));
                shots.Add(new Shot(shotTexture, new Vector2(xwing.Position.X + xwing.Texture.Bounds.Width - 12,
                                                                xwing.Position.Y + 8)));
                xWingFireSound.Play();
            }

            foreach (Enemy e in tieFighterList)
            {
                if (System.Math.Abs(e.LastShot - gameTime.TotalGameTime.Milliseconds) > e.FireRate)
                {
                    e.LastShot = gameTime.TotalGameTime.Milliseconds;
                    enemyShots.Add(new Shot(enemyShotTexture, new Vector2(e.Position.X, e.Position.Y)));
                }
            }

            //Check if the enemy is moving outside the bounds of the game windows
            foreach (Enemy e in tieFighterList)
            {
                if (e.Position.X <= 0 || e.Position.X >= Window.ClientBounds.Width - e.Texture.Width)
                {
                    e.Speed *= -1; //Change direction
                }
                e.MoveX();
            }
            
            //Loop through all player shots, to move them upwards and check if a shot hits an enemy
            foreach (Shot s in shots)
            {
                bool hit = false; //Check if there is a hit, to end the loop when shot is removed
                s.Move(1);
                foreach (Enemy e in tieFighterList)
                {
                    //If a shot hits a TIE fighter
                    if (s.BoundingBox.Intersects(e.BoundingBox))
                    {
                        hit = true;
                        playerScore++; //Increase score if player hits enemy
                        tieFighterList.Remove(e);
                        shots.Remove(s);
                        break;
                    }
                }
                if (hit)
                    break;
            }

            //Loop through all enemy shots, to move them downwards and check if a shot hits the player
            foreach(Shot s in enemyShots)
            {
                s.Move(-1);
                if (s.BoundingBox.Intersects(xwing.BoundingBox))
                {
                    playerScore -= 1; //Decrease score if player is hit
                    enemyShots.Remove(s);
                    break;
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            xwing.Draw(spriteBatch);

            //Draw all TIE fighters
            foreach (Enemy e in tieFighterList)
            {
                e.Draw(spriteBatch);
            } 

            foreach(Shot s in shots)
            {
                s.Draw(spriteBatch);
            }

            foreach(Shot s in enemyShots)
            {
                s.Draw(spriteBatch);
            }

            spriteBatch.DrawString(gameFont, "Score: "+playerScore, new Vector2(0, 0), Color.White);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
