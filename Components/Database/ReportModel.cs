using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    class ReportModel
    {
        public Int32 sender_id { private set; get; }
        public string sender { private set; get; }
        public Int32 recipient_id { private set; get; }
        public string recipient { private set; get; }
        public DateTime sendDate { private set; get; }
        public bool isRead { private set; get; }
        public bool isMark { private set; get; }


        }
        
}
