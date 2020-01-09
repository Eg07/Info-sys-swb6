using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Win32;
using PropertyManagement.Database.DataModels;

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
                    ImportCsvData(filePath);
                    break;
                case ".xls":
                    ImportsXlsData(filePath);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void ImportCsvData(string filePath)
        {
            // tuple contains booking date, valuta date, type, description, payer, amount
            var transactions = new List<(DateTime, DateTime, string, string, string, double)>();

            using (var reader = new StreamReader(filePath))
            {
                // skip first line
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if(line == null) return;

                    var values = line.Split(';');
                    transactions.Add((DateTime.Parse(values[0]), DateTime.Parse(values[1]), values[2], values[3], values[4], double.Parse(values[5])));
                }
            }

            var payments = new List<G3Payments>();
            var operatingCosts = new List<G3OperatingCosts>();

                transactions.Where(transaction => transaction.Item6 >= 0).ToList().ForEach(payment => payments.Add(new G3Payments()
                {
                    BookingDate = payment.Item1,
                    ValutaDate = payment.Item2,
                    Type = payment.Item3,
                    Description = payment.Item4,
                    Amount = payment.Item6
                }));
                transactions.Where(transaction => transaction.Item6 < 0).ToList().ForEach(cost => operatingCosts.Add(new G3OperatingCosts()
                {
                    BookingDate = cost.Item1,
                    ValutaDate = cost.Item2,
                    Type = cost.Item3,
                    Description = cost.Item4,
                    Amount = cost.Item6,
                    DistributionKey = 111111111 // TODO
                }));
        }

        private void ImportsXlsData(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
