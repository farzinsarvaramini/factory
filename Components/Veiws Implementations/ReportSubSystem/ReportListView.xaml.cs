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
    /// Interaction logic for ReportListView.xaml
    /// </summary>
    public partial class ReportListView : Window
    {
        public ReportListView()
        {
            InitializeComponent();
        }

        public CViewReportController _controller;
        private List<Tuple<Report, ReportCategory, Attachments>> _reports;
        private int _selectedReportId;
        private string _searchTitle;
        private string _searchCategory;
        private int _searchUserId;
        private DateTime _searchDateStart;
        private DateTime _searchDateEnd;


        // this function show dialog
        public void show()
        {
            this.Show();
        }

        // this function close dialog
        public void close()
        {
            this.Close();
        }

        public void setAllReportList(List<Report> r)
        {
            //
            List<Report> reps = new List<Report>();
            reps.Add(new Report(2,"فرزین","بیا بگیر گزارشتو بابا","گزارش اول"));
            reps.Add(new Report(4, "فرزینdd", "بیا بگیر گزارشتddو بابا", "گزارش ddاول"));
            allReports_dg.ItemsSource = reps;

        }
        
        public void setSentReportList(List<Report> r)
        {



        }
        public void setRecievedReportList(List<Report> r)
        {



        }



    //    public VReporListView (CViewReportController c)
    //    {
    //        this._controller = c;
    //    }



    //    /*
    //     * this function update _selectedReportId
    //     * this function return id of selected report
    //     */
    //    public int getSelectedReportId();

    //    /*
    //     * this function add new report to list
    //     * gui must updated
    //     */
    //    public void addNewReport(Tuple<addNewReport, ReportCategory, Attachment> report);

    //    // this function delete a report from list in gui
    //    public void deleteFromList(int id){

    //    }
		
    //    // this function return search title in text box
    //    public string getSearchTitle(){

    //    }

    //    // this function return id of selected user in search comboBox
    //    public int getSearchUser(){

    //    }
		
    //    // this function return title if selected category in search group
    //    public string getSearchCategory(){

    //    }

    //    // these functions return start and end report date in search section
    //    public DateTime getSearchStartDate(){

    //    }

    //    public DateTime getSearchEndDate()
    //    {

    //    }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showReport_b_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (allReports_dg.SelectedItems.Count == 1)
                {
                    _controller.showReportView((Report)allReports_dg.SelectedItem);
                }
                else if (sentReports_dg.SelectedItems.Count == 1)
                {
                    _controller.showReportView((Report)sentReports_dg.SelectedItem);
                }
                else if (sentReports_dg.SelectedItems.Count == 1)
                {
                    _controller.showReportView((Report)recievedReports_dg.SelectedItem);
                }
                else
                {
                    MessageBox.Show("لطفا یکی از گزارش ها را برای نمایش انتخاب نمایید !!!", "خطا");
                }
            }
            catch
            {
                MessageBox.Show("لطفا گزارش معتبر انتخاب کنید !!!", "خطا");
            }
        }

        private void deleteReport_b_Click(object sender, RoutedEventArgs e)
        {
            //if a report is selected
            //_controller.DeleteReport(selected report)
        }

        private void markReport_b_Click(object sender, RoutedEventArgs e)
        {
            //if a report is selected
            //_controller.MarkReport(selected report)
        }

        private void returnBack_b_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void search_b_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
