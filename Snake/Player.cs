using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Player
    {
        Snake sHead;
        Snake sTail;
        int[] sDirection = new int[2] {0, 0};

        int points = 0;
        int growthqueue = 0;

        Color color = Color.Green;
        public Color getcolor => color;
        public Player(int[] startPosition)
        {
            sHead = sTail = new Snake(startPosition, this, null);
        }

        public void AppendPoints(int addpoints)
        {
            points += addpoints;
        }

        public void Grow(int addqueue)
        {
            growthqueue += addqueue;
        }

        void Move()
        {
            int[] movePosition = new int[] { sHead.getPosition[0] + sDirection[0], sHead.getPosition[0] + sDirection[0] };
            if (growthqueue > 0) {
                growthqueue--;
                Snake newPart = new Snake(movePosition, this, sHead);
                sHead = newPart;
            } 
            else if (growthqueue < 0 && sHead != sTail){
                growthqueue++;
                sHead.SetNextPart(sTail);
                sHead = sTail;
                sHead.Move(movePosition);
                sTail = sHead.getNextPart;
                sHead.SetNextPart(null);

                sTail = sTail = sTail.getNextPart;
            }
            else {
                sHead.SetNextPart(sTail);
                sHead = sTail;
                sHead.Move(movePosition);
                sTail = sHead.getNextPart;
                sHead.SetNextPart(null);
            }
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }
    }
}
