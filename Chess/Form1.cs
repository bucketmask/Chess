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


            //creates the chess board class
            board board = new board(panel1);




            //adds the elements
            this.Controls.Add(panel1);
        }

    }
}
