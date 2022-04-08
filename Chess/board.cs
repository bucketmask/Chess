using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class board
    {
        PictureBox[] ParentSquares = new PictureBox[64];



        public board(Panel panel1)
        {
            pieces[] boardpieces = CreateBoardFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    PictureBox pictureBox1 = new PictureBox();
                    ParentSquares[y * 8 + x] = pictureBox1;
                    pictureBox1.Padding = new Padding(0, 0, 0, 0);
                    pictureBox1.Margin = new Padding(0);
                    pictureBox1.Size = new Size(panel1.Size.Width / 8, panel1.Size.Height / 8);
                    pictureBox1.Location = new Point(x * panel1.Size.Width / 8, y * panel1.Size.Height / 8);

                    if ((x % 2 == 0 && y % 2 == 1) || (x % 2 == 1 && y % 2 == 0))
                    {
                        pictureBox1.BackColor = Color.Brown;
                        pictureBox1.Size = new Size(panel1.Size.Width / 8, panel1.Size.Height / 8); ;

                    }
                    else
                    {
                        pictureBox1.BackColor = Color.White;
                        pictureBox1.Size = new Size(panel1.Size.Width / 8, panel1.Size.Height / 8);
                    }
                    panel1.Controls.Add((pictureBox1));

                }
            }

            DrawPieces(panel1, boardpieces);

        }

        public void DrawPieces(Panel panel1, pieces[] boardpieces)
        {
            for (int i = 0; i < boardpieces.Length; i++)
            {
                if (boardpieces[i] != null) 
                {
                    boardpieces[i].pictureBox.Location = new Point(i % 8 * panel1.Size.Width/8, i / 8 * panel1.Size.Width / 8);
                    boardpieces[i].pictureBox.Size = new Size(panel1.Size.Width / 8, panel1.Size.Height / 8);
                    boardpieces[i].pictureBox.BackColor = ParentSquares[i].BackColor;
                    panel1.Controls.Add(boardpieces[i].pictureBox);
                    boardpieces[i].pictureBox.BringToFront();
                }
                
            }
        }

        public pieces[] CreateBoardFromFen(string fen)
        {
            pieces[] boardpieces = new pieces[64];

            string[] fenSplit = fen.Split('/');

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


    }
}
