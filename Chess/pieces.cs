using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class Pieces
    {
        public static int[] CovertLocationToXY(int location)
        {
            int[] xY = new int[2] {location % 8, location / 8};
            return xY;
        }
    }
}
