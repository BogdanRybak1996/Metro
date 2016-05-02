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
    class Depot: Station
    {
        private Rectangle rect;   // Для малювання
        public TextBlock getCaption(string textAligment)
        {
            TextBlock caption = base.getCaption();
            caption.FontSize = 13;
            if(textAligment == "left") {                    // Назви депо вирівнюються по різному
                caption.TextAlignment = TextAlignment.Left;
            }
            if(textAligment == "right")
            {
                caption.TextAlignment = TextAlignment.Right;
            }
            caption.TextWrapping = TextWrapping.Wrap;
            caption.Width = 95;
            CaptionHeight = caption.ActualHeight;
            return caption;
        }
        public Depot(string name) : base(name) // Викликаємо батьківський конструктор
        {
        }
        public Rectangle getRect()
        {
            return rect;
        }
        public override Rectangle draw()
        {
            rect = new Rectangle();
            rect.Width = 50;
            rect.Height = 50;
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"src\depot.png",UriKind.Relative);
            img.EndInit();
            rect.Fill = new ImageBrush(img);
            return rect;
        }        
    }
}
