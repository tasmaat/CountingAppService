
namespace CountingForms.ParentForms
{
    partial class AktNewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AktNewForm));
            this.btnClear = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgBagsDefects = new System.Windows.Forms.DataGridView();
            this.dgBagDefectFactors = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgBagsDefects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBagDefectFactors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(311, 281);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(131, 43);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(182, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(98, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Введите номер повреждения";
            // 
            // dgBagsDefects
            // 
            this.dgBagsDefects.AllowUserToAddRows = false;
            this.dgBagsDefects.AllowUserToDeleteRows = false;
            this.dgBagsDefects.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgBagsDefects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgBagsDefects.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgBagsDefects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBagsDefects.Location = new System.Drawing.Point(15, 51);
            this.dgBagsDefects.Name = "dgBagsDefects";
            this.dgBagsDefects.ReadOnly = true;
            this.dgBagsDefects.RowHeadersWidth = 62;
            this.dgBagsDefects.Size = new System.Drawing.Size(265, 209);
            this.dgBagsDefects.TabIndex = 11;
            this.dgBagsDefects.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgBagsDefects_CellMouseClick);
            // 
            // dgBagDefectFactors
            // 
            this.dgBagDefectFactors.AllowUserToAddRows = false;
            this.dgBagDefectFactors.AllowUserToDeleteRows = false;
            this.dgBagDefectFactors.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgBagDefectFactors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgBagDefectFactors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgBagDefectFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBagDefectFactors.Location = new System.Drawing.Point(311, 51);
            this.dgBagDefectFactors.Name = "dgBagDefectFactors";
            this.dgBagDefectFactors.ReadOnly = true;
            this.dgBagDefectFactors.RowHeadersWidth = 62;
            this.dgBagDefectFactors.Size = new System.Drawing.Size(345, 209);
            this.dgBagDefectFactors.TabIndex = 12;
            this.dgBagDefectFactors.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBagDefectFactors_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(526, 281);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 43);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AktNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 332);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgBagDefectFactors);
            this.Controls.Add(this.dgBagsDefects);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnClear);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AktNewForm";
            this.Text = "Акт";
            ((System.ComponentModel.ISupportInitialize)(this.dgBagsDefects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBagDefectFactors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgBagsDefects;
        private System.Windows.Forms.DataGridView dgBagDefectFactors;
        private System.Windows.Forms.Button btnSave;
    }
}