using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class GameObject
    {
        int[] Position = new int[2] {0, 0};
        public int[] getPosition => Position;

        protected Pen pen = new Pen(Color.Black);

        public GameObject(int[] setPosition)
        {
            Position = setPosition;
            pen.Color = Color.Black;
        }

        public virtual void Update()
        {

        }

        public void Move(int[] newPosition)
        {
            Position[0] = newPosition[0];
            Position[1] = newPosition[1];
        }

        public void Collission(GameObject other)
        {
            if (other.getPosition[0] == this.getPosition[0] &&
                other.getPosition[1] == this.getPosition[1])
            {
                CollisionExe(other);
            }
        }

        protected virtual void CollisionExe(GameObject other)
        {

        }

        public virtual void Draw(Graphics graphics)
        {
            int width = 10;
            graphics.DrawRectangle(pen, Position[0], Position[1], width, width);
        }
    }

    class Consumable : GameObject
    {
        Random r;
        int[,] Values = { {-1, 1}, { 1, 1 }, { 2, 5 } };
        int ValuesCurrent = 1;
        
        public Consumable(int[] setPosition) : base(setPosition)
        {
            GenerateValues();

            pen.Color = Color.Yellow;
        }

        void GenerateValues()
        {
            ValuesCurrent = r.Next(Values.Length);
        }

        protected override void CollisionExe(GameObject other)
        {
            if (other.GetType() == typeof(Snake))
            {
                Snake otherSnake = (Snake)other;
                otherSnake.getPlayer.Grow(Values[ValuesCurrent, 0]);
                otherSnake.getPlayer.AppendPoints(Values[ValuesCurrent, 1]);
            }
        }
    }

    class Snake : GameObject
    {
        Player player;
        public Player getPlayer => player;
        Snake NextPart = null;
        public Snake getNextPart => NextPart;
        public Snake(int[] setPosition, Player setPlayer, Snake setnextPart) : base(setPosition)
        {
            player = setPlayer;
            pen.Color = player.getcolor;
            SetNextPart(setnextPart);
        }

        public void SetNextPart(Snake setnextPart)
        {
            NextPart = setnextPart;
        }

        protected override void CollisionExe(GameObject other)
        {
            if (other.GetType() != typeof(Consumable))
            {

            }
        }
    }
}
