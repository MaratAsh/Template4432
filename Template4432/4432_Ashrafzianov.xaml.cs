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

namespace Template4432.Actions
{
    public class Action
    {
        private string _title;
        public delegate void _action_type();
        private _action_type _action;
        public string title
        {
            get
            {
                return this._title;
            }
        }
        public _action_type action { get { return this._action; } }

        public Action(string title, _action_type action)
        {
            this._title = title;
            this._action = action;
        }
    }
}

namespace Template4432
{
    /// <summary>
    /// Interaction logic for _4432_Ashrafzianov.xaml
    /// </summary>
    public partial class _4432_Ashrafzianov : Window
    {
        DateTime birth;
        String flname;
        List<Actions.Action> actions;
        public _4432_Ashrafzianov()
        {
            InitializeComponent();
            birth = new DateTime(2003, 06, 03);
            flname = "Ашрафзянов Марат";
            actions = new List<Actions.Action>();
            actions.Add(new Actions.Action("Import Excel", () => {
                Windows.ImportExcelWindow iew = new Windows.ImportExcelWindow();
                iew.Show();
            }));
            actions.Add(new Actions.Action("Export Excel", () => {
                Windows.ExportExcelWindow iew = new Windows.ExportExcelWindow();
                iew.Show();
            }));
        }
        public _4432_Ashrafzianov(String flname, DateTime dateTime)
        {
            InitializeComponent();
            this.birth = dateTime;
            this.flname = flname;
            actions = new List<Actions.Action>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TimeSpan dt = DateTime.Now - birth;
            ageTB.Text = (dt.Days / 365).ToString();
            ageDateTB.Text = birth.ToString("dd.MM.yyyy");
            nameTB.Text = flname;
            ActionsPanel.Children.Clear();
            foreach (Actions.Action a in this.actions)
            {
                Button b = new Button() { Style = FindResource("ActionBtn") as Style };
                b.Content = a.title;
                b.Click += (o, s) => { a.action(); };
                ActionsPanel.Children.Add(b);
            }
        }
    }
}
