using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class GameObject
    {
        public GameObject(Tile setTile)
        {
            tile = setTile;
            tile.Occupie(this);
        }
        protected Color color = Color.Black;
        public Color getColor => color;

        Tile tile = null;
        public void Clear() { tile.Clear(); }

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
        public Snake(Player setPlayer, Tile setTile) : base(setTile)
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
        public Consumable(Tile setTile) : base(setTile)
        {
            color = Color.Black;
        }

        protected int valuePoints = 0;
        protected int valueGrowth = 0;
        public int ValuePoints => valuePoints;
        public int ValueGrowth => valueGrowth;

        public static Consumable GenerateConsumable(Random r, Tile setTile)
        {
            switch (r.Next(2))
            {
                default:
                case 0:
                    return new ConsumableFoodNormal(setTile);
                    break;

                case 1:
                    return new ConsumableFoodBig(setTile);
                    break;

                case 2:
                    return new ConsumableFoodSmall(setTile);
                    break;
            }
        }
    }


    class ConsumableFoodNormal : Consumable
    {
        public ConsumableFoodNormal(Tile setTile) : base(setTile) 
        { 
            color = Color.Yellow;
            valuePoints = 1;
            valueGrowth = 1;
        }
    }

    class ConsumableFoodBig : Consumable
    {
        public ConsumableFoodBig(Tile setTile) : base(setTile)
        { 
            color = Color.SandyBrown;
            valuePoints = 5;
            valueGrowth = 2;
        }
    }

    class ConsumableFoodSmall : Consumable
    {
        public ConsumableFoodSmall(Tile setTile) : base(setTile)
        {
            color = Color.Blue;
            valuePoints = 1;
            valueGrowth = -1;
        }
    }
}
