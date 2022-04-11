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

        public bool[] FindMovesFromVariations(int currentLocation, pieces[] boardpieces, int[] xVariations, int[] yVariations)
        {
            bool[] moves = new bool[64];
            int[] xY = ConvertToXY(currentLocation);
            int[] move = new int[2];

            for (int i = 0; i < xVariations.Length; i++)
            {
                move[0] = xVariations[i] + xY[0];
                move[1] = yVariations[i] + xY[1];
                if (move[0] < 8 && move[0] > -1 && move[1] < 8 && move[1] > -1)
                {
                    int moveLocation = ConverToLocation(move);
                    if (boardpieces[moveLocation] == null || boardpieces[moveLocation].isWhite != this.isWhite)
                    {
                        moves[moveLocation] = true;
                    }
                }
            }
            return moves;
        }

        public bool[] FindMovesStraightLinesVariations(int currentLocation, pieces[] boardpieces, int[] xVariations, int[] yVariations)
        {
            bool[] moves = new bool[64];
            int[] xY = ConvertToXY(currentLocation);
            int[] move = new int[2];


            for (int i = 0; i < xVariations.Length; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    move[0] = (xVariations[i] * j) + xY[0];
                    move[1] = (yVariations[i] * j) + xY[1];
                    if (move[0] < 8 && move[0] > -1 && move[1] < 8 && move[1] > -1)
                    {
                        int moveLocation = ConverToLocation(move);
                        if (boardpieces[moveLocation] == null) {moves[moveLocation] = true;}
                        else if(boardpieces[moveLocation].isWhite != this.isWhite) { moves[moveLocation] = true; break; }
                        else if(boardpieces[moveLocation].isWhite == this.isWhite) { break; }
                    }
                }
            }


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

        public override bool[] AvalibleMoves(int currentLocation, pieces[] boardpieces)
        {
            bool[] moves = new bool[64];
            int[] xY = ConvertToXY(currentLocation);
            int location;
            int[] file = new int[] {6, 1};
            int[] xVariations = new int[] {-1, 1};
            int[] move = new int[] { xY[0], xY[1]};
            int colour = 1;
            if(isWhite == 0) { colour = -1; }
            

            for(int i = 1; i < 3; i++)
            {
                move[1] = xY[1] + i * colour;
                move[0] = xY[0];
                if (move[1] < 8 && move[1] > -1){
                    location = ConverToLocation(move);
                    if (boardpieces[location] == null)
                    {
                        if (i == 1)
                        {
                            moves[location] = true;
                        }
                        else if (xY[1] == file[isWhite])
                        {
                            Console.WriteLine($"{location}");
                            moves[location] = true;
                        }
                        Console.WriteLine($"{xY[1]}:{file[isWhite]}");
                    }
                }
                move[1] = xY[1] + 1 * colour;
                move[0] = xY[0] + xVariations[i-1];
                if (move[0] < 8 && move[0] > -1 && move[1] < 8 && move[1] > -1) {
                    location = ConverToLocation(move);
                    if (boardpieces[location] != null && boardpieces[location].isWhite != this.isWhite)
                    {
                        moves[location] = true;
                    }
                }

            }




            return moves;
        }
    }

    public class rook : pieces
    {
        public rook(int isWhite) : base(isWhite)
        {
            if (isWhite == 0) { setPictureBox("../../assets/wR.png"); }
            else { setPictureBox("../../assets/bR.png"); }
        }

        public override bool[] AvalibleMoves(int currentLocation, pieces[] boardpieces)
        {
            int[] xVariations = new int[] { -1, 1, 0, 0 };
            int[] yVariations = new int[] { 0, 0, -1, 1 };

            bool[] moves = FindMovesStraightLinesVariations(currentLocation, boardpieces, xVariations, yVariations);
            return moves;
        }
    }

    public class bishop : pieces
    {
        public bishop(int isWhite) : base(isWhite)
        {
            if (isWhite == 0) { setPictureBox("../../assets/wB.png"); }
            else { setPictureBox("../../assets/bB.png"); }

        }

        public override bool[] AvalibleMoves(int currentLocation, pieces[] boardpieces)
        {

            int[] xVariations = new int[] { -1, -1, 1, 1 };
            int[] yVariations = new int[] { -1, 1, -1, 1 };

            bool[] moves = FindMovesStraightLinesVariations(currentLocation, boardpieces, xVariations, yVariations);
            return moves;
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
            int[] xVariations = new int[] {-1, 1, -2, 2, -2, 2, -1, 1 };
            int[] yVariations = new int[] {2, 2, 1, 1, -1, -1, -2, -2 };
            bool[] moves = FindMovesFromVariations(currentLocation, boardpieces, xVariations, yVariations);
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
        public override bool[] AvalibleMoves(int currentLocation, pieces[] boardpieces)
        {
            int[] xVariations = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] yVariations = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };

            bool[] moves = FindMovesStraightLinesVariations(currentLocation, boardpieces, xVariations, yVariations);
            return moves;
        }
    }

    public class king : pieces
    {
        public king(int isWhite) : base(isWhite)
        {
            if (isWhite == 0) { setPictureBox("../../assets/wK.png"); }
            else { setPictureBox("../../assets/bK.png"); }

        }

        public override bool[] AvalibleMoves(int currentLocation, pieces[] boardpieces)
        {
            int[] xVariations = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] yVariations = new int[] { 1, 1, 1, 0, 0, -1, -1, -1 };
            bool[] moves = FindMovesFromVariations(currentLocation, boardpieces, xVariations, yVariations);
            return moves;
        }
    }
}
