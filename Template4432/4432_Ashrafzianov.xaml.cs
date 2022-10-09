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

namespace Template4432
{
    /// <summary>
    /// Interaction logic for _4432_Ashrafzianov.xaml
    /// </summary>
    public partial class _4432_Ashrafzianov : Window
    {
        DateTime birth;
        String flname;
        public _4432_Ashrafzianov()
        {
            InitializeComponent();
            birth = new DateTime(2003, 06, 03);
            flname = "Ашрафзянов Марат";
        }
        public _4432_Ashrafzianov(String flname, DateTime dateTime)
        {
            InitializeComponent();
            this.birth = dateTime;
            this.flname = flname;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TimeSpan dt = DateTime.Now - birth;
            ageTB.Text = (dt.Days / 365).ToString();
            ageDateTB.Text = birth.ToString("dd.MM.yyyy");
            nameTB.Text = flname;
        }
    }
}
