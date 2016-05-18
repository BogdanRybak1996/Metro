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

namespace Metro
{
    class StandartStation : Station
    {
        private double left;           //Координати при емуляції
        private double top;
        public StandartStation(string name) : base(name) { }            // Викликаємо батьківський конструктор
        public double Top
        {
            get { return top; }
            set { top = value; }
        }
        public double Left
        {
            get { return left; }
            set { left = value; }
        }
        private Rectangle rect;
        public Rectangle getRect()
        {
            return rect;
        }
        public override TextBlock getCaption()
        {
             TextBlock caption = base.getCaption();
            if (MainWindow.CountOfStations < 10)
            {
                caption.FontSize = 12;
                caption.Width = 120;
            }
            if(MainWindow.CountOfStations >=10 && MainWindow.CountOfStations <= 17)
            {
                caption.FontSize = 10;
                caption.Width = 90;
            }
            if(MainWindow.CountOfStations > 17)
            {
                caption.FontSize = 9;
                caption.Width = 73;
            }

            caption.TextAlignment = System.Windows.TextAlignment.Center;
            caption.TextWrapping = System.Windows.TextWrapping.Wrap;
            CaptionHeight = caption.ActualHeight;
            return caption;
        }
        public override Rectangle draw()
        {
            rect = new Rectangle();
            ToolTip tooltip = new ToolTip();
            StackPanel toolTipPanel = new StackPanel();
            toolTipPanel.Children.Add(new TextBlock { Text = "Табло станції "+"\""+Name+"\"",FontSize=16 });
            tooltip.Content = toolTipPanel;
            //Буде табло на кожній станції
            rect.ToolTip = tooltip;
            rect.Width = 31.2;
            rect.Height = 36.3;
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"src\station.png",UriKind.Relative);
            img.EndInit();
            rect.Fill = new ImageBrush(img);
            return rect;
        }
    }
}
