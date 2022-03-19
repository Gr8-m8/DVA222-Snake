using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class GameObject
    {
        int[] Position;
        public int[] getPosition => Position;

        public GameObject(int[] setPosition)
        {
            Position = setPosition;
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }
    }
}
