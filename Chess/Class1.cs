using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class board
    {
        public board()
        {
            Console.WriteLine("board");
        }

        public void DrawBoard()
        {
            Console.WriteLine("Draw");
        }
        public void userclick(object sender, EventArgs e)
        {
            System.Windows.Forms.Button buttonpressed = sender as System.Windows.Forms.Button;
            Console.WriteLine(buttonpressed.Location);
        }
    }
}
