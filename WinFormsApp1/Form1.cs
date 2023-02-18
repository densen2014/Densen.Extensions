using ce.office.extension;
using DocumentFormat.OpenXml.Drawing;
using NPOI.Util;
using OpenXmlPowerTools;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Task.Run(OnAfterRenderAsync);
            //u1.LoadFile("https://blazor.app1.es/_content/DemoShared/samples/sample3.xlsx");
            //u1.LoadFile("reviewsss.xlsx");
            //u2.LoadFile("sample.xlsx");
            //u3.LoadFile("sample2.docx");
            //u4.LoadFile("sample2.xlsx");
            //u5.LoadFile("sample3.xlsx");
            //u6.LoadFile("sample.docx");
            //u1.ConvertXlsxToHtml("sample.xlsx");
        }

        protected async Task OnAfterRenderAsync()
        {
            await u1.ReloadAsync("https://blazor.app1.es/_content/DemoShared/samples/sample3.xlsx");
        }


    }
}