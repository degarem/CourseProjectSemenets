using System.IO;
using System.Windows.Forms;
using GemBox.Spreadsheet;
namespace TableSort.Models
{
    public static class FileManager
    {
        public static File OpenFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Worksheets|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                    ExcelFile excelFile = ExcelFile.Load(openFileDialog.FileName);

                    return new File(openFileDialog.FileName, excelFile);
                }
                else
                {
                    throw new System.Exception("FileDialog have result is not Ok");
                }
            }
        }

        public static void SaveFile(File file)
        {
            using(SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Excel Worksheets|*.xls;*.xlsx" })
            {
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file.Tables.Save(saveFileDialog.FileName);
                }
                else
                {
                    throw new System.Exception("FileDialog have result is not Ok");
                }
            }
        }
    }
}
