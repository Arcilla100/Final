using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
using System.Xml.Linq;
using Spire.Xls;

namespace Final
{
    public partial class Register : Form
    {
        private string excelPath = @"C:\Users\ACT-STUDENT\Desktop\Final1.xlsx";
        private Workbook workbook;
        private Worksheet sheet;
        private int editingRowIndex = -1;

        public Register(int rowIndex, string fullName, string gender, string course, string hobbies, string fc,
    string birthday, string age, string address, string email, string username, string password, string saying)
        {
            InitializeComponent();
            Load += Register_Load;
            editingRowIndex = rowIndex;

            if (editingRowIndex >= 0)
            {
                txtFullName.Text = fullName;
                SetGender(gender);
                SetHobbies(hobbies);
                try
                {
                    if (!string.IsNullOrWhiteSpace(birthday))
                    {
                        if (DateTime.TryParse(birthday, out DateTime parsedDate))
                        {
                            if (parsedDate >= dateTimePicker1.MinDate && parsedDate <= dateTimePicker1.MaxDate)
                            {
                                dateTimePicker1.Value = parsedDate;
                            }
                            else
                            {
                                MessageBox.Show("Birthday date is out of range. Setting to today.");
                                dateTimePicker1.Value = DateTime.Today;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid birthday format. Setting to today.\nValue: " + birthday);
                            dateTimePicker1.Value = DateTime.Today;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Birthday is empty. Setting to today.");
                        dateTimePicker1.Value = DateTime.Today;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error setting birthday: " + ex.Message);
                    dateTimePicker1.Value = DateTime.Today;
                }
                txtAge.Text = age;
                txtCourse.Text = course;
                cboFC.Text = fc;
                txtAddress.Text = address;
                txtEmail.Text = email;
                txtUsername.Text = username;
                txtPassword.Text = password;
                txtSaying.Text = saying;
                btnUpdate.Visible = true;
                btnAdd.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int newRow = sheet.LastRow + 1;
            WriteToSheet(newRow);
            workbook.SaveToFile(excelPath, FileFormat.Version2013);
            MessageBox.Show("Added successfully!");
            this.Close();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            workbook = new Workbook();
            workbook.LoadFromFile(excelPath);
            sheet = workbook.Worksheets[0];
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            student_Information infoForm = new student_Information();
            infoForm.ShowDialog();
            //infoForm.Show();
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            int updateRow = editingRowIndex + 2;
            WriteToSheet(updateRow);
            workbook.SaveToFile(excelPath, FileFormat.Version2013);
            MessageBox.Show("Updated successfully!");
            this.Close();
        }
        private void SetGender(string gender)
        {
            if (gender == "Male") radMale.Checked = true;
            if (gender == "Female") radFemale.Checked = true;
        }

        private void SetHobbies(string hobbies)
        {
            string[] list = hobbies.Split(',');
            chkBasketball.Checked = list.Contains("Basketball");
            chkDancing.Checked = list.Contains("Dancing");
            chkVolleyball.Checked = list.Contains("Volleyball");
            chkRunning.Checked = list.Contains("Running");
            chkGamming.Checked = list.Contains("Gamming");
        }
        private string GetGender()
        {
            return radMale.Checked ? "Male" : radFemale.Checked ? "Female" : "";
        }

        private string GetHobbies()
        {
            List<string> hobbies = new List<string>();
            if (chkBasketball.Checked) hobbies.Add("Basketball");
            if (chkDancing.Checked) hobbies.Add("Dancing");
            if (chkVolleyball.Checked) hobbies.Add("Volleyball");
            if (chkRunning.Checked) hobbies.Add("Running");
            if (chkGamming.Checked) hobbies.Add("Gamming");
            return string.Join(", ", hobbies);
        }
        public void ClearForm()
        {
            txtFullName.Clear();
            radMale.Checked = false;
            radFemale.Checked = false;
            chkBasketball.Checked = chkDancing.Checked = chkVolleyball.Checked = chkRunning.Checked = chkGamming.Checked = false;
            dateTimePicker1.Value = DateTime.Today;
            txtAge.Clear();
            txtCourse.Clear();
            cboFC.SelectedIndex = -1;
            txtSaying.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            pbxProfile.Image = null;
        }
        private void WriteToSheet(int row)
        {
            if (row < 1) return;


            sheet.Range[row, 1].Text = txtFullName.Text;
            sheet.Range[row, 2].Text = GetGender();
            sheet.Range[row, 3].Text = txtCourse.Text;
            sheet.Range[row, 4].Text = GetHobbies();
            sheet.Range[row, 5].Text = cboFC.Text;
            sheet.Range[row, 6].Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            sheet.Range[row, 7].Text = txtAge.Text;
            sheet.Range[row, 8].Text = txtAddress.Text;
            sheet.Range[row, 9].Text = txtEmail.Text;
            sheet.Range[row, 10].Text = txtUsername.Text;
            sheet.Range[row, 11].Text = txtPassword.Text;
            sheet.Range[row, 12].Text = txtSaying.Text;
        }
    }
}
