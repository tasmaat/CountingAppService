namespace CountingForms.ParentForms
{
    partial class Counterfeit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Counterfeit));
            this.dgCounterfiet = new System.Windows.Forms.DataGridView();
            this.lblDenomination = new System.Windows.Forms.Label();
            this.cbDenomination = new System.Windows.Forms.ComboBox();
            this.lblSerial = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.tbSerial = new System.Windows.Forms.TextBox();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblDesc = new System.Windows.Forms.Label();
            this.dgCondition = new System.Windows.Forms.DataGridView();
            this.dgConditionFactor = new System.Windows.Forms.DataGridView();
            this.tbEvidence = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReading = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgCounterfiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgConditionFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // dgCounterfiet
            // 
            this.dgCounterfiet.AllowUserToAddRows = false;
            this.dgCounterfiet.AllowUserToDeleteRows = false;
            this.dgCounterfiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCounterfiet.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgCounterfiet.Location = new System.Drawing.Point(16, 14);
            this.dgCounterfiet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgCounterfiet.MultiSelect = false;
            this.dgCounterfiet.Name = "dgCounterfiet";
            this.dgCounterfiet.ReadOnly = true;
            this.dgCounterfiet.RowHeadersWidth = 62;
            this.dgCounterfiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCounterfiet.Size = new System.Drawing.Size(727, 245);
            this.dgCounterfiet.TabIndex = 0;
            this.dgCounterfiet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCounterfiet_CellDoubleClick);
            this.dgCounterfiet.SelectionChanged += new System.EventHandler(this.DgCounterfiet_SelectionChanged);
            // 
            // lblDenomination
            // 
            this.lblDenomination.AutoSize = true;
            this.lblDenomination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDenomination.Location = new System.Drawing.Point(19, 279);
            this.lblDenomination.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDenomination.Name = "lblDenomination";
            this.lblDenomination.Size = new System.Drawing.Size(84, 20);
            this.lblDenomination.TabIndex = 1;
            this.lblDenomination.Text = "Номинал";
            // 
            // cbDenomination
            // 
            this.cbDenomination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDenomination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbDenomination.FormattingEnabled = true;
            this.cbDenomination.Location = new System.Drawing.Point(135, 276);
            this.cbDenomination.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbDenomination.Name = "cbDenomination";
            this.cbDenomination.Size = new System.Drawing.Size(260, 28);
            this.cbDenomination.TabIndex = 2;
            this.cbDenomination.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cbDenomination_MouseDown);
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSerial.Location = new System.Drawing.Point(19, 320);
            this.lblSerial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(61, 20);
            this.lblSerial.TabIndex = 3;
            this.lblSerial.Text = "Серия";
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNumber.Location = new System.Drawing.Point(19, 354);
            this.lblNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(64, 20);
            this.lblNumber.TabIndex = 4;
            this.lblNumber.Text = "Номер";
            // 
            // tbSerial
            // 
            this.tbSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSerial.Location = new System.Drawing.Point(135, 313);
            this.tbSerial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSerial.MaxLength = 10;
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.Size = new System.Drawing.Size(260, 26);
            this.tbSerial.TabIndex = 5;
            // 
            // tbNumber
            // 
            this.tbNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbNumber.Location = new System.Drawing.Point(135, 347);
            this.tbNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbNumber.MaxLength = 15;
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(260, 26);
            this.tbNumber.TabIndex = 6;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAdd.Location = new System.Drawing.Point(16, 759);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(109, 34);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnModify.Location = new System.Drawing.Point(135, 759);
            this.btnModify.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(109, 34);
            this.btnModify.TabIndex = 13;
            this.btnModify.Text = "Изменить";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDelete.Location = new System.Drawing.Point(253, 759);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(109, 34);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClose.Location = new System.Drawing.Point(632, 759);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 34);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Выйти";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Location = new System.Drawing.Point(515, 759);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(109, 34);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDesc.Location = new System.Drawing.Point(19, 380);
            this.lblDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(100, 20);
            this.lblDesc.TabIndex = 11;
            this.lblDesc.Text = "Состояние";
            // 
            // dgCondition
            // 
            this.dgCondition.AllowUserToAddRows = false;
            this.dgCondition.AllowUserToDeleteRows = false;
            this.dgCondition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCondition.Location = new System.Drawing.Point(23, 402);
            this.dgCondition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgCondition.MultiSelect = false;
            this.dgCondition.Name = "dgCondition";
            this.dgCondition.ReadOnly = true;
            this.dgCondition.RowHeadersWidth = 51;
            this.dgCondition.RowTemplate.Height = 24;
            this.dgCondition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCondition.Size = new System.Drawing.Size(373, 325);
            this.dgCondition.TabIndex = 20;
            this.dgCondition.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCondition_CellClick);
            // 
            // dgConditionFactor
            // 
            this.dgConditionFactor.AllowUserToAddRows = false;
            this.dgConditionFactor.AllowUserToDeleteRows = false;
            this.dgConditionFactor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgConditionFactor.Location = new System.Drawing.Point(419, 402);
            this.dgConditionFactor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgConditionFactor.Name = "dgConditionFactor";
            this.dgConditionFactor.ReadOnly = true;
            this.dgConditionFactor.RowHeadersWidth = 51;
            this.dgConditionFactor.RowTemplate.Height = 24;
            this.dgConditionFactor.Size = new System.Drawing.Size(411, 325);
            this.dgConditionFactor.TabIndex = 21;
            this.dgConditionFactor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgConditionFactor_CellClick);
            this.dgConditionFactor.SelectionChanged += new System.EventHandler(this.dgConditionFactor_SelectionChanged);
            // 
            // tbEvidence
            // 
            this.tbEvidence.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbEvidence.Location = new System.Drawing.Point(16, 801);
            this.tbEvidence.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbEvidence.Multiline = true;
            this.tbEvidence.Name = "tbEvidence";
            this.tbEvidence.ReadOnly = true;
            this.tbEvidence.Size = new System.Drawing.Size(811, 95);
            this.tbEvidence.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(725, 380);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "Признак";
            // 
            // btnReading
            // 
            this.btnReading.Location = new System.Drawing.Point(443, 332);
            this.btnReading.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReading.Name = "btnReading";
            this.btnReading.Size = new System.Drawing.Size(229, 41);
            this.btnReading.TabIndex = 24;
            this.btnReading.Text = "Получить данные";
            this.btnReading.UseVisualStyleBackColor = true;
            this.btnReading.Click += new System.EventHandler(this.btnReading_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(443, 276);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(229, 41);
            this.btnPrint.TabIndex = 53;
            this.btnPrint.Text = "Печать отчета";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // Counterfeit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(853, 912);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnReading);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbEvidence);
            this.Controls.Add(this.dgConditionFactor);
            this.Controls.Add(this.dgCondition);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.tbSerial);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.cbDenomination);
            this.Controls.Add(this.lblDenomination);
            this.Controls.Add(this.dgCounterfiet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Counterfeit";
            this.Text = "Сомнительные";
            ((System.ComponentModel.ISupportInitialize)(this.dgCounterfiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgConditionFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCounterfiet;
        private System.Windows.Forms.Label lblDenomination;
        private System.Windows.Forms.ComboBox cbDenomination;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TextBox tbSerial;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.DataGridView dgCondition;
        private System.Windows.Forms.DataGridView dgConditionFactor;
        private System.Windows.Forms.TextBox tbEvidence;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReading;
        private System.Windows.Forms.Button btnPrint;
    }
}