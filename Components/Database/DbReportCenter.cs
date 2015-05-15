using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    class DbReportCenter:DbCenter
    {
        
        public DbReportCenter instance;

        public DbReportCenter()
        {
            instance = this; // skeleton patern
        }



        public bool newReport(Report r,ReportCategory rc,Attachments atach=null){
              
            Report newReport = clientDb.Reports.Create();
          //  ReportCategory newRc = clientDb.ReportCategories.Create();
          //  Attachments newAtach = clientDb.Attachments.Create();

            newReport.Sender_ID = r.Sender_ID;
            newReport.Sender = r.Sender;
            newReport.Recipient = r.Recipient;
            newReport.Recipient_ID = r.Recipient_ID;
            newReport.SendDate = r.SendDate;
            newReport.isRead = r.isRead;
            newReport.isMark = r.isMark;
            if(atach!=null)
                newReport.Attachment = atach;
            newReport.ReportCategories.Add(rc);
            clientDb.Reports.Add(newReport);
            try{
                clientDb.SaveChanges();
                Console.WriteLine(r.Id.ToString()+"is Added");
                return true;
            }
            catch{
                return false;
            }
        }


        public bool deleteReport(Int32 ID)
        {
            //var r = clientDb.Reports.Where(id1 => id1.Id == ID);
            //List<Report> r = clientDb.Reports.Where(i => i.Id == ID).ToList();
          //  ReportModel r = clientDb.Reports.Where(i => i.Id == ID).First();
            using (clientDb)
            {
                Report r = clientDb.Reports.Where(i => i.Id == ID).First();
                clientDb.Reports.Remove(r);
            }
            
            try
            {
                clientDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public Report getReportDetails(int id){
            Report r = clientDb.Reports.Where(iid => iid.Id == id).First();
            return r;
        }



        public bool markReport(int id)
        {
            clientDb.Reports.Where(iid => iid.Id == id).First().isMark = true;
            clientDb.SaveChanges();
            return true;
        }

        
        
        public bool readReport(int id){
            clientDb.Reports.Where(iid => iid.Id == id).First().isRead=true;
            return true;
        }


    }
}
