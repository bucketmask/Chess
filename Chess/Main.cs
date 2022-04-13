using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Main
    {
        public Main(Form1 form)
        {
            Graphics graphics = new Graphics(form);
            //creates the chess board class
            Graphics.GraphicalBoard graphicalBoard = new Graphics.GraphicalBoard(form);
            History history = new History();
            Board board = new Board(history, graphicalBoard);
        }
    }
}
