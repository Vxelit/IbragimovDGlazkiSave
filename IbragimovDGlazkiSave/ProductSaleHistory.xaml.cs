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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IbragimovDGlazkiSave
{
    /// <summary>
    /// Логика взаимодействия для ProductSaleHistory.xaml
    /// </summary>
    public partial class ProductSaleHistory : Page
    {

        Agent _currentAgent;

        public ProductSaleHistory(Agent currentAgent)
        {
            InitializeComponent();


            _currentAgent = currentAgent;
            var context = IbragimovDGlazkiSaveEntities.GetContext();
            var currentProductSales = context.ProductSale.Where(p => p.AgentID.Equals(currentAgent.ID)).ToList();

            ProductSaleListView.ItemsSource = currentProductSales;

            UpdateProductSales();
        }

        private void UpdateProductSales()
        {
            var currentProductsSales = IbragimovDGlazkiSaveEntities.GetContext().ProductSale.Where(p => p.AgentID.Equals(_currentAgent.ID)).ToList();

            currentProductsSales = currentProductsSales.Where(p => p.ProductTitle.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            ProductSaleListView.ItemsSource = currentProductsSales;
        }


        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProductSales();
        }

        private void ProductSaleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductSaleListView.SelectedItems.Count >= 1)
            {
                DeleteBtn.Visibility = Visibility.Visible;
            }
            else
            {
                DeleteBtn.Visibility = Visibility.Hidden;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductSaleAddWindow addWindow = new ProductSaleAddWindow(_currentAgent);

            addWindow.ShowDialog();

            UpdateProductSales();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var context = IbragimovDGlazkiSaveEntities.GetContext();
            var selectedProductSale = ProductSaleListView.SelectedItems;

            foreach (ProductSale productSale in selectedProductSale)
            {
                context.ProductSale.Remove(productSale);
            }

            context.SaveChanges();

            UpdateProductSales();
        }
    }
}
