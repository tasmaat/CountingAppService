namespace XMLReader
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnServiceInstall = new System.Windows.Forms.Button();
            this.btnServiceUninstall = new System.Windows.Forms.Button();
            this.btnServiceStop = new System.Windows.Forms.Button();
            this.btnServiceStart = new System.Windows.Forms.Button();
            this.btnWriteMap = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.tbReadyPath = new System.Windows.Forms.TextBox();
            this.tbErrorPath = new System.Windows.Forms.TextBox();
            this.btnOpenDialog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(776, 145);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoad.Location = new System.Drawing.Point(688, 164);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 34);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Считать";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // btnServiceInstall
            // 
            this.btnServiceInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnServiceInstall.Location = new System.Drawing.Point(32, 301);
            this.btnServiceInstall.Name = "btnServiceInstall";
            this.btnServiceInstall.Size = new System.Drawing.Size(100, 44);
            this.btnServiceInstall.TabIndex = 2;
            this.btnServiceInstall.Text = "Установить службу";
            this.btnServiceInstall.UseVisualStyleBackColor = true;
            this.btnServiceInstall.Click += new System.EventHandler(this.BtnServiceInstall_Click);
            // 
            // btnServiceUninstall
            // 
            this.btnServiceUninstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnServiceUninstall.Location = new System.Drawing.Point(137, 301);
            this.btnServiceUninstall.Name = "btnServiceUninstall";
            this.btnServiceUninstall.Size = new System.Drawing.Size(100, 44);
            this.btnServiceUninstall.TabIndex = 3;
            this.btnServiceUninstall.Text = "Удалить службу";
            this.btnServiceUninstall.UseVisualStyleBackColor = true;
            this.btnServiceUninstall.Click += new System.EventHandler(this.BtnServiceUninstall_Click);
            // 
            // btnServiceStop
            // 
            this.btnServiceStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnServiceStop.Location = new System.Drawing.Point(137, 351);
            this.btnServiceStop.Name = "btnServiceStop";
            this.btnServiceStop.Size = new System.Drawing.Size(100, 46);
            this.btnServiceStop.TabIndex = 4;
            this.btnServiceStop.Text = "Остановить службу";
            this.btnServiceStop.UseVisualStyleBackColor = true;
            this.btnServiceStop.Click += new System.EventHandler(this.BtnServiceStop_Click);
            // 
            // btnServiceStart
            // 
            this.btnServiceStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnServiceStart.Location = new System.Drawing.Point(32, 351);
            this.btnServiceStart.Name = "btnServiceStart";
            this.btnServiceStart.Size = new System.Drawing.Size(100, 46);
            this.btnServiceStart.TabIndex = 5;
            this.btnServiceStart.Text = "Запустить службу";
            this.btnServiceStart.UseVisualStyleBackColor = true;
            this.btnServiceStart.Click += new System.EventHandler(this.BtnServiceStart_Click);
            // 
            // btnWriteMap
            // 
            this.btnWriteMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnWriteMap.Location = new System.Drawing.Point(688, 203);
            this.btnWriteMap.Margin = new System.Windows.Forms.Padding(2);
            this.btnWriteMap.Name = "btnWriteMap";
            this.btnWriteMap.Size = new System.Drawing.Size(100, 39);
            this.btnWriteMap.TabIndex = 6;
            this.btnWriteMap.Text = "Записать";
            this.btnWriteMap.UseVisualStyleBackColor = true;
            this.btnWriteMap.Click += new System.EventHandler(this.WriteMap_Click);
            // 
            // tbPath
            // 
            this.tbPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPath.Location = new System.Drawing.Point(13, 164);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(205, 22);
            this.tbPath.TabIndex = 7;
            // 
            // tbReadyPath
            // 
            this.tbReadyPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbReadyPath.Location = new System.Drawing.Point(13, 192);
            this.tbReadyPath.Name = "tbReadyPath";
            this.tbReadyPath.Size = new System.Drawing.Size(205, 22);
            this.tbReadyPath.TabIndex = 8;
            // 
            // tbErrorPath
            // 
            this.tbErrorPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbErrorPath.Location = new System.Drawing.Point(12, 220);
            this.tbErrorPath.Name = "tbErrorPath";
            this.tbErrorPath.Size = new System.Drawing.Size(205, 22);
            this.tbErrorPath.TabIndex = 9;
            // 
            // btnOpenDialog
            // 
            this.btnOpenDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOpenDialog.Location = new System.Drawing.Point(224, 162);
            this.btnOpenDialog.Name = "btnOpenDialog";
            this.btnOpenDialog.Size = new System.Drawing.Size(39, 24);
            this.btnOpenDialog.TabIndex = 10;
            this.btnOpenDialog.Text = "...";
            this.btnOpenDialog.UseVisualStyleBackColor = true;
            this.btnOpenDialog.Click += new System.EventHandler(this.BtnOpenDialog_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenDialog);
            this.Controls.Add(this.tbErrorPath);
            this.Controls.Add(this.tbReadyPath);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.btnWriteMap);
            this.Controls.Add(this.btnServiceStart);
            this.Controls.Add(this.btnServiceStop);
            this.Controls.Add(this.btnServiceUninstall);
            this.Controls.Add(this.btnServiceInstall);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnServiceInstall;
        private System.Windows.Forms.Button btnServiceUninstall;
        private System.Windows.Forms.Button btnServiceStart;
        private System.Windows.Forms.Button btnServiceStop;
        private System.Windows.Forms.Button btnWriteMap;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.TextBox tbReadyPath;
        private System.Windows.Forms.TextBox tbErrorPath;
        private System.Windows.Forms.Button btnOpenDialog;
    }
}

