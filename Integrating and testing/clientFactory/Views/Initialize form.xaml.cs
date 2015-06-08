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

namespace clientFactory
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class VInitializeForm : Window
    {
        private CUserController controller { get; set; }

        public VInitializeForm()
        {
            InitializeComponent();
        }

        public string GetUserName()
        {
            return user_name.Text;
        }

        public bool IsAgree()
        {
            return isAgree.IsChecked.Value;
        }

        public void ErrorMessage(string message)
        {
            MessageBox.Show(message,
                "خطا",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isAgree.IsChecked.Value)
                ErrorMessage("شما باید موافق شخصی سازی این رایانه باشید");
            else
                controller.Personalize();
        }

    }
}
