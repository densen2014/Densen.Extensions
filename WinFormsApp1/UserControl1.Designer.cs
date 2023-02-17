namespace WinFormsApp1
{
    partial class UserControl1
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            webBrowser1 = new System.Windows.Forms.WebBrowser();
            textBox1 = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // webBrowser1
            // 
            webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            webBrowser1.Location = new System.Drawing.Point(0, 34);
            webBrowser1.Margin = new System.Windows.Forms.Padding(4);
            webBrowser1.MinimumSize = new System.Drawing.Size(24, 27);
            webBrowser1.Name = "webBrowser1";
            webBrowser1.Size = new System.Drawing.Size(734, 638);
            webBrowser1.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            textBox1.Location = new System.Drawing.Point(0, 0);
            textBox1.Margin = new System.Windows.Forms.Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(734, 34);
            textBox1.TabIndex = 3;
            // 
            // UserControl1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(webBrowser1);
            Controls.Add(textBox1);
            Name = "UserControl1";
            Size = new System.Drawing.Size(734, 672);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox textBox1;
    }
}
