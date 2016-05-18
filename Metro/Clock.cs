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
    class Clock
    {
        private string Hour;
        private string Minutes;
        private string Seconds;
        private DateTime time;
        private Label timeLabel = new Label();

        public DateTime Time
        {
            get { return time; }
        }

        public Clock(int hour,int minutes,int seconds)              // Конструктор ініціалізує годинник заданим часом
        {
            time = new DateTime(0, 0, 0, hour, minutes, seconds);
            Hour = time.Hour.ToString();
            Minutes = time.Minute.ToString();
            Seconds = time.Second.ToString();
            updateLabel();
        }
        public void addSeconds(int seconds) {                      //Додаємо секунди до годинники (так як крок моделювання в секундах, цього достатньо)
            TimeSpan timespan = new TimeSpan(0, 0, seconds);
            time.Add(timespan);
            Hour = time.Hour.ToString();
            Minutes = time.Minute.ToString();
            Seconds = time.Second.ToString();
            updateLabel();
        }
        private void updateLabel()          // Метод, виключно для внутрішнього використання
        {
            string labelText = Hour.ToString() + ":" + Minutes.ToString() + ":" + Seconds.ToString();
            timeLabel.Content = labelText;
        }
        public Label getLabel()
        {
            return timeLabel;
        }
    }
}
