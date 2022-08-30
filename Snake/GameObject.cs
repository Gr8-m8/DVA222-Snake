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

        public virtual void Draw(Graphics graphics, Font font, Vector position, float gsize, int margin)
        {
            graphics.FillRectangle(new SolidBrush(color), position.X * gsize +margin, position.Y * gsize +margin, gsize-2*margin, gsize-2*margin);
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

        public static Consumable GenerateConsumable(Random r, Tile setTile, ConsumableCombo cc = null)
        {
            if (cc != null)
            {
                return new ConsumableCombo(setTile, cc);
            }

            switch (r.Next(4))
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

                case 3:
                    return new ConsumableCombo(setTile);
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

    //Extension
    class ConsumableCombo : Consumable
    {
        Player comboPlayer;
        public ConsumableCombo(Tile setTile, ConsumableCombo cc = null) : base(setTile)
        {
            color = Color.Violet;
            valueGrowth = 1;
            if (cc == null)
            {
                valuePoints = 1;
            } else
            {
                comboPlayer = cc.comboPlayer;
                valuePoints = cc.valuePoints * 2;
            }
        }

        public void setPlayer(Player p) { comboPlayer = p; }
        public Player getPlayer => comboPlayer;
    }
}
