using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    //this contains all the graphical code for the chess apllication
    // the diffrent components are nested in the Graphics class, BUT IS DOESNT DO ANYTHING!!!!, sadly
    public class Graphics
    {
        public Form graphicsForm;
        public GraphicalBoard GraphicalBoard;
        public GraphicalHistory GraphicalHistory;
        public GraphicalNavBar GraphicalNavBar;
        public GraphicalServerConnect GraphicalServerConnect;
        public GraphicalImport GraphicalImport;
        //public GraphicalOpeningNames GraphicalOpeningNames;
        public Graphics(Form1 form, Main main)
        {
            this.graphicsForm = form;
            //form
            graphicsForm.SuspendLayout();
            graphicsForm.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            graphicsForm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            graphicsForm.BackColor = System.Drawing.Color.White;
            graphicsForm.ClientSize = new System.Drawing.Size(1000, 700);
            graphicsForm.Name = "Chess";
            graphicsForm.Text = "Chess";
            graphicsForm.ResumeLayout(false);
            graphicsForm.Click += new EventHandler(UserClicked);

            //intilizes Graphical components in the graphics object, Gateway
            GraphicalNavBar = new GraphicalNavBar(this);
            GraphicalImport = new GraphicalImport(this, main);
            GraphicalServerConnect = new GraphicalServerConnect(this, main);
            GraphicalBoard = new GraphicalBoard(this);
            GraphicalHistory = new GraphicalHistory(this);
            //GraphicalOpeningNames = new GraphicalOpeningNames(this);

            TextBox tb = new TextBox();
            graphicsForm.Controls.Add(tb);
            tb.KeyPress += new KeyPressEventHandler(KeyDown);


            graphicsForm.KeyPress += new KeyPressEventHandler(KeyDown);
        }
        public void UserClicked(object sender, EventArgs e)
        {
            //hides all other components when click off
            GraphicalImport.hide();
            GraphicalServerConnect.hide();
        }
        private void KeyDown(object sender, EventArgs e)
        {
            Console.WriteLine("sad");
        }

    }
}
