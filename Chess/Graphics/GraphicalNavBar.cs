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
    public class GraphicalNavBar
    {
        Graphics graphics;
        MenuStrip menu;
        public GraphicalNavBar(Graphics parentGraphics)
        {
            graphics = parentGraphics;
            menu = new MenuStrip();
            menu.BackColor = Color.White;

            Panel backPanel = new Panel();
            backPanel.Location = new Point(0, 0);
            backPanel.Size = new Size(graphics.graphicsForm.Size.Width, menu.Size.Height + 2);
            backPanel.Margin = new Padding(0);
            backPanel.Padding = new Padding(0);
            backPanel.BackColor = Color.Gray;

            ToolStripMenuItem importButton = new ToolStripMenuItem();
            importButton.Text = "Import";
            importButton.TextAlign = ContentAlignment.BottomRight;
            menu.Items.Add(importButton);
            importButton.Click += new EventHandler(import);

            ToolStripMenuItem connectButton = new ToolStripMenuItem();
            connectButton.Text = "Connect";
            connectButton.TextAlign = ContentAlignment.BottomRight;
            menu.Items.Add(connectButton);
            connectButton.Click += new EventHandler(connect);


            graphics.graphicsForm.Controls.Add(backPanel);
            backPanel.Controls.Add(menu);
        }

        private void connect(object sender, EventArgs e)
        {
            graphics.UserClicked(sender, e);
            graphics.GraphicalServerConnect.show();
        }
        private void import(object sender, EventArgs e)
        {
            graphics.UserClicked(sender, e);
            graphics.GraphicalImport.show();
        }

    }
}

