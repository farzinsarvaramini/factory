using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    public class DbCenter
    {
        protected clientContainer clientDb;

        public DbReportCenter ReportCenter;
        public DbUserCenter UserCenter;

        private static DbCenter _instance;
        public static DbCenter Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                else
                    return null;
            }
        }
        public DbCenter()
        {
            _instance = this;
            clientDb = new clientContainer();
            ReportCenter = new DbReportCenter(clientDb);
            UserCenter = new DbUserCenter(clientDb);
        }
    }
}