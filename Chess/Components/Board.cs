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
        //all graphical board uses are in, initialization and userclicks, so can make virtual board :)


        //graphic is used for mkaing the graphics board
        Graphics graphics;
        Pieces[] piecesOnBoard;
        int selectedPiece = -1;
        bool[] avalibleMoves = new bool[64];
        //history can change, and it is used to get current board, to see if their is game, contains all information
        History history;
        Main main;

        //on startup, board is given a new history, and so it makes a new board graphics and draws the pieces.
        public Board(Graphics graphics1, Main main1)
        {
            main = main1;
            graphics = graphics1;
            graphics.GraphicalBoard.CurrentBoard = this;
        }

        public History History
        {
            set 
            { 
                history = value;
                piecesOnBoard = history.GetCurrentBoard();
                if(graphics != null) { graphics.GraphicalBoard.DrawAllPiecesOnBoard(piecesOnBoard); }
            }
            get { return history; }
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
            if (fromToLocation[1] == history.enpassantMoveSquareLocation[1] && piecesOnBoard[fromToLocation[0]].ID == "P" && history.enpassantMoveSquareLocation[0] == history.MoveNumber - 1)
            {
                piecesOnBoard[history.enpassantMoveSquareLocation[2]] = null;
                piecesOnBoard[fromToLocation[1]] = piecesOnBoard[fromToLocation[0]];
                piecesOnBoard[fromToLocation[0]] = null;
            }
            else
            {
                piecesOnBoard[fromToLocation[1]] = piecesOnBoard[fromToLocation[0]];
                piecesOnBoard[fromToLocation[0]] = null;
            }

        }

        public bool[] CanCastleQueenKingSide(int colour)
        {
            int[] colourRank = { 7, 0 };
            int[] blankLocationsX = { 1, 2, 3, 5, 6 };
            int[] pieceLocations = { 0, 4, 7 };
            bool[] queenKingside = { true, true };
            for(int i = 0; i < 5; i++)
            {
                int[] xy = { blankLocationsX[i], colourRank[colour] };
                int location = ConvertXYToLocation(xy);
                if (piecesOnBoard[location] != null && (piecesOnBoard[location].ID != "K" && piecesOnBoard[location].ID != "R"))
                {
                    if(i <= 2) { queenKingside[0] = false; }
                    else { queenKingside[1] = false; }
                }
            }
            for (int n = 0; n < 3; n++)
            {
                int[] xy = { pieceLocations[n], colourRank[colour] };
                int location = ConvertXYToLocation(xy);

                if (piecesOnBoard[location] == null || (piecesOnBoard[location].ID != "K" && piecesOnBoard[location].ID != "R") || piecesOnBoard[location].hasMoved == true)
                {
                    if(n == 0 || n == 1) { queenKingside[0] = false; }
                    if(n == 2 || n == 1) { queenKingside[1] = false; }
                }
            }
            //
            for(int y = 0; y < queenKingside.Length; y++)
            {
                Console.WriteLine(queenKingside[y]);
            }
            //
            return queenKingside;
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
            //king
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
        public bool[] GetAvalibleMoves(int location)
        {

            avalibleMoves = piecesOnBoard[location].AvalibleMoves(location, piecesOnBoard);
            for(int j = 0; j < avalibleMoves.Length; j++)
            {
                if(avalibleMoves[j] == true)
                {
                    int[] fromToLocation = { location, j };
                    if (isMoveLegal(fromToLocation) == false)
                    {
                        avalibleMoves[j] = false;
                    }
                }
            }
            return avalibleMoves;
        }

        private bool isMoveLegal(int[] fromToLocation)
        {
            //creats backup of board
            Pieces[] originalBoard = new Pieces[64];
            Array.Copy(piecesOnBoard, originalBoard, piecesOnBoard.Length);

            //does move
            move(fromToLocation);

            //see if the YOUR king is in check 
            if (IsKingInCheck(piecesOnBoard, piecesOnBoard[fromToLocation[1]].colour) == false)
            {
                Array.Copy(originalBoard, piecesOnBoard, piecesOnBoard.Length);
                return true;
            }
            else
            {
                Array.Copy(originalBoard, piecesOnBoard, piecesOnBoard.Length);
                return false;
            }
        }

        private int nextColour(int currentColour)
        {
            if (currentColour == 0) { return 1; }
            else { return 0; }
        }

        //this is what happens when the user clicks on the board, gets sent the xylocation
        public void UserClicked(object sender, EventArgs e)
        {
            int playerColour = history.playerColour;
            int playerTurn = history.MoveNumber % 2;
            PictureBox pictureBox = sender as PictureBox;
            if (playerColour == 3) { playerColour = playerTurn; }
            //Console.WriteLine($"{pictureBox.Location}");
            int location = pictureBox.Location.X / (GraphicalBoard.chessBoardSize / 8) + pictureBox.Location.Y / (GraphicalBoard.chessBoardSize / 8) * 8;
            //Console.WriteLine(place);

            if (playerColour == playerTurn)
            {
                if (selectedPiece == -1 && piecesOnBoard[location] != null && piecesOnBoard[location].colour == playerTurn)
                {
                    selectedPiece = location;
                    avalibleMoves = GetAvalibleMoves(location);
                    graphics.GraphicalBoard.ColourSquaresFromAvalibleMoves(avalibleMoves);
                }
                else if (selectedPiece != -1 && avalibleMoves[location] == true)
                {
                    //makes move
                    int[] fromToLocation = { selectedPiece, location };
                    //
                    CanCastleQueenKingSide(playerTurn);
                    //
                    history.LogMove(fromToLocation, piecesOnBoard[selectedPiece], piecesOnBoard);
                    move(fromToLocation);
                    if (piecesOnBoard[fromToLocation[1]].ID == "K" || piecesOnBoard[fromToLocation[1]].ID == "R") { piecesOnBoard[fromToLocation[1]].hasMoved = true; }
                    graphics.GraphicalBoard.ResetColourOfBackSquares();
                    graphics.GraphicalBoard.MovePiece(fromToLocation);
                    if (history.enpassantMoveSquareLocation[0] == history.MoveNumber - 2 && fromToLocation[1] == history.enpassantMoveSquareLocation[1] && piecesOnBoard[fromToLocation[1]].ID == "P")
                    {
                        graphics.GraphicalBoard.CheckBoard(piecesOnBoard);
                    }
                    selectedPiece = -1;
                    if (tick(playerColour) == false)
                    {
                        if (IsKingInCheck(piecesOnBoard, nextColour(playerTurn)))
                        {
                            Console.WriteLine("checkmate");
                        }
                        else { Console.WriteLine("stalemate"); }
                        Console.WriteLine("end");
                        main.NewGame();
                    }

                }
                else if (selectedPiece != -1 && avalibleMoves[location] == false && piecesOnBoard[location] != null && piecesOnBoard[location].colour == playerTurn)
                {
                    selectedPiece = location;
                    avalibleMoves = GetAvalibleMoves(location);
                    graphics.GraphicalBoard.ResetColourOfBackSquares();
                    graphics.GraphicalBoard.ColourSquaresFromAvalibleMoves(avalibleMoves);
                }
                else
                {
                    selectedPiece = -1;
                    graphics.GraphicalBoard.ResetColourOfBackSquares();
                }
            }
            else { Console.WriteLine("notgo"); selectedPiece = -1; }
        }
    }
}
