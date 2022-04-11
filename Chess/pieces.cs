using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class pieces
    {
        //pulic varables
        public int isWhite;
        public PictureBox pictureBox;

        //this runs when a new piece is made
        public pieces(int isWhite) { this.isWhite = isWhite; }

        //this makes the picture box class for the piece object
        //runs on piece generation
        public void setPictureBox(string filedir)
        {
            pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile(filedir);
            pictureBox.Padding = new Padding(0, 0, 0, 0);
            pictureBox.Margin = new Padding(0, 0, 0, 0);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public virtual bool[] AvalibleMoves(int currentLocation, pieces[] boardpieces)
        {
            bool[] moves = new bool[64];
            return moves;
        }

        public int[] ConvertToXY(int location)
        {
            int[] XY = new int[] { location % 8, location / 8 };
            return XY;
        }

        public int ConverToLocation(int[] XY)
        {
            int location = (XY[1] * 8) + XY[0];
            return location;
        }

    }
    //All pieces types are a member of the pieces class
    //useful for move, and other universal functions
    public class pawn : pieces
    {
        public pawn(int isWhite) : base(isWhite)
        {
            if (isWhite == 0) { setPictureBox("../../assets/wP.png"); }
            else { setPictureBox("../../assets/bP.png"); }
        }       
    }

    public class rook : pieces
    {
        public rook(int isWhite) : base(isWhite)
        {
            if (isWhite == 0) { setPictureBox("../../assets/wR.png"); }
            else { setPictureBox("../../assets/bR.png"); }

        }
    }

    public class bishop : pieces
    {
        public bishop(int isWhite) : base(isWhite)
        {
            if (isWhite == 0) { setPictureBox("../../assets/wB.png"); }
            else { setPictureBox("../../assets/bB.png"); }

        }
    }

    public class knight : pieces
    {
        public knight(int isWhite) : base(isWhite)
        {
            if (isWhite == 0) { setPictureBox("../../assets/wN.png"); }
            else { setPictureBox("../../assets/bN.png"); }

        }

        public override bool[] AvalibleMoves(int currentLocation, pieces[] boardpieces)
        {
            bool[] moves = new bool[64];
            int[] xVariations = new int[] {-1, 1, -2, 2, -2, 2, -1, 1 };
            int[] yVariations = new int[] {2, 2, 1, 1, -1, -1, -2, -2 };
            int[] xY = ConvertToXY(currentLocation);
            int[] move = new int[2];

            for (int i = 0; i < 8; i++)
            {
                move[0] = xVariations[i] + xY[0];
                move[1] = yVariations[i] + xY[1];
                if(move[0] < 8 && move[0] > -1 && move[1] < 8 && move[1] > -1)
                {
                    int moveLocation = ConverToLocation(move);
                    if(boardpieces[moveLocation] == null || boardpieces[moveLocation].isWhite != this.isWhite)
                    {
                        moves[moveLocation] = true;
                    }
                }
            }
            return moves;
        }
    }

    public class queen : pieces
    {
        public queen(int isWhite) : base(isWhite)
        {
            if (isWhite == 0) { setPictureBox("../../assets/wQ.png"); }
            else { setPictureBox("../../assets/bQ.png"); }

        }
    }

    public class king : pieces
    {
        public king(int isWhite) : base(isWhite)
        {
            if (isWhite == 0) { setPictureBox("../../assets/wK.png"); }
            else { setPictureBox("../../assets/bK.png"); }

        }
    }
}
