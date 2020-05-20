using GemBox.Spreadsheet;
using GemBox.Spreadsheet.WinFormsUtilities;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TableSort.Models;

namespace TableSort
{
    public partial class цц : Form
    {
        private File File { get; set; }
        public цц()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SelectColumComboBox.Items.Clear();
            tabControl1.TabPages.Clear();
            try
            {
                File = FileManager.OpenFile();
                txtFilename.Text = File.Name;
                foreach (ExcelWorksheet table in File.Tables.Worksheets)
                {
                    tabControl1.TabPages.Add(table.Name);
                }
                if (tabControl1.TabCount > 1)
                {
                    tabControl1.DeselectTab(0);
                    tabControl1.SelectTab(0);
                }
                else
                {
                    tabControl1_Selected(new object(), new TabControlEventArgs(tabControl1.SelectedTab, 0, TabControlAction.Selected));
                }
            }
            catch (Exception)
            {

            }
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            var selectedColumn = SelectColumComboBox.SelectedIndex;
            if (sortSwitcher.Value == 0)
            {
                tableGridView1.Sort(tableGridView1.Columns[selectedColumn], System.ComponentModel.ListSortDirection.Ascending);
            }
            else
            {
                tableGridView1.Sort(tableGridView1.Columns[selectedColumn], System.ComponentModel.ListSortDirection.Descending);
            }
            SelectColumComboBox.SelectedIndex = selectedColumn;
        }

        private void SelectColumComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
                int selectedColumn = SelectColumComboBox.SelectedIndex;

                foreach (DataGridViewColumn column in tableGridView1.Columns)
                {
                    column.DefaultCellStyle.BackColor = Color.White;
                }
                tableGridView1.Columns[selectedColumn].DefaultCellStyle.BackColor = Color.LightGreen;

                SortButton.Enabled = true;
                saveButton.Enabled = true;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                DataGridViewConverter.ExportToDataGridView(
                               File.Tables.Worksheets[tabControl1.SelectedIndex],
                               this.tableGridView1,
                               new ExportToDataGridViewOptions() { ColumnHeaders = true });
                SelectColumComboBox.Items.Clear();
                for (int i = 0; i < tableGridView1.Columns.Count; i++)
                {
                    SelectColumComboBox.Items.Add(tableGridView1.Columns[i].HeaderText);
                }
                tabControl1.SelectedTab.Controls.Add(tableGridView1);
                tableGridView1.Location = new Point(0, 0);
                tableGridView1.Size = PreferredSize;
                tableGridView1.AutoResizeColumns();
                tableGridView1.AutoResizeRows();
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DataGridViewConverter.ImportFromDataGridView(
               File.Tables.Worksheets[tabControl1.SelectedIndex],
               this.tableGridView1,
               new ImportFromDataGridViewOptions() { ColumnHeaders = true });
            try
            {
                FileManager.SaveFile(File);
            }
            catch (Exception)
            {

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtFilename_TextChanged(object sender, EventArgs e)
        {

        }

        private void sortSwitcher_Scroll(object sender, EventArgs e)
        {

        }
    }
}
