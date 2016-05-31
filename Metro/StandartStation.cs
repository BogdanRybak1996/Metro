using System;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Metro
{
    class StandartStation : Station
    {
        private double left;           //Координати при емуляції
        private double top;
        DateTime durationLastTrain = new DateTime(1, 1, 1, 0, 0, 0);
        StackPanel panel;
        ToolTip tooltip;
        TextBlock board = new TextBlock { Text = "Час відправки останнього потягу: " + "-" + "\n" + "Час стоянки останнього потягу: " + "-",FontSize=12 };
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
        public void updateBoard(DateTime lastTrain, int duration)
        {
            panel.Children.Remove(board);
            DateTime localLastTrain = new DateTime(1, 1, 1, lastTrain.Hour, lastTrain.Minute, lastTrain.Second);
            if(duration == 0)
            {
                durationLastTrain = new DateTime(1,1,1,0,0,0);
            }
            durationLastTrain = durationLastTrain.AddSeconds(duration);
            board.Text = "Час відправки останнього потягу: " + localLastTrain.ToString("T") + "\n" + "Час стоянки останнього потягу: " + durationLastTrain.ToString("T");
            panel.Children.Add(board);
            tooltip.Content = panel;
            rect.ToolTip = tooltip;
        }
        public void resetDurationLastTrain()
        {
            durationLastTrain = new DateTime(1,1,1,0,0,0);
        }
        public override Rectangle draw()
        {
            rect = new Rectangle();
            tooltip = new ToolTip();
            panel = new StackPanel();
            panel.Children.Add(new TextBlock { Text = "Табло станції "+"\""+Name+"\"",FontSize=16 });
            panel.Children.Add(board);
            tooltip.Content = panel;
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
