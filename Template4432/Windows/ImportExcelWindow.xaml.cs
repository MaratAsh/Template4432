using Microsoft.Win32;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace Template4432.Windows
{
    /// <summary>
    /// Interaction logic for ImportExcelWindow.xaml
    /// </summary>
    public partial class ImportExcelWindow : Window
    {
        private string fileName;
        private string sheetName;
        private List<Datum> _data;
        public ImportExcelWindow()
        {
            InitializeComponent();
            fileName = null;
            selectedFileTB.Visibility = Visibility.Hidden;
        }

        private void selectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog()
            {
                DefaultExt = "*.xls;*.xlsx",
                Filter = "файл Excel (Spisok.xlsx)|*.xlsx",
                Title = "Select a File"
            };
            if ((bool)openDialog.ShowDialog())
            {
                fileName = openDialog.FileName;
                selectedFileTB.Text = fileName;
                fileNotSelectedTB.Visibility = Visibility.Hidden;
                selectedFileTB.Visibility = Visibility.Visible;
            }
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            if (fileName == null)
                return;
            _data = new List<Datum>();


            var ObjWorkExcel = new Excel.Application();

            var ObjWorkBook = ObjWorkExcel.Workbooks.Open(fileName);

            Excel.Worksheet ObjWorkSheet;
            if (sheetName != null)
                ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[sheetName];
            else
                ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];

            var lastCell = ObjWorkSheet
                .Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);

            var _columns = lastCell.Column;

            var _rows = lastCell.Row;


            int startingPosition = 1;
            int loadErrors = 0;
            for (int j = startingPosition; j < _rows; j++)
            {
                string[] list = new string[_rows];
                try
                {
                    for (int i = 0; i < _columns; i++)
                        list[i] = ObjWorkSheet.Cells[j + 1, i + 1].Text;
                    int ID = Convert.ToInt32(list[1]);
                    string fullName = list[0];
                    DateTime birthDate = Convert.ToDateTime(list[2]);
                    int BuildingNumber = Convert.ToInt32(list[6]);
                    int AppartmentNumber = Convert.ToInt32(list[7]);
                    _data.Add(
                        new Datum()
                        {
                            ID = ID,
                            FullName = fullName,
                            BirthDate = birthDate,
                            PostCode = list[2],
                            City = list[4],
                            Street = list[5],
                            BuildingNumber = BuildingNumber,
                            AppartmentNumber = AppartmentNumber,
                            Email = list[8]
                        }
                    );
                }
                catch (Exception ex)
                {
                    loadErrors++;
                    loadErrorsTB.Text = loadErrors.ToString();
                    loadErrorsStack.Children.Add(
                        new TextBlock()
                        {
                            Text = ex.ToString()
                        });
                }
                
            }
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);

            ObjWorkExcel.Quit();

            GC.Collect();
            countTB.Text = _data.Count.ToString();
        }
        int databaseErrors = 0;
        private void databaseInsertButton_Click(object sender, RoutedEventArgs e)
        {
            using (Entities entities = new Entities())
            {
                foreach (Datum d in _data)
                {
                    try
                    {
                        entities.Data.Add(d);
                        entities.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        databaseErrors++;
                        databaseErrorsTB.Text = databaseErrors.ToString();
                        databaseErrorsStack.Children.Add(
                            new TextBlock() {
                                Text = ex.ToString()
                            });
                    }
                }
            }
        }

        private void managerButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerDataWindow mdg = new ManagerDataWindow(_data, managerSave);
            mdg.Closed += (p1, p2) =>
            {
                this.Visibility = Visibility.Visible;
            };
            this.Visibility = Visibility.Hidden;
            mdg.Show();
        }
        private void managerSave(List<Datum> new_data)
        {
            _data = new_data;
            countTB.Text = _data.Count.ToString();
        }

        private void excelSheetNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Visibility != Visibility.Visible)
                return;
            if (excelSheetNameTB.Text != "")
                sheetName = excelSheetNameTB.Text;
            else
                sheetName = null;
        }
    }
}
