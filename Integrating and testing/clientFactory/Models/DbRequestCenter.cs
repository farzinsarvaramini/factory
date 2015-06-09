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
            RequestModel newReq = _container.RequestModels1.Create();
            newReq.Answer = req.Answer;
            newReq.Context = req.Context;
            newReq.Follow = req.Follow;
            newReq.IsAnswered = req.IsAnswered;
            newReq.IsNew = req.IsNew;
            newReq.Recipient = req.Recipient;
            newReq.RecipientId = req.RecipientId;
            newReq.SendDate = req.SendDate;
            newReq.Sender = req.Sender;
            newReq.SenderId = req.SenderId;
            newReq.Status = req.Status;
            newReq.Title = req.Title;

            _container.RequestModels1.Add(newReq);

            try
            {
                _container.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public int AddRequestWithId(RequestModel req)
        {
            RequestModel newReq = _container.RequestModels1.Create();
            newReq.Answer = req.Answer;
            newReq.Context = req.Context;
            newReq.Follow = req.Follow;
            newReq.IsAnswered = req.IsAnswered;
            newReq.IsNew = req.IsNew;
            newReq.Recipient = req.Recipient;
            newReq.RecipientId = req.RecipientId;
            newReq.SendDate = req.SendDate;
            newReq.Sender = req.Sender;
            newReq.SenderId = req.SenderId;
            newReq.Status = req.Status;
            newReq.Title = req.Title;

            _container.RequestModels1.Add(newReq);

            try
            {
                _container.SaveChanges();
                return newReq.Id;
            }
            catch{
                return 0;
            }

        }

        public RequestModel GetRequestDetails(int request_id)
        {
            var req = _container.RequestModels1.Where(r => r.Id == request_id).First();
            return req;
        }

        public List<RequestModel> GetNewRequest(int recipient_id)
        {
            var newReq = _container.RequestModels1.Where(r => r.RecipientId == recipient_id && r.IsNew==true).ToList();
            foreach (RequestModel r in newReq)
            {
                r.IsNew = false;
            }

            _container.SaveChanges();
            return newReq;
            
            
        }

        public bool AnswerToRequest(int request_id, bool isAccepted, string answer)
        {
            var req = _container.RequestModels1.Where(r => r.Id == request_id).First();
            if (isAccepted == true)
            {
                req.Status = 1;
            }
            else
                req.Status = -1;

            req.Answer = answer;
            req.IsNew = true;
            _container.SaveChanges();
            return true;

        }

        public List<RequestModel> GetSentRequests(int sender_id)
        {
            var req = _container.RequestModels1.Where(r => r.SenderId == sender_id).ToList();
            return req;
        }

        public List<RequestModel> GetReceivedrequests(int recipient_id)
        {
            var req = _container.RequestModels1.Where(r => r.RecipientId == recipient_id).ToList();
            return req;
        }

        public bool FollowRequest(int request_id)
        {
            var req = _container.RequestModels1.Where(r => r.Id == request_id).First().Follow = true;
            _container.SaveChanges();
            return true;
        }

        public List<RequestModel> GetAnsweredRequest(int sender_id)
        {
            var req = _container.RequestModels1.Where(r => r.IsAnswered == true && r.SenderId == sender_id).ToList();
            foreach (RequestModel r in req)
            {
                r.IsAnswered = false;
            }
            _container.SaveChanges();
            return req;
        }

    }
}
