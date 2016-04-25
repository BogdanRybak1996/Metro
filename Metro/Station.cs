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
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        virtual public Ellipse draw() {

        }
    }
}
