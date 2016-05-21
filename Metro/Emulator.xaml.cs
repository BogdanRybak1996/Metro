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
        private List<Train> upTrains;
        private List<Train> downTrains;
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
            upTrains = new List<Train>();
            downTrains = new List<Train>();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            if (MainWindow.Speed == "30 мілісекунд")               // switch - case не працює зі string
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
            mainClock.addSeconds(15);           // Крок моделювання - 15 секунд
            Schedule.updateSchedule(mainClock.Time);
            if (upTrains.Count == 0)
            {
                Train firstUpTrain = new Train("Right");
                Canvas.SetLeft(firstUpTrain.getEllipse(), firstUpTrain.Left);
                Canvas.SetTop(firstUpTrain.getEllipse(), (int)SystemParameters.PrimaryScreenHeight / 2 - 20);
                mainCanvas.Children.Add(firstUpTrain.getEllipse());
                upTrains.Add(firstUpTrain);
            }
            if (downTrains.Count == 0)
            {
                Train firstDownTrain = new Train("Left");
                Canvas.SetLeft(firstDownTrain.getEllipse(), firstDownTrain.Left);
                Canvas.SetTop(firstDownTrain.getEllipse(), (int)SystemParameters.PrimaryScreenHeight / 2 + 10);
                mainCanvas.Children.Add(firstDownTrain.getEllipse());
                downTrains.Add(firstDownTrain);
            }
            for (int i = 0; i < upTrains.Count; i++)
            {
                upTrains[i].TimeAfterStart += 15;
                bool endOfMovement = false;     // Якщо їдемо в депо - true
                if (upTrains[i].Status == true)     // Якщо поїзд рухається
                {
                    double movePixels = 0;
                    // Якщо поїзд тільки виїхав (ще не був на жодній проміжній станції)
                    if (upTrains[i].NumberOfNextStation == 0)
                    {
                        movePixels = (SystemParameters.PrimaryScreenWidth - 65 - standStations[standStations.Count - 1].Left) / upTrains[i].IntervalTime;
                        upTrains[i].Left -= movePixels;
                        Canvas.SetLeft(upTrains[i].getEllipse(), upTrains[i].Left);
                    }
                    // Якщо поїзд уже відвідував проміжні станції
                    else
                    {
                        if (upTrains[i].NumberOfNextStation != standStations.Count)
                        {
                            movePixels = (standStations[standStations.Count-1-upTrains[i].NumberOfNextStation+1].Left - standStations[standStations.Count - 1 - upTrains[i].NumberOfNextStation].Left) / upTrains[i].IntervalTime;
                            upTrains[i].Left -= movePixels;
                            Canvas.SetLeft(upTrains[i].getEllipse(), upTrains[i].Left);
                        }
                        else   // Якщо поїзд їде в депо
                        {
                            endOfMovement = true;
                            movePixels = (standStations[0].Left - 10) / upTrains[i].IntervalTime;
                            upTrains[i].Left -= movePixels;
                            Canvas.SetLeft(upTrains[i].getEllipse(), upTrains[i].Left);
                        }
                    }
                    if(endOfMovement && upTrains[i].Left <= 10)
                    {
                        mainCanvas.Children.Remove(upTrains[i].getEllipse());
                        upTrains.RemoveAt(i);
                        i--;
                        continue;
                    }
                    if (!endOfMovement)
                    {
                        if ((upTrains[i].Left - standStations[standStations.Count - 1 - upTrains[i].NumberOfNextStation].Left)<=2 || (upTrains[i].Left - standStations[standStations.Count - 1 - upTrains[i].NumberOfNextStation].Left) <= -2)
                        { // Якщо доїхали до станції
                            standStations[standStations.Count - upTrains[i].NumberOfNextStation - 1].updateBoard(mainClock.Time,0);
                            upTrains[i].Left = standStations[standStations.Count - 1 - upTrains[i].NumberOfNextStation].Left;
                            Canvas.SetLeft(upTrains[i].getEllipse(), upTrains[i].Left);
                            upTrains[i].NumberOfNextStation++;
                            upTrains[i].Status = false;             // Поїзд стоїть
                            return;
                        }
                    }
                }


                if(upTrains[i].Status == false)     // Якщо поїзд стоїть
                {
                    upTrains[i].StayTime += 1;
                    standStations[standStations.Count - 1 - upTrains[i].NumberOfNextStation + 1].updateBoard(mainClock.Time, 15);
                    if (upTrains[i].StayTime >= Schedule.StayTime + upTrains[i].TimeOfDelay)
                    {
                        upTrains[i].Status = true;
                        upTrains[i].StayTime = 0;
                    }
                }
                if (i == upTrains.Count - 1)    // Додаємо новий потяг, якщо це потрібно
                {
                    if (upTrains[i].TimeAfterStart >= Schedule.Interval*15 + Schedule.StayTime*15)
                    {
                        Train newUpTrain = new Train("Right");
                        Canvas.SetLeft(newUpTrain.getEllipse(), newUpTrain.Left);
                        Canvas.SetTop(newUpTrain.getEllipse(), (int)SystemParameters.PrimaryScreenHeight / 2 - 20);
                        mainCanvas.Children.Add(newUpTrain.getEllipse());
                        upTrains.Add(newUpTrain);
                    }
                }
            }
            if (mainClock.Time.Hour == Convert.ToInt32(MainWindow.EndHour) || mainClock.Time.Hour == 0)
            {
                BStop_Click(new object(), new RoutedEventArgs());
            }
        }
    }
}
