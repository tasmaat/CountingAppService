namespace CountingForms.DictionaryForms
{
    partial class ConditionDictForm
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
            this.lblVisible = new System.Windows.Forms.Label();
            this.cbVisisble = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dg = new System.Windows.Forms.DataGridView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnDeleteEvidence = new System.Windows.Forms.Button();
            this.btnModifyEvidence = new System.Windows.Forms.Button();
            this.btnAddEvidence = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // lblVisible
            // 
            this.lblVisible.AutoSize = true;
            this.lblVisible.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVisible.Location = new System.Drawing.Point(325, 76);
            this.lblVisible.Name = "lblVisible";
            this.lblVisible.Size = new System.Drawing.Size(79, 16);
            this.lblVisible.TabIndex = 4;
            this.lblVisible.Text = "Видимость";
            // 
            // cbVisisble
            // 
            this.cbVisisble.AutoSize = true;
            this.cbVisisble.Location = new System.Drawing.Point(427, 76);
            this.cbVisisble.Name = "cbVisisble";
            this.cbVisisble.Size = new System.Drawing.Size(86, 17);
            this.cbVisisble.TabIndex = 5;
            this.cbVisisble.Text = "Невидимый";
            this.cbVisisble.UseVisualStyleBackColor = true;
            this.cbVisisble.CheckedChanged += new System.EventHandler(this.cbVisisble_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.dg);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.btnDeleteEvidence);
            this.panel1.Controls.Add(this.btnModifyEvidence);
            this.panel1.Controls.Add(this.btnAddEvidence);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(325, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 349);
            this.panel1.TabIndex = 10;
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(5, 110);
            this.dg.Margin = new System.Windows.Forms.Padding(2);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersWidth = 51;
            this.dg.RowTemplate.Height = 24;
            this.dg.Size = new System.Drawing.Size(420, 201);
            this.dg.TabIndex = 19;
            this.dg.SelectionChanged += new System.EventHandler(this.dg_SelectionChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(102, 45);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(323, 60);
            this.textBox2.TabIndex = 18;
            // 
            // btnDeleteEvidence
            // 
            this.btnDeleteEvidence.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDeleteEvidence.Location = new System.Drawing.Point(337, 316);
            this.btnDeleteEvidence.Name = "btnDeleteEvidence";
            this.btnDeleteEvidence.Size = new System.Drawing.Size(84, 29);
            this.btnDeleteEvidence.TabIndex = 17;
            this.btnDeleteEvidence.Text = "Удалить";
            this.btnDeleteEvidence.UseVisualStyleBackColor = true;
            this.btnDeleteEvidence.Click += new System.EventHandler(this.btnDeleteEvidence_Click);
            // 
            // btnModifyEvidence
            // 
            this.btnModifyEvidence.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnModifyEvidence.Location = new System.Drawing.Point(247, 316);
            this.btnModifyEvidence.Name = "btnModifyEvidence";
            this.btnModifyEvidence.Size = new System.Drawing.Size(84, 29);
            this.btnModifyEvidence.TabIndex = 16;
            this.btnModifyEvidence.Text = "Изменить";
            this.btnModifyEvidence.UseVisualStyleBackColor = true;
            this.btnModifyEvidence.Click += new System.EventHandler(this.btnModifyEvidence_Click);
            // 
            // btnAddEvidence
            // 
            this.btnAddEvidence.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddEvidence.Location = new System.Drawing.Point(157, 316);
            this.btnAddEvidence.Name = "btnAddEvidence";
            this.btnAddEvidence.Size = new System.Drawing.Size(84, 29);
            this.btnAddEvidence.TabIndex = 14;
            this.btnAddEvidence.Text = "Добавить";
            this.btnAddEvidence.UseVisualStyleBackColor = true;
            this.btnAddEvidence.Click += new System.EventHandler(this.btnAddEvidence_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(2, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Описание";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(2, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Признак";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(102, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(323, 20);
            this.textBox1.TabIndex = 9;
            // 
            // ConditionDictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(758, 456);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbVisisble);
            this.Controls.Add(this.lblVisible);
            this.Name = "ConditionDictForm";
            this.Load += new System.EventHandler(this.ConditionDictForm_Load);
            this.Controls.SetChildIndex(this.lblVisible, 0);
            this.Controls.SetChildIndex(this.cbVisisble, 0);
            this.Controls.SetChildIndex(this.lblDictName, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVisible;
        private System.Windows.Forms.CheckBox cbVisisble;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnAddEvidence;
        private System.Windows.Forms.Button btnDeleteEvidence;
        private System.Windows.Forms.Button btnModifyEvidence;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dg;
    }
}
