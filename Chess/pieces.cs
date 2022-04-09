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
        public bool isWhite;
        public PictureBox pictureBox;

        //this runs when a new piece is made
        public pieces(bool isWhite) { this.isWhite = isWhite; }

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

        //builds a threatmap of bool[64] 1-1 to boardpieces
        //used for check purposes, build every move
        //=======maybe put in board class=======
        public bool[] ThreatMap()
        {
            //need to develop
            bool[] hello = new bool[2];
            return hello;
        }
    }

    //All pieces types are a member of the pieces class
    //useful for move, and other universal functions
    public class pawn : pieces
    {
        public pawn(bool isWhite) : base(isWhite)
        {
            if (isWhite) { setPictureBox("../../assets/wP.png"); }
            else { setPictureBox("../../assets/bP.png"); }
        }       
    }

    public class rook : pieces
    {
        public rook(bool isWhite) : base(isWhite)
        {
            if (isWhite) { setPictureBox("../../assets/wR.png"); }
            else { setPictureBox("../../assets/bR.png"); }

        }
    }

    public class bishop : pieces
    {
        public bishop(bool isWhite) : base(isWhite)
        {
            if (isWhite) { setPictureBox("../../assets/wB.png"); }
            else { setPictureBox("../../assets/bB.png"); }

        }
    }

    public class knight : pieces
    {
        public knight(bool isWhite) : base(isWhite)
        {
            if (isWhite) { setPictureBox("../../assets/wN.png"); }
            else { setPictureBox("../../assets/bN.png"); }

        }
    }

    public class queen : pieces
    {
        public queen(bool isWhite) : base(isWhite)
        {
            if (isWhite) { setPictureBox("../../assets/wQ.png"); }
            else { setPictureBox("../../assets/bQ.png"); }

        }
    }

    public class king : pieces
    {
        public king(bool isWhite) : base(isWhite)
        {
            if (isWhite) { setPictureBox("../../assets/wK.png"); }
            else { setPictureBox("../../assets/bK.png"); }

        }
    }
}
