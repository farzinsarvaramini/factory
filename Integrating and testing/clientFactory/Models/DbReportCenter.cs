using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    public class DbReportCenter
    {
        private clientContainer clientDb;
        public DbReportCenter(clientContainer con)
        {
            clientDb = con;
        }
        public bool newReport(Report r, ReportCategory rc, Attachments atach = null)
        {

            Report newReport = clientDb.Reports.Create();
            ReportCategory newRc = clientDb.ReportCategories.Create();
            Attachments newAtach = clientDb.Attachments.Create();

            newReport.Sender_ID = r.Sender_ID;
            newReport.Sender = r.Sender;
            newReport.Recipient = r.Recipient;
            newReport.Recipient_ID = r.Recipient_ID;
            newReport.SendDate = r.SendDate;
            newReport.isRead = r.isRead;
            newReport.isMark = r.isMark;
            newReport.Title = r.Title;
            newReport.Description = r.Description;
            if (atach != null)
                newReport.Attachment = newAtach;
            newReport.ReportCategory = newRc;


            newRc.Reports.Add(newReport);
            newRc.Title = rc.Title;

            newAtach.FileLocation = atach.FileLocation;
            newAtach.Report = newReport;
            newAtach.uploadTime = atach.uploadTime;

            clientDb.Reports.Add(newReport);
            try
            {
                clientDb.SaveChanges();
                Console.WriteLine("000000000000\n");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("goh \n" + e.StackTrace + "    ljkkjhghj    " + e.Message);
                return false;
            }
        }

        public bool setServerId(int id)
        {
            clientDb.Reports.Where(s => s.Id == id).First().ServerId = id;
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

        public List<ReportCategory> getCategoryList()
        {
            var s1 = from s in clientDb.ReportCategories select s;
            var s2 = s1.ToList();
            return s2;
        }

        public List<User> getAllowedRecipientsList()
        {
            var s = from s1 in clientDb.Users select s1;
            var s2 = s.ToList();
            return s2;
        }



        public ReportCategory getReportCategory(Int32 Id)
        {
            var RepCat = clientDb.ReportCategories.Where(s => s.Id == Id).First();
            return RepCat;
        }

        public Attachments newAttachment(Report r, string fileLoc)
        {

            Attachments newAtach = clientDb.Attachments.Create();
            newAtach.FileLocation = fileLoc;
            newAtach.Report = r;
            newAtach.uploadTime = DateTime.Now;
            clientDb.Attachments.Add(newAtach);
            try
            {
                clientDb.SaveChanges();
                return newAtach;
            }
            catch
            {
                return null;
            }

        }


        public bool deleteReport(Int32 ID)
        {
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

        public Report getReportDetails(int id)
        {
            Report r = clientDb.Reports.Where(iid => iid.Id == id).First();
            return r;
        }

        public bool markReport(int id)
        {
            clientDb.Reports.Where(iid => iid.Id == id).First().isMark = true;
            clientDb.SaveChanges();
            return true;
        }

        public bool readReport(int id)
        {
            clientDb.Reports.Where(iid => iid.Id == id).First().isRead = true;
            return true;
        }

        public int GetServerReportId(int reportId)
        {
            return clientDb.Reports.Where(iid => iid.Id == reportId).First().Id;
        }

        public Tuple<Report, ReportCategory, Attachments> GetReportDetails(int reportId)
        {
            Report r = clientDb.Reports.Where(s => s.Id == reportId).First();
            Tuple<Report, ReportCategory, Attachments> t = new Tuple<Report, ReportCategory, Attachments>(r, r.ReportCategory, r.Attachment);
            return t;
        }
    }
}
