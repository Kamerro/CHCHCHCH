using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CHCHCHCH
{
    internal class Primitive2D
    {
        protected Vector2 Position = Vector2.Zero;
        protected Texture2D Texture;
        public Guid contactWith;
        public Guid guid;
        public Rectangle rect;
        public Primitive2D()
        {
            this.guid = Guid.NewGuid();
        }
        public virtual Vector2 GetPosition()
        {
            return Position;
        }

        public virtual Texture2D GetTexture()
        {
            return Texture;
        }

        public virtual void SetPicture(Texture2D texture2D)
        {
            Texture = texture2D;
            rect = texture2D.Bounds;
        }

        public virtual void SetPosition(Vector2 vector2)
        {
            Position = vector2;
        }

    }
}