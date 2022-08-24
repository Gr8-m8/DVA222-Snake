using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Snake
{
    class GameGrapics : Form
    {
        Game game;
        public GameGrapics(Game setgame) : base()
        {
            game = setgame;

            Text = "Snake";
            DoubleBuffered = true;
            Width = 800;
            Height = 600;

            KeyDown += GameGrapics_KeyDown;
        }

        private void GameGrapics_KeyDown(object? sender, KeyEventArgs e)
        {
            //Player 0
            if (e.KeyCode == Keys.D) { game.GetPlayers[0].setDirection(new Vector(1, 0)); }
            if (e.KeyCode == Keys.A) { game.GetPlayers[0].setDirection(new Vector(-1, 0)); }
            if (e.KeyCode == Keys.S) { game.GetPlayers[0].setDirection(new Vector(0, 1)); }
            if (e.KeyCode == Keys.W) { game.GetPlayers[0].setDirection(new Vector(0, -1)); }

            //Player 1
            if (e.KeyCode == Keys.L) { game.GetPlayers[1].setDirection(new Vector(1, 0)); }
            if (e.KeyCode == Keys.J) { game.GetPlayers[1].setDirection(new Vector(-1, 0)); }
            if (e.KeyCode == Keys.K) { game.GetPlayers[1].setDirection(new Vector(0, 1)); }
            if (e.KeyCode == Keys.I) { game.GetPlayers[1].setDirection(new Vector(0, -1)); }

            //Utility Keys
            if (e.KeyCode == Keys.Escape) { System.Environment.Exit(0); }
            if (e.KeyCode == Keys.Enter) { game.StartNewGame(); }
        }
    }

    class Game
    {
        public void Run()
        {
            //Debug.WriteLine("RUN");
            Grapics.Paint += Draw;
            timer.Tick += Update;
            timer.Interval = 240;
            timer.Start();

            Application.Run(Grapics);
        }

        public Game()
        {
            Grapics = new GameGrapics(this);
        }

        enum GameState
        {
            Pregame = -1,
            Ingame = 0,
            Postgame = 1,
        }
        GameState gamestate = GameState.Pregame;
        public void StartNewGame()
        {
            if (gamestate == GameState.Ingame) return;
            gamestate = GameState.Ingame;
            int size = 17;
            board = new Board(new Vector(size, size));

            players = new Player[2];// { new Player(this), new Player(this) };
            for (int si = 0; si < players.Length; si++)
            {
                players[si] = new Player(this);
                switch (si)
                {
                    default:
                    case -1:
                        players[si].SnakeStartValues(new Vector(), new Vector(), Color.Black);
                        break;

                    case 0:
                        players[si].SnakeStartValues(new Vector(0 + 1, board.getSizeY / 2), new Vector(1, 0), Color.Green);
                        break;

                    case 1:
                        players[si].SnakeStartValues(new Vector((board.getSizeX-1)-1, board.getSizeY / 2), new Vector(-1, 0), Color.Red);
                        break;
                }

            }

            for (int i = 0; i < 2; i++)
            {
                getBoard.SpawnConsumable(r);
            }
        }

        

        Random r = new Random();

        GameGrapics Grapics;// = new GameGrapics();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int TickRate = 60;

        Board board;
        public Board getBoard => board;
        
        Player[] players;
        public Player[] GetPlayers => players;

        void Update(Object obj, EventArgs args)
        {
            switch (gamestate)
            {
                case GameState.Pregame:
                    break;

                case GameState.Ingame:
                    if (players.Length > 0) foreach (Player player in players) { player.Update(); }
                    
                    bool gameover = true;
                    foreach (Player p in players) { if (!p.isDead) gameover = false; }
                    if (gameover) gamestate = GameState.Postgame;
                    break;

                case GameState.Postgame:
                    break;
            }
            

            Grapics.Refresh();
        }

        Font font = new Font("Consolas", 12);
        void Draw(Object obj,PaintEventArgs args)
        {
            switch (gamestate)
            {
                case GameState.Pregame:
                    args.Graphics.DrawString("Snake vs Snake", font, new SolidBrush(Color.Orange), new PointF(this.Grapics.Width / 3, this.Grapics.Height / 4));
                    args.Graphics.DrawString("<Press [Enter] to play>", font, new SolidBrush(Color.LawnGreen), new PointF(this.Grapics.Width / 3, this.Grapics.Height / 4 + font.Size * 3 / 2));
                    break;

                case GameState.Ingame:
                    for (int i = 0; i < players.Length; i++) { players[i].Draw(args.Graphics, font, new Vector((int)font.Size + Math.Min(this.Grapics.Width, this.Grapics.Height), (int)((font.Size) / 2 * (i + 1) + (font.Size * i))), i + 1); }
                    
                    getBoard.Draw(args.Graphics, font, new Vector(0, (int)font.Size * (players.Length + 1)), Math.Min(this.Grapics.Width, this.Grapics.Height));
                    break;

                case GameState.Postgame:
                    args.Graphics.DrawString("Game Over", font, new SolidBrush(Color.Red), new PointF(this.Grapics.Width / 3, this.Grapics.Height / 4));
                    args.Graphics.DrawString("<Press [Enter] to play again>", font, new SolidBrush(Color.LawnGreen), new PointF(this.Grapics.Width / 3, this.Grapics.Height / 4 + font.Size*3/2));

                    string winnertext = "Draw!";
                    if (players[0].Points != players[1].Points)
                        if (players[0].Points > players[1].Points) winnertext = $"Player {0+1} Winns!"; else winnertext = $"Player {1 + 1} Winns!";
                    
                    args.Graphics.DrawString(winnertext, font, new SolidBrush(Color.Orange), new PointF(this.Grapics.Width / 3, this.Grapics.Height / 2 + (font.Size * -1.5f)));

                    for (int i = 0; i < players.Length; i++)
                        args.Graphics.DrawString($"Player {i+1} score: {players[i].Points}", font, new SolidBrush(players[i].getColor), new PointF(this.Grapics.Width / 3, this.Grapics.Height / 2 + font.Size*i +(font.Size/2)*i));
                    break;
            }
        }
    }
}
