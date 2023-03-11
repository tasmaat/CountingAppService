namespace CountingForms
{
    partial class ManagePermisions
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
            this.LBControl = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LBRoles = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbEnab = new System.Windows.Forms.CheckBox();
            this.cbVis = new System.Windows.Forms.CheckBox();
            this.tvRolePermisions = new System.Windows.Forms.TreeView();
            this.cbSanction = new System.Windows.Forms.CheckBox();
            this.cbSelfSanction = new System.Windows.Forms.CheckBox();
            this.cbSanctioner = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LBControl
            // 
            this.LBControl.FormattingEnabled = true;
            this.LBControl.Location = new System.Drawing.Point(222, 25);
            this.LBControl.Name = "LBControl";
            this.LBControl.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.LBControl.Size = new System.Drawing.Size(242, 186);
            this.LBControl.TabIndex = 0;
            this.LBControl.SelectedValueChanged += new System.EventHandler(this.LBControl_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Элементы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Роли";
            // 
            // LBRoles
            // 
            this.LBRoles.FormattingEnabled = true;
            this.LBRoles.Location = new System.Drawing.Point(15, 25);
            this.LBRoles.Name = "LBRoles";
            this.LBRoles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.LBRoles.Size = new System.Drawing.Size(194, 186);
            this.LBRoles.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(499, 330);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 27);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbSanctioner);
            this.panel1.Controls.Add(this.cbSelfSanction);
            this.panel1.Controls.Add(this.cbSanction);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.cbEnab);
            this.panel1.Controls.Add(this.cbVis);
            this.panel1.Location = new System.Drawing.Point(481, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 186);
            this.panel1.TabIndex = 6;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(17, 147);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(125, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cbEnab
            // 
            this.cbEnab.AutoSize = true;
            this.cbEnab.Checked = true;
            this.cbEnab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnab.Location = new System.Drawing.Point(17, 39);
            this.cbEnab.Name = "cbEnab";
            this.cbEnab.Size = new System.Drawing.Size(65, 17);
            this.cbEnab.TabIndex = 1;
            this.cbEnab.Text = "Enabled";
            this.cbEnab.UseVisualStyleBackColor = true;
            // 
            // cbVis
            // 
            this.cbVis.AutoSize = true;
            this.cbVis.Checked = true;
            this.cbVis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVis.Location = new System.Drawing.Point(17, 16);
            this.cbVis.Name = "cbVis";
            this.cbVis.Size = new System.Drawing.Size(56, 17);
            this.cbVis.TabIndex = 0;
            this.cbVis.Text = "Visible";
            this.cbVis.UseVisualStyleBackColor = true;
            // 
            // tvRolePermisions
            // 
            this.tvRolePermisions.Location = new System.Drawing.Point(15, 227);
            this.tvRolePermisions.Name = "tvRolePermisions";
            this.tvRolePermisions.Size = new System.Drawing.Size(449, 130);
            this.tvRolePermisions.TabIndex = 8;
            // 
            // cbSanction
            // 
            this.cbSanction.AutoSize = true;
            this.cbSanction.Location = new System.Drawing.Point(17, 62);
            this.cbSanction.Name = "cbSanction";
            this.cbSanction.Size = new System.Drawing.Size(69, 17);
            this.cbSanction.TabIndex = 3;
            this.cbSanction.Text = "Санкция";
            this.cbSanction.UseVisualStyleBackColor = true;
            this.cbSanction.Visible = false;
            // 
            // cbSelfSanction
            // 
            this.cbSelfSanction.AutoSize = true;
            this.cbSelfSanction.Location = new System.Drawing.Point(17, 85);
            this.cbSelfSanction.Name = "cbSelfSanction";
            this.cbSelfSanction.Size = new System.Drawing.Size(95, 17);
            this.cbSelfSanction.TabIndex = 4;
            this.cbSelfSanction.Text = "Самосанкция";
            this.cbSelfSanction.UseVisualStyleBackColor = true;
            this.cbSelfSanction.Visible = false;
            // 
            // cbSanctioner
            // 
            this.cbSanctioner.AutoSize = true;
            this.cbSanctioner.Location = new System.Drawing.Point(17, 108);
            this.cbSanctioner.Name = "cbSanctioner";
            this.cbSanctioner.Size = new System.Drawing.Size(87, 17);
            this.cbSanctioner.TabIndex = 5;
            this.cbSanctioner.Text = "Санкционер";
            this.cbSanctioner.UseVisualStyleBackColor = true;
            this.cbSanctioner.Visible = false;
            // 
            // ManagePermisions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 369);
            this.Controls.Add(this.tvRolePermisions);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.LBRoles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LBControl);
            this.Name = "ManagePermisions";
            this.Text = "ManagePermisions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManagePermisions_FormClosing);
            this.Load += new System.EventHandler(this.ManagePermisions_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LBControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox LBRoles;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbEnab;
        private System.Windows.Forms.CheckBox cbVis;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TreeView tvRolePermisions;
        private System.Windows.Forms.CheckBox cbSanctioner;
        private System.Windows.Forms.CheckBox cbSelfSanction;
        private System.Windows.Forms.CheckBox cbSanction;
    }
}