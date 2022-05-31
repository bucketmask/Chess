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
        string[] moveHistory = new string[512];
        public int MoveNumber;
        public int playerColour;

        //on startup the graphics is made public and set
        //move number is set to 0(index for move history) and if its even its white turn
        //and the player is set, o is white, 1 is black, and 3 is both, 3 is defult at the moment
        public History(int colourforPlayer, Graphics graphics1, Board board)
        {
            
            graphics = graphics1;
            MoveNumber = 0;
            playerColour = colourforPlayer;
            board.History = this;
        }

        //this sees if any moves where made, and if not sends a blank board
        //TODO needs work far future
        public Pieces[] GetCurrentBoard()
        {
            if (moveHistory[0] == null) { return resetBoard(); }
            else { return resetBoard(); }
        }

        //This will be able to handle imports
        //TODO again future
        public History(string PGN) { }

        //This logs the move in a given format in the move history[]
        public void LogMove(int[] toFromLocation, Pieces movedPiece, Pieces[] oldBoard) 
        { 
            MoveNumber++;
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
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Pawn(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Pawn(0); }

                    }

                    else if (fenSplit[i][j].ToString().ToLower() == "r")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Rook(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Rook(0); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "b")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Bishop(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Bishop(0); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "n")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Knight(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Knight(0); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "k")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new King(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new King(0); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "q")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new Queen(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new Queen(0); }
                    }
                    Pointer++;
                }
            }
            return boardpieces;
        }
    }
}
