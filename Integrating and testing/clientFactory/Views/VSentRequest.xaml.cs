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
    /// Interaction logic for SentRequest.xaml
    /// </summary>
    public partial class VSentRequest : Window
    {
        public CRequestController controller;

        public RequestModel current_request;

        public VSentRequest()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Subject.Text = current_request.Title;
            Send_Date.Text = current_request.SendDate.ToString();
            Recipient.Text = current_request.Recipient;
            Description.Text = current_request.Context;

            float status = current_request.Status;
            if (status == 0)
                State.Content = "پذیرنده درخواست هنور پاسخی ارسال نکرده است";
            else
            {
                if (status == 1)
                    State.Content = "پذیرنده درخواست هنور پاسخی ارسال نکرده است";
                else if ( status == -1 )
                    State.Content = "پذیرنده درخواست هنور پاسخی ارسال نکرده است";
                Answer.Text = current_request.Answer;
            }

            if (current_request.Follow)
                Follow_Btn.IsEnabled = false;

            Show();
        }

        private void BackEvent_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Follow_Btn_Click(object sender, RoutedEventArgs e)
        {
            controller.FollowRequest();
        }

        public void ErrorMessage(string message)
        {
            MessageBox.Show(message,
                "خطا",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
        }

        public void SuccessMessage(string message)
        {
            MessageBox.Show(message,
                "نتیجه",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
