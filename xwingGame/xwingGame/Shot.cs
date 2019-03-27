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
        Rectangle boundingBox;

        public Shot(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
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

        public Vector2 Position
        {
            set { position = value; }
            get { return position; }
        }
    }
}
