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

namespace Template4432.Windows
{
    /// <summary>
    /// Interaction logic for ManagerDataWindow.xaml
    /// </summary>
    public partial class ManagerDataWindow : Window
    {
        private List<Datum> _data;
        Action<List<Datum>> _save_action;
        public ManagerDataWindow()
        {
            InitializeComponent();
            _data = new List<Datum>();
            _save_action = null;
            update();

        }
        public ManagerDataWindow(List<Datum> data, Action<List<Datum>> save_action)
        {
            InitializeComponent();
            _data = data;
            _save_action = save_action;
            update();
        }
        public ManagerDataWindow(List<Datum> data)
        {
            InitializeComponent();
            _data = data;
            _save_action = null;
            update();
        }
        private void update()
        {
            dataDG.ItemsSource = _data;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_save_action != null)
            {
                _save_action(_data);
            }
            Close();
        }
    }
}
