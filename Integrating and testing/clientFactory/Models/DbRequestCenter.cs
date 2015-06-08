using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    class DbRequestCenter
    {
        // IsAnswer Flag
        // change status to shortint
        // add serverid field
        private clientContainer _container;
        public DbRequestCenter(clientContainer con)
        {
            _container = con;
        }

        public bool AddRequest(RequestModel req)
        {
            return true;
        }

        public RequestModel GetRequestDetails(int request_id)
        {
            return null;
        }

        public List<RequestModel> GetNewRequest(int recipient_id)
        {
            return null;
        }

        public bool AnswerToRequest(int request_id, bool isAccepted, string answer)
        {
            return true;
        }

        public List<RequestModel> GetSentRequests(int sender_id)
        {
            return null;
        }

        public List<RequestModel> GetReceivedrequests(int recipient_id)
        {
            return null;
        }

        public bool FollowRequest(int request_id)
        {
            return true;
        }


    }
}
