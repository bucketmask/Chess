using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    public class GraphicalPromotion
    {
        GraphicalBoard graphicalBoard;
        Panel backPanel;
        int sizeOfSquare;
        Board board;
        int[] fromToLocation;
        string[] avaliblePiecesID = { "Q", "R", "B", "N" };
        PictureBox[] picturesOnPanel = new PictureBox[4];
        public GraphicalPromotion(GraphicalBoard parentGraphicalBoard)
        {
            graphicalBoard = parentGraphicalBoard;
            sizeOfSquare = GraphicalBoard.chessBoardSize / 8;
            backPanel = new Panel();
            backPanel.Size = new Size(sizeOfSquare * 4, sizeOfSquare);
            backPanel.Margin = new Padding(0);
            backPanel.Padding = new Padding(0);
            backPanel.BackColor = Color.WhiteSmoke;

            for (int i = 0; i < avaliblePiecesID.Length; i++)
            {
                PictureBox picture = new PictureBox();
                picture.Size = new Size(sizeOfSquare, sizeOfSquare);
                picture.Location = new Point(i * sizeOfSquare, 0);
                picture.Click += new EventHandler(ReturnPiece);
                picturesOnPanel[i] = picture;
                backPanel.Controls.Add(picture);
            }

        }
        public void Show(int[] fromToLocation1, int colour, Board board1)
        {
            board = board1;
            fromToLocation = fromToLocation1;
            int[] xy = Board.ConvertLocationToXY(fromToLocation[1]);
            if (xy[0] < 5) { backPanel.Location = new Point((xy[0]) * sizeOfSquare, xy[1] * sizeOfSquare); }
            else { backPanel.Location = new Point((xy[0] - 3) * sizeOfSquare , xy[1] * sizeOfSquare); }
            for(int i = 0; i < avaliblePiecesID.Length; i++)
            {
                string imagedir = graphicalBoard.getPieceDir(colour, avaliblePiecesID[i]);
                picturesOnPanel[i].Image = Image.FromFile(imagedir);
                picturesOnPanel[i].BringToFront();
            }
            graphicalBoard.BoardPanel.Controls.Add(backPanel);
            backPanel.BringToFront();
        }
        public void Hide()
        {
            graphicalBoard.BoardPanel.Controls.Remove(backPanel);
        }
        public void ReturnPiece(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            string id = avaliblePiecesID[pictureBox.Location.X / sizeOfSquare];
            Console.WriteLine(id);
            //board promot
            Hide();
            board.PromotionMove(id, fromToLocation);

            
        }
    }
}
