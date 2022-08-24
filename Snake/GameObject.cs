using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class GameObject
    {
        protected Color color = Color.Black;
        public Color getColor => color;

        public GameObject()
        {

        }

        public void Draw(Graphics graphics, Font font, Vector position, float gsize, int margin)
        {
            graphics.FillRectangle(new SolidBrush(color), position.X * gsize +margin, position.Y * gsize +margin, gsize-2*margin, gsize-2*margin);
        }

        public override string ToString()
        {
            return $"{this.GetType()} [{color}]";
        }
    }

    class Snake : GameObject
    {
        public Snake(Player setPlayer) : base()
        {
            player = setPlayer;
            color = player.getColor;
        }

        Player player;
        public Player getPlayer => player;

        public override string ToString()
        {
            return $"Player ({player.getColor}) snake";
        }
    }

    class Consumable : GameObject
    {
        public Consumable() : base()
        {
            color = Color.Black;
        }

        protected int valuePoints = 0;
        protected int valueGrowth = 0;
        public int ValuePoints => valuePoints;
        public int ValueGrowth => valueGrowth;

        public static Consumable GenerateConsumable(Random r)
        {
            switch (r.Next(2))
            {
                default:
                case 0:
                    return new ConsumableFoodNormal();
                    break;

                case 1:
                    return new ConsumableFoodBig();
                    break;

                case 2:
                    return new ConsumableFoodSmall();
                    break;
            }
        }
    }


    class ConsumableFoodNormal : Consumable
    {
        public ConsumableFoodNormal() : base() 
        { 
            color = Color.Yellow;
            valuePoints = 1;
            valueGrowth = 1;
        }
    }

    class ConsumableFoodBig : Consumable
    {
        public ConsumableFoodBig() : base() 
        { 
            color = Color.SandyBrown;
            valuePoints = 5;
            valueGrowth = 2;
        }
    }

    class ConsumableFoodSmall : Consumable
    {
        public ConsumableFoodSmall() : base() 
        {
            color = Color.Blue;
            valuePoints = 1;
            valueGrowth = -1;
        }
    }
}
