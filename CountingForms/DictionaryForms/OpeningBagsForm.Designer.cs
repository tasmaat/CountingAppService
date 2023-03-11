namespace CountingForms.DictionaryForms
{
    partial class OpeningBagsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpeningBagsForm));
            this.Add2 = new System.Windows.Forms.Button();
            this.tbOpening = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Add1 = new System.Windows.Forms.Button();
            this.dgOpen = new System.Windows.Forms.DataGridView();
            this.dgNotOpen = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotOpen)).BeginInit();
            this.SuspendLayout();
            // 
            // Add2
            // 
            this.Add2.Location = new System.Drawing.Point(298, 363);
            this.Add2.Name = "Add2";
            this.Add2.Size = new System.Drawing.Size(75, 27);
            this.Add2.TabIndex = 24;
            this.Add2.Text = "Ввод";
            this.Add2.UseVisualStyleBackColor = true;
            this.Add2.Click += new System.EventHandler(this.Add2_Click);
            // 
            // tbOpening
            // 
            this.tbOpening.Location = new System.Drawing.Point(287, 335);
            this.tbOpening.Name = "tbOpening";
            this.tbOpening.Size = new System.Drawing.Size(100, 22);
            this.tbOpening.TabIndex = 23;
            this.tbOpening.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbOpening_KeyDown);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(324, 114);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(32, 33);
            this.Cancel.TabIndex = 22;
            this.Cancel.Text = "<";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Add1
            // 
            this.Add1.Location = new System.Drawing.Point(324, 49);
            this.Add1.Name = "Add1";
            this.Add1.Size = new System.Drawing.Size(32, 33);
            this.Add1.TabIndex = 21;
            this.Add1.Text = ">";
            this.Add1.UseVisualStyleBackColor = true;
            this.Add1.Click += new System.EventHandler(this.Add1_Click);
            // 
            // dgOpen
            // 
            this.dgOpen.AllowUserToAddRows = false;
            this.dgOpen.AllowUserToDeleteRows = false;
            this.dgOpen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOpen.Location = new System.Drawing.Point(409, 16);
            this.dgOpen.MultiSelect = false;
            this.dgOpen.Name = "dgOpen";
            this.dgOpen.ReadOnly = true;
            this.dgOpen.RowHeadersWidth = 51;
            this.dgOpen.RowTemplate.Height = 24;
            this.dgOpen.Size = new System.Drawing.Size(250, 419);
            this.dgOpen.TabIndex = 20;
            // 
            // dgNotOpen
            // 
            this.dgNotOpen.AllowUserToAddRows = false;
            this.dgNotOpen.AllowUserToDeleteRows = false;
            this.dgNotOpen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNotOpen.Location = new System.Drawing.Point(22, 16);
            this.dgNotOpen.MultiSelect = false;
            this.dgNotOpen.Name = "dgNotOpen";
            this.dgNotOpen.ReadOnly = true;
            this.dgNotOpen.RowHeadersWidth = 51;
            this.dgNotOpen.RowTemplate.Height = 24;
            this.dgNotOpen.Size = new System.Drawing.Size(250, 419);
            this.dgNotOpen.TabIndex = 19;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(287, 238);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 35);
            this.btnUpdate.TabIndex = 25;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // OpeningBagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 450);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.Add2);
            this.Controls.Add(this.tbOpening);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Add1);
            this.Controls.Add(this.dgOpen);
            this.Controls.Add(this.dgNotOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpeningBagsForm";
            this.Text = "Вскрытие сумок";
            this.Load += new System.EventHandler(this.OpeningBagsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotOpen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Add2;
        private System.Windows.Forms.TextBox tbOpening;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Add1;
        private System.Windows.Forms.DataGridView dgOpen;
        private System.Windows.Forms.DataGridView dgNotOpen;
        private System.Windows.Forms.Button btnUpdate;
    }
}