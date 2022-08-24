using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Snake
{
    internal class Player
    {
        public Player(Game setgame)
        {
            game = setgame;
            snake = new List<Snake>();
        }
        Game game;

        
        List<Snake> snake;

        Vector position;
        Vector direction;// = new Vector();
        public Vector getPosition => position;
        public void setPosition(Vector setPosition) { position = setPosition; }

        public Vector getDirection => direction;
        public void setDirection(Vector setDirection) { direction = setDirection; }

        int points = 0;
        int growth = 0;
        public int Points => points;

        Color color = Color.Black;
        public Color getColor => color;

        bool dead;
        public bool isDead => dead;
        public void Die(bool setdead = true)
        {
            dead = setdead;
        }

        public void AppendPoints(int addpoints)
        {
            points += addpoints;
        }

        public void AppendGrowth(int addgrowth)
        {
            growth += addgrowth;
        }

        public void SnakeStartValues(Vector setPosition, Vector setDirection, Color setColor)
        {
            position = setPosition;
            direction = setDirection;
            color = setColor;

            snake.Add(new Snake(this, game.getBoard.getTile(position)));
            game.getBoard.getTile(position).Occupie(snake.First());
        }

        void Move(Board board)
        {
            setPosition(new Vector(position.X + direction.X, position.Y + direction.Y));
            Tile moveTile = board.getTile(position);
            if (moveTile == null) { this.Die(); return; }
            GameObject other;
            if (moveTile.Occupied(out other))
            {
                Debug.WriteLine($"Snake {color} collision: {other.GetType().BaseType}");
                if (other.GetType() == typeof(Snake)) { Snake otherSnake = (Snake)other; otherSnake.getPlayer.AppendPoints(5); this.Die(); return; }
                if (other.GetType().BaseType == typeof(Consumable)) { Consumable otherConsumable = (Consumable)other; AppendPoints(otherConsumable.ValuePoints); AppendGrowth(otherConsumable.ValuePoints); game.SpawnConsumable(); }
            }

            snake.Add(new Snake(this, moveTile));
            if (growth > 0) { growth--; } else { snake.FirstOrDefault().Clear(); snake.Remove(snake.FirstOrDefault()); }
        }

        public void Update()
        {
            if (!dead) Move(game.getBoard);
        }

        
        public void Draw(Graphics graphics, Font font, Vector position, int playerIndex)
        {
            //game.BoardTileScale/2+game.BoardTileScale*8*(Index-1)
            //game.BoardTileScale/2

            graphics.DrawString($"Player {playerIndex}: {this.points}", font, new SolidBrush(color), position.X, position.Y);
        }
    }
}
