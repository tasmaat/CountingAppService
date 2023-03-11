namespace CountingForms.DictionaryForms
{
    partial class CennostForm
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
            this.lblVisible = new System.Windows.Forms.Label();
            this.cbVisisble = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            // 
            // lblVisible
            // 
            this.lblVisible.AutoSize = true;
            this.lblVisible.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVisible.Location = new System.Drawing.Point(328, 80);
            this.lblVisible.Name = "lblVisible";
            this.lblVisible.Size = new System.Drawing.Size(79, 16);
            this.lblVisible.TabIndex = 5;
            this.lblVisible.Text = "Видимость";
            // 
            // cbVisisble
            // 
            this.cbVisisble.AutoSize = true;
            this.cbVisisble.Location = new System.Drawing.Point(427, 82);
            this.cbVisisble.Name = "cbVisisble";
            this.cbVisisble.Size = new System.Drawing.Size(86, 17);
            this.cbVisisble.TabIndex = 6;
            this.cbVisisble.Text = "Невидимый";
            this.cbVisisble.UseVisualStyleBackColor = true;
            // 
            // CennostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 468);
            this.Controls.Add(this.cbVisisble);
            this.Controls.Add(this.lblVisible);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "CennostForm";
            this.Text = "CennostForm";
            this.Load += new System.EventHandler(this.CennostForm_Load);
            this.Controls.SetChildIndex(this.lblDictName, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.lblVisible, 0);
            this.Controls.SetChildIndex(this.cbVisisble, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVisible;
        private System.Windows.Forms.CheckBox cbVisisble;
    }
}