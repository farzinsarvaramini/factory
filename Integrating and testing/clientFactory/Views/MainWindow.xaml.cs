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

    public partial class MainWindow : Window
    {
        private CSendReportController _sCon = new CSendReportController(new DbReportCenter(new clientContainer()));
        private CViewReportController _vCon = new CViewReportController(new DbReportCenter(new clientContainer()));
        
       // ReportView r = new ReportView();
       // ReportListView rl = new ReportListView();
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void report_button_Click(object sender, RoutedEventArgs e)
        {
            this._sCon.showView();
            this._vCon.showReportListView();
            
            //r.show();
            //rl.show();
        }
    }
}
