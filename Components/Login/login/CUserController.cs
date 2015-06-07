using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login
{
    class CUserController
    {
        private VLoginForm _loginView;
        private VInitializeForm _initForm;

        private DbCenter _db;
        private User _defaultUser;
        private ClientCommunication _cc;

        public CUserController(DbCenter db, ClientCommunication c)
        {
            _loginView = new VLoginForm();
            _initForm = new VInitializeForm();
            _db = db;
            _defaultUser = _db.UserCenter.GetDefaultUser();
            _cc = c;
        }

        public void ShowLoginForm()
        {
            if ( _loginView == null )
                _loginView = new VLoginForm();
            _loginView.username.Text = _defaultUser.user_name;
            _loginView.Show();
        }

        public void CloseLoginForm()
        {
            _loginView.Close();
        }

        public void ShowInitializeForm()
        {
            if (_initForm == null )
                _initForm = new VInitializeForm();
            _initForm.Show();
        }

        public void CloseInitializeForm()
        {
            _initForm.Close();
        }

        public bool SetDefaultUser(string un)
        {
            object[] req_content = {un};
            Request userDetailrequest = new Request(RequestType.Get_User, req_content);
            object[] result = _cc.SendRequest(userDetailrequest);
            
            if ( result.Length > 0 )
            {
                User usr = (User)result[0];
                _db.UserCenter.AddUser(usr);
                _db.UserCenter.SetDefaultUser(usr.id);
                return true;
            }
            return false;
        }

        public void Login()
        {
            if (_defaultUser.username == _loginView.GetUsername() && _defaultUser.password == _loginView.GetPassword())
            {
                // create and show home
            }
            else if (_defaultUser.password != _loginView.GetPassword())
            {
                _loginView.ErrorMessage("رمز عبور اشتباه است");
            }
        }

        public void Personalize()
        {
            if ( SetDefaultUser(_initForm.GetUserName()) )
            {
                // get stuffs and store in db
                GetDataFromServer();
                CloseInitializeForm();
                ShowLoginForm();
            }
            else
            {
                _initForm.ErrorMessage("چنین نام کاربری ای در سیستم ثبت نشده است");
            }
        }
        
        private void GetDataFromServer()
        {
            Request getReq = new Request(RequestType.Get);
            _cc.SendRequest(getReq);
        }
    }
}
