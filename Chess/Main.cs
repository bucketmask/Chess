using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Main
    {
        public Graphics Graphics;
        public Main(Form1 form)
        {
            Graphics = new Graphics(form);
            //creates the chess board class
            History history = new History(3, Graphics);
            Board board = new Board(history, Graphics);
        }
    }
}
