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

namespace clientFactory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VLoginForm : Window
    {
        private CUserController _controller;

        public VLoginForm()
        {
            InitializeComponent();
           // ErrorMessage("vdnd");
        }

        public void SetController(CUserController con)
        {
            _controller = con;
        }

        public void SetUsername(string un)
        {
            username.Text = un;
        }

        public string GetUsername()
        {
            return username.Text;
        }

        public string GetPassword()
        {
            return pass.Password;
        }

        public void ErrorMessage(string message)
        {
            MessageBox.Show(message,
                "خطا",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
        }

        private void LoginClicked(object sender, RoutedEventArgs e)
        {
            _controller.Login();
        }
    }
}
