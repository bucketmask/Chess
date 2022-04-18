using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    //the board class is constant
    public class Board
    {
        //=++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++=
        //  location is another word for the index of the piece in the pieces[] of 64 which represents the board
        //  All components of the application will follow this structure
        //=++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++=

        //graphic is used for mkaing the graphics board
        Graphics graphics;
        Pieces[] piecesOnBoard;
        int selectedPiece = -1;
        bool[] avalibleMoves = new bool[64];
        //history can change, and it is used to get current board, to see if their is game, contains all information
        History history;
        Graphics.GraphicalBoard GraphicalBoard;

        //on startup, board is given a new history, and so it makes a new board graphics and draws the pieces.
        public Board(History history1, Graphics graphics1)
        {
            graphics = graphics1;
            history = history1;
            GraphicalBoard = new Graphics.GraphicalBoard(graphics, this);
            piecesOnBoard = history.GetCurrentBoard();
            GraphicalBoard.DrawAllPiecesOnBoard(piecesOnBoard);
        }

        //converts from arr index to a usable format(X,Y co-ordinates)
        public static int[] CovertLocationToXY(int location)
        {
            int[] xY = new int[2] { location % 8, location / 8 };
            return xY;
        }
        public static int ConvertXYToLocation(int[] xY)
        {
            int location = (xY[1] * 8) + xY[0];
            return location;
        }

        //this moves a piece both in the pieces[] and in the graphical realm
        void move(int[] fromToLocation)
        {
            piecesOnBoard[fromToLocation[1]] = piecesOnBoard[fromToLocation[0]];
            piecesOnBoard[fromToLocation[0]] = null;

        }

        bool IsKingInCheck(Pieces[] piecesOnBoard, int coloursKing)
        {
            bool[] threatMap = new bool[64];
            bool kingInCheck = false;
            int kingLocation = -1;
            //first find the kings location
            for (int i = 0; i < piecesOnBoard.Length; i++)
            {
                if (piecesOnBoard[i] != null && piecesOnBoard[i].ID == "K" && piecesOnBoard[i].colour == coloursKing)
                {
                    kingLocation = i;
                    break;
                }
            }

            //then see if the king is in threat
            for (int i = 0; i < piecesOnBoard.Length; i++)
            {
                if (piecesOnBoard[i] != null && piecesOnBoard[i].colour != coloursKing && kingLocation != -1)
                {
                    bool[] tempThreatMap;
                    tempThreatMap = piecesOnBoard[i].ThreatMap(i, piecesOnBoard);
                    if (tempThreatMap[kingLocation] == true)
                    {
                        kingInCheck = true;
                        break;
                    }
                }
            }
            Console.WriteLine(kingInCheck);
            return kingInCheck;
        }

        bool tick(int colour)
        {

            bool nextColourCanMove = false;
            int nextColour;
            if(colour == 0) { nextColour = 1; }
            else { nextColour = 0; }

            for (int i = 0; i < piecesOnBoard.Length; i++)
            {
                if (piecesOnBoard[i] != null && piecesOnBoard[i].colour == nextColour && nextColourCanMove == false)
                {
                    //test all avalible moves for every piece of the colour of the next turn
                    bool[] avalibleMoves = piecesOnBoard[i].AvalibleMoves(i, piecesOnBoard);
                    for (int j = 0; j < avalibleMoves.Length; j++)
                    {
                        if (avalibleMoves[j] == true)
                        {
                            Pieces[] originalBoard = new Pieces[64];
                            Array.Copy(piecesOnBoard, originalBoard, piecesOnBoard.Length);

                            int[] toFromLocation = new int[2] { i, j };
                            move(toFromLocation);
                            if (IsKingInCheck(piecesOnBoard, nextColour) == false)
                            {
                                nextColourCanMove = true;
                                Array.Copy(originalBoard, piecesOnBoard, piecesOnBoard.Length);
                                break;
                            }
                            Array.Copy(originalBoard, piecesOnBoard, piecesOnBoard.Length);
                        }
                    }
                }
            }
            return nextColourCanMove;
        }

        //this is what happens when the user clicks on the board, gets sent the xylocation
        public void UserClicked(object sender, EventArgs e)
        {
            int playerColour = history.playerColour;
            int playerTurn = history.MoveNumber % 2;
            PictureBox pictureBox = sender as PictureBox;
            if (playerColour == 3) { playerColour = playerTurn; }
            //Console.WriteLine($"{pictureBox.Location}");
            int location = pictureBox.Location.X / (Graphics.GraphicalBoard.chessBoardSize / 8) + pictureBox.Location.Y / (Graphics.GraphicalBoard.chessBoardSize / 8) * 8;
            //Console.WriteLine(place);

            if (playerColour == playerTurn)
            {
                if (selectedPiece == -1 && piecesOnBoard[location] != null && piecesOnBoard[location].colour == playerTurn)
                {
                    selectedPiece = location;
                    avalibleMoves = piecesOnBoard[location].AvalibleMoves(location, piecesOnBoard);
                }
                else if (selectedPiece != -1 && avalibleMoves[location] == true)
                {
                    int[] fromToLocation = new int[] { selectedPiece, location };
                    Pieces[] temboard = new Pieces[64];
                    Array.Copy(piecesOnBoard, temboard, temboard.Length);
                    move(fromToLocation);
                    Console.WriteLine("d");
                    if (IsKingInCheck(piecesOnBoard, playerTurn))
                    {
                        Array.Copy(temboard, piecesOnBoard, temboard.Length);
                    }
                    else
                    {
                        //............
                        history.LogMove(fromToLocation, piecesOnBoard[selectedPiece], piecesOnBoard);
                        GraphicalBoard.MovePiece(fromToLocation);
                        selectedPiece = -1;
                        if (tick(playerColour) == false)
                        {
                            int nextColour;
                            if (playerTurn == 0) { nextColour = 1; }
                            else { nextColour = 0; }
                            if (IsKingInCheck(piecesOnBoard, nextColour))
                            {
                                Console.WriteLine("checkmate");
                            }
                            else { Console.WriteLine("stalemate"); }
                        }
                        //.............................
                    }
                }
                else if (selectedPiece != -1 && avalibleMoves[location] == false && piecesOnBoard[location] != null && piecesOnBoard[location].colour == playerTurn)
                {
                    selectedPiece = location;
                    avalibleMoves = piecesOnBoard[location].AvalibleMoves(location, piecesOnBoard);
                }
                else
                {
                    selectedPiece = -1;
                }
            }
            else { Console.WriteLine("notgo"); selectedPiece = -1; }
        }
    }
}
