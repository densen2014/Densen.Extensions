using ce.office.extension;
using DocumentFormat.OpenXml.Drawing;
using NPOI.Util;
using OpenXmlPowerTools;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            u1.LoadFile("reviewsss.xlsx");
            u2.LoadFile("sample.xlsx");
            u3.LoadFile("sample2.docx");
            u4.LoadFile("sample2.xlsx");
            u5.LoadFile("sample3.xlsx");
            u6.LoadFile("sample.docx");
        }
    }
}