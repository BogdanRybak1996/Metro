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
using System.Windows.Media.Animation;

namespace Metro
{
    class Train
    {
        private Ellipse trainEllipse;
        private double left;
        private int numberOfLastStation = -1;
        
        
        public double Left
        {
            get { return left; }
            set { left = value; }
        }
        public Train()       //Стандартна ініціалізація
        {
            draw("Right");
        }
        public Ellipse draw(string depot)
        {
            trainEllipse = new Ellipse();
            if(depot == "Right")
            {
                left = SystemParameters.PrimaryScreenWidth - 65;
            }
            else
            {
                left = 60;      // Якщо введені неправильні дані - стандартно присвоюється ліва станція
            }
            trainEllipse.Width = 10;
            trainEllipse.Height = 10;
            trainEllipse.Fill = Brushes.Green;
            return trainEllipse;
        }
        public Ellipse getEllipse()
        {
            return trainEllipse;
        }
        public DoubleAnimation getMoveAnimation(double To,int duration)
        {
            DoubleAnimation db = new DoubleAnimation();
            db.To = To;
            db.Duration = TimeSpan.FromSeconds(duration);
            left = To;
            db.AccelerationRatio = 0.4;
            db.DecelerationRatio = 0.4;
            db.FillBehavior = FillBehavior.HoldEnd;
            return db;
        }
    }
}
