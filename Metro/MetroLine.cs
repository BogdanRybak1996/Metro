using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Metro
{
    class MetroLine
    {
        private Line line = new Line();         // Лінія для відображення в вікні
        public Line draw(int y)         // Повертає лінію, яку можна додати до контейнера в вікні
        {
            line.Stroke = Brushes.Black;
            line.X1 = 60;                           // Стандартний відступ від країв вікна
            line.X2 = SystemParameters.PrimaryScreenWidth - 60; // Ширина екрану                         
            line.Y1 = y;
            line.Y2 = y;
            line.HorizontalAlignment = HorizontalAlignment.Left;
            line.VerticalAlignment = VerticalAlignment.Center;
            line.StrokeThickness = 2;
            return line;
        }        
    }
}
