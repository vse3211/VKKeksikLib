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

namespace VKKeksikLib.Tests.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckInput(object sender, RoutedEventArgs e)
        {
            PonchikClient.CallBack client = new VKKeksikLib.PonchikClient.CallBack(Convert.ToInt32(GUID.Text), Token.Text, Secret.Text, Confirmation.Text);
            /* Объявляем функцию для эвента OnNewConfirmation */
            client.OnNewConfirmation += client_OnNewConfirmation;
            /* Объявляем функцию для эвента OnNewDonate */
            client.OnNewDonate += client_OnNewDonate;
            /* Объявляем функцию для эвента OnNewPaymentStatus */
            client.OnNewPaymentStatus += client_OnNewPaymentStatus;
            /* Объявляем функцию для эвента OnError */
            client.OnError += client_OnError;

            /* Передаем в обработчик CallBack запросов полученный JSON массив */
            client.Input(Input.Text);
            void client_OnError(string type, string answer, object obj = null)
            {
                _Result.Text = answer;
            }

            void client_OnNewPaymentStatus(string type, string answer, object obj = null)
            {
                _Result.Text = answer;
            }

            void client_OnNewDonate(string type, string answer, object obj = null)
            {
                _Result.Text = answer;
            }

            void client_OnNewConfirmation(string type, string answer, object obj = null)
            {
                _Result.Text = answer;
            }
        }
    }
}
