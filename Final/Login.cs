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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Workbook book = new Workbook();
            book.LoadFromFile(@"C:\Users\ACT-STUDENT\Desktop\Final.xlsx");
            Worksheet sheet = book.Worksheets[0];
            int rowNumber = sheet.LastRow;
            bool log = false;
            string name;

            for (int i = 2; i <= rowNumber; i++) 
            {
 
            }
        }
    }
}
