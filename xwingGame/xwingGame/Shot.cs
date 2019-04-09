using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwingGame
{
    class Shot
    {
        Vector2 position;
        Texture2D texture;
        Rectangle boundingBox; //Used for hit detection
        int speed = 3;

        public Shot(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public int Speed
        {
            set { speed = value; }
            get { return speed; }
        }

        public Rectangle BoundingBox
        {
            set { boundingBox = value; }
            get { return boundingBox; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        //direction 1: up, direction -1: down
        public void Move(int direction)
        {
            position.Y -= speed*direction;
            boundingBox.Location = position.ToPoint();
        }

        public Vector2 Position
        {
            set { position = value; }
            get { return position; }
        }
    }
}
