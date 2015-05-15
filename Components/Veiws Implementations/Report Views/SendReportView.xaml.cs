using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace clientFactory
{
    /// <summary>
    /// Interaction logic for SendReportView.xaml
    /// </summary>
    public partial class SendReportView : Window
    {
        public SendReportView()
        {
            InitializeComponent();
        }

        private void SendReport_Click(object sender, RoutedEventArgs e)
        {
            if (title_textbox.Text == null || description_textbox.Text == null && recepient_combobox.SelectedValue == null || category_combobox.SelectedValue == null)
            {
                MessageBox.Show("لطفا همه ی بخش های لازم را پر کنید","Error");
                VSendReportForm a = new VSendReportForm();

            }
                
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
         //OpenFileDialog ofd = new OpenFileDialog();
         //System.Windows.Forms.DialogResult dr = ofd.ShowDialog();
         //if (dr == DialogResult.OK)
         //{
         //    userSelectedFilePath = ofd.FileName;
         //}
         Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
         dlg.Filter = "Text documents (.txt)|*.txt";
         Nullable<bool> result = dlg.ShowDialog();
         if (result == true)
         {
             // Open document 
             userSelectedFilePath = dlg.FileName;
         }  

     }

    }
}
