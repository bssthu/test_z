namespace eso_zh_server
{
    partial class FormConfig
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIconConfig = new System.Windows.Forms.NotifyIcon(this.components);
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.labelAppKey = new System.Windows.Forms.Label();
            this.textBoxAppKey = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonCheckUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // notifyIconConfig
            // 
            this.notifyIconConfig.Text = "notifyIcon1";
            this.notifyIconConfig.Visible = true;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInfo.Location = new System.Drawing.Point(12, 67);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfo.Size = new System.Drawing.Size(283, 256);
            this.textBoxInfo.TabIndex = 0;
            // 
            // labelAppKey
            // 
            this.labelAppKey.AutoSize = true;
            this.labelAppKey.Location = new System.Drawing.Point(12, 16);
            this.labelAppKey.Name = "labelAppKey";
            this.labelAppKey.Size = new System.Drawing.Size(41, 12);
            this.labelAppKey.TabIndex = 1;
            this.labelAppKey.Text = "AppKey";
            // 
            // textBoxAppKey
            // 
            this.textBoxAppKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAppKey.Location = new System.Drawing.Point(59, 13);
            this.textBoxAppKey.Name = "textBoxAppKey";
            this.textBoxAppKey.Size = new System.Drawing.Size(236, 21);
            this.textBoxAppKey.TabIndex = 2;
            this.textBoxAppKey.TextChanged += new System.EventHandler(this.textBoxAppKey_TextChanged);
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(12, 43);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 12);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "端口";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPort.Location = new System.Drawing.Point(59, 40);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.ReadOnly = true;
            this.textBoxPort.Size = new System.Drawing.Size(122, 21);
            this.textBoxPort.TabIndex = 2;
            this.textBoxPort.Text = "无效";
            this.textBoxPort.TextChanged += new System.EventHandler(this.textBoxAppKey_TextChanged);
            // 
            // buttonCheckUpdate
            // 
            this.buttonCheckUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheckUpdate.Location = new System.Drawing.Point(187, 38);
            this.buttonCheckUpdate.Name = "buttonCheckUpdate";
            this.buttonCheckUpdate.Size = new System.Drawing.Size(108, 23);
            this.buttonCheckUpdate.TabIndex = 3;
            this.buttonCheckUpdate.Text = "检查更新(&U)";
            this.buttonCheckUpdate.UseVisualStyleBackColor = true;
            this.buttonCheckUpdate.Click += new System.EventHandler(this.buttonCheckUpdate_Click);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 335);
            this.Controls.Add(this.buttonCheckUpdate);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxAppKey);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelAppKey);
            this.Controls.Add(this.textBoxInfo);
            this.Name = "FormConfig";
            this.Text = "esozh - 设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconConfig;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Label labelAppKey;
        private System.Windows.Forms.TextBox textBoxAppKey;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonCheckUpdate;
    }
}

