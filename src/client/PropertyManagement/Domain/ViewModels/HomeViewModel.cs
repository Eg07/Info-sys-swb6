using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PropertyManagement.Database;
using PropertyManagement.Database.DataModels;
// ReSharper disable StringLiteralTypo

namespace PropertyManagement.Domain.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand ImportTransactionsCommand { get; private set; }

        private static readonly List<G3Tenant> TenantList = InfoSysDbContext.G3Tenant.Include(i => i.G3BankAccount).ToList();

        // TODO: move to transactions
        private static int _totalResidents = InfoSysDbContext.G3Unit.Sum(unit => unit.ResidentNr);
        private static double _totalArea = InfoSysDbContext.G3Unit.Sum(unit => unit.Area);

        public HomeViewModel()
        {
            // TODO: get total area and total residents for property
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
            var transactions = GenerateTransactionsFromCsv(filePath);
            if(transactions == null) return;

            var payments = ExtractPaymentsFromTransactions(transactions);
            var operatingCosts = ExtractOperatingCostsFromTransactions(transactions);
            
            
        }

        private List<(DateTime, DateTime, string, string, string, double, string)> GenerateTransactionsFromCsv(string filePath)
        {
            // tuple contains booking date, valuta date, type, description, payer, amount, Iban
            var transactions = new List<(DateTime, DateTime, string, string, string, double, string)>();

            using (var reader = new StreamReader(filePath))
            {
                // skip first line
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) return null;

                    var values = line.Split(';');
                    transactions.Add((DateTime.Parse(values[0]), DateTime.Parse(values[1]), values[2], values[3], values[4], double.Parse(values[5]), values[6]));
                }
            }

            return transactions;
        }

        private List<G3Payments> ExtractPaymentsFromTransactions(IEnumerable<(DateTime, DateTime, string, string, string, double, string)> transactions)
        {
            var payments = new List<G3Payments>();
            transactions.Where(transaction => transaction.Item6 >= 0).ToList().ForEach(payment => payments.Add(new G3Payments()
            {
                BookingDate = payment.Item1,
                ValutaDate = payment.Item2,
                Type = payment.Item3,
                Description = payment.Item4,
                Association = payment.Item5,
                Amount = payment.Item6,
                Iban = payment.Item7
            }));

            payments = TryAscribePaymentToTenant(payments);
            return payments;
        }

        private List<G3OperatingCosts> ExtractOperatingCostsFromTransactions(IEnumerable<(DateTime, DateTime, string, string, string, double, string)> transactions)
        {
            var operatingCosts = new List<G3OperatingCosts>();
            transactions.Where(transaction => transaction.Item6 < 0).ToList().ForEach(cost => operatingCosts.Add(new G3OperatingCosts()
            {
                BookingDate = cost.Item1,
                ValutaDate = cost.Item2,
                Type = cost.Item3,
                Description = cost.Item4,
                Association = cost.Item5,
                Amount = cost.Item6
            }));

            operatingCosts = AssignDistributionKeyToCosts(operatingCosts);
            return operatingCosts;
        }

        /// <summary>
        /// Decide whether the costs should be distributed by number of residents or m² of the unit
        /// </summary>
        /// <param name="operatingCostsList"></param>
        /// <returns></returns>
        private List<G3OperatingCosts> AssignDistributionKeyToCosts(List<G3OperatingCosts> operatingCostsList)
        {
            // distribute on area if these strings are found
            var distributeOnResidents = new List<string>
            {
                "wasser",
                "strom",
                "müll"
            };

            operatingCostsList.ForEach(transaction =>
                {
                    distributeOnResidents.ForEach(word =>
                    {

                        if (transaction.Description.ToLower().ReplaceUmlauts().Contains(word))
                            transaction.DistributionKey = (int) DistributionKey.DistributeByResidents;
                        else transaction.DistributionKey = (int) DistributionKey.DistributeByArea;
                    });
                });

            return operatingCostsList;
        }

        /// <summary>
        /// Tries to ascribe payment to tenant
        /// </summary>
        /// <param name="paymentList"></param>
        /// <returns></returns>
        private List<G3Payments> TryAscribePaymentToTenant(List<G3Payments> paymentList)
        {
            foreach (var payment in paymentList)
            {
                // first try to match IBANs
                var matchedTenant = TenantList.FirstOrDefault(tenant => tenant.G3BankAccount.First().Iban == payment.Iban);
                if (matchedTenant != null) break;
                // secondly try to match name in description or association
                matchedTenant = TenantList.FirstOrDefault(tenant =>
                    {
                        var firstName = tenant.FirstName.ToLower().ReplaceUmlauts();
                        var lastName = tenant.LastName.ToLower().ReplaceUmlauts();
                        var paymentDescription = payment.Description.ToLower().ReplaceUmlauts();
                        return paymentDescription.Contains(firstName) ||
                               paymentDescription.Contains(lastName) ||
                               paymentDescription.Contains(firstName) ||
                               paymentDescription.Contains(lastName);
                    }
                );

                if (matchedTenant == null) break;
                payment.IbanNavigation = matchedTenant.G3BankAccount.First();
                payment.IbanNavigation.Tenant = matchedTenant;
            }

            return paymentList;
        }

        private void ImportsXlsData(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
