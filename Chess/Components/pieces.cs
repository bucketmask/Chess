using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    //all pieces are members of the pieces class
    //will be able to derermine ther own sudo legal moves but needs the current board
    //also each type as a public id for history purposes
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class Pieces
    {
        public int colour;
        public string ID;
        public bool hasMoved;
        public Board board;
        public History history;
        public Pieces(int inputcolour, Board board1)
        {
            colour = inputcolour;
            board = board1;
            history = board.History;
        }

        public bool[] FindMovesFromVariations(int currentLocation, Pieces[] boardpieces, int[] xVariations, int[] yVariations)
        {
            bool[] moves = new bool[64];
            int[] xY = Board.ConvertLocationToXY(currentLocation);
            int[] move = new int[2];

            for (int i = 0; i < xVariations.Length; i++)
            {
                move[0] = xVariations[i] + xY[0];
                move[1] = yVariations[i] + xY[1];
                if (move[0] < 8 && move[0] > -1 && move[1] < 8 && move[1] > -1)
                {
                    int moveLocation = Board.ConvertXYToLocation(move);
                    if (boardpieces[moveLocation] == null || boardpieces[moveLocation].colour != this.colour)
                    {
                        moves[moveLocation] = true;
                    }
                }
            }
            return moves;
        }
        public bool[] FindMovesStraightLinesVariations(int currentLocation, Pieces[] boardpieces, int[] xVariations, int[] yVariations)
        {
            bool[] moves = new bool[64];
            int[] xY = Board.ConvertLocationToXY(currentLocation);
            int[] move = new int[2];


            for (int i = 0; i < xVariations.Length; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    move[0] = (xVariations[i] * j) + xY[0];
                    move[1] = (yVariations[i] * j) + xY[1];
                    if (move[0] < 8 && move[0] > -1 && move[1] < 8 && move[1] > -1)
                    {
                        int moveLocation = Board.ConvertXYToLocation(move);
                        if (boardpieces[moveLocation] == null) { moves[moveLocation] = true; }
                        else if (boardpieces[moveLocation].colour != this.colour) { moves[moveLocation] = true; break; }
                        else if (boardpieces[moveLocation].colour == this.colour) { break; }
                    }
                }
            }
            return moves;
        }

        public virtual bool[] AvalibleMoves(int currentLocation, Pieces[] boardpieces)
        {
            bool[] moves = new bool[64];
            return moves;
        }

        public virtual bool[] ThreatMap(int currentLocation, Pieces[] boardpieces)
        {
            bool[] moves = new bool[64];
            return moves;
        }
    }
    public class Pawn : Pieces
    {
        public Pawn(int inputcolour, Board board) : base(inputcolour, board) { ID = "P"; }
        public override bool[] ThreatMap(int currentLocation, Pieces[] boardpieces)
        {
            bool[] moves = new bool[64];
            int[] xY = Board.ConvertLocationToXY(currentLocation);
            int location;
            int[] xVariations = new int[] { -1, 1 };
            int[] move = new int[] { xY[0], xY[1] };
            //if white the x must decress, else it must incress
            int direction = 1;
            if (colour == 0) { direction = -1; }
            else { direction = 1; }

            for (int i = 1; i < 3; i++)
            {
                move[1] = xY[1] + 1 * direction;
                move[0] = xY[0] + xVariations[i - 1];
                if (move[0] < 8 && move[0] > -1 && move[1] < 8 && move[1] > -1)
                {
                    location = Board.ConvertXYToLocation(move);
                    if (boardpieces[location] != null && boardpieces[location].colour != this.colour)
                    {
                        moves[location] = true;
                    }
                }
            }
            return moves;
        }
        public override bool[] AvalibleMoves(int currentLocation, Pieces[] boardpieces)
        {
            bool[] moves = new bool[64];
            bool pieceInWay = false;
            int[] xY = Board.ConvertLocationToXY(currentLocation);
            int location;
            int[] file = new int[] { 6, 1 };
            int[] xVariations = new int[] { -1, 1 };
            int[] move = new int[] { xY[0], xY[1] };
            //if white the x must decress, else it must incress
            int direction = 1;
            if (colour == 0) { direction = -1; }
            else { direction = 1; }


            for (int i = 1; i < 3; i++)
            {
                move[1] = xY[1] + i * direction;
                move[0] = xY[0];
                if (move[1] < 8 && move[1] > -1)
                {
                    location = Board.ConvertXYToLocation(move);
                    if (boardpieces[location] == null)
                    {
                        if (i == 1)
                        {
                            moves[location] = true;
                        }
                        else if (xY[1] == file[colour] && pieceInWay == false)
                        {
                            moves[location] = true;
                        }
                    }
                    else { pieceInWay = true; }
                }
                move[1] = xY[1] + 1 * direction;
                move[0] = xY[0] + xVariations[i - 1];
                if (move[0] < 8 && move[0] > -1 && move[1] < 8 && move[1] > -1)
                {
                    location = Board.ConvertXYToLocation(move);
                    if (boardpieces[location] != null && boardpieces[location].colour != this.colour)
                    {
                        moves[location] = true;
                    }
                    if (history.enpassantMoveSquareLocation[1] == location && history.enpassantMoveSquareLocation[0] == history.MoveNumber - 1)
                    {
                        moves[location] = true;
                    }
                }

            }
            return moves;
        }
    }
    public class King : Pieces
    {
        public King(int inputcolour, Board board) : base(inputcolour, board) { ID = "K"; }
        public override bool[] ThreatMap(int currentLocation, Pieces[] boardpieces) 
        {
            int[] xVariations = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] yVariations = new int[] { 1, 1, 1, 0, 0, -1, -1, -1 };
            bool[] moves = FindMovesFromVariations(currentLocation, boardpieces, xVariations, yVariations);
            return moves; 
        }
        public override bool[] AvalibleMoves(int currentLocation, Pieces[] boardpieces)
        {
            int[] castlingLocationQK = { 2, 6 };
            int[] xy = Board.ConvertLocationToXY(currentLocation);
            bool[] moves = this.ThreatMap(currentLocation, boardpieces);
            bool[] castleQK = board.CanCastleQueenKingSide(colour);

            for(int i = 0; i < 2; i++)
            {
                if (castleQK[i])
                {
                    xy[0] = castlingLocationQK[i];
                    moves[Board.ConvertXYToLocation(xy)] = true;

                }
            }


            return moves;
        }
    }
    public class Rook : Pieces
    {
        public Rook(int inputcolour, Board board) : base(inputcolour, board) { ID = "R"; }
        public override bool[] ThreatMap(int currentLocation, Pieces[] boardpieces) { return AvalibleMoves(currentLocation, boardpieces); }
        public override bool[] AvalibleMoves(int currentLocation, Pieces[] boardpieces)
        {
            int[] xVariations = new int[] { -1, 1, 0, 0 };
            int[] yVariations = new int[] { 0, 0, -1, 1 };

            bool[] moves = FindMovesStraightLinesVariations(currentLocation, boardpieces, xVariations, yVariations);
            return moves;
        }
    }
    public class Knight : Pieces
    {
        public Knight(int inputcolour, Board board) : base(inputcolour, board) { ID = "N"; }
        public override bool[] ThreatMap(int currentLocation, Pieces[] boardpieces) { return AvalibleMoves(currentLocation, boardpieces); }
        public override bool[] AvalibleMoves(int currentLocation, Pieces[] boardpieces)
        {
            int[] xVariations = new int[] { -1, 1, -2, 2, -2, 2, -1, 1 };
            int[] yVariations = new int[] { 2, 2, 1, 1, -1, -1, -2, -2 };
            bool[] moves = FindMovesFromVariations(currentLocation, boardpieces, xVariations, yVariations);
            return moves;
        }
    }
    public class Bishop : Pieces
    {
        public Bishop(int inputcolour, Board board) : base(inputcolour, board) { ID = "B"; }
        public override bool[] ThreatMap(int currentLocation, Pieces[] boardpieces) { return AvalibleMoves(currentLocation, boardpieces); }
        public override bool[] AvalibleMoves(int currentLocation, Pieces[] boardpieces)
        {

            int[] xVariations = new int[] { -1, -1, 1, 1 };
            int[] yVariations = new int[] { -1, 1, -1, 1 };

            bool[] moves = FindMovesStraightLinesVariations(currentLocation, boardpieces, xVariations, yVariations);
            return moves;
        }
    }
    public class Queen : Pieces
    {
        public Queen(int inputcolour, Board board) : base(inputcolour, board) { ID = "Q"; }
        public override bool[] ThreatMap(int currentLocation, Pieces[] boardpieces) { return AvalibleMoves(currentLocation, boardpieces); }
        public override bool[] AvalibleMoves(int currentLocation, Pieces[] boardpieces)
        {
            int[] xVariations = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] yVariations = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };

            bool[] moves = FindMovesStraightLinesVariations(currentLocation, boardpieces, xVariations, yVariations);
            return moves;
        }

    }


}
