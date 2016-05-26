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
        bool status = true;         // true - run, false - stay on station
        private Ellipse trainEllipse;
        private double left;
        private int numberOfNextStation = 0;
        private int intervalTime;
        private int stayTime;
        private int timeOfDelay = 0;
        private int timeAfterStart = 0;

        public int TimeOfDelay
        {
            get { return timeOfDelay; }
            set { timeOfDelay = value; }
        }
        public int IntervalTime
        {
            get { return intervalTime; }
            set { intervalTime = value; }
        }
        public int NumberOfNextStation
        {
            get { return numberOfNextStation; }
            set { numberOfNextStation = value; }
        }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        public double Left
        {
            get { return left; }
            set { left = value; }
        }
        public int TimeAfterStart
        {
            get { return timeAfterStart; }
            set { timeAfterStart = value; }
        }
        public int StayTime{
            get { return stayTime; }
            set { stayTime = value; }
            }
        public Train(string depot)       //Стандартна ініціалізація
        {
            draw(depot);            //"Right" or "Left"
            intervalTime = Schedule.Interval;
            stayTime = 0;

        }
        public void generateDelay()
        {
            Random rand = new Random(DateTime.Now.Second);
            Random rand2 = new Random(DateTime.Now.Millisecond);
            int prop = rand.Next(0, 100);
            if (prop <= Schedule.ProbabilityOfDelays)
            {
                timeOfDelay += rand2.Next(30, 240);
            }
        }
        private Ellipse draw(string depot)
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
    }
}
