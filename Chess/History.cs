using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
namespace Chess
{
    public class History
    {
        const string startingFen = "";
        //Each pointer?? is a halfmove
        //so even is white, odd is black moves
        string[] moveHistory = new string[512];
        int moveNumber;


        public History()
        {
            moveNumber = 0;
        }

        public void LogMove(int[] toFromLocation, Pieces movedPiece, Pieces[] oldBoard) { moveNumber++; }

        public History(string PGN) { }

        public int MoveNumber { get { return MoveNumber; } }

        public Pieces[] ResetBoard() 
        {

        }
    }
}
