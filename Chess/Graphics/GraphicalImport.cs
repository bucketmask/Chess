using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace Chess
{
    public class GraphicalImport
    {
        Graphics graphics;
        Panel backpanel;
        TCPclient client;
        int[] sizeXY = { 200, 100 };
        public TextBox textBox;
        public GraphicalImport(Graphics parentGraphics, Main main)
        {
            graphics = parentGraphics;
            backpanel = new Panel();
            backpanel.Size = new Size(sizeXY[0], sizeXY[1]);
            backpanel.Location = new Point(4, 28);
            backpanel.BackColor = Color.WhiteSmoke;

            textBox = new TextBox();
            textBox.Size = new Size(sizeXY[0] / 2, sizeXY[1] / 2 );
            textBox.Location = new Point(sizeXY[0] / 10, sizeXY[1] / 10);
            textBox.Multiline = true;
            textBox.Text = "ksaklnd";

            Button button = new Button();
            button.Text = "import";
            button.Size = new Size(50, 25);
            button.Location = new Point(150, 70);
            button.Click += new EventHandler(import);



            backpanel.Controls.Add(button);
            backpanel.Controls.Add(textBox);
        }
        public void show()
        {
            graphics.graphicsForm.Controls.Add(backpanel);
            backpanel.BringToFront();
        }
        public void hide()
        {
            graphics.graphicsForm.Controls.Remove(backpanel);
        }
        private void import(object sender, EventArgs e)
        {
            graphics.UserClicked(sender, e);
            Console.WriteLine(textBox.Text);
        }

    }
}