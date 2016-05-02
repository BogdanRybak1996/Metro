﻿using System;
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
        public Depot(string name)
        {
            Name = name;
            setDisplay(name);
        }
        public override Label getDisplay()      // В депо надписи будуть більшими
        {
            Label display = base.getDisplay();
            display.FontSize = 16;
            return display;
        }
        public Rectangle getRect()
        {
            return rect;
        }
        public override Rectangle draw()
        {
            rect = new Rectangle();
            rect.Width = 30;
            rect.Height = 50;
            rect.Fill = new SolidColorBrush(Colors.Green);
            return rect;
        }        
    }
}
