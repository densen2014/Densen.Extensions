using ce.office.extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public void LoadFile(string filename,bool? isExcel=null)
        {
            textBox1.Text = filename;
            isExcel = isExcel ?? filename.EndsWith(".xlsx");
            var Html = !(isExcel ?? false) ? WordHelper.ToHtml(filename) : ExcelHelper.ToHtml(filename);
            webBrowser1.DocumentText = Html;
        }
    }
}
