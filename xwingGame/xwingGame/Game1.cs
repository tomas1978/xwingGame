using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Texture2D xwing;
        Vector2 xwingPos = new Vector2(100, 300);
        KeyboardState kstate = new KeyboardState();
        Texture2D shotTexture;
        List<Shot> shots = new List<Shot>();

        public Game1()
        {
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
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here
            xwing = Content.Load<Texture2D>("xwing");
            shotTexture = Content.Load<Texture2D>("shot");

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
                xwingPos.X--;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                xwingPos.X++;
            }
            if (kstate.IsKeyDown(Keys.Up))
            {
                xwingPos.Y--;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                xwingPos.Y++;
            }
            if (kstate.IsKeyDown(Keys.Space))
            {
                shots.Add(new Shot(shotTexture, new Vector2(xwingPos.X,xwingPos.Y)));
            }

            foreach(Shot s in shots)
            {
                s.Position = new Vector2(s.Position.X, s.Position.Y - 3);
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
            spriteBatch.Draw(xwing, xwingPos, Color.White);
            foreach(Shot s in shots)
            {
                s.Draw(spriteBatch);
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
