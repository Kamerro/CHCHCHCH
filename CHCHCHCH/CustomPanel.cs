using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Windows.Forms;

namespace CHCHCHCH
{
    internal class CustomPanel:Primitive2D
    {
        private bool isVisible;
        private Rectangle rect;
        public bool isPanelEnabled = false;
        public List<Primitive2D> controls = new List<Primitive2D>();

        public CustomButton button = new CustomButton();
        public CustomButton buttonChangeSpeed = new CustomButton();
        public CustomButton buttonChangeGravity = new CustomButton();
        public CustomPanel(List<Texture2D> listOfTextures)
        {
            isVisible = false;
            controls.Add(this);
            button.SetPosition(new Vector2(625, 25));
            button.SetPicture(listOfTextures[0]);
            buttonChangeSpeed.SetPosition(new Vector2(650, 50));
            buttonChangeSpeed.SetPicture(listOfTextures[1]);
            buttonChangeGravity.SetPosition(new Vector2(650, 100));
            buttonChangeGravity.SetPicture(listOfTextures[2]);
            controls.Add(button);
            controls.Add(buttonChangeSpeed);
            controls.Add(buttonChangeGravity);

        }

        internal void Update()
        {
            if (isVisible)
            {
                MouseState mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();

                if (rect.Intersects(new Rectangle(mouse.X - 5, mouse.Y - 5, 10, 10)) && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    WorldConfiguration.MainCharSpeed = 99;

                }
            }
        }
        public override void SetPicture(Texture2D texture2D)
        {
            base.SetPicture(texture2D);
            rect = new Rectangle((int)base.GetPosition().X, (int)base.GetPosition().Y, (int)base.GetTexture().Width, (int)base.GetTexture().Height);
        }

        public void SetVisibility(bool state)
        {
            isVisible = state;
        }

       
    }
}