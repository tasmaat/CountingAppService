namespace ServiceInstaller
{
    partial class ServiceInstaller
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnServiceInstall = new System.Windows.Forms.Button();
            this.btnServiceUninstall = new System.Windows.Forms.Button();
            this.btnServiceStop = new System.Windows.Forms.Button();
            this.btnServiceStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnServiceInstall
            // 
            this.btnServiceInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnServiceInstall.Location = new System.Drawing.Point(3, 3);
            this.btnServiceInstall.Name = "btnServiceInstall";
            this.btnServiceInstall.Size = new System.Drawing.Size(109, 50);
            this.btnServiceInstall.TabIndex = 0;
            this.btnServiceInstall.Text = "Install";
            this.btnServiceInstall.UseVisualStyleBackColor = true;
            this.btnServiceInstall.Click += new System.EventHandler(this.BtnServiceInstall_Click);
            // 
            // btnServiceUninstall
            // 
            this.btnServiceUninstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnServiceUninstall.Location = new System.Drawing.Point(118, 3);
            this.btnServiceUninstall.Name = "btnServiceUninstall";
            this.btnServiceUninstall.Size = new System.Drawing.Size(109, 50);
            this.btnServiceUninstall.TabIndex = 1;
            this.btnServiceUninstall.Text = "Uninstall";
            this.btnServiceUninstall.UseVisualStyleBackColor = true;
            this.btnServiceUninstall.Click += new System.EventHandler(this.BtnServiceUninstall_Click);
            // 
            // btnServiceStop
            // 
            this.btnServiceStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnServiceStop.Location = new System.Drawing.Point(118, 59);
            this.btnServiceStop.Name = "btnServiceStop";
            this.btnServiceStop.Size = new System.Drawing.Size(109, 50);
            this.btnServiceStop.TabIndex = 2;
            this.btnServiceStop.Text = "Stop";
            this.btnServiceStop.UseVisualStyleBackColor = true;
            this.btnServiceStop.Click += new System.EventHandler(this.BtnServiceStop_Click);
            // 
            // btnServiceStart
            // 
            this.btnServiceStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnServiceStart.Location = new System.Drawing.Point(3, 59);
            this.btnServiceStart.Name = "btnServiceStart";
            this.btnServiceStart.Size = new System.Drawing.Size(109, 50);
            this.btnServiceStart.TabIndex = 3;
            this.btnServiceStart.Text = "Start";
            this.btnServiceStart.UseVisualStyleBackColor = true;
            this.btnServiceStart.Click += new System.EventHandler(this.BtnServiceStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnServiceUninstall);
            this.panel1.Controls.Add(this.btnServiceStop);
            this.panel1.Controls.Add(this.btnServiceStart);
            this.panel1.Controls.Add(this.btnServiceInstall);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 113);
            this.panel1.TabIndex = 4;
            // 
            // ServiceInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 142);
            this.Controls.Add(this.panel1);
            this.Name = "ServiceInstaller";
            this.Text = "Service";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnServiceInstall;
        private System.Windows.Forms.Button btnServiceUninstall;
        private System.Windows.Forms.Button btnServiceStop;
        private System.Windows.Forms.Button btnServiceStart;
        private System.Windows.Forms.Panel panel1;
    }
}

