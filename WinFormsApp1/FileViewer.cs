using ce.office.extension;
using NPOI.SS.Converter;
using NPOI.Util;
using NPOI.XSSF.UserModel;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FileViewer : UserControl
    {
        /// <summary>
        /// 获得/设置 Excel/Word 文件路径或者URL
        /// </summary>
        public string? Filename { get; set; }
        
        /// <summary>
        /// 获得/设置 Html 直接渲染 
        /// </summary>
        public string? Html { get; set; }

        /// <summary>
        /// 获得/设置 用于渲染的文件流,为空则用Filename参数读取文件
        /// </summary>
        public Stream? Stream { get; set; }

        /// <summary>
        /// 获得/设置 文件流模式需要指定是否 Excel. 默认为 false
        /// </summary>
        public bool IsExcel { get; set; }

        /// <summary>
        /// 获得/设置 无数据提示文本,默认为 无数据
        /// </summary>
        public string NodataString { get; set; } = "无数据";

        /// <summary>
        /// 获得/设置 载入中提示文本,默认为 载入中...
        /// </summary>
        public string LoadingString { get; set; } = "载入中...";

        string? ErrorMessage { get; set; }

        public FileViewer()
        {
            InitializeComponent();
            this.Load += this.FileViewer_Load;
        }

        private void FileViewer_Load(object? sender, EventArgs e)
        {
            Task.Run(OnAfterRenderAsync);
        }

        protected async Task OnAfterRenderAsync()
        {
            await RefreshUI();
        }


        /// <summary>
        /// 重新载入文件
        /// </summary>
        /// <returns></returns>
        public virtual async Task ReloadAsync(string filename)
        {
            Filename = filename;
            Html = null;
            Stream = null;
            await RefreshUI();
        }


        /// <summary>
        /// 重新载入流
        /// </summary>
        /// <returns></returns>
        public virtual async Task ReloadAsync(Stream stream)
        {
            Stream = stream;
            Html = null;
            Filename = null;
            await RefreshUI();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <returns></returns>
        public virtual async Task RefreshUI()
        {
            if (ErrorMessage != null)
            {
                ErrorMessage = null; 
            }

            try
            {
                if (Stream != null)
                {
                    string tempFile = Path.GetTempFileName() + (IsExcel ? ".xlsx" : ".docx");
                    using (Stream fileStream = File.OpenWrite(tempFile))
                    {
                        await Stream.CopyToAsync(fileStream);
                    }
                    if (IsExcel)
                    {
                        Html = ExcelHelper.ToHtml(tempFile); 
                    }
                    else
                    {
                        Html = WordHelper.ToHtml(tempFile); 
                    }
                    File.Delete(tempFile);
                }
                else if (!string.IsNullOrEmpty(Filename))
                {
                    var tempFile = Filename;
                    if (Filename.StartsWith("http"))
                    {
                        var client = new HttpClient();
                        tempFile = Path.GetTempFileName() + (Filename.EndsWith("xlsx") ? ".xlsx" : ".docx");
                        var fileBytes = await client.GetByteArrayAsync(Filename);
                        await File.WriteAllBytesAsync(tempFile, fileBytes);
                    }
                    if (IsExcel || Filename.EndsWith("xlsx"))
                    {
                        Html = ExcelHelper.ToHtml(tempFile); 
                    }
                    else
                    {
                        Html = WordHelper.ToHtml(tempFile); 
                    }
                    if (Filename.StartsWith("http"))
                    {
                        File.Delete(tempFile);
                    }
                }

                webBrowser1.DocumentText = Html;
           }
            catch (Exception e)
            {
                ErrorMessage = e.Message; 
            }

        }

        public async void LoadFile(string filename,bool? isExcel=null)
        {
            textBox1.Text = filename;
            isExcel = isExcel ?? filename.EndsWith(".xlsx");
            var Html = !(isExcel ?? false) ? WordHelper.ToHtml(filename) : ExcelHelper.ToHtml(filename);
            if (File.Exists(filename + ".html")) File.Delete(filename + ".html");
            await File.WriteAllTextAsync(filename+".html",Html);
            webBrowser1.DocumentText = Html;
        }

        public void ConvertXlsxToHtml(string filename)
        {
            XSSFWorkbook xssfwb;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                xssfwb = new XSSFWorkbook(file);

                ExcelToHtmlConverter excelToHtmlConverter = new ExcelToHtmlConverter();

                //set output parameter
                excelToHtmlConverter.OutputColumnHeaders = false;
                excelToHtmlConverter.OutputHiddenColumns = true;
                excelToHtmlConverter.OutputHiddenRows = true;
                excelToHtmlConverter.OutputLeadingSpacesAsNonBreaking = false;
                excelToHtmlConverter.OutputRowNumbers = false;
                excelToHtmlConverter.UseDivsToSpan = true;

                //process the excel file
                excelToHtmlConverter.ProcessWorkbook(xssfwb);

                //output the html file
                excelToHtmlConverter.Document.Save(Path.ChangeExtension(filename, "html"));

                var ms = new MemoryStream();

                excelToHtmlConverter.Document.Save(ms); 

                Html = Encoding.UTF8.GetString(ms.ToArray());

            }
            webBrowser1.DocumentText = Html;
        }
    }
}
