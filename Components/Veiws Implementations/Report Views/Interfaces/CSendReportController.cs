using System;
using System.Collections.Generic;

namespace View
{
	public class VSendReporForm
	{
		private CSendReportController _controller;
		private List<Category> _categories;
		private List<Recipient> _allowedRecipients;
		
		public VSendReporForm (CSendReportController c)
		{
			this._controller = c;
		}
		
		// this function show dialog
		public void show();
		
		// this function close dialog
		public void close();
		
		// this function return new report title
		public string getTitle();
		
		// this function return new report description
		public string getTDescription();
		
		// this function return id of selected recipienter
		public int getRecipientID();
		
		// this function return title if selected category
		public string getCategoryTitle();
		
		/* 
		 * this function set list of categories which show in form
		 * this list provided by controller
		 * controller ask this list from DbCenter and pass it to this form
		 */
		public void setCategoriesList(List<Category> cl);
		
		/* 
		 * this function set list of recipienters which this user can send report to them
		 * this list provided by controller
		 * controller ask this list from DbCenter and pass it to this form
		 */
		public void setRecipientsList(List<Recipient> r);
		
		// this function return attachments location in client system
		public string getAttachment();
		
		/* 
		 * when user click on send button this function called.
		 * if everything entered by user this function call correspondig function in controller
		 * else show error message to user and ask him(her) to fill all of fields
		 */
		public void send();
		
		/* 
		 * when user click on cancel button this function called.
		 * all of the text boxes should be empty
		 */
		public void cancel();
	}
}

