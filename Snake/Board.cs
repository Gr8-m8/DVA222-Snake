using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Board
    {
        public Board(Vector setsize)
        {
            size = setsize;
            tiles = new Tile[size.X, size.Y];
            for (int y = 0; y < getSizeX; y++)
            {
                for (int x = 0; x < getSizeY; x++)
                {
                    tiles[x, y] = new Tile();
                }
            }
        }

        Vector size;
        public int getSizeX => size.X;
        public int getSizeY => size.Y;

        Tile[,] tiles;
        public Tile getTile(Vector position)
        {
            if (position.X < 0 || position.X > getSizeX-1) return null;
            if (position.Y < 0 || position.Y > getSizeY-1) return null;
            return tiles[position.X, position.Y];
        }

        public Tile getRandomEmptyTile(Random r)
        {
            Vector position;
            do
            {
                position = new Vector(r.Next(getSizeX-1), r.Next(getSizeY-1));
            } while (tiles[position.X, position.Y].Occupied());

            return tiles[position.X, position.Y];
        }

        public void SpawnConsumable(Random r)
        {
            Consumable.GenerateConsumable(r, getRandomEmptyTile(r));
        }

        public void SpawnConsumableCombo(Random r, ConsumableCombo cc)
        {
            Consumable.GenerateConsumable(r, getRandomEmptyTile(r), cc);
        }

        public void Draw(Graphics graphics, Font font, Vector position, float gsize)
        {
            for (int y = 0; y < getSizeX; y++)
            {
                for (int x = 0; x < getSizeY; x++)
                {
                    tiles[x, y].Draw(graphics, font, new Vector(x, y), gsize/Math.Min(getSizeX+1, getSizeY+1));
                }
            }
        }
    }

    class Tile
    {
        public Tile()
        {
            occupant = null;
        }
        GameObject occupant;
        public GameObject getOccupant => occupant;

        public bool Occupied() { return occupant != null; }
        public bool Occupied(out GameObject getOccupant) { getOccupant = occupant; return occupant != null; }

        public void Clear() { occupant = null; }

        public void Occupie(GameObject go)
        {
            occupant = go;
        }

        public void Draw(Graphics graphics, Font font, Vector position, float gsize)
        {
            graphics.DrawRectangle(new Pen(Color.Black), position.X*gsize, position.Y*gsize, gsize, gsize);

            int margin = 5;
            if(Occupied()) occupant.Draw(graphics, font, new Vector(position.X, position.Y), gsize, margin);
            
        }
    }

    class Vector
    {
        public Vector(int setX = 0, int setY = 0)
        {
            Set(setX, setY);
        }
        public int X => x;
        public int Y => y;
        int x;
        int y;

        public int[] Get()
        {
            return new int[] {x, y};
        }

        public void Set(int setX, int setY)
        {
            x = setX;
            y = setY;
        }

        public override string ToString()
        {
            return $"<{x},{y}>";
        }
    }
}
