using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.AccessControl;

namespace CHCHCHCH
{
    internal class MainCharacter:Base2D
    {
        DateTime dt = DateTime.Now;
        int jumppower=0;
        private int time = 0;
        private int MaxJumpValue;
        public MainCharacter()
        {
            MaxJumpValue = 12;
        }
        public override void CalculatePosition()
        {

            //IF THERE IS ANY COLLISION WITH OTHER BLOCKS -> CHECK WITH SERVICE
           //WHY TF IT HURTS SO MUCH WTF
            KeyboardState kbstate = Keyboard.GetState();


            if (kbstate.IsKeyDown(Keys.Right))
            {
                for (int i = 0; i < WorldConfiguration.MainCharSpeed; i++)
                    if (!GameServiceCollisionChecker.CheckCollisionsX(1))
                    {
                        MakeMoveX(1);
                    }
            }
            else if (kbstate.IsKeyDown(Keys.Left))
            {
                for (int i = 0; i < WorldConfiguration.MainCharSpeed; i++)
                    if (!GameServiceCollisionChecker.CheckCollisionsX(-1))
                    {
                        MakeMoveX(-1);
                    }
            }
            if (jumppower < 0)
            {
                for (int i = 0; i > jumppower; i--)
                {
                    if (!GameServiceCollisionChecker.CheckCollisions(-1))
                    {
                        MakeMoveY(-1);
                    }
                    else { jumppower = -1;break; }

                    
                }
                jumppower += 1;
            }
            if (kbstate.IsKeyDown(Keys.Up))
            {
                Jump();
              
            }
            CalculateGravityForce();


        }

        private void MakeMoveY(int v)
        {
            Position.Y += v;
            GameServiceCollisionChecker.PopulateRectanglesWithCollisionBorders();
        }

        private void MakeMoveX(int v)
        {
            Position.X += v;
            GameServiceCollisionChecker.PopulateRectanglesWithCollisionBorders();
        }

        private void CalculateGravityForce()
        {
            bool marker = false;
            for (int i = 0; i < WorldConfiguration.GravityForce; i++)
            {
                if (!GameServiceCollisionChecker.CheckCollisions(1))
                {
                    MakeMoveY(1);
                }
                else
                {
                    time = 0;
                    marker = true;
                }
            }
            if(!marker)
            time++;
        }

        private void Jump()
        {
           TimeSpan tsp = DateTime.Now - dt;
            if (tsp.Milliseconds > 499 && jumppower == 0)
            {
                jumppower = -MaxJumpValue;
                dt = DateTime.Now;
            }

        }
    }
}