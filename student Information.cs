using Spire.Xls;
using Spire.Xls.Core;
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
    public partial class student_Information: Form
    {
        private string excelPath = @"C:\Users\ACT-STUDENT\Desktop\Final1.xlsx";
        private Workbook workbook;
        private Worksheet sheet;

        public student_Information()
        {
            InitializeComponent();
            Load += student_Information_Load;
            dataGridView1.CellMouseDoubleClick += dataGridView1_CellMouseDoubleClick;
        }

        private void student_Information_Load(object sender, EventArgs e)
        {
            LoadExcelToDataGrid();
        }
        private void LoadExcelToDataGrid()
        {
            Workbook book = new Workbook();
            book.LoadFromFile(@"C:\Users\ACT-STUDENT\Desktop\Final1.xlsx");
            Worksheet sheet = book.Worksheets[0];
            if (sheet.Rows.Length > 0 && sheet.Columns.Length > 0)
            {
                DataTable dt = sheet.ExportDataTable(sheet.AllocatedRange, true, true);
                dataGridView1.DataSource = dt;
            }
        }
        //public void loadExcelFile()
        //{
        //    Workbook book = new Workbook();
        //    book.LoadFromFile(@"C:\Users\Admin\Downloads\Final.xlsx");
        //    Worksheet sheet = book.Worksheets[0];
        //    DataTable dt = sheet.ExportDataTable(sheet.AllocatedRange, true, true);
        //    dataGridView1.DataSource = dt;
        //}
        public void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

                var row = dataGridView1.Rows[e.RowIndex];
                string fullName = row.Cells[0].Value?.ToString();
                string gender = row.Cells[1].Value?.ToString();
                string course = row.Cells[2].Value?.ToString();
                string hobbies = row.Cells[3].Value?.ToString();
                string fc = row.Cells[4].Value?.ToString();
                string birthday = row.Cells[5].Value?.ToString();
                string age = row.Cells[6].Value?.ToString();
                string address = row.Cells[7].Value?.ToString();
                string email = row.Cells[8].Value?.ToString();
                string username = row.Cells[9].Value?.ToString();
                string password = row.Cells[10].Value?.ToString();
                string saying = row.Cells[11].Value != null ? row.Cells[11].Value.ToString() : "";
            Register registerForm = new Register(
               rowIndex: e.RowIndex,
               fullName: fullName,
               gender: gender,
               course: course,
               hobbies: hobbies,
               fc: fc,
               birthday: birthday,
               age: age,
               address: address,
               email: email,
               username: username,
               password: password,
               saying: saying


                );

                registerForm.ShowDialog();

                LoadExcelToDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (MessageBox.Show("Are you sure you want to delete this row?", "Message!", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(row);
                }
                //if (dataGridView1.SelectedRows.Count > 0)
                //{
                //    workbook = new Workbook();
                //    workbook.LoadFromFile(excelPath);
                //    sheet = workbook.Worksheets[0];

                //    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                //    {
                //        int rowIndex = row.Index + 2; // Skip header
                //        sheet.DeleteRow(rowIndex);
                //    }

                //    workbook.SaveToFile(excelPath, FileFormat.Version2013);
                //    MessageBox.Show("Selected row(s) deleted successfully.");
                //    LoadExcelToDataGrid(); // Refresh
                //}
            }
        }

        public void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

            sheet.Range[e.RowIndex + 2, e.ColumnIndex + 1].Text = newValue;

            workbook.SaveToFile(excelPath, FileFormat.Version2013);
            MessageBox.Show("Excel updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Excel: " + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit", "Message!!", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.ClearSelection();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(txtSearch.Text))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}

