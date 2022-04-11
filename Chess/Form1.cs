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

            int borderSize = 20;
            int chessBoardSize = 480;


            //form
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Name = "Chess";
            this.Text = "Chess";
            this.ResumeLayout(false);

            //panel for chess board
            Panel panel1 = new Panel();
            panel1.Location = new Point(10, 10);
            //has to be a multiple of 8
            panel1.Size = new Size(chessBoardSize, chessBoardSize);
            panel1.Margin = new Padding(0);
            panel1.Padding = new Padding(0);
            panel1.BackColor = Color.Blue;

            //panle for chess background
            Panel panel2 = new Panel();
            panel2.Location = new Point(10, 10);
            //has to be a multiple of 8
            panel2.Size = new Size(chessBoardSize + borderSize, chessBoardSize + borderSize);
            panel2.Margin = new Padding(0);
            panel2.Padding = new Padding(0);
            panel2.BackColor = Color.Black;



            //creates the chess board class
            board board = new board(panel1);




            //adds the elements
            this.Controls.Add(panel2);
            panel2.Controls.Add(panel1);

        }

    }
}
