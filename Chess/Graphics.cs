using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    //this contains all the graphical code for the chess apllication
    // the diffrent components are nested in the Graphics class, BUT IS DOESNT DO ANYTHING!!!!, sadly
    public class Graphics
    {
        Form graphicsForm;
        public Graphics(Form1 form)
        {
            this.graphicsForm = form;
            //form
            graphicsForm.SuspendLayout();
            graphicsForm.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            graphicsForm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            graphicsForm.BackColor = System.Drawing.Color.White;
            graphicsForm.ClientSize = new System.Drawing.Size(1000, 700);
            graphicsForm.Name = "Chess";
            graphicsForm.Text = "Chess";
            graphicsForm.ResumeLayout(false);



        }

        public class GraphicalBoard
        {
            public static int chessBoardSize = 480;
            static int borderSize = 20;
            Panel boardPanel;
            PictureBox[] ParentSquares = new PictureBox[64];
            PictureBox[] PiecesOnBoard = new PictureBox[64];
            Board currentBoard;
            public GraphicalBoard(Graphics parentGraphics, Board parentBoard)
            {
                currentBoard = parentBoard;
                //panel for chess board
                boardPanel = new Panel();
                boardPanel.Location = new Point(10, 10);
                //has to be a multiple of 8
                boardPanel.Size = new Size(chessBoardSize, chessBoardSize);
                boardPanel.Margin = new Padding(0);
                boardPanel.Padding = new Padding(0);
                boardPanel.BackColor = Color.Blue;

                //creates squares
                createBackSquares();

                //panle for chess background
                Panel backPanel = new Panel();
                backPanel.Location = new Point(10, 10);
                //has to be a multiple of 8
                backPanel.Size = new Size(borderSize + chessBoardSize, borderSize + chessBoardSize);
                backPanel.Margin = new Padding(0);
                backPanel.Padding = new Padding(0);
                backPanel.BackColor = Color.Black;

                parentGraphics.graphicsForm.Controls.Add(backPanel);
                backPanel.Controls.Add(boardPanel);
            }
            private void createBackSquares()
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        PictureBox pictureBox1 = new PictureBox();
                        ParentSquares[y * 8 + x] = pictureBox1;
                        pictureBox1.Padding = new Padding(0, 0, 0, 0);
                        pictureBox1.Margin = new Padding(0);
                        pictureBox1.Size = new Size(chessBoardSize / 8, chessBoardSize / 8);
                        pictureBox1.Location = new Point(x * chessBoardSize / 8, y * chessBoardSize / 8);

                        if ((x % 2 == 0 && y % 2 == 1) || (x % 2 == 1 && y % 2 == 0))
                        {
                            pictureBox1.BackColor = Color.DarkCyan;
                            pictureBox1.Size = new Size(chessBoardSize / 8, chessBoardSize / 8); ;

                        }
                        else
                        {
                            pictureBox1.BackColor = Color.LightCyan;
                            pictureBox1.Size = new Size(chessBoardSize / 8, chessBoardSize / 8);
                        }
                        if(currentBoard != null) { pictureBox1.Click += new EventHandler(currentBoard.UserClicked); }
                        boardPanel.Controls.Add((pictureBox1));

                    }
                }
            }
            public void DrawAllPiecesOnBoard(Pieces[] AllPieces)
            {
                for(int i = 0; i < 64; i++)
                {
                    if (AllPieces[i] != null)
                    {
                        DrawSinglePieceOnBoard(i, AllPieces[i]);
                    }
                }
            }
            public void DrawSinglePieceOnBoard(int location, Pieces piece)
            {
                string piecesDir = getPieceDir(piece);
                int[] xY = Board.CovertLocationToXY(location);
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(piecesDir);
                pictureBox.Padding = new Padding(0, 0, 0, 0);
                pictureBox.Margin = new Padding(0, 0, 0, 0);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Location = new Point(xY[0] * chessBoardSize / 8, xY[1] * chessBoardSize / 8);
                pictureBox.Size = new Size(chessBoardSize / 8, chessBoardSize / 8);
                pictureBox.BackColor = ParentSquares[location].BackColor;
                if (currentBoard != null) { pictureBox.Click += new EventHandler(currentBoard.UserClicked); }
                PiecesOnBoard[location] = pictureBox;
                boardPanel.Controls.Add(pictureBox);
                pictureBox.BringToFront();
            }
            public void MovePiece(int[] fromToLocation)
            {
                if (PiecesOnBoard[fromToLocation[1]] != null)
                {
                    boardPanel.Controls.Remove(PiecesOnBoard[fromToLocation[1]]);
                }
                PiecesOnBoard[fromToLocation[0]].BackColor = ParentSquares[fromToLocation[1]].BackColor;
                PiecesOnBoard[fromToLocation[0]].Location = new Point(fromToLocation[1] % 8 * boardPanel.Size.Width / 8, fromToLocation[1] / 8 * boardPanel.Size.Width / 8);
                PiecesOnBoard[fromToLocation[1]] = PiecesOnBoard[fromToLocation[0]];
                PiecesOnBoard[fromToLocation[0]] = null;
            }
            private string getPieceDir(Pieces piece)
            {
                string colourChar;
                if(piece.colour == 0) { colourChar = "w"; }
                else { colourChar = "b"; }
                string pieceDir = "../../assets/" + colourChar + piece.ID + ".png";
                return pieceDir;
            }

        }

    }
}
