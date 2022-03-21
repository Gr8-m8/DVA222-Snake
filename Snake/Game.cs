using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Game
    {
        int TickTimer = 0;
        int Tick = 60;

        int[] board = { 30, 30 };

        List<GameObject> GameObjects = new List<GameObject>();
        public Game()
        {

        }

        void Update()
        {

            //TICKED UPDATE
            if (++TickTimer%Tick != 0) return;

            foreach(GameObject go in GameObjects)
            {
                go.Update();
                foreach (GameObject other in GameObjects)
                {
                    if (other != go)
                    {
                        go.Collission(other);
                    }
                }
            }
        }

        void Draw(PaintEventArgs args)
        {
            foreach(GameObject go in GameObjects)
            {
                go.Draw(args.Graphics);
            }

        }
    }
}
