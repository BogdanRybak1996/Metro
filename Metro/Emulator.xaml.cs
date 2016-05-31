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
            try
            {
                MessageBox.Show("Кількість затримок: " + Stat.Count + "\n" + "Середня величина затримок: " + Stat.LengthOfDelay / Stat.Count * 15 + " секунд" + "\n", "Статистика");
                MainWindow mw = new MainWindow();
                mw.Show();
            }
            catch {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
                return;
            }
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
            // Оновлюємо стан лінії
            LineUpdate("Right",upTrains);
            LineUpdate("Left", downTrains);
            if (mainClock.Time.Hour == Convert.ToInt32(MainWindow.EndHour) || mainClock.Time.Hour == 0)
            {
                //Подія завершення моделювання (використовується подія кліка на кнопку "Стоп")
                BStop_Click(new object(), new RoutedEventArgs());
            }
        }

        /*Метод обновляє одну сторону лінії метро
         * Використовується виключно всередині класу
         Параметри: рядок з інформацією про сторону лінії, масив з відповідними потягами*/
        private void LineUpdate(string side,List<Train> currentTrains)
        {
            for (int i = 0; i < currentTrains.Count; i++)
            {

                currentTrains[i].TimeAfterStart += 15;
                if (currentTrains[i].TimeOfDelay == 0)
                {
                    currentTrains[i].makeGreen();
                }
                bool endOfMovement = false;     // Якщо їдемо в депо - true
                if (currentTrains[i].Status == true)     // Якщо поїзд рухається
                {
                    double movePixels = 0;
                    // Якщо поїзд тільки виїхав (ще не був на жодній проміжній станції)
                    if (currentTrains[i].NumberOfNextStation == 0)
                    {                       
                        currentTrains[i].IntervalTime = Schedule.Interval - currentTrains[i].NumberOfSteps;
                        //if (currentTrains[i].TimeOfDelay > Schedule.Interval / 2)
                        //{
                          //  currentTrains[i].TimeOfDelay--;
                            //currentTrains[i].IntervalTime--;
                        //}
                        if (currentTrains[i].IntervalTime == 0)
                        {
                            currentTrains[i].IntervalTime = 1;
                        }
                        if (side == "Right")
                        {
                            movePixels = (currentTrains[i].Left - standStations[standStations.Count - 1].Left) / currentTrains[i].IntervalTime;
                            currentTrains[i].Left -= movePixels;
                        }
                        if(side == "Left")
                        {
                            movePixels = (standStations[0].Left - currentTrains[i].Left) / currentTrains[i].IntervalTime;
                            currentTrains[i].Left += movePixels;
                        }
                        currentTrains[i].NumberOfSteps++;       // Кількість зроблених кроків між станціями
                        Canvas.SetLeft(currentTrains[i].getEllipse(), currentTrains[i].Left);
                    }
                    // Якщо поїзд уже відвідував проміжні станції
                    else
                    {
                        if (currentTrains[i].NumberOfNextStation != standStations.Count)
                        {

                            currentTrains[i].IntervalTime = Schedule.Interval - currentTrains[i].NumberOfSteps;
                            //if (currentTrains[i].TimeOfDelay > Schedule.Interval / 2)
                            //{
                              //  currentTrains[i].TimeOfDelay--;
                                //currentTrains[i].IntervalTime--;
                            //}
                            if (currentTrains[i].IntervalTime == 0)
                            {
                                currentTrains[i].IntervalTime = 1;
                            }
                            if (side == "Right")
                            {
                                movePixels = (currentTrains[i].Left - standStations[standStations.Count - 1 - currentTrains[i].NumberOfNextStation].Left) / currentTrains[i].IntervalTime;
                                currentTrains[i].Left -= movePixels;
                            }
                            if (side == "Left")
                            {
                                movePixels = (standStations[currentTrains[i].NumberOfNextStation].Left - currentTrains[i].Left) / currentTrains[i].IntervalTime;
                                currentTrains[i].Left += movePixels;
                            }
                            currentTrains[i].NumberOfSteps++;       // Кількість зроблених кроків між станціями
                            Canvas.SetLeft(currentTrains[i].getEllipse(), currentTrains[i].Left);
                        }
                        else   // Якщо поїзд їде в депо
                        {

                            currentTrains[i].IntervalTime = Schedule.Interval - currentTrains[i].NumberOfSteps;
                           // if (currentTrains[i].TimeOfDelay > Schedule.Interval / 2)
                            //{
                              //  currentTrains[i].TimeOfDelay--;
                                //currentTrains[i].IntervalTime--;
                           // }
                            currentTrains[i].NumberOfSteps++;       // Кількість зроблених кроків між станціями
                            endOfMovement = true;
                            if (side == "Right")
                            {
                                movePixels = (currentTrains[i].Left - 10) / currentTrains[i].IntervalTime;
                                currentTrains[i].Left -= movePixels;
                            }
                            if (side == "Left")
                            {
                                movePixels = (SystemParameters.PrimaryScreenWidth - 55 - currentTrains[i].Left) / currentTrains[i].IntervalTime;
                                currentTrains[i].Left += movePixels;
                            }
                            Canvas.SetLeft(currentTrains[i].getEllipse(), currentTrains[i].Left);
                        }
                    }
                    /*Якщо доїхали до депо*/
                    if (endOfMovement && (currentTrains[i].Left <= 15 || currentTrains[i].Left >= SystemParameters.PrimaryScreenWidth-60))
                    {
                        mainCanvas.Children.Remove(currentTrains[i].getEllipse());
                        currentTrains.RemoveAt(i);
                        i--;
                        continue;
                    }
                    if (!endOfMovement)
                    {
                        double razn=0;
                        if(side == "Right")
                        {
                            razn = currentTrains[i].Left - standStations[standStations.Count - 1 - currentTrains[i].NumberOfNextStation].Left;
                        }
                        if(side == "Left")
                        {
                            razn = currentTrains[i].Left - standStations[currentTrains[i].NumberOfNextStation].Left;
                        }
                        /*Похибка в 2 пікселі*/
                        if (razn <= 2 && razn >= -2)
                        { // Якщо доїхали до станції
                            currentTrains[i].NumberOfSteps = 0;
                            if (side == "Right")
                            {
                                standStations[standStations.Count - currentTrains[i].NumberOfNextStation - 1].updateBoard(mainClock.Time, 0);
                                currentTrains[i].Left = standStations[standStations.Count - 1 - currentTrains[i].NumberOfNextStation].Left;
                            }
                            if(side == "Left")
                            {
                                standStations[currentTrains[i].NumberOfNextStation].updateBoard(mainClock.Time, 0);
                                currentTrains[i].Left = standStations[currentTrains[i].NumberOfNextStation].Left;
                            }
                            if (currentTrains[i].TimeOfDelay != 0)
                            {
                                if (currentTrains[i].StayTime == 0)
                                {
                                    if (currentTrains[i].TimeOfDelay > Math.Abs(4 - Schedule.StayTime))
                                    {
                                        currentTrains[i].StayTime += Math.Abs(4 - Schedule.StayTime);
                                        currentTrains[i].TimeOfDelay -= Math.Abs(4 - Schedule.StayTime);
                                        currentTrains[i].EconomedTime += Math.Abs(4 - Schedule.StayTime);
                                    }
                                    else
                                    {
                                        currentTrains[i].StayTime += currentTrains[i].TimeOfDelay;
                                        currentTrains[i].TimeOfDelay = 0;
                                        currentTrains[i].EconomedTime += currentTrains[i].TimeOfDelay;
                                        currentTrains[i].makeGreen();
                                    }
                                }
                            }
                            else
                            {
                                    currentTrains[i].generateDelay();       //Генеруємо затримку (з певною імовірністю)
                                    if (currentTrains[i].TimeOfDelay != 0)
                                    {
                                        currentTrains[i].StayTime -= currentTrains[i].TimeOfDelay;  //Затримуємо потяг
                                        currentTrains[i].makeRed();         //Зафарбовуємо червоним, щоб позначити затримку
                                        if (i != currentTrains.Count - 1)                          //Якщо поїзд не останній
                                        {
                                            for (int j = i + 1; j < currentTrains.Count; j++)
                                            {
                                                currentTrains[j].StayTime -= currentTrains[i].TimeOfDelay;      //Затримуємо подальші поїзди
                                                currentTrains[j].TimeOfDelay += currentTrains[i].TimeOfDelay;  //Подальші поїзди також затримуються на такий же час
                                                currentTrains[j].makeRed();         //Зафарбовуємо червоним, щоб позначити затримку

                                            }
                                        }
                                    }
                            }
                            Canvas.SetLeft(currentTrains[i].getEllipse(), currentTrains[i].Left);
                            currentTrains[i].NumberOfNextStation++;
                            currentTrains[i].Status = false;             // Поїзд стоїть
                            continue;
                        }
                    }
                }
                if (i == currentTrains.Count - 1)    // Додаємо новий потяг, якщо це потрібно
                {
                    double coordsFirstStation = 0;
                    if (side == "Right")
                    {
                        coordsFirstStation = standStations[standStations.Count - 1].Left;
                    }
                    if (side == "Left")
                    {
                        coordsFirstStation = standStations[0].Left;
                    }
                    if ((currentTrains[i].Left > coordsFirstStation && side == "Left") || ((currentTrains[i].Left < coordsFirstStation && side == "Right")))
                    {
                        Train newTrain = null;
                        if (side == "Right")
                        {
                            newTrain = new Train("Right");
                            Canvas.SetTop(newTrain.getEllipse(), (int)SystemParameters.PrimaryScreenHeight / 2 - 20);
                        }
                        if (side == "Left")
                        {
                            newTrain = new Train("Left");
                            Canvas.SetTop(newTrain.getEllipse(), (int)SystemParameters.PrimaryScreenHeight / 2 + 10);
                        }
                        Canvas.SetLeft(newTrain.getEllipse(), newTrain.Left);
                        mainCanvas.Children.Add(newTrain.getEllipse());
                        if (currentTrains[i].TimeOfDelay != 0)       //Новостворений потяг також запізнюється
                        {
                            newTrain.makeRed();
                            newTrain.TimeOfDelay = currentTrains[i].TimeOfDelay;
                        }
                        currentTrains.Add(newTrain);

                    }
                }
                if (currentTrains[i].Status == false)     // Якщо поїзд стоїть
                {
                    currentTrains[i].StayTime += 1;
                    if (side == "Right")
                    {
                        standStations[standStations.Count - 1 - currentTrains[i].NumberOfNextStation + 1].updateBoard(mainClock.Time, 15);
                    }
                    if(side == "Left")
                    {
                        standStations[currentTrains[i].NumberOfNextStation - 1].updateBoard(mainClock.Time, 15);
                    }
                    if (currentTrains[i].StayTime >= Schedule.StayTime)
                    {
                        currentTrains[i].Status = true;
                        currentTrains[i].StayTime = 0;
                    }

                }
            }
        }
    }
}
   
