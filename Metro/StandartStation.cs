using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Metro
{
    class StandartStation : Station
    {
        private int left;           //Координати при емуляції
        public int Left
        {
            get { return left; }
            set { left = value; }
        }
        private Rectangle rect;
        public Rectangle getRect()
        {
            return rect;
        }
        public override Rectangle draw()
        {
            rect = new Rectangle();
            rect.Width = 5;
            rect.Height = 50;
            rect.Fill = new SolidColorBrush(Colors.Blue);
            return rect;
        }
    }
}
