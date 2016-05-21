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
        private static int countOfStations = 7;           // Кількість станцій обирає користувач (статичні - через те, що головне вікно одне, до того ж, ми не можемо взяти його об'єкт)
        private static string typeOfDay;
        private static int startHour;
        private static int endHour;
        private static string speed;
        public static int CountOfStations                     
        {
            get { return countOfStations; }
        }
        public static string TypeOfDay
        {
            get { return typeOfDay; }
        }
        public static int StartHour
        {
            get { return startHour; }
        }
        public static int EndHour
        {
            get { return endHour; }
        }
        public static string Speed
        {
            get { return speed; }
        }
        public MainWindow()
        {
            InitializeComponent();
            for(int i = 6; i <= 23; i++)
            {
                CBHourStart.Items.Add(i);
            }
            
            CBDayOfWeek.Items.Add("Робочий день");
            CBDayOfWeek.Items.Add("Вихідний");
            CBDayOfWeek.SelectedIndex = 0;

            CBHourStart.SelectedIndex = 0;
            for(int i = Convert.ToInt32(CBHourStart.SelectedValue) + 1; i <= 24; i++)
            {
                CBHourEnd.Items.Add(i);
            }
            CBHourEnd.SelectedIndex = 0;

            CBSpeed.Items.Add("30 мілісекунд");
            CBSpeed.Items.Add("1 секунда");
            CBSpeed.Items.Add("1.5 секунди");
            CBSpeed.Items.Add("2 секунди");
            CBSpeed.SelectedIndex = 1;              // Стандартно - 1 секунда
            speed = CBSpeed.SelectedValue.ToString();
            startHour = Convert.ToInt32(CBHourStart.SelectedValue);
            endHour = Convert.ToInt32(CBHourEnd.SelectedValue);
            typeOfDay = CBDayOfWeek.SelectedValue.ToString();


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(countOfStations >= 10 && SystemParameters.PrimaryScreenWidth < 1366)
            {
                MessageBox.Show("Роздільна здатність вашого екрану є низькою\nПри великій кількості станцій відображення може бути неправильним", "Попередження");
            }
            Emulator em = new Emulator();
            em.Show();
        }

        private void CBcountOfStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            countOfStations = Convert.ToInt32(((ListBoxItem)CBcountOfStations.SelectedItem).Content);
        }

        private void CBHourStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBHourEnd.Items.Clear();
            for (int i = Convert.ToInt32(CBHourStart.SelectedValue) + 1; i <= 24; i++)
            {
                CBHourEnd.Items.Add(i);
            }
            CBHourEnd.SelectedIndex = 0;
            startHour = Convert.ToInt32(CBHourStart.SelectedValue);
            endHour = Convert.ToInt32(CBHourEnd.SelectedValue);
        }

        private void CBHourEnd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            endHour = Convert.ToInt32(CBHourEnd.SelectedValue);
        }

        private void CBDayOfWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            typeOfDay = CBDayOfWeek.SelectedValue.ToString();
        }

        private void CBSpeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            speed = CBSpeed.SelectedValue.ToString();
        }
    }
}
