using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    public class CHomePageController
    {

        ClientCommunication client;
        private HomePage homePage;
        private DbCenter dbCenter;

        public CSendReportController sReportC;
        public CViewReportController vReportC;
        public CRequestController cRequestC;
        public CUserController cUserC;

        public CHomePageController(DbCenter db,ClientCommunication cC)
        {
            client = cC;
            dbCenter = db;
            sReportC =
            new CSendReportController(db, cC);
            vReportC = new CViewReportController(db, cC);
            cRequestC = new CRequestController(db, cC);
            cUserC = new CUserController(db, cC);
        }

        public void showHomePage()
        {
            homePage = new HomePage();
            homePage.controller = this;

            //other code

            homePage.show();
        }

    }
}
