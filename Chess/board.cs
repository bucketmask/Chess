using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class board
    {
        pieces[] boardArr = new pieces[64];
        public board()
        {
            boardArr[1] = new pawn(1,0);
            boardArr[63] = new pawn(7,7);
            
        }

        public void DrawBoard(System.Windows.Forms.Panel tableLayoutPanel1)
        {
            Console.WriteLine(tableLayoutPanel1.Location);
            //every obj is drawn
            for (int i = 0; i < 64; i++)
            {
                if (boardArr[i] != null) { boardArr[i].draw(tableLayoutPanel1); }
            }
        }
        public void userclick(object sender, EventArgs e)
        {
            System.Windows.Forms.PictureBox picture = sender as System.Windows.Forms.PictureBox;
            Console.WriteLine(picture.Location);
        }
    }
}
