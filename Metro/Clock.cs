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
            time = new DateTime(1, 1, 1, hour, minutes, seconds);
            Hour = time.Hour.ToString();
            Minutes = time.Minute.ToString();
            Seconds = time.Second.ToString();
            updateLabel();
        }
        public void addSeconds(int seconds) {                      //Додаємо секунди до годинники (так як крок моделювання в секундах, цього достатньо)
            time = time.AddSeconds(seconds);
            Hour = time.Hour.ToString();
            Minutes = time.Minute.ToString();
            Seconds = time.Second.ToString();
            updateLabel();
        }
        private void updateLabel()          // Метод, виключно для внутрішнього використання
        {
            string hour = Hour.ToString();
            string minutes = Minutes.ToString();
            string seconds = Seconds.ToString();
            if (Hour.ToString().Length == 1)
            {
                hour = "0" + Hour.ToString();
            }
            if (Minutes.ToString().Length == 1)
            {
                minutes = "0" + Minutes.ToString();
            }
            if (Seconds.ToString().Length == 1)
            {
                seconds = "0" + Seconds.ToString();
            }
            string labelText = hour + ":" + minutes + ":" + seconds;
            timeLabel.Content = labelText;
        }
        public Label getLabel()
        {
            return timeLabel;
        }
        public void setLabelBigText()
        {
            timeLabel.FontSize = 20;
        }
    }
}
