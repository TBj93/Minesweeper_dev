using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Minesweeper.Classes.Container
{
    internal class CclField
    {
        internal List<CclField> Neighbours { get; }

        internal bool IsFlagged { get { return FieldButton.Image != null; } set { SetIsFlagged(value); } }

        internal bool HasMine { get; set; }

        internal bool IsShown { get { return !FieldButton.Visible; } }

        internal int NeighbouringMines { get; set; }

        internal Button FieldButton { get; }

        internal Label NumOfMines { get; }

        internal PictureBox Mine { get; }

        internal Panel Background { get; }


     


        internal CclField(Control _CtrlParent, int _iTop, int _iLeft, int _iHeight, int _iWidth )
        {
            Neighbours = new List<CclField>();

            Background = new Panel();
            NumOfMines = new Label();
            Mine = new PictureBox();
            FieldButton = new Button();
            

            Background.Controls.Add(FieldButton);
            Background.Controls.Add(Mine);
            Background.Controls.Add(NumOfMines);    

            Mine.Dock = DockStyle.Fill;
            NumOfMines.Dock = DockStyle.Fill;
            FieldButton.Dock = DockStyle.Fill;



            FieldButton.BringToFront();
            Mine.Visible = false;   
            NumOfMines.Visible = false;

            

            Background.Top = _iTop;
            Background.Left = _iLeft;
            Background.Width = _iWidth;
            Background.Height = _iHeight;


            //  FieldButton.Click += button1_Click;
            FieldButton.MouseDown += button1_MouseDown;

            _CtrlParent.Controls.Add(Background);
        }

     

        public void test()
        {
            int top = Background.Top;
            int left = Background.Left;
            int width = Background.Width;

            Console.WriteLine(top / width);
            Console.WriteLine(left/width);
            Console.WriteLine("##################");
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button  == MouseButtons.Right)
            {
               
                IsFlagged = !IsFlagged;
                
            }
            else if (e.Button == MouseButtons.Left)
            {
                Show(!IsShown);
              //  test();
                
                
            }
        }

        //HKBHJBHJ
        /// <summary>
        /// Setter für 'IsFlagged'.
        /// </summary>
        /// <param name="_bValue">Der neue Wert von 'IsFlagged'.</param>
        private void SetIsFlagged(bool _bValue)
        {
            if (IsShown)
                return;
            // end loop if field is already shown
            FieldButton.Image = _bValue ? Properties.Resources.Screenshot_2023_06_02_140931
                                        : null;
        }


        //show mine
        private void SetHasMine(bool _bValue)
        {
            if (IsShown)
                return;

            FieldButton.Image = _bValue ? Properties.Resources.Screenshot_2023_06_02_140812
                                        : null;
        }

        private void Show(bool _bValue)
        {
            if (IsShown)
                return;

            FieldButton.Image = _bValue? Properties.Resources.open
                                        : null;
    }


    //add mines  umbau nachbarminen 

}
}
