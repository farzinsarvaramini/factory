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

            clientContainer clientDb = new clientContainer();

            Report re = clientDb.Reports.Create();
            ReportCategory rc = clientDb.ReportCategories.Create();
            Attachments atach = clientDb.Attachments.Create();
           


            re.Attachment = atach;
            re.Description = "یییییییی";
            re.isMark = true;
            re.isRead = true;
            re.Recipient = "ss";
            re.Recipient_ID = 222;
            re.ReportCategory = rc;
            re.SendDate = DateTime.Now;
            re.Sender = "Ali";
            re.Sender_ID = 23323;
            re.Title = "hhhd";

            rc.Title = "تولید";
            rc.Reports.Add(re);

            atach.FileLocation = "fads";
            atach.Report = re;
            atach.uploadTime = DateTime.Now;



          

            clientDb.ReportCategories.Add(rc);

            try
            {
                clientDb.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Not Addedddddddddddddddddd");
            }

            User usr = clientDb.Users.Create();
            User usr1 = clientDb.Users.Create();

            usr.Address = "لنگرود"; usr1.Address = "همدان";
            usr.Age = 20; usr1.Age = 20;
            usr.AvatarLocation = "ABC"; usr1.AvatarLocation = "ABC";
            usr.Email = "e@yahoo"; usr1.Email = "e@yahoo";
            usr.EmploymentDate = DateTime.Now; usr1.EmploymentDate = DateTime.Now;
            usr.FirstName = "احسان"; usr1.FirstName = "فرزین";
            usr.Gender = true; usr1.Gender = true;
            usr.LastName = "گلشنی"; usr1.LastName = "امینی";
            usr.NationalId = 32433; usr1.NationalId = 32433;
            usr.Password = "sdf"; usr1.Password = "sdfdf";
            usr.PhoneNumber = "000122312"; usr1.PhoneNumber = "000122312";
            usr.Resume = "بیکار"; usr1.Resume = "رئیس";
            usr.RoleId = 23433; usr1.RoleId = 234232;
            usr.RoleName = "للل"; usr1.RoleName = "سیسشب";
            //usr.UserId = usr1.Id; usr1.UserId = usr.Id;
            usr.Username = "aaa"; usr1.Username = "bbb";
            usr.DefaultUser = false; usr1.DefaultUser = false;
            usr.IsNew = false; usr1.IsNew = false;


            clientDb.Users.Add(usr);
            clientDb.Users.Add(usr1);

            try
            {
                clientDb.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Not Addedddddddddddddddddd");
            }

            //r.show();
            //rl.show();
        }
    }
}
