using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Main
    {
        Graphics graphics;
        History history;
        Board board;
        int num = 0;
        public Main(Form1 form)
        {
            graphics = new Graphics(form);
            //creates the chess board class
            history = new History(3, graphics);
            board = new Board(history, graphics, this);
        }
        public void NewGame()
        {
            history = new History(3, graphics);
            board = new Board(history, graphics, this);
            Console.WriteLine(num);
            num++;
            NewGame();
        }
    }
}
