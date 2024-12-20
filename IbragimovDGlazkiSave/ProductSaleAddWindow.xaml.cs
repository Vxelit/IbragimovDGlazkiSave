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

namespace IbragimovDGlazkiSave
{
    /// <summary>
    /// Логика взаимодействия для ProductSaleAddWindow.xaml
    /// </summary>
    public partial class ProductSaleAddWindow : Window
    {

        ProductSale currentProductSale = new ProductSale();

        public ProductSaleAddWindow(Agent currentAgent)
        {
            InitializeComponent();

            var context = IbragimovDGlazkiSaveEntities.GetContext();


            currentProductSale.AgentID = currentAgent.ID;

            DataContext = currentProductSale;

            ProductComboBox.ItemsSource = context.Product.Select(p => p.Title).ToList();

        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentProductSale.ProductID.ToString()))
                errors.AppendLine("Укажите продукт");
            if (string.IsNullOrWhiteSpace(currentProductSale.SaleDate.ToString()))
                errors.AppendLine("Укажите дату");

            if (string.IsNullOrWhiteSpace(currentProductSale.ProductCount.ToString()) || currentProductSale.ProductCount > 0)
                errors.AppendLine("Количество не указано или указано не верно.");


            IbragimovDGlazkiSaveEntities.GetContext().ProductSale.Add(currentProductSale);

            try
            {
                IbragimovDGlazkiSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }


        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            currentProductSale.ProductID = ProductComboBox.SelectedIndex + 1;
            
        }
    }
}
