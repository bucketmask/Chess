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

        public board(Panel panel1)
        {
            //makes the panel and form public to board class
            boardPanel = panel1;
            //builds the board from a given fen
            //each pieces is a pieces class
            boardpieces = CreateBoardFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");



            // creates the squares on the board
            //adds them to a identical array to board pieces so they can be called appon
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



            //Draws the pieces that are on the current board
            for (int i = 0; i < boardpieces.Length; i++)
            {
                if (boardpieces[i] != null)
                {
                    DrawPieces(i);
                }

            }






            //test+++++++++++++++++++++++++++
            for (int i = 0; i < AvalibleMoves.Length; i++)
            {
                AvalibleMoves[i] = true;
            }
            //test+++++++++++++++++++++++++++

        }

        //this takes the array of pieces on the board
        //it then draws each one to their locastion and brings them to the front
        //i is location of the pieces on boardpieces
        public void DrawPieces(int i)
        {

            boardpieces[i].pictureBox.Click += new EventHandler(userClicked);
            boardpieces[i].pictureBox.Location = new Point(i % 8 * boardPanel.Size.Width / 8, i / 8 * boardPanel.Size.Width / 8);
            boardpieces[i].pictureBox.Size = new Size(boardPanel.Size.Width / 8, boardPanel.Size.Height / 8);
            boardpieces[i].pictureBox.BackColor = ParentSquares[i].BackColor;
            boardPanel.Controls.Add(boardpieces[i].pictureBox);
            boardpieces[i].pictureBox.BringToFront();

        }

        //this simpily moves a selected piece to a location
        public void move(int place)
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

        //creats the board and puts the pieses object into a array
        //returns the array
        public pieces[] CreateBoardFromFen(string fen)
        {
            pieces[] boardpieces = new pieces[64];

            string[] fenSplit = fen.Split('/');

            //uses a pointer
            //im amazing
            int Pointer = 0;
            for (int i = 0; i < fenSplit.Length; i++)
            {
                for (int j = 0; j < fenSplit[i].Length; j++)
                {
                    int number;
                    if (int.TryParse(fenSplit[i][j].ToString(), out number))
                    {
                        Pointer += number - 1;
                    }
                    if (fenSplit[i][j].ToString().ToLower() == "p")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new pawn(false); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new pawn(true); }

                    }

                    else if (fenSplit[i][j].ToString().ToLower() == "r")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new rook(false); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new rook(true); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "b")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new bishop(false); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new bishop(true); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "n")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new knight(false); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new knight(true); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "k")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new king(false); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new king(true); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "q")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new queen(false); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new queen(true); }
                    }
                    Pointer++;
                }
            }
            return boardpieces;
        }

        //everytime a user clicks a pieces or square this handles it
        public void userClicked(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            //Console.WriteLine($"{pictureBox.Location}");
            int place = pictureBox.Location.X / (boardPanel.Size.Width / 8) + pictureBox.Location.Y / (boardPanel.Size.Width / 8) * 8;
            //Console.WriteLine(place);

            if (SelectedPiece == -1 && boardpieces[place] != null)
            {
                Console.WriteLine("select piece");
                SelectedPiece = place;
            }
            else if (SelectedPiece != -1 && AvalibleMoves[place] == true)
            {
                if(place != SelectedPiece)
                {
                    Console.WriteLine("can move");
                    move(place);
                    SelectedPiece = -1;
                }
            }
            else if (SelectedPiece != -1 && AvalibleMoves[place] == false && boardpieces[place] != null)
            {
                Console.WriteLine("select other");
                SelectedPiece = place;
            }
            else
            {
                Console.WriteLine("deselect");
                SelectedPiece = -1;
            }
        }


    }
}
