using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Transactions;

namespace CHCHCHCH
{
    internal class GameServiceCollisionChecker
    {
        private static bool started = false;
        private static List<Primitive2D> objects = new List<Primitive2D>();
        private static List<Rectangle> objects_rects = new List<Rectangle>();
        internal static bool CheckCollisions()
        {
            if (started)
            {
                for(int i = 1; i < objects_rects.Count; i++)
                {
                    if (objects_rects[0].Intersects(objects_rects[i]))
                    {
                        objects[0].contactWith = objects[i].guid;
                        Debug.WriteLine(objects[0].contactWith);

                        return true;
                    }
                }
            }
            return false;
        }

        internal static void PopulateService(System.Collections.Generic.List<Base2D> base2D, System.Collections.Generic.List<Primitive2D> Primitive2D, List<Collectible> coins)
        {
           
            foreach(var obj in base2D)
            {
                objects.Add(obj);
            }
            foreach(var obj in Primitive2D)
            {
                objects.Add(obj);
            }
            foreach (var obj in coins)
            {
                objects.Add(obj);
            }

            //If there is only one object dont calculate any boundries.
            if (objects.Count>1)
            PopulateRectanglesWithCollisionBorders();
            

        }

        public static void PopulateRectanglesWithCollisionBorders()
        {
            objects_rects.Clear();
            int i = 0;
            foreach (var obj in objects)
            {
                if (i == 0)
                {
                    objects_rects.Add(new Rectangle((int)objects[i].GetPosition().X+10, (int)objects[i].GetPosition().Y,
                                               (int)objects[i].GetTexture().Width-5, (int)objects[i].GetTexture().Height));
                }
                else
                objects_rects.Add(new Rectangle((int)objects[i].GetPosition().X, (int)objects[i].GetPosition().Y,
                                                 (int)objects[i].GetTexture().Width, (int)objects[i].GetTexture().Height));
                i++;
            }
        }

        internal static void Start()
        {
            started = true;
        }

        internal static bool CheckCollisions(object v)
        {
            int gravity = (int)v;
            Rectangle gracz = objects_rects[0];
            gracz.Y += gravity;
            _ = objects[0];
            for (int i = 1; i < objects_rects.Count; i++)
            {
                if (gracz.Intersects(objects_rects[i]))
                {
                    objects[0].contactWith = objects[i].guid;
                    if (objects[i] is Collectible)
                    {
                        objects[i].SetPosition(new Vector2(-100, -100));
                        Task.Factory.StartNew(() =>
                        {
                            using (System.Media.SoundPlayer player = new System.Media.SoundPlayer())
                            {
                                player.SoundLocation = @"C:\Users\kamil\OneDrive\Pulpit\Coin.wav";
                                player.Play();

                            }
                        });



                    }
                    return true;
                }
            }
            return false;
        }


        internal static bool CheckCollisionsX(int speed)
        {
           
            Rectangle gracz = objects_rects[0];
            gracz.X += speed;
            for (int i = 1; i < objects_rects.Count; i++)
            {
                if (gracz.Intersects(objects_rects[i]))
                {
                    objects[0].contactWith = objects[i].guid;
                    if (objects[i] is Collectible)
                    {
                        objects[i].SetPosition(new Vector2(-100, -100));
                        Task.Factory.StartNew(() =>
                        {
                            using (System.Media.SoundPlayer player = new System.Media.SoundPlayer()) {
                               
                                player.SoundLocation = @"C:\Users\kamil\OneDrive\Pulpit\Coin.wav";
                                player.Play();

                            }
                        });


                    }
                    return true;
                }
            }
            return false;
        }
    }
}