using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwingGame
{
    class Enemy
    {
        Vector2 position;
        Texture2D texture;
        int speed; //Positive speed: Move right, negative speed: Move left
        Rectangle boundingBox;
        int fireRate = 300; //Controls the time between shots
        int lastShot = 0; //Last time the player fired a shot


        public Enemy(Texture2D newTexture, Vector2 newPosition, int newSpeed)
        {
            position = newPosition;
            texture = newTexture;
            speed = newSpeed;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        }

        public Rectangle BoundingBox
        {
            set { boundingBox = value; }
            get { return boundingBox; }
        }

        public int FireRate
        {
            set { fireRate = value; }
            get { return fireRate; }
        }

        public int LastShot
        {
            set { lastShot = value; }
            get { return lastShot; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void MoveX()
        {
            position.X += this.speed;
            boundingBox.Location = position.ToPoint();
        }

        public int Speed
        {
            set { speed = value; }
            get { return speed; }
        }

        public Vector2 Position
        {
            set { position = value; }
            get { return position; }
        }

        public Texture2D Texture
        {
            set { texture = value; }
            get { return texture; }
        }
    }
}
