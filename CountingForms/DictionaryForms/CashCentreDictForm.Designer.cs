namespace CountingForms.DictionaryForms
{
    partial class CashCentreDictForm
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
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.nUTC = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nUTC)).BeginInit();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            // 
            // cbCity
            // 
            this.cbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(640, 118);
            this.cbCity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(490, 33);
            this.cbCity.TabIndex = 4;
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCity.Location = new System.Drawing.Point(561, 123);
            this.lblCity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(69, 25);
            this.lblCity.TabIndex = 5;
            this.lblCity.Text = "Город";
            // 
            // nUTC
            // 
            this.nUTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nUTC.Location = new System.Drawing.Point(640, 185);
            this.nUTC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nUTC.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nUTC.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.nUTC.Name = "nUTC";
            this.nUTC.Size = new System.Drawing.Size(192, 30);
            this.nUTC.TabIndex = 6;
            this.nUTC.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nUTC.ValueChanged += new System.EventHandler(this.nUTC_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(486, 188);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Часовой пояс";
            // 
            // CashCentreDictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.ClientSize = new System.Drawing.Size(1137, 702);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nUTC);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.cbCity);
            this.Margin = new System.Windows.Forms.Padding(6, 9, 6, 9);
            this.Name = "CashCentreDictForm";
            this.Load += new System.EventHandler(this.CashCentreDictForm_Load);
            this.Controls.SetChildIndex(this.lblDictName, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.cbCity, 0);
            this.Controls.SetChildIndex(this.lblCity, 0);
            this.Controls.SetChildIndex(this.nUTC, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nUTC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.NumericUpDown nUTC;
        private System.Windows.Forms.Label label1;
    }
}
