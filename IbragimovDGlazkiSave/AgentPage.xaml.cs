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

        int CountRecords;
        int CountPage;
        int CurrentPage = 0;

        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;

        public AgentPage()
        {
            InitializeComponent();

            var currentAgents = IbragimovDGlazkiSaveEntities.GetContext().Agent.ToList();

            AgentsListView.ItemsSource = currentAgents;

            ComboFilter.SelectedIndex = 0;
            ComboSort.SelectedIndex = 0;

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
            currentAgents = currentAgents
                .Where(p =>
                    p.Title.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                    p.Email.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                    p.Phone
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("-", "")
                        .Replace("+", "")
                        .Replace(" ", "")
                        .Contains(TBoxSearch.Text.ToLower()
                )).ToList();


            // сортировка

            //if (ComboSort.SelectedIndex == 0)
            //{

            //}

            if (ComboSort.SelectedIndex == 1)
            { // по возрастанию
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }
            if (ComboSort.SelectedIndex == 2)
            { // по убыванию
                currentAgents = currentAgents.OrderByDescending(p => p.Title).ToList();
            }
            if (ComboSort.SelectedIndex == 3)
            {
                currentAgents = currentAgents.OrderBy(p => p.Discount).ToList();
            }
            if (ComboSort.SelectedIndex == 4)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Discount).ToList();
            }
            if (ComboSort.SelectedIndex == 5)
            {
                currentAgents = currentAgents.OrderBy(p => p.Priority).ToList();

            }
            if (ComboSort.SelectedIndex == 6)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Priority).ToList();

            }

            AgentsListView.ItemsSource = currentAgents.ToList();
            TableList = currentAgents;

            ChangePage(0, 0);
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

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }

        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }

            Boolean IfUpdate = true;

            int min;

            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountPage ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            IfUpdate = false;
                        }
                        break;

                    case 2: 
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            IfUpdate = false;
                        }
                        break;
                }
            }
            
            if (IfUpdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }

                PageListBox.SelectedIndex = CurrentPage;

                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();

                AgentsListView.ItemsSource = CurrentPageList;
                AgentsListView.Items.Refresh();
            }
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                IbragimovDGlazkiSaveEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                AgentsListView.ItemsSource = IbragimovDGlazkiSaveEntities.GetContext().Agent.ToList();
                UpdateAgents();
            }
        }

        private void AgentsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AgentsListView.SelectedItems.Count > 1)
            {
                PriorityChangeButton.Visibility = Visibility.Visible;
            } else
            {
                PriorityChangeButton.Visibility = Visibility.Hidden;
            }
        }

        private void PriorityChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAgents = AgentsListView.SelectedItems;

            int maxPriority = 1;

            foreach (Agent agent in selectedAgents)
            {
                if (maxPriority < agent.Priority)
                {
                    maxPriority = agent.Priority;
                }
            }

            
            AgentPriorityChangeWindow windowChange = new AgentPriorityChangeWindow(maxPriority);

            windowChange.ShowDialog();

            

            if (AgentPriorityChangeWindow.isCloseStatusOk)
            {

                foreach (Agent agent in selectedAgents)
                {
                    agent.Priority = AgentPriorityChangeWindow.newPriority;
                }

                IbragimovDGlazkiSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("Изменения сохранены.");

            } else
            {
                MessageBox.Show("Изменений не было.");
            }





            UpdateAgents();
        }
    }
}
