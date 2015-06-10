using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class CRequestController
    {
        private VSendRequest _sendRequestView;
        private VRecievedRequest _recivedRequestView;
        private VSentRequest _sentRequestView;
        private VRequestList _requestListView;

        private DbCenter _db;
        private ClientCommunication _cc;

        private List<User> _allowedRecipients;

        public CRequestController(DbCenter db, ClientCommunication c)
        {
            _db = db;
            _cc = c;
            _sendRequestView = new VSendRequest();
            _recivedRequestView = new VRecievedRequest();
            _sentRequestView = new VSentRequest();
            _requestListView = new VRequestList();
        }

        public void ShowSendRequestForm()
        {
            if ( _sendRequestView == null )
                _sendRequestView = new VSendRequest();
            _sendRequestView.Date.Content = DateTime.Today;
            _allowedRecipients = _db.ReportCenter.getAllowedRecipientsList();
            foreach (User usr in _allowedRecipients)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = usr.FirstName + usr.LastName + " - " + usr.RoleName;
                item.Value = usr.Id;
                _sendRequestView.Recipients.Items.Add(item);
            }
            _sendRequestView.Show();
        }

        public void SendRequestEvent()
        {
            RequestModel req = new RequestModel();
            req.Title = _sendRequestView.Title;
            req.SenderId = SessionInfos.login_user.Id;
            req.Sender = SessionInfos.login_user.FirstName + SessionInfos.login_user.LastName + " - " + SessionInfos.login_user.RoleName;
            req.SendDate = DateTime.Now;
            ComboboxItem selectedItem = _sendRequestView.Recipients.SelectedItem as ComboboxItem;
            req.RecipientId = (int)(selectedItem).Value;
            req.Recipient = selectedItem.Text;
        }
    }
}
