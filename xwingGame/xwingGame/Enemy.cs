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

        public Enemy(Texture2D newTexture, Vector2 newPosition, int newSpeed)
        {
            position = newPosition;
            texture = newTexture;
            speed = newSpeed; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void MoveX()
        {
            position.X += this.speed;
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
