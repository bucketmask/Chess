using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class pieces
    {
        public bool isWhite;
        public PictureBox pictureBox;
        public pieces(bool isWhite) { this.isWhite = isWhite; }

        public void setPictureBox(string filedir)
        {
            pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile(filedir);
            pictureBox.Padding = new Padding(0, 0, 0, 0);
            pictureBox.Margin = new Padding(0, 0, 0, 0);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }

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
