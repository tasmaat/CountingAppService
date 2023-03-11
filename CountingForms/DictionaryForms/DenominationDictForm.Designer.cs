namespace CountingForms.DictionaryForms
{
    partial class DenominationDictForm
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
            this.cbCurrency = new System.Windows.Forms.ComboBox();
            this.nValue = new System.Windows.Forms.NumericUpDown();
            this.dtValid = new System.Windows.Forms.DateTimePicker();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblValid = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nValue)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // tbName
            // 
            this.tbName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            // 
            // cbCurrency
            // 
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbCurrency.FormattingEnabled = true;
            this.cbCurrency.Location = new System.Drawing.Point(427, 76);
            this.cbCurrency.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(151, 24);
            this.cbCurrency.TabIndex = 4;
            // 
            // nValue
            // 
            this.nValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nValue.Location = new System.Drawing.Point(427, 115);
            this.nValue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nValue.Name = "nValue";
            this.nValue.Size = new System.Drawing.Size(150, 23);
            this.nValue.TabIndex = 5;
            this.nValue.ValueChanged += new System.EventHandler(this.nValue_ValueChanged);
            // 
            // dtValid
            // 
            this.dtValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtValid.Location = new System.Drawing.Point(427, 154);
            this.dtValid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtValid.Name = "dtValid";
            this.dtValid.Size = new System.Drawing.Size(151, 23);
            this.dtValid.TabIndex = 6;
            this.dtValid.ValueChanged += new System.EventHandler(this.dtValid_ValueChanged);
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrency.Location = new System.Drawing.Point(367, 80);
            this.lblCurrency.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(58, 17);
            this.lblCurrency.TabIndex = 7;
            this.lblCurrency.Text = "Валюта";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblValue.Location = new System.Drawing.Point(354, 117);
            this.lblValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(73, 17);
            this.lblValue.TabIndex = 8;
            this.lblValue.Text = "Значение";
            // 
            // lblValid
            // 
            this.lblValid.AutoSize = true;
            this.lblValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblValid.Location = new System.Drawing.Point(324, 154);
            this.lblValid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValid.Name = "lblValid";
            this.lblValid.Size = new System.Drawing.Size(103, 17);
            this.lblValid.TabIndex = 9;
            this.lblValid.Text = "Действителен";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(375, 197);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Тип";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(427, 194);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(151, 21);
            this.comboBox1.TabIndex = 11;
            // 
            // DenominationDictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(758, 457);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblValid);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.dtValid);
            this.Controls.Add(this.nValue);
            this.Controls.Add(this.cbCurrency);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "DenominationDictForm";
            this.Load += new System.EventHandler(this.DenominationDictForm_Load);
            this.Controls.SetChildIndex(this.cbCurrency, 0);
            this.Controls.SetChildIndex(this.nValue, 0);
            this.Controls.SetChildIndex(this.dtValid, 0);
            this.Controls.SetChildIndex(this.lblCurrency, 0);
            this.Controls.SetChildIndex(this.lblValue, 0);
            this.Controls.SetChildIndex(this.lblValid, 0);
            this.Controls.SetChildIndex(this.lblDictName, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCurrency;
        private System.Windows.Forms.NumericUpDown nValue;
        private System.Windows.Forms.DateTimePicker dtValid;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblValid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
