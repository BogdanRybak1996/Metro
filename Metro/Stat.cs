using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro
{
    static class Stat
    {
        private static int count = 0; //кількість затримок
        private static int lenghtOfDelay = 0;   //Середня величина затримки

        public static int Count
        {
            get { return count; }
            set { count = value; }
        }
        public static int LengthOfDelay
        {
            get { return lenghtOfDelay; }
            set { lenghtOfDelay += value; }
        }
    }
}
