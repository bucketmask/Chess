using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    class Graphics
    {
        public Graphics(Form1 graphicsForm)
        {

            //form
            graphicsForm.SuspendLayout();
            graphicsForm.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            graphicsForm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            graphicsForm.BackColor = System.Drawing.Color.White;
            graphicsForm.ClientSize = new System.Drawing.Size(1000, 700);
            graphicsForm.Name = "Chess";
            graphicsForm.Text = "Chess";
            graphicsForm.ResumeLayout(false);


            //creates the chess board class
            Graphics.board graphicalBoard = new Graphics.board(graphicsForm);

        }
        public class board
        {
            static int chessBoardSize = 480;
            static int borderSize = 20;
            Panel boardPanel;
            PictureBox[] ParentSquares = new PictureBox[64];
            public board(Form graphicsForm)
            {
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

                graphicsForm.Controls.Add(backPanel);
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
                        //pictureBox1.Click += new EventHandler(userClicked);
                        boardPanel.Controls.Add((pictureBox1));

                    }
                }
            }
            public void DrawPieceOnBoard(PictureBox piece)
            {

            }

        }
        public class pieces
        {
            public pieces(int location, string piecesDir)
            {

            }

        }
    }
}
