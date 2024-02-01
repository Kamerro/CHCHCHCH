using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace CHCHCHCH
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        List<Base2D> baseList_Base2D = new List<Base2D>();
        List<Primitive2D> baseList_Primitive2D = new List<Primitive2D>();
        MainCharacter _MainChar = new MainCharacter();
        Obstacle obst1;
        private Obstacle obst3;
        internal Obstacle obst4 { get; private set; }
        private Obstacle obst2;
        private Obstacle obst5;
        private Texture2D world;
        private List<Collectible> coins = new List<Collectible>();
        private Collectible coin;

        GameServiceUpdater gameserviceMP = new GameServiceUpdater();
        CustomPanel panel;

        List<CustomButton> listOfButtons = new List<CustomButton>();
        //End of defining objects that are neccesary. Maybe there is possibility to define them in other place.
        //Below constructor AND neccesary methods with initializations.

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            List<Texture2D> listToPanel = new List<Texture2D>();
            
            obst1 = new Obstacle(Content.Load<Texture2D>("Log"));
            obst2 = new Obstacle(Content.Load<Texture2D>("Obstacle_vertical"));
            obst3 = new Obstacle(Content.Load<Texture2D>("Log"));
            obst4 = new Obstacle(Content.Load<Texture2D>("Log"));
            obst5 = new Obstacle(Content.Load<Texture2D>("Log"));

            listToPanel.Add(Content.Load<Texture2D>("DebugButton"));
            listToPanel.Add(Content.Load<Texture2D>("buttOn"));
            listToPanel.Add(Content.Load<Texture2D>("ChangeGrav"));

            panel = new CustomPanel(listToPanel);
            panel.SetPosition(new Vector2(625, 25));
            panel.SetPicture(Content.Load<Texture2D>("Panel"));
            panel.buttonChangeSpeed.click += changeSpeed;
            panel.button.click += enablePanel;
            panel.button.isVisible = true;
            listOfButtons.Add(panel.controls[1] as CustomButton);
            listOfButtons.Add(panel.controls[2] as CustomButton);
            world = Content.Load<Texture2D>("Forest");
            _MainChar.SetPicture(Content.Load<Texture2D>("Chipek"));
            _MainChar.SetPosition(new Vector2 (100, 100));
            obst1.SetPosition(new Vector2(100, 400));
            obst2.SetPosition(new Vector2(385, 220));
            obst3.SetPosition(new Vector2(300, 400));
            obst4.SetPosition(new Vector2(418, 400));
            obst5.SetPosition(new Vector2(500, 300));
            base.Initialize();
        }

        private void enablePanel()
        {
            WorldConfiguration.IsPanelEnabled = true;
            panel.button.isVisible = false;
            panel.buttonChangeSpeed.isVisible = true;
            panel.buttonChangeGravity.isVisible = true;
        }

        private void changeSpeed()
        {
            WorldConfiguration.MainCharSpeed = 1;
        }

        protected override void LoadContent()
        {
            coin = new Collectible();
            coin.SetPicture(Content.Load<Texture2D>("coin"));
            coin.SetPosition(new Vector2(100, 350));


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            baseList_Base2D.Add(_MainChar);

            baseList_Primitive2D.Add(obst1);
            baseList_Primitive2D.Add(obst3);
            baseList_Primitive2D.Add(obst4);
            baseList_Primitive2D.Add(obst5);

            coins.Add(coin);
            GameServiceCollisionChecker.PopulateService(baseList_Base2D,baseList_Primitive2D,coins);
            GameServiceCollisionChecker.Start();
        }

        //Update that is made every tick.
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

                foreach (Base2D base2 in baseList_Base2D)
                    base2.CalculatePosition();

            base.Update(gameTime);
        }
        bool debug = true;
        bool isPanelOpen = false;
        //Draw is after update.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _spriteBatch.Draw(world, new Vector2(0,0), Color.White);
            GameServiceCollisionChecker.PopulateRectanglesWithCollisionBorders();
            foreach(Base2D obj in baseList_Base2D)
                _spriteBatch.Draw(obj.GetTexture(), obj.GetPosition(), Color.White);

            foreach (Primitive2D obj in baseList_Primitive2D)
                _spriteBatch.Draw(obj.GetTexture(), obj.GetPosition(), Color.White);

            foreach (Primitive2D obj in coins)
                _spriteBatch.Draw(obj.GetTexture(), obj.GetPosition(), Color.White);
            

            gameserviceMP.GameServiceUpdateMousePosition(listOfButtons);

            if (debug && !WorldConfiguration.IsPanelEnabled)
            {
                _ = panel.button.isVisible;
                _spriteBatch.Draw(panel.button.GetTexture(), panel.button.GetPosition(), Color.White);
            }
            else if (debug)
            {
                foreach (var control in panel.controls)
                    _spriteBatch.Draw(control.GetTexture(), control.GetPosition(), Color.White);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}