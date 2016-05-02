using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Metro
{
    class StationChooser
    {
        private List<StandartStation> stations = new List<StandartStation>();               // Всі вибрані станції
        private Depot firstDepot;
        private Depot secondDepot;
        public List<StandartStation> Stations
        {
            get { return stations; }
        }
        public Depot FirstDepot
        {
            get { return firstDepot; }
        }
        public Depot SecondDepot
        {
            get { return secondDepot; }
        }
        public StationChooser()
        {
            try
            {
                StreamReader sr = new StreamReader(@"src\Stantion's.txt", System.Text.Encoding.Default);
                int countOfStations = MainWindow.CountOfStations;
                Random rand = new Random(DateTime.Now.Millisecond);
                int firstStation = rand.Next(20 - countOfStations);
                for (int i = 0; i < firstStation; i++)
                {
                    sr.ReadLine();                          // Пропускаємо станції, які не використовуватимемо
                }
                firstDepot = new Depot(sr.ReadLine());
                for (int i = 1; i < countOfStations-1; i++)
                {
                    stations.Add(new StandartStation(sr.ReadLine()));
                }
                secondDepot = new Depot(sr.ReadLine());
            }
            catch
            {
                return;
            }
        }
    }
}
