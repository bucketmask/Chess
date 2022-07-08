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
        public TCPclient client;
        public Main(Form1 form)
        {
            client = new TCPclient();
            graphics = new Graphics(form, this);
            NewGame();
        }
        public void NewGame()
        {
            board = new Board(graphics, this);
            history = new History(3, graphics, board);
            //NewGame();
    
        }
    }
}
