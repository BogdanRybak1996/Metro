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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Metro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int countOfStations = 2;           // Кількість станцій обирає користувач (статичні - через те, що головне вікно одне, до того ж, ми не можемо взяти його об'єкт)
        public static int CountOfStations                     
        {
            get { return countOfStations; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Emulator em = new Emulator();
            em.Show();
        }

        private void CBcountOfStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            countOfStations = Convert.ToInt32(((ListBoxItem)CBcountOfStations.SelectedItem).Content);
        }
    }
}
