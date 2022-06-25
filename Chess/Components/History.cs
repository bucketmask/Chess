using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
namespace Chess
{
    //this class contains the high level information of the chess game,
    //like, player, player colour, if their is a game, past moves and other important info
    public class History
    {
        //needs graphics component
        Graphics graphics;
        const string startingFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        //const string startingFen = "nbqrkrbn/pppppppp/8/8/8/8/PPPPPPPP/NBQRKRBN";
        //Each pointer?? is a halfmove
        //so even is white, odd is black moves
        List<string> moveHistory = new List<string>();
        public int[] enpassantMoveSquareLocation = new int[3];
        public int MoveNumber;
        public int playerColour;
        Board board;

        //on startup the graphics is made public and set
        //move number is set to 0(index for move history) and if its even its white turn
        //and the player is set, o is white, 1 is black, and 3 is both, 3 is defult at the moment
        public History(int colourforPlayer, Graphics graphics1, Board board1)
        {
            
            graphics = graphics1;
            board = board1;
            MoveNumber = 0;
            playerColour = colourforPlayer;
            board.History = this;
        }

        //this sees if any moves where made, and if not sends a blank board
        //TODO needs work far future
        public Pieces[] GetCurrentBoard()
        {
            if (moveHistory.Count == 0) { return resetBoard(); }
            else { return resetBoard(); }
        }

        //This will be able to handle imports
        //TODO again future
        public History(string PGN) { }

        //This logs the move in a given format in the move history[]
        public void LogMove(int[] fromToLocation, Pieces movedPiece, Pieces[] oldBoard) 
        {
            string output = "";
            string movedPieceRankFile = convertLocationToRanksFiles(fromToLocation[0]);
            int[] fromxy = Board.ConvertLocationToXY(fromToLocation[0]);
            bool[] samexy = { false, false };
            if (movedPiece.ID != "P")
            {
                output = output + movedPiece.ID;
            }
            else if (Math.Abs(fromToLocation[0] - fromToLocation[1]) == 16)
            {
                enpassantMoveSquareLocation[0] = MoveNumber;
                enpassantMoveSquareLocation[2] = fromToLocation[1];
                if(movedPiece.colour == 1)
                {
                    enpassantMoveSquareLocation[1] = fromToLocation[0] + 8;
                }
                else
                {
                    enpassantMoveSquareLocation[1] = fromToLocation[0] - 8;
                }
            }
            //loopx, loopy, if both do, if one do, if none, none
            for(int i = 0; i < 8; i++)
            {
                int x = (fromxy[1] * 8) + i;
                int y = (8 * i) + fromxy[0];
                if (oldBoard[x] != null && x != fromToLocation[0])
                {
                    if ((oldBoard[x].ID == movedPiece.ID) && (oldBoard[x].colour == movedPiece.colour))
                    {
                        if (oldBoard[x].AvalibleMoves(x, oldBoard)[fromToLocation[1]] == true)
                        {
                            samexy[0] = true;
                        }
                    }
                }
                if (oldBoard[y] != null && y != fromToLocation[0])
                {
                    if ((oldBoard[y].ID == movedPiece.ID) && (oldBoard[y].colour == movedPiece.colour))
                    {
                        if (oldBoard[y].AvalibleMoves(y, oldBoard)[fromToLocation[1]] == true)
                        {
                            samexy[1] = true;
                        }
                    }
                }
            }
            if (samexy[0]) { output = output + movedPieceRankFile[0]; }
            if (samexy[1]) { output = output + movedPieceRankFile[1]; }
            if (oldBoard[fromToLocation[1]] != null) { output = output + 'x'; }
            output = output + convertLocationToRanksFiles(fromToLocation[1]);


            moveHistory.Add(output);
            if (graphics != null) { graphics.GraphicalHistory.AddItem(MoveNumber, output); }
            MoveNumber++;
        }
        public void LogMove(string output)
        {
            moveHistory.Add(output);
            if (graphics != null) { graphics.GraphicalHistory.AddItem(MoveNumber, output); }
            MoveNumber++;
        }

        private string convertLocationToRanksFiles(int location)
        {
            char[] files = { 'h', 'g', 'f', 'e', 'd', 'c', 'b', 'a', };
            location = 63 - location;
            int[] xy = Board.ConvertLocationToXY(location);
            string rank = (xy[1] + 1).ToString();
            string file = files[xy[0]].ToString();
            return file + rank;
        }

        //this returns the pieces of the intial board, can be used for future things where you have to add moves from a intial situation
        Pieces[] resetBoard()
        {
            Pieces[] boardpieces = new Pieces[64];
            string[] fenSplit = startingFen.Split('/');
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
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Pawn(1, board); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Pawn(0, board); }

                    }

                    else if (fenSplit[i][j].ToString().ToLower() == "r")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Rook(1, board); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Rook(0, board); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "b")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Bishop(1, board); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Bishop(0, board); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "n")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Knight(1, board); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Knight(0, board); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "k")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new King(1, board); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new King(0, board); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "q")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Queen(1, board); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Queen(0, board); }
                    }
                    Pointer++;
                }
            }
            return boardpieces;
        }
    }
}
