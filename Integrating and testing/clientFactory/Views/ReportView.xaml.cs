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
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : Window
    {
        public ReportView()
        {
            InitializeComponent();
        }


        public CViewReportController _controller;
        public Report report;
        private DateTime _date;
        private bool _isSendingReport;
        private string _senderOrRecepient;
        private string _description;
        private string _attachmentFileName;
        private string[] _categories;



        public void show()
        {
            
            this.Show();
        }
        public void close()
        {
            this.close();
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _controller.DeleteReport(this.report);
        }

        private void Mark_Click(object sender, RoutedEventArgs e)
        {
            _controller.MarkReport(this.report);
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            ///badan
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void getAttachedFiles_b_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
