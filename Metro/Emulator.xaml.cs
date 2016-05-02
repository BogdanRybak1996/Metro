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

namespace Metro
{
    /// <summary>
    /// Interaction logic for Emulator.xaml
    /// </summary>
    public partial class Emulator : Window
    {
        public Emulator()
        {
            InitializeComponent();

            StationChooser stationChooser = new StationChooser();

            /*Відразу будуємо лінію метро та розставляємо станції*/
            MetroLine rightLine = new MetroLine();
            MetroLine leftLine = new MetroLine();
            mainCanvas.Children.Add(rightLine.draw((int)SystemParameters.PrimaryScreenHeight/2 - 15));
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
            double left = 25 + interval;
            List<StandartStation> standStations = stationChooser.Stations;
            for (int i = 0; i < MainWindow.CountOfStations-2; i++)
            {
                standStations[i].Left = left;
                standStations[i].Top = top;
                mainCanvas.Children.Add(standStations[i].draw());
                Canvas.SetLeft(standStations[i].getRect(), left);
                Canvas.SetTop(standStations[i].getRect(), top);
                mainCanvas.Children.Add(standStations[i].getCaption());
                Canvas.SetLeft(standStations[i].getCaption(), left - standStations[i].getCaption().Width/2 + 15.6);
                Canvas.SetTop(standStations[i].getCaption(), top - 40);
                left += interval;
            }
        }
    }
}
