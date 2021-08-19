using System;
using System.IO;
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
            if (File.Exists("GUID.save")) GUID.Text = File.ReadAllText("GUID.save");
            if (File.Exists("Token.save")) Token.Text = File.ReadAllText("Token.save");
            if (File.Exists("Secret.save")) Secret.Text = File.ReadAllText("Secret.save");
            if (File.Exists("Confirmation.save")) Confirmation.Text = File.ReadAllText("Confirmation.save");
            if (File.Exists("Input.save")) Input.Text = File.ReadAllText("Input.save");
        }

        private void CheckInput(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("GUID.save", GUID.Text);
            File.WriteAllText("Token.save", Token.Text);
            File.WriteAllText("Secret.save", Secret.Text);
            File.WriteAllText("Confirmation.save", Confirmation.Text);
            File.WriteAllText("Input.save", Input.Text);


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
