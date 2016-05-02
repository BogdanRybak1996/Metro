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
            /*Відразу будуємо лінію метро та розставляємо станції*/
            MetroLine rightLine = new MetroLine();
            MetroLine leftLine = new MetroLine();
            mainCanvas.Children.Add(rightLine.draw((int)SystemParameters.PrimaryScreenHeight/2 - 15));
            mainCanvas.Children.Add(leftLine.draw((int)SystemParameters.PrimaryScreenHeight / 2 + 15));

            // Ліва кінцева станція
            Depot leftDepot = new Depot("Left Depot");
            mainCanvas.Children.Add(leftDepot.draw());
            Canvas.SetLeft(leftDepot.getRect(), 20);
            Canvas.SetTop(leftDepot.getRect(), (int)SystemParameters.PrimaryScreenHeight / 2 - 25);
            mainCanvas.Children.Add(leftDepot.getDisplay());
            Canvas.SetLeft(leftDepot.getDisplay(), 5);
            Canvas.SetTop(leftDepot.getDisplay(), (int)SystemParameters.PrimaryScreenHeight / 2 - 50);
            
            // Права кінцева станція
            Depot rightDepot = new Depot("Right Depot");
            mainCanvas.Children.Add(rightDepot.draw());
            Canvas.SetLeft(rightDepot.getRect(), (int)SystemParameters.PrimaryScreenWidth - 50);
            Canvas.SetTop(rightDepot.getRect(), (int)SystemParameters.PrimaryScreenHeight / 2 - 25);
            mainCanvas.Children.Add(rightDepot.getDisplay());
            Canvas.SetLeft(rightDepot.getDisplay(), (int)SystemParameters.PrimaryScreenWidth - 100);
            Canvas.SetTop(rightDepot.getDisplay(), (int)SystemParameters.PrimaryScreenHeight / 2 - 50);

            //Розтравлення проміжних станцій
            int interval = ((int)SystemParameters.PrimaryScreenWidth - 200) / (MainWindow.CountOfStations - 2);   //Довжину лінії розділили на кількість станцій
            int top = (int)SystemParameters.PrimaryScreenHeight / 2 - 25;
            int left = interval+50;
            List<StandartStation> standStations = new List<StandartStation>();
            for (int i = 0; i < MainWindow.CountOfStations-2; i++)
            {
                standStations.Add(new StandartStation());
                standStations[i].Left = left;
                standStations[i].draw();
                mainCanvas.Children.Add(standStations[i].getRect());
                Canvas.SetLeft(standStations[i].getRect(), left);
                Canvas.SetTop(standStations[i].getRect(), top);
                left += interval;
            }
        }
    }
}
