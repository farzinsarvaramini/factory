using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using clientFactory.Models;

namespace clientFactory
{

    public partial class SendReportView : Window
    {
        private DbReportCenter _dbReportSend;
        public CSendReportController _controller;
        private List<ReportCategory> _categories;
        private List<User> _allowedRecipienters;


        public SendReportView()
        {

            InitializeComponent();
            
        }

        public void show()
        {
            this.Show();
        }

        public void close()
        {
            this.Close();
        }

        public string getTitle()
        {
            return this.title_textbox.ToString();
        }

        public string getTDescription()
        {
            return this.description_textbox.ToString();
        }

        public Int32 getRecipientID()
        {
            return (Int32)this.recepient_combobox.SelectedValue;
        }

        public string getRecipient()
        {
            return this.recepient_combobox.Text.ToString();
        }

        public string getCategoryTitle()
        {
            return this.category_combobox.Text.ToString();
        }
        public Int32 getCategoryId()
        {
            return (Int32)this.category_combobox.SelectedValue;
        }

        public void setCategoriesList(List<ReportCategory> cl)
        {
            foreach (ReportCategory item in cl)
            {
                Console.WriteLine("ssssssss: "+cl.Count);
                if (cl.Count != 0)
                {
                    
                    this.category_combobox.Items.Add(item);
                    this.category_combobox.DisplayMemberPath = "Title";
                    this.category_combobox.SelectedValuePath = "Id";
                    
                   
                }
            }
        }

        public void setRecipientList(List<User> r)
        {
            foreach (User item in r)
            {
                this.recepient_combobox.Items.Add(item);
                this.recepient_combobox.DisplayMemberPath = "LastName";
                this.recepient_combobox.SelectedValuePath = "Id";
            }
        }

        public string getAttachments()
        {
            return this.userSelectedFilePath;
        }

        public void send()
        {
            //MessageBox.Show("v");
            this._controller.sendReport();
        }


        public void cancel()
        {
            close();
        }

        private void SendReport_Click(object sender, RoutedEventArgs e)
        {
            if (title_textbox.Text == null || description_textbox.Text == null && recepient_combobox.SelectedValue == null || category_combobox.SelectedValue == null)
            {
                MessageBox.Show("لطفا همه ی بخش های لازم را پر کنید", "Error");
            }
            else
                this.send();
                
        }

        private void AddFile_button(object sender, RoutedEventArgs e)
        {
            LoadNewFile();
        }

     public string userSelectedFilePath{
            get{
                return FilePath_tb.Text;
        }
            set
        {

            FilePath_tb.Text = value;
        }
}
     private void LoadNewFile()
     {
         Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
         dlg.Filter = "Text documents (.txt)|*.txt";
         Nullable<bool> result = dlg.ShowDialog();
         if (result == true)
         {
             userSelectedFilePath = dlg.FileName;
         }  

     }

     private void Button_Click(object sender, RoutedEventArgs e)
     {
         this.Close();
     }

    }
}
