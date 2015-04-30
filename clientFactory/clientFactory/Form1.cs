using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clientFactory
{
    public partial class Form1 : Form
    {
        clientContainer clientDb;
        public Form1()
        {
            InitializeComponent();


            Report r = new Report();
            ReportCategory newRc = clientDb.ReportCategories.Create();
            newRc.Title = "ALi";
            newRc.Reports = r;
            r.isMark = false;
            r.isRead = false;
            r.Recipient = "Ali";
            r.Recipient_ID = 111111;
            r.SendDate = DateTime.Now;
            r.Sender = "Farzin";
            r.Sender_ID = 222222;
            r.Attachment= new Attachments();
            r.ReportCategories = new ReportCategory();
            DbReportCenter a = new DbReportCenter();
            if (!a.newReport(r))
                Console.WriteLine(2);
            


            clientDb = new clientContainer();
            ReportCategory test = clientDb.ReportCategories.Create();
            test.Title = "testTitle";

            clientDb.ReportCategories.Add(test);
            clientDb.SaveChanges();
            MessageBox.Show("Added " + test.Id.ToString());
        }

    }
}
