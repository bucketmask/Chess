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
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Name = "Chess";
            this.Text = "Chess";
            this.ResumeLayout(false);

            //panel for chess board
            Panel panel1 = new Panel();
            panel1.Location = new Point(10, 10);
            panel1.Size = new Size(400, 400);
            panel1.Margin = new Padding(0);
            panel1.Padding = new Padding(0);
            panel1.BackColor = System.Drawing.Color.Blue;


            ////makes grid
            //TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            //tableLayoutPanel1.RowCount = 8;
            //tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1/8));
            //tableLayoutPanel1.ColumnCount = 8;
            //tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1 / 8));
            //tableLayoutPanel1.Dock = DockStyle.Fill;
            //tableLayoutPanel1.BackColor = Color.Blue;
            //tableLayoutPanel1.Margin = new Padding(0);
            //tableLayoutPanel1.Padding = new Padding(0);


            //tableLayoutPanel1.Click += new EventHandler(board.userclick);

            //panel1.Controls.Add(tableLayoutPanel1);




            //chess squares as buttons with positional info


            for (int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    //Console.WriteLine($"({x}:{y}){coord[0]}:{coord[1]}");


                    PictureBox pictureBox1 = new PictureBox();
                    pictureBox1.Padding = new Padding(0, 0, 0, 0);
                    pictureBox1.Margin = new Padding(0);
                    pictureBox1.Size = new Size(panel1.Size.Width / 8, panel1.Size.Height / 8);
                    pictureBox1.Location = new Point(x * panel1.Size.Width / 8, y * panel1.Size.Height / 8);

                    if ((x % 2 == 0 && y % 2 == 1) || (x % 2 == 1 && y % 2 == 0))
                    {
                        pictureBox1.BackColor = Color.Black;
                        pictureBox1.Size = new Size(panel1.Size.Width / 8, panel1.Size.Height / 8);;

                    }
                    else 
                    {
                        pictureBox1.BackColor= Color.White;
                        pictureBox1.Size = new Size(panel1.Size.Width / 8, panel1.Size.Height / 8);
                    }

                    //this will execute a function when the botton is pressed
                    //button.Click += new  EventHandler(board.userclick);
                    pictureBox1.Click += new EventHandler(board.userclick);
                    panel1.Controls.Add((pictureBox1));
                }
            }




            //draws the chess board on
            board.DrawBoard(panel1);



            //adds the elements
            this.Controls.Add(panel1);
        }

    }
}
