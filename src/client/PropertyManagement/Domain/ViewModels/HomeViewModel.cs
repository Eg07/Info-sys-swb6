using System;
using System.IO;
using System.Windows.Input;
using LinqToExcel;
using Microsoft.Win32;

namespace PropertyManagement.Domain.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand ImportTransactionsCommand { get; private set; }

        public HomeViewModel()
        {
            ImportTransactionsCommand = new CommandImplementation(o => ImportTransactions());
        }

        private void ImportTransactions()
        {
            string filePath = null;
            var openFileDialog = new OpenFileDialog {Filter = "CSV files (*.csv)|*.csv|XLS files (*.xls)|*.xls"};
            if (openFileDialog.ShowDialog() == true)
               filePath = openFileDialog.FileName;

            if (filePath == null)
            {
                Snackbar.Enqueue("Operation cancelled");
                return;
            }

            switch (Path.GetExtension(filePath))
            {
                case ".csv":
                    break;
                case ".xls":
                    ImportsXlsData(filePath);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void ImportsXlsData(string filePath)
        {
            var file = new ExcelQueryFactory(filePath);
        }
    }
}
