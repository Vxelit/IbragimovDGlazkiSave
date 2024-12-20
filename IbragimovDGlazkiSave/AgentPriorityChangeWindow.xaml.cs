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
    /// Логика взаимодействия для AgentPriorityChangeWindow.xaml
    /// </summary>
    public partial class AgentPriorityChangeWindow : Window
    {

        static public bool isCloseStatusOk = false;
        static public int newPriority;

        public AgentPriorityChangeWindow(int maxPriority)
        {
            InitializeComponent();

            PriorityTextBox.Text = maxPriority.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PriorityTextBox.Text))
                MessageBox.Show("введите приоритет в поле.");
            else
            {
                try
                {
                    int priority = Int32.Parse(PriorityTextBox.Text);

                    if (priority >= 0)
                    {
                        newPriority = priority;
                        isCloseStatusOk = true;
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("приоритет не может быть меньше 0.");
                    }
                }
                catch
                {
                    MessageBox.Show("Введите в поле только цифры");
                }
            }

        }
    }
}
