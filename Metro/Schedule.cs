using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro
{
    /*Статичний клас, який регулює графік руху*/
    static class Schedule
    {
        private static int stayTime;
        private static int interval;

        public static int StayTime
        {
            get { return stayTime; }
        }
        public static int Interval
        {
            get { return interval; }
        }

        public static void updateSchedule(DateTime time)
        {
            if(MainWindow.TypeOfDay == "Будень")
            {
                if(time.Hour >=6 && time.Hour <= 12)
                {
                    interval = 1;
                    stayTime = 2;
                }
                if(time.Hour>12 && time.Hour <= 18)
                {
                    interval = 3;
                    stayTime = 1;
                }
                if (time.Hour > 18)
                {
                    interval = 1;
                    stayTime = 2;
                }
            }
            else
            {
                if (time.Hour >= 6 && time.Hour <= 12)
                {
                    interval = 2;
                    stayTime = 3;
                }
                if (time.Hour > 12 && time.Hour <= 18)
                {
                    interval = 4;
                    stayTime = 2;
                }
                if (time.Hour > 18)
                {
                    interval = 2;
                    stayTime = 3;
                }
            }
        }
    }
}
