using GemBox.Spreadsheet;

namespace TableSort.Models
{
    public class File
    {
        public string Name { get; set; }
        public ExcelFile Tables { get; set; }
        public File(string name, ExcelFile tableCollection)
        {
            this.Name = name;
            this.Tables = tableCollection;
        }
        public File()
        {
        }
    }
}
