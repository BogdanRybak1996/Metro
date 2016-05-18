using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Metro
{
    /// <summary>
    /// Interaction logic for Emulator.xaml
    /// </summary>
    public partial class Emulator : Window
    {
        private System.Windows.Threading.DispatcherTimer timer;
        private List<StandartStation> standStations;
        private List<Train> UpTrains;
        private List<Train> DownTrains;
        private Clock mainClock;
        private Label allTimeLabel;

        public Emulator()
        {
            InitializeComponent();

            StationChooser stationChooser = new StationChooser();

            /*Відразу будуємо лінію метро та розставляємо станції*/
            MetroLine rightLine = new MetroLine();
            MetroLine leftLine = new MetroLine();
            mainCanvas.Children.Add(rightLine.draw((int)SystemParameters.PrimaryScreenHeight / 2 - 15));
            mainCanvas.Children.Add(leftLine.draw((int)SystemParameters.PrimaryScreenHeight / 2 + 15));

            // Ліва кінцева станція
            Depot leftDepot = stationChooser.FirstDepot;
            mainCanvas.Children.Add(leftDepot.draw());
            Canvas.SetLeft(leftDepot.getRect(), 10);
            Canvas.SetTop(leftDepot.getRect(), (int)SystemParameters.PrimaryScreenHeight / 2 - 30);
            mainCanvas.Children.Add(leftDepot.getCaption("left"));
            Canvas.SetLeft(leftDepot.getCaption(), 10);
            Canvas.SetTop(leftDepot.getCaption(), (int)SystemParameters.PrimaryScreenHeight / 2 - 60 - leftDepot.CaptionHeight);

            // Права кінцева станція
            Depot rightDepot = stationChooser.SecondDepot;
            mainCanvas.Children.Add(rightDepot.draw());
            Canvas.SetLeft(rightDepot.getRect(), (int)SystemParameters.PrimaryScreenWidth - 60);
            Canvas.SetTop(rightDepot.getRect(), (int)SystemParameters.PrimaryScreenHeight / 2 - 30);
            mainCanvas.Children.Add(rightDepot.getCaption("right"));
            Canvas.SetLeft(rightDepot.getCaption(), (int)SystemParameters.PrimaryScreenWidth - 100);
            Canvas.SetTop(rightDepot.getCaption(), (int)SystemParameters.PrimaryScreenHeight / 2 - 60 - rightDepot.CaptionHeight);

            //Розтравлення проміжних станцій
            double interval = (SystemParameters.PrimaryScreenWidth - 180) / (MainWindow.CountOfStations - 2);   //Довжину лінії розділили на кількість станцій
            double top = (int)SystemParameters.PrimaryScreenHeight / 2 - 60;
            double left = 20 + interval;
            standStations = stationChooser.Stations;
            for (int i = 0; i < MainWindow.CountOfStations - 2; i++)
            {
                standStations[i].Left = left;
                standStations[i].Top = top;
                mainCanvas.Children.Add(standStations[i].draw());
                Canvas.SetLeft(standStations[i].getRect(), left);
                Canvas.SetTop(standStations[i].getRect(), top);
                mainCanvas.Children.Add(standStations[i].getCaption());
                Canvas.SetLeft(standStations[i].getCaption(), left - standStations[i].getCaption().Width / 2 + 15.6);
                Canvas.SetTop(standStations[i].getCaption(), top - 40);
                left += interval;
            }
            //Додаємо загальний годинник
            mainClock = new Clock(MainWindow.StartHour, 0, 0);
            mainClock.setLabelBigText();
            allTimeLabel = mainClock.getLabel();
            mainCanvas.Children.Add(allTimeLabel);
            Canvas.SetLeft(allTimeLabel, 1250);
            Canvas.SetTop(allTimeLabel, 50);
        }

        private void BStart_Click(object sender, RoutedEventArgs e)
        {
            /*Запускаємо емуляцію*/
            BStart.IsEnabled = false;
            BStop.IsEnabled = true;
            UpTrains = new List<Train>();
            DownTrains = new List<Train>();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            if(MainWindow.Speed=="30 мілісекунд")               // switch - case не працює зі string
            {
                timer.Interval = new TimeSpan(0, 0, 0, 0, 30);     // Частота оновлення - 30 мілісекунд
            }
            if (MainWindow.Speed == "1 секунда")
            {
                timer.Interval = new TimeSpan(0, 0, 0, 1);     // Частота оновлення - 1 секунда
            }
            if (MainWindow.Speed == "1.5 секунди")
            {
                timer.Interval = new TimeSpan(0, 0, 0, 1, 30);     // Частота оновлення - 1.5 секунди
            }
            if (MainWindow.Speed == "2 секунди")
            {
                timer.Interval = new TimeSpan(0, 0, 0, 2, 0);     // Частота оновлення - 2 мілісекунд
            }
            timer.Start();
        }
        private void BStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            BStop.IsEnabled = false;
            BStart.IsEnabled = true;
            MessageBox.Show("", "Статистика");
            this.Close();
        }
        /*Таймер імітує кроки емуляції*/
        private void TimerTick(object sender, EventArgs e)
        {
            mainClock.addSeconds(30);           // Крок моделювання - 30 секунд

            if (UpTrains.Count == 0)
            {
                Train firstUpTrain = new Train();
                firstUpTrain.draw("Right");
                Canvas.SetLeft(firstUpTrain.getEllipse(), firstUpTrain.Left);
                Canvas.SetTop(firstUpTrain.getEllipse(), (int)SystemParameters.PrimaryScreenHeight / 2 - 20);
                mainCanvas.Children.Add(firstUpTrain.getEllipse());
                UpTrains.Add(firstUpTrain);
            }
            if (DownTrains.Count == 0)
            {
                Train firstDownTrain = new Train();
                firstDownTrain.draw("Left");
                Canvas.SetLeft(firstDownTrain.getEllipse(), firstDownTrain.Left);
                Canvas.SetTop(firstDownTrain.getEllipse(), (int)SystemParameters.PrimaryScreenHeight / 2 + 10);
                mainCanvas.Children.Add(firstDownTrain.getEllipse());
                DownTrains.Add(firstDownTrain);
            }

            if (mainClock.Time.Hour == Convert.ToInt32(MainWindow.EndHour) || mainClock.Time.Hour == 0)
            {
                BStop_Click(new object(), new RoutedEventArgs());
            }
        }
    }
}
