using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    public class CUserController
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
            _loginView.SetController(this);
            _loginView.username.Text = _defaultUser.Username;
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
            _initForm.SetController(this);
            _initForm.Show();
        }

        public void CloseInitializeForm()
        {
            _initForm.Close();
        }

        private bool SetDefaultUser(string un)
        {
            object[] req_content = {un};
            Request userDetailrequest = new Request(RequestType.INIT, req_content);
            string result = _cc.ProcessRequest(userDetailrequest);
            if (result == "Code #3")
            {
                _defaultUser = _db.UserCenter.GetDefaultUser();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void Login()
        {
            if (_defaultUser.Username == _loginView.GetUsername() && _defaultUser.Password == _loginView.GetPassword())
            {
                SessionInfos.login_user = _defaultUser;
                // create and show home
                _loginView.ErrorMessage("User login user_name: " + _defaultUser.Username + " \nPass: " + _defaultUser.Password);
            }
            else if (_defaultUser.Password != _loginView.GetPassword())
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
            object[] req_content = { _defaultUser.Id };
            Request getReq = new Request(RequestType.GET, req_content);
            _cc.SendRequest(getReq);
        }
    }
}
