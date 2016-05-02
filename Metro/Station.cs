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
        private TextBlock caption = new TextBlock();
        private TextBlock display = new TextBlock();
        private double captionHeight;
        public double CaptionHeight{
            get { return captionHeight; }
            set { captionHeight = value; }
            }
        
        public Station(string name)
        {
            this.name = name;
            setCaption(name);
            captionHeight = caption.ActualHeight;
        }

        // Табло з назвою станції
        public virtual TextBlock getCaption()
        {
            return caption;
        }
        public virtual void setCaption(string text)
        {
            caption.Text = text;
        }

        // Інформаційне табло на станції
        public virtual TextBlock getDisplay()
        {
            return display;
        }
        public void setDisplay(string text) {
            display.Text = text;
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public abstract Rectangle draw();
    }
}
