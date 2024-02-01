using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CHCHCHCH
{
    internal class CustomButton:Primitive2D
    {
        private Rectangle rect;
        public bool isPanelEnabled = false;
        public bool isVisible;
        internal void Update()
        {
            MouseState mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();

            if(rect.Intersects(new Rectangle(mouse.X - 5, mouse.Y - 5, 10, 10)) && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if(isVisible)
                click?.Invoke();
            }
        }
        public override void SetPicture(Texture2D texture2D)
        {
            base.SetPicture(texture2D);
            rect = new Rectangle((int)base.GetPosition().X , (int)base.GetPosition().Y, (int)base.GetTexture().Width, (int)base.GetTexture().Height);
        }
        public delegate void Click();
        public event Click click;
    }
}