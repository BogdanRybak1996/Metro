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
            /*Відразу будуємо дві лінії метро*/
            MetroLine rightLine = new MetroLine();
            MetroLine leftLine = new MetroLine();
            mainCanvas.Children.Add(rightLine.draw(280));
            mainCanvas.Children.Add(leftLine.draw(320));
        }
    }
}
