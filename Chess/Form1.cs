using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DrawChessGUI();
        }


        public void DrawChessGUI()
        {
            //creates the chess board class
            board board = new board();


            //form
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Name = "Chess";
            this.Text = "Chess";
            this.ResumeLayout(false);

            //panel for chess board
            Panel panel1 = new Panel();
            panel1.Location = new Point(0, 0);
            panel1.Size = new Size(400, 400);
            panel1.Margin = new Padding(0);
            panel1.Padding = new Padding(0);
            panel1.BackColor = System.Drawing.Color.White;



            //chess squares as buttons with positional info
            for(int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    //Console.WriteLine($"({x}:{y}){coord[0]}:{coord[1]}");

                    Button button = new Button();
                    button.Location = new Point(panel1.Size.Width / 8 * x, panel1.Size.Height / 8 * y);
                    button.Size = new Size(panel1.Size.Width/8, panel1.Size.Height/8);
                    button.AutoSize = true;
                    button.Margin = new Padding(0);
                    button.Padding = new Padding(0);
                    if ((x % 2 == 0 && y % 2 == 1) || (x % 2 == 1 && y % 2 == 0))
                    {
                        button.BackColor = System.Drawing.Color.Black;
                    }
                    else 
                    {
                        button.BackColor = System.Drawing.Color.White;
                    }

                    //this will execute a function when the botton is pressed
                    button.Click += new  EventHandler(board.userclick);
                    panel1.Controls.Add(button);
                }
            }




            //draws the chess board on
            board.DrawBoard();



            //adds the elements
            this.Controls.Add(panel1);
        }

    }
}
