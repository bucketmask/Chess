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
    public class GraphicalServerConnect
    {
        Graphics graphics;
        Panel backpanel;
        TCPclient client;
        int[] sizeXY = { 400, 200 };
        public GraphicalServerConnect(Graphics parentGraphics, Main main)
        {
            graphics = parentGraphics;
            client = main.client;
            backpanel = new Panel();
            backpanel.Size = new Size(sizeXY[0], sizeXY[1]);
            backpanel.Location = new Point(4, 28);
            backpanel.BackColor = Color.WhiteSmoke;

        }
        public void show()
        {
            graphics.graphicsForm.Controls.Add(backpanel);
            if(client.connected != true)
            {
                AskServerIP();
            }
            backpanel.BringToFront();
        }
        public void hide()
        {
            graphics.graphicsForm.Controls.Remove(backpanel);
        }

        public void AskServerIP()
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(sizeXY[0]/ 2, sizeXY[1]/ 2);
            string var = textBox.Text;
            backpanel.Controls.Add(textBox);
        }
    }
}