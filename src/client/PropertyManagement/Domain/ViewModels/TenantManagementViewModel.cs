using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using IronPdf;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database;
using PropertyManagement.Database.DataModels;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class TenantManagementViewModel : ViewModelBase
    {
        public List<G3Tenant> TenantList { get; set; }
        public ObservableCollection<TenantDisplayContainer> Tenants { get; set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand GenerateReportCommand { get; private set; }

        private double _totalArea;
        private double _totalResidents;

        public TenantManagementViewModel()
        {
            UpdateCommand = new CommandImplementation(o => UpdateDatabaseData());
            GenerateReportCommand = new CommandImplementation(o => GenerateReport());

            PopulateDynamicData();
        }

        private void UpdateDatabaseData()
        {
            var idList = Tenants.Where(tenant => tenant.IsSelected).Select(tenant => tenant.Id).ToList();
            var updateList = TenantList.Where(tenant => idList.Contains(tenant.Id)).ToList();
            if (updateList.Count < 1) return;

            var status = true;
            updateList.ForEach(item =>
            {
                // if one item is false status will remain false (simple boolean and)
                status &= InfoSysDbContext.UpdateDatabaseEntry(item);
            });

            var itemsUpdated = GetIdsOfSelectedItems(updateList);
            Snackbar.Enqueue(status ? $"Tenant data successfully updated for Ids: {itemsUpdated}" : $"Update operation failed for Ids: {itemsUpdated}");
        }

        private void GenerateReport()
        {
            var renderer = new HtmlToPdf
            {
                PrintOptions =
                {
                    FirstPageNumber = 1,
                    Header =
                    {
                        DrawDividerLine = true,
                        CenterText = "DreamHouse Account Statement",
                        FontFamily = "Helvetica,Arial",
                        FontSize = 12
                    },
                    Footer =
                    {
                        DrawDividerLine = true,
                        FontFamily = "Arial",
                        FontSize = 10,
                        LeftText = "{date} {time}",
                        RightText = "{page} of {total-pages}"
                    }
                }
            };

            var selectedTenantsId = Tenants.Where(tenant => tenant.IsSelected).Select(tenant => tenant.Id).ToList();
            var selectedTenants = TenantList.Where(tenant => selectedTenantsId.Contains(tenant.Id)).ToList();
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            foreach (var tenant in selectedTenants)
            {
                var htmlString = CreateHtmlRepresentationOfPdf(tenant);
                var pdf = renderer.RenderHtmlAsPdf(htmlString);
                pdf.SaveAs($"{path}\\account_statement_{tenant.FirstName}_{tenant.LastName}.pdf");
            }

            Snackbar.Enqueue($"Account statement(s) successfully created at {path}");
        }

        private string CreateHtmlRepresentationOfPdf(G3Tenant tenant)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<!DOCTYPE html> <html> <head> <style> table { font-family: arial, sans-serif; border-collapse: collapse; width: 100%; } td, th { border: 1px solid #dddddd; text-align: left; padding: 8px; } tr:nth-child(even) { background-color: #dddddd; } tr:nth-child(even) { background-color: #dddddd; } </style> </head> <body>");
            stringBuilder.Append($"<h3 style=\"text-align:center\">Account statement for {tenant.FirstName} {tenant.LastName}</h3>");
            stringBuilder.Append("<p><b>Payments due - monthly</b></p><table><tr><th>Date</th><th>Description</th><th>Monthly cost</th><th>Sum</th></tr>");
            
            //// add payments due \\\\
            var lease = tenant.G3Lease.First();
            var dateBegin = new DateTime(2019, 1, 1);
            var dateEnd = new DateTime(2019, 12, 31);

            // monthly rent
            stringBuilder.Append($"<tr><td>{dateBegin.ToShortDateString()} - {dateEnd.ToShortDateString()}</td>");
            stringBuilder.Append("<td>Monthly rent</td>");
            stringBuilder.Append($"<td>{lease.Cost}€</td>");
            stringBuilder.Append($"<td>{lease.Cost*12}€</td></tr></table>");

            // utilities
            var opCosts = InfoSysDbContext.G3OperatingCosts.ToList();
            var utilityCost = 0.0;

            stringBuilder.Append("<p><b>Payments due - non recurring</b></p><table><tr><th>Date</th><th>Description</th><th>Distribution key</th><th>Total cost</th><th>Unit cost</th></tr>");
            opCosts.RemoveAll(cost => cost.UnitId != null && cost.UnitId != tenant.G3Lease.First().UnitId);
            opCosts.Where(cost => cost.UnitId != null).ToList().ForEach(cost =>
            {
                stringBuilder.Append($"<tr><td>{cost.ValutaDate.ToShortDateString()}</td>");
                stringBuilder.Append($"<td>{cost.Description}</td>");
                stringBuilder.Append("<td>Unit</td>");
                stringBuilder.Append($"<td>{Math.Abs(cost.Amount):C}</td>");
                stringBuilder.Append($"<td>{Math.Abs(cost.Amount):C}</td></tr>");
                utilityCost += cost.Amount;
            });

            var areaPercentage = tenant.G3Lease.First().Unit.Area / _totalArea;
            opCosts.Where(cost => cost.DistributionKey == (int)DistributionKey.DistributeByArea).ToList().ForEach(
                cost =>
                {
                    stringBuilder.Append($"<tr><td>{cost.ValutaDate.ToShortDateString()}</td>");
                    stringBuilder.Append($"<td>{cost.Description}</td>");
                    stringBuilder.Append($"<td>{(DistributionKey)cost.DistributionKey}</td>");
                    stringBuilder.Append($"<td>{Math.Abs(cost.Amount):C}</td>");
                    stringBuilder.Append($"<td>{Math.Abs(cost.Amount) * areaPercentage:C}</td></tr>");
                    utilityCost += cost.Amount * areaPercentage;
                });

            var residentPercentage = tenant.G3Lease.First().Unit.ResidentNr / _totalResidents;
            opCosts.Where(cost => cost.DistributionKey == (int)DistributionKey.DistributeByResidents).ToList().ForEach(
                cost =>
                {
                    stringBuilder.Append($"<tr><td>{cost.ValutaDate.ToShortDateString()}</td>");
                    stringBuilder.Append($"<td>{cost.Description}</td>");
                    stringBuilder.Append($"<td>{(DistributionKey)cost.DistributionKey}</td>");
                    stringBuilder.Append($"<td>{Math.Abs(cost.Amount):C}</td>");
                    stringBuilder.Append($"<td>{Math.Abs(cost.Amount) * residentPercentage:C}</td></tr>");
                    utilityCost += cost.Amount * residentPercentage;
                });

            var sumRentUtilities = 12 * lease.Cost + Math.Abs(utilityCost);
            stringBuilder.Append($"</table><p style=\"text-align:right; margin-right:5%;\"><b>Total: {sumRentUtilities:C}</b></p><hr>");
            //// add payments due \\\\


            //// add payments made \\\\
            var payments = InfoSysDbContext.G3Payments.Where(payment => payment.LeaseId == tenant.G3Lease.First().Id).ToList();
            var paymentTotal = 0.0;
            stringBuilder.Append("<p><b>Payments made</b></p><table><tr><th>Date</th><th>Description</th><th>Amount</th></tr>");
            payments.ForEach(payment =>
                {
                    stringBuilder.Append($"<tr><td>{payment.ValutaDate.ToShortDateString()}</td><td>{payment.Description}</td><td>{payment.Amount:C}</td></tr>");
                    paymentTotal += payment.Amount;
                });
            stringBuilder.Append($"</table></table><p style=\"text-align:right; margin-right:5%;\"><b>Total: {paymentTotal:C}</b></p><hr>");
            //// add payments made \\\\

            //// add summary \\\\
            var difference = paymentTotal - sumRentUtilities;
            stringBuilder.Append("<p><b>Summary</b></p><table><tr><th>Total payments due</th><th>Total payments made</th><th>Difference</th></tr>");
            stringBuilder.Append($"<tr><td>{sumRentUtilities:C}</td><td>{paymentTotal:C}</td><td>{difference:C}</td></table><hr>");

            if (difference >= 0)
                stringBuilder.Append($"We will transfer your remaining balance of {Math.Abs(difference):C} to your bank account {tenant.G3BankAccount.First().Iban} in the next 14 business days.");
            else
                stringBuilder.Append($"Please transfer your remaining outstanding dues in the amount of {Math.Abs(difference):C} to our bank account: DE17500105178533959591 in the next 14 business days");
            stringBuilder.Append("</body></html>");
            //// add summary \\\\

            return stringBuilder.ToString();
        }

        private string GetIdsOfSelectedItems(List<G3Tenant> items)
        {
            var ids = new StringBuilder(items.First().Id.ToString());
            items.Remove(items.First());
            items.ForEach(item => ids.Append($",{item.Id} "));

            return ids.ToString();
        }

        private void PopulateDynamicData()
        {
            // populate basic info
            TenantList = InfoSysDbContext.G3Tenant
                .Include(tenant => tenant.G3BankAccount)
                .Include(tenant => tenant.G3Lease)
                .ThenInclude(lease => lease.Unit)
                .ThenInclude(unit => unit.Property)
                .ThenInclude(property => property.Adress)
                .ToList();
            var displayContainerList = new List<TenantDisplayContainer>();

            TenantList.ForEach(tenant => displayContainerList.Add(new TenantDisplayContainer(tenant)));
            Tenants = new ObservableCollection<TenantDisplayContainer>(displayContainerList);

            _totalArea = TenantList.Sum(tenant => tenant.G3Lease.Sum(lease => lease.Unit.Area));
            _totalResidents = TenantList.Sum(tenant => tenant.G3Lease.Sum(lease => lease.Unit.ResidentNr));
        }
    }
}
