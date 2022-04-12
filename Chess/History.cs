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
        string[] GameHistory = new string[512];
        public int MoveNumber;
        string startingFEN;


        public History()
        {
            MoveNumber = 0;
            startingFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        }


    
        public History(string pgn) { }



        public void LogMove(pieces piece) 
        {
            MoveNumber++;
        }


        //creats the board and puts the pieses object into a array
        //returns the array
        public pieces[] CreateBoardFromFen()
        {
            pieces[] boardpieces = new pieces[64];

            string[] fenSplit = startingFEN.Split('/');

            //uses a pointer
            //im amazing
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
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new pawn(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new pawn(0); }

                    }

                    else if (fenSplit[i][j].ToString().ToLower() == "r")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new rook(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new rook(0); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "b")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new bishop(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new bishop(0); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "n")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new knight(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new knight(0); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "k")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new king(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new king(0); }
                    }
                    else if (fenSplit[i][j].ToString().ToLower() == "q")
                    {
                        if (char.IsLower(fenSplit[i][j])) { boardpieces[Pointer] = new queen(1); }
                        else if (char.IsUpper(fenSplit[i][j])) { boardpieces[Pointer] = new queen(0); }
                    }
                    Pointer++;
                }
            }
            return boardpieces;
        }




    }
}
