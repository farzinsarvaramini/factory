using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    class VSendReportForm
    {
        private CSendReportController _controller;
		private List<ReportCategory> _categories;
		private List<User> _allowedRecipienters;
        private SendReportView ReportView;

		public VSendReporForm (CSendReportController c)
		{
			this._controller = c;
            SendReportView ReportView = new SendReportView();
		}

		// this function show dialog
		public void show(){
            ReportView.Show();
        }

		// this function close dialog
		public void close(){
            ReportView.Close();
        }

		// this function return new report title
		public string getTitle(){
            return ReportView.title_textbox.ToString();
        }

		// this function return new report description
		public string getTDescription(){
            return ReportView.description_textbox.ToString();
        }

		// this function return id of selected recipient
		public int getRecipientID();
           // ReportView.recepient_combobox.
        

		// this function return title if selected category
		public string getCategoryTitle(){
            return ReportView.category_combobox.Text.ToString();
        }

		/* this function set list of categories which show in form
		 * this list provided by controller
		 * controller ask this list from DbCenter and pass it to this form
		 */
		public void setCategoriesList(List<ReportCategory> cl){
            foreach (ReportCategory item in cl){
                 ReportView.category_combobox.Items.Add(item);
                 ReportView.category_combobox.DisplayMemberPath="Title";
                ReportView.category_combobox.SelectedValuePath="Id";
            }
        }
		/* this function set list of recipienters which this user can send report to them
		 * this list provided by controller
		 * controller ask this list from DbCenter and pass it to this form
		 */
		public void setRecipientList(List<User> r){
            foreach(User item in r){
                ReportView.recepient_combobox.Items.Add(item);
                ReportView.category_combobox.DisplayMemberPath = "LastName";
                ReportView.category_combobox.SelectedValuePath = "Id";
            }
        }

		// this function return attachments location in client system
		public string getAttachments(){
            return ReportView.userSelectedFilePath;
        }

		/* when user click on send button this function called.
		 * if everything entered by user this function call correspondig function in controller
		 * else show error message to user and ask him(her) to fill all of fields
		 */
		public void send(){
            if(ReportView.title_textbox.Text == null || ReportView.description_textbox.Text ==null && ReportView.recepient_combobox.SelectedValue ==null || ReportView.category_combobox.SelectedValue == null )
                
        }

		/* when user click on cancel button this function called.
		 * all of the text boxes should be empty
		 */
		public void cancel();
	}
}
