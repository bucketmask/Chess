using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class board
    {
        //public classes acsessible to all board methods
        PictureBox[] ParentSquares = new PictureBox[64];
        Panel boardPanel;
        pieces[] boardpieces;
        bool[] AvalibleMoves = new bool[64];
        int SelectedPiece = -1;
        String[] PlayerNamesWB = new string[] { "default", "default" };
        int player = 0;
        History history;
        int Tick;

        public board(Panel panel1)
        {
            //makes the panel and form public to board class
            boardPanel = panel1;
            //Creates history class, for:
            //-legality resons
            //-multiplayer + spectators
            //-review game
            history = new History();
            Tick = history.MoveNumber;
            //each pieces is a pieces class
            boardpieces = history.CreateBoardFromFen();



            // creates the squares on the board
            //adds them to a identical array to board pieces so they can be called appon
            DrawBoard();

            //Draws the pieces that are on the current board
            DrawAllPieces();

        }






        //this takes the array of pieces on the board
        //it then draws each one to their locastion and brings them to the front
        //i is location of the pieces on boardpieces
        void DrawPieces(int i)
        {

            boardpieces[i].pictureBox.Click += new EventHandler(userClicked);
            boardpieces[i].pictureBox.Location = new Point(i % 8 * boardPanel.Size.Width / 8, i / 8 * boardPanel.Size.Width / 8);
            boardpieces[i].pictureBox.Size = new Size(boardPanel.Size.Width / 8, boardPanel.Size.Height / 8);
            boardpieces[i].pictureBox.BackColor = ParentSquares[i].BackColor;
            boardPanel.Controls.Add(boardpieces[i].pictureBox);
            boardpieces[i].pictureBox.BringToFront();

        }
        void DrawAllPieces()
        {
            for (int i = 0; i < boardpieces.Length; i++)
            {
                if (boardpieces[i] != null)
                {
                    DrawPieces(i);
                }

            }
        }

        void DrawBoard()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    PictureBox pictureBox1 = new PictureBox();
                    ParentSquares[y * 8 + x] = pictureBox1;
                    pictureBox1.Padding = new Padding(0, 0, 0, 0);
                    pictureBox1.Margin = new Padding(0);
                    pictureBox1.Size = new Size(boardPanel.Size.Width / 8, boardPanel.Size.Height / 8);
                    pictureBox1.Location = new Point(x * boardPanel.Size.Width / 8, y * boardPanel.Size.Height / 8);

                    if ((x % 2 == 0 && y % 2 == 1) || (x % 2 == 1 && y % 2 == 0))
                    {
                        pictureBox1.BackColor = Color.DarkCyan;
                        pictureBox1.Size = new Size(boardPanel.Size.Width / 8, boardPanel.Size.Height / 8); ;

                    }
                    else
                    {
                        pictureBox1.BackColor = Color.LightCyan;
                        pictureBox1.Size = new Size(boardPanel.Size.Width / 8, boardPanel.Size.Height / 8);
                    }
                    pictureBox1.Click += new EventHandler(userClicked);
                    boardPanel.Controls.Add((pictureBox1));

                }
            }
        }

        //this simpily moves a selected piece to a location
        void move(int place)
        {
            if (boardpieces[place] != null)
            {
                boardPanel.Controls.Remove(boardpieces[place].pictureBox);
            }
            boardpieces[place] = boardpieces[SelectedPiece];
            boardpieces[SelectedPiece] = null;
            boardpieces[place].pictureBox.BackColor = ParentSquares[place].BackColor;
            boardpieces[place].pictureBox.Location = new Point(place % 8 * boardPanel.Size.Width / 8, place / 8 * boardPanel.Size.Width / 8);
        }

        //builds a threatmap of bool[64] 1-1 to boardpieces
        //used for check purposes, build every move
        bool[] ThreatMap()
        {
            //need to develop
            bool[] hello = new bool[2];
            return hello;
        }


        //everytime a user clicks a pieces or square this handles it
        void userClicked(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            //Console.WriteLine($"{pictureBox.Location}");
            int place = pictureBox.Location.X / (boardPanel.Size.Width / 8) + pictureBox.Location.Y / (boardPanel.Size.Width / 8) * 8;
            //Console.WriteLine(place);

            if (PlayerNamesWB[Tick % 2] == PlayerNamesWB[player])
            {
                if (SelectedPiece == -1 && boardpieces[place] != null && boardpieces[place].isWhite == Tick % 2)
                {
                    SelectedPiece = place;
                    AvalibleMoves = boardpieces[place].AvalibleMoves(place, boardpieces);
                }
                else if (SelectedPiece != -1 && AvalibleMoves[place] == true)
                {
                    if (place != SelectedPiece)
                    {
                        move(place);
                        history.LogMove(boardpieces[SelectedPiece]);
                        SelectedPiece = -1;
                    }
                }
                else if (SelectedPiece != -1 && AvalibleMoves[place] == false && boardpieces[place] != null && boardpieces[place].isWhite == Tick % 2)
                {
                    SelectedPiece = place;
                    AvalibleMoves = boardpieces[place].AvalibleMoves(place, boardpieces);
                }
                else
                {
                    SelectedPiece = -1;
                }
            }
        }


    }
}
