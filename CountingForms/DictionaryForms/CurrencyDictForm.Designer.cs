namespace CountingForms.DictionaryForms
{
    partial class CurrencyDictForm
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
            this.lblCode = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.lblCurRate = new System.Windows.Forms.Label();
            this.tbCurRate = new System.Windows.Forms.TextBox();
            this.cbBlockCur = new System.Windows.Forms.CheckBox();
            this.tbSort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // btnAdd
            // 
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // tbName
            // 
            this.tbName.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCode.Location = new System.Drawing.Point(521, 98);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(42, 20);
            this.lblCode.TabIndex = 4;
            this.lblCode.Text = "Код";
            // 
            // tbCode
            // 
            this.tbCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCode.Location = new System.Drawing.Point(569, 95);
            this.tbCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(436, 27);
            this.tbCode.TabIndex = 5;
            this.tbCode.TextChanged += new System.EventHandler(this.tbCode_TextChanged);
            // 
            // lblCurRate
            // 
            this.lblCurRate.AutoSize = true;
            this.lblCurRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurRate.Location = new System.Drawing.Point(516, 145);
            this.lblCurRate.Name = "lblCurRate";
            this.lblCurRate.Size = new System.Drawing.Size(47, 20);
            this.lblCurRate.TabIndex = 6;
            this.lblCurRate.Text = "Курс";
            // 
            // tbCurRate
            // 
            this.tbCurRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCurRate.Location = new System.Drawing.Point(569, 142);
            this.tbCurRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbCurRate.Name = "tbCurRate";
            this.tbCurRate.Size = new System.Drawing.Size(436, 27);
            this.tbCurRate.TabIndex = 7;
            this.tbCurRate.TextChanged += new System.EventHandler(this.tbCurRate_TextChanged);
            // 
            // cbBlockCur
            // 
            this.cbBlockCur.AutoSize = true;
            this.cbBlockCur.Location = new System.Drawing.Point(867, 236);
            this.cbBlockCur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbBlockCur.Name = "cbBlockCur";
            this.cbBlockCur.Size = new System.Drawing.Size(131, 21);
            this.cbBlockCur.TabIndex = 9;
            this.cbBlockCur.Text = "Заблокировать";
            this.cbBlockCur.UseVisualStyleBackColor = true;
            this.cbBlockCur.CheckedChanged += new System.EventHandler(this.cbBlockCur_CheckedChanged);
            // 
            // tbSort
            // 
            this.tbSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSort.Location = new System.Drawing.Point(772, 189);
            this.tbSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbSort.Name = "tbSort";
            this.tbSort.Size = new System.Drawing.Size(233, 27);
            this.tbSort.TabIndex = 11;
            this.tbSort.TextChanged += new System.EventHandler(this.tbSort_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(565, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Порядковый номер";
            // 
            // CurrencyDictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1011, 561);
            this.Controls.Add(this.tbSort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbBlockCur);
            this.Controls.Add(this.tbCurRate);
            this.Controls.Add(this.lblCurRate);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.lblCode);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "CurrencyDictForm";
            this.Load += new System.EventHandler(this.CurrencyDictForm_Load);
            this.Controls.SetChildIndex(this.lblDictName, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.lblCode, 0);
            this.Controls.SetChildIndex(this.tbCode, 0);
            this.Controls.SetChildIndex(this.lblCurRate, 0);
            this.Controls.SetChildIndex(this.tbCurRate, 0);
            this.Controls.SetChildIndex(this.cbBlockCur, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.tbSort, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label lblCurRate;
        private System.Windows.Forms.TextBox tbCurRate;
        private System.Windows.Forms.CheckBox cbBlockCur;
        private System.Windows.Forms.TextBox tbSort;
        private System.Windows.Forms.Label label1;
    }
}
