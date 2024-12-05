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
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        public AgentPage()
        {
            InitializeComponent();

            var currentAgents = IbragimovDGlazkiSaveEntities.GetContext().Agent.ToList();

            AgentsListView.ItemsSource = currentAgents;

            ComboFilter.SelectedIndex = 0;

            UpdateAgents();
        }

        private void UpdateAgents()
        {
            var currentAgents = IbragimovDGlazkiSaveEntities.GetContext().Agent.ToList();

            // фильтр

            //if (ComboFilter.SelectedIndex == 0)
            //{
            //    currentAgents = IbragimovDGlazkiSaveEntities.GetContext().Agent.ToList();
            //}

            if (ComboFilter.SelectedIndex == 1)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.Contains("МФО")).ToList();
            }
            if (ComboFilter.SelectedIndex == 2)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.Contains("ООО")).ToList();
            }
            if (ComboFilter.SelectedIndex == 3)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.Contains("ЗАО")).ToList();
            }
            if (ComboFilter.SelectedIndex == 4)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.Contains("МКК")).ToList();
            }
            if (ComboFilter.SelectedIndex == 5)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.Contains("ОАО")).ToList();
            }
            if (ComboFilter.SelectedIndex == 6)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.Contains("ПАО")).ToList();
            }


            // поиск
            currentAgents = currentAgents.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            AgentsListView.ItemsSource = currentAgents.ToList();

            // сортировка

            if (ComboSort.SelectedIndex == 0)
            { // по возрастанию
                AgentsListView.ItemsSource = currentAgents.OrderBy(p => p.Title).ToList();

            }
            if (ComboSort.SelectedIndex == 1)
            { // по убыванию
                AgentsListView.ItemsSource = currentAgents.OrderByDescending(p => p.Title).ToList();
            }
            if (ComboSort.SelectedIndex == 2)
            {

            }
            if (ComboSort.SelectedIndex == 3)
            {

            }
            if (ComboSort.SelectedIndex == 4)
            {
                AgentsListView.ItemsSource = currentAgents.OrderBy(p => p.Priority).ToList();

            }
            if (ComboSort.SelectedIndex == 5)
            {
                AgentsListView.ItemsSource = currentAgents.OrderByDescending(p => p.Priority).ToList();

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }
    }
}
