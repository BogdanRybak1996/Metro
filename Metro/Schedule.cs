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
        private static int probabilityOfDelays;

        public static int StayTime
        {
            get { return stayTime; }
        }
        public static int Interval
        {
            get { return interval; }
        }
        public static int ProbabilityOfDelays
        {
            get { return probabilityOfDelays; }
        }

        public static void updateSchedule(DateTime time)
        {
            if(MainWindow.TypeOfDay == "Робочий день")
            {
                if(time.Hour >=6 && time.Hour < 12)
                {
                    interval = 12;       // За основу взятий період моделювання (15 секунд)
                    stayTime = 16;
                    probabilityOfDelays = 30;
                }
                if(time.Hour>=12 && time.Hour < 18)
                {
                    interval = 16;
                    stayTime = 8;
                    probabilityOfDelays = 20;
                }
                if (time.Hour >= 18)
                {
                    interval = 10;
                    stayTime = 12;
                    probabilityOfDelays = 40;
                }
            }
            else
            {
                if (time.Hour >= 6 && time.Hour < 12)
                {
                    interval = 9;
                    stayTime = 12;
                    probabilityOfDelays = 10;
                }
                if (time.Hour >= 12 && time.Hour < 18)
                {
                    interval = 16;
                    stayTime = 8;
                    probabilityOfDelays = 15;
                }
                if (time.Hour >= 18)
                {
                    interval = 9;
                    stayTime = 12;
                    probabilityOfDelays = 35;
                }
            }
        }
    }
}
