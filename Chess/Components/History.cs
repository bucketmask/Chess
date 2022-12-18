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
        List<string> DisplayHistory = new List<string>();
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

            //reset graphics
            graphics.GraphicalHistory.Reset();
        }

        //this sees if any moves where made, and if not sends a blank board
        //TODO needs work far future
        public Pieces[] GetCurrentBoard()
        {
            if (DisplayHistory.Count == 0) { return resetBoard(); }
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
            if (oldBoard[fromToLocation[0]].ID != "P")
            {
                output = output + oldBoard[fromToLocation[0]].ID;
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
                    if ((oldBoard[x].ID == oldBoard[fromToLocation[0]].ID) && (oldBoard[x].colour == oldBoard[fromToLocation[0]].colour))
                    {
                        if (oldBoard[x].AvalibleMoves(x, oldBoard)[fromToLocation[1]] == true)
                        {
                            samexy[0] = true;
                        }
                    }
                }
                if (oldBoard[y] != null && y != fromToLocation[0])
                {
                    if ((oldBoard[y].ID == oldBoard[fromToLocation[0]].ID) && (oldBoard[y].colour == movedPiece.colour))
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
            if (oldBoard[fromToLocation[1]] != null) 
            { 
                if(output.Length < 1) { output = convertLocationToRanksFiles(fromToLocation[0])[0].ToString(); }
                output = output + 'x'; 
            }
            output = output + convertLocationToRanksFiles(fromToLocation[1]);
            if (oldBoard[fromToLocation[0]] != movedPiece)
            {
                output = output + "=" + movedPiece.ID;
            }


            DisplayHistory.Add(output);
            if (graphics != null) { graphics.GraphicalHistory.AddItem(MoveNumber, output); }
            MoveNumber++;
        }
        public void LogMove(string output)
        {
            DisplayHistory.Add(output);
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
                    else
                    {
                        if (char.IsLower(fenSplit[i][j])) boardpieces[Pointer] = GetPiece(fenSplit[i][j], 1);
                        else boardpieces[Pointer] = GetPiece(fenSplit[i][j], 0); 
                    }
                    Pointer++;
                }
            }
            return boardpieces;
        }
        public Pieces GetPiece(char id, int colour)
        {
            id = char.ToLower(id);
            Pieces piece = null;
            if (id == 'p')
            {
                piece = new Pawn(colour, board);
            }
            else if (id == 'r')
            {
                piece = new Rook(colour, board);
            }
            else if (id == 'b')
            {
                piece = new Bishop(colour, board);
            }
            else if (id == 'n')
            {
                piece = new Knight(colour, board);
            }
            else if (id == 'k')
            {
                piece = new King(colour, board);
            }
            else if (id == 'q')
            {
                piece = new Queen(colour, board);
            }
            return piece;
        }
    }
}
