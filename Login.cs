using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();;
        }
        Dashboard Dash = new Dashboard();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Workbook book = new Workbook();
            book.LoadFromFile(@"C:\Users\ACT-STUDENT\Desktop\Final1.xlsx");
            Worksheet sheet = book.Worksheets[0];
            int rowNumber = sheet.LastRow;
            bool log = false;
            string name;

            for (int i = 2; i <= rowNumber; i++) 
            {
                if (sheet.Range[i, 10].Value == txtUsername.Text && sheet.Range[i, 11].Value == txtPassword.Text) 
                {
                    Dash.lblUsername.Text = sheet.Range[i, 1].Value;
                    log = true;
                    name = i.ToString();
                    break;
                }
            }
            if (log)
            {
                this.Hide();
                Dash.ShowDialog();
           
            }
            else
            {
                MessageBox.Show("Invalid Input. ");
            }
        }
    }
}
