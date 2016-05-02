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
    /*Абстрактний клас для різного типу станцій*/
    abstract class Station
    {
        private string name;
        private Label display = new Label();

        // Інформаційне табло на станції
        public virtual Label getDisplay()
        {
            return display;
        }
        public void setDisplay(string text) {
            display.Content = text;
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public abstract Rectangle draw();
    }
}
