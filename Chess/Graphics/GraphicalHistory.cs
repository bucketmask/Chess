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
    public class GraphicalHistory
    {
        Graphics graphics;
        int startlocation;
        int tableSize = 200;
        int borderPadding = 40;
        ListView table;
        List<ListViewItem> items;
        public GraphicalHistory(Graphics parentGraphics)
        {
            graphics = parentGraphics;

            table = new ListView();
            items = new List<ListViewItem>();

            //table size
            startlocation = GraphicalBoard.chessBoardSize + borderPadding;
            table.Bounds = new Rectangle(new Point(startlocation, 30), new Size(tableSize, GraphicalBoard.chessBoardSize + 20));
            table.Margin = new Padding(0);
            table.Padding = new Padding(0);

            //table config
            table.View = View.Details;
            table.AllowColumnReorder = false;
            table.FullRowSelect = false;
            table.GridLines = false;

            // Create columns for the items and subitems.
            int columLength = (tableSize - (tableSize / 5)) / 2 - 2;
            table.Columns.Add("Move", tableSize / 5, HorizontalAlignment.Center);
            table.Columns.Add("White", columLength, HorizontalAlignment.Center);
            table.Columns.Add("Black", columLength, HorizontalAlignment.Center);
            graphics.graphicsForm.Controls.Add(table);
        }

        public void AddItem(int movenumber, string move)
        {
            ListViewItem newItem;
            if (movenumber % 2 == 0)
            {
                newItem = new ListViewItem((movenumber / 2 + 1).ToString() + ".", 0);
                items.Add(newItem);
                table.Items.Add(newItem);
            }
            else
            {
                newItem = items[movenumber / 2];
            }

            newItem.SubItems.Add(move);
        }

        public void Reset()
        {
            items.Clear();
            table.Items.Clear();
        }
    }
}
