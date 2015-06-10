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
    /// Interaction logic for RecievedRequest.xaml
    /// </summary>
    public partial class VRecievedRequest : Window
    {
        private CRequestController _controller;
        public RequestModel current_request;

        public VRecievedRequest()
        {
            InitializeComponent();
        }

        public void SetController(CRequestController con)
        {
            _controller = con;
        }

        private void BackEvent_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void ShowForm()
        {
            Subject.Text = current_request.Title;
            Sender.Text = current_request.Sender;
            Send_Date.Text = current_request.SendDate.ToString();
            Description.Text = current_request.Context;

            float status = current_request.Status;
            if (status != 0)
            {
                Answer.Text = current_request.Answer;
                Answer.IsEnabled = false;
                if (status == 1)
                    Accept_check.IsChecked = true;
                else if (status == -1)
                    Reject_check.IsChecked = true;
                Accept_check.IsEnabled = false;
                Reject_check.IsEnabled = false;

                Send_AnswerBtn.IsEnabled = false;
            }

            Show();
        }

        private void Send_AnswerBtn_Click(object sender, RoutedEventArgs e)
        {
            _controller.SendRequestAnswer();
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
