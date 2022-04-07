using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    public class pieces
    {
        public int x;
        public int y;

        public pieces(int inputX, int inputY)
        {
            x = inputX;
            y = inputY;
        }

        public virtual void draw(System.Windows.Forms.Panel tableLayoutPanel1)
        {
            Console.WriteLine("1");
        }
    }

    public class pawn : pieces
    {
        public pawn(int x, int y) : base(x,y)
        { 
            Console.WriteLine(x.ToString());
        }
        public override void draw(System.Windows.Forms.Panel tableLayoutPanel1)
        {
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Image = Image.FromFile("C:/Users/liamp/source/repos/Chess/Chess/assets/bP.png");
            pictureBox1.Padding = new Padding(0,0,0,0);
            pictureBox1.Size = new Size(tableLayoutPanel1.Width/8,tableLayoutPanel1.Height/8);
            Console.WriteLine($"{x * tableLayoutPanel1.Width / 8}:{y * tableLayoutPanel1.Height / 8}");
            pictureBox1.Location = new Point(x* tableLayoutPanel1.Width / 8, y* tableLayoutPanel1.Height / 8);
            //pictureBox1.BackColor = Color.Yellow;
            tableLayoutPanel1.Controls.Add(pictureBox1);
            pictureBox1.BringToFront();
        }
    }
}
