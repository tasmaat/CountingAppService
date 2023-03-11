﻿namespace CountingForms
{
    partial class CountingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountingForm));
            this.tbCard = new System.Windows.Forms.TextBox();
            this.rblCurrency = new EWSoftware.ListControls.RadioButtonList();
            this.lblCard = new System.Windows.Forms.Label();
            this.dgDenomCount = new System.Windows.Forms.DataGridView();
            this.lblClient = new System.Windows.Forms.Label();
            this.lblClientName = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblCountNum = new System.Windows.Forms.Label();
            this.cbManualMachine = new System.Windows.Forms.CheckBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCountManual = new System.Windows.Forms.Label();
            this.lblCountMachine = new System.Windows.Forms.Label();
            this.lblSumValue = new System.Windows.Forms.Label();
            this.rblCondition = new EWSoftware.ListControls.RadioButtonList();
            this.btnFalse = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.rblCenn = new EWSoftware.ListControls.RadioButtonList();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.rblCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDenomCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rblCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rblCenn)).BeginInit();
            this.SuspendLayout();
            // 
            // tbCard
            // 
            this.tbCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCard.Location = new System.Drawing.Point(91, 42);
            this.tbCard.MaxLength = 15;
            this.tbCard.Name = "tbCard";
            this.tbCard.Size = new System.Drawing.Size(100, 22);
            this.tbCard.TabIndex = 2;
            this.tbCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCard_KeyDown);
            this.tbCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCard_KeyPress);
            this.tbCard.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCard_KeyUp);
            // 
            // rblCurrency
            // 
            this.rblCurrency.Appearance = System.Windows.Forms.Appearance.Button;
            this.rblCurrency.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rblCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rblCurrency.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rblCurrency.LayoutMethod = EWSoftware.ListControls.LayoutMethod.AcrossThenDown;
            this.rblCurrency.Location = new System.Drawing.Point(5, 123);
            this.rblCurrency.Name = "rblCurrency";
            this.rblCurrency.Size = new System.Drawing.Size(557, 42);
            this.rblCurrency.TabIndex = 7;
            this.rblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rblCurrency.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rblCurrency.SelectedIndexChanged += new System.EventHandler(this.rblCurrency_SelectedIndexChanged);
            // 
            // lblCard
            // 
            this.lblCard.AutoSize = true;
            this.lblCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCard.Location = new System.Drawing.Point(2, 45);
            this.lblCard.Name = "lblCard";
            this.lblCard.Size = new System.Drawing.Size(71, 16);
            this.lblCard.TabIndex = 8;
            this.lblCard.Text = "Номер КР";
            // 
            // dgDenomCount
            // 
            this.dgDenomCount.AllowUserToAddRows = false;
            this.dgDenomCount.AllowUserToDeleteRows = false;
            this.dgDenomCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDenomCount.Location = new System.Drawing.Point(5, 171);
            this.dgDenomCount.MultiSelect = false;
            this.dgDenomCount.Name = "dgDenomCount";
            this.dgDenomCount.RowHeadersWidth = 62;
            this.dgDenomCount.Size = new System.Drawing.Size(557, 234);
            this.dgDenomCount.TabIndex = 9;
            this.dgDenomCount.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDenomCount_CellValueChanged);
            this.dgDenomCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgDenomCount_KeyDown);
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblClient.Location = new System.Drawing.Point(2, 72);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(55, 16);
            this.lblClient.TabIndex = 10;
            this.lblClient.Text = "Клиент";
            this.lblClient.Visible = false;
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblClientName.Location = new System.Drawing.Point(103, 72);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(0, 16);
            this.lblClientName.TabIndex = 11;
            this.lblClientName.Visible = false;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCount.Location = new System.Drawing.Point(2, 100);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(95, 16);
            this.lblCount.TabIndex = 12;
            this.lblCount.Text = "№ пересчета";
            this.lblCount.Visible = false;
            // 
            // lblCountNum
            // 
            this.lblCountNum.AutoSize = true;
            this.lblCountNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCountNum.Location = new System.Drawing.Point(103, 100);
            this.lblCountNum.Name = "lblCountNum";
            this.lblCountNum.Size = new System.Drawing.Size(0, 16);
            this.lblCountNum.TabIndex = 13;
            this.lblCountNum.Visible = false;
            // 
            // cbManualMachine
            // 
            this.cbManualMachine.AutoSize = true;
            this.cbManualMachine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbManualMachine.Location = new System.Drawing.Point(14, 412);
            this.cbManualMachine.Name = "cbManualMachine";
            this.cbManualMachine.Size = new System.Drawing.Size(160, 20);
            this.cbManualMachine.TabIndex = 14;
            this.cbManualMachine.Text = "Ввод без пересчёта";
            this.cbManualMachine.UseVisualStyleBackColor = true;
            this.cbManualMachine.Click += new System.EventHandler(this.cbManualMachine_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Enabled = false;
            this.btnValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnValidate.Location = new System.Drawing.Point(5, 438);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(92, 35);
            this.btnValidate.TabIndex = 15;
            this.btnValidate.Text = "Принять";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnClear
            // 
            this.btnClear.Enabled = false;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Location = new System.Drawing.Point(232, 438);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(92, 35);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExit.Location = new System.Drawing.Point(470, 438);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 35);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotal.Location = new System.Drawing.Point(176, 412);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(46, 16);
            this.lblTotal.TabIndex = 18;
            this.lblTotal.Text = "Всего";
            // 
            // lblCountManual
            // 
            this.lblCountManual.AutoSize = true;
            this.lblCountManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCountManual.Location = new System.Drawing.Point(291, 412);
            this.lblCountManual.Name = "lblCountManual";
            this.lblCountManual.Size = new System.Drawing.Size(15, 16);
            this.lblCountManual.TabIndex = 19;
            this.lblCountManual.Text = "0";
            // 
            // lblCountMachine
            // 
            this.lblCountMachine.AutoSize = true;
            this.lblCountMachine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCountMachine.Location = new System.Drawing.Point(350, 413);
            this.lblCountMachine.Name = "lblCountMachine";
            this.lblCountMachine.Size = new System.Drawing.Size(15, 16);
            this.lblCountMachine.TabIndex = 20;
            this.lblCountMachine.Text = "0";
            // 
            // lblSumValue
            // 
            this.lblSumValue.AutoSize = true;
            this.lblSumValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSumValue.Location = new System.Drawing.Point(417, 412);
            this.lblSumValue.Name = "lblSumValue";
            this.lblSumValue.Size = new System.Drawing.Size(15, 16);
            this.lblSumValue.TabIndex = 21;
            this.lblSumValue.Text = "0";
            // 
            // rblCondition
            // 
            this.rblCondition.Appearance = System.Windows.Forms.Appearance.Button;
            this.rblCondition.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rblCondition.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rblCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rblCondition.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rblCondition.LayoutMethod = EWSoftware.ListControls.LayoutMethod.AcrossThenDown;
            this.rblCondition.Location = new System.Drawing.Point(453, 72);
            this.rblCondition.Name = "rblCondition";
            this.rblCondition.Size = new System.Drawing.Size(339, 44);
            this.rblCondition.TabIndex = 22;
            this.rblCondition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rblCondition.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.rblCondition.SelectedIndexChanged += new System.EventHandler(this.rblCondition_SelectedIndexChanged);
            // 
            // btnFalse
            // 
            this.btnFalse.Enabled = false;
            this.btnFalse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFalse.Location = new System.Drawing.Point(599, 206);
            this.btnFalse.Name = "btnFalse";
            this.btnFalse.Size = new System.Drawing.Size(122, 35);
            this.btnFalse.TabIndex = 23;
            this.btnFalse.Text = "Сомнительные";
            this.btnFalse.UseVisualStyleBackColor = true;
            this.btnFalse.Click += new System.EventHandler(this.btnFalse_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(212, 42);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 30);
            this.button1.TabIndex = 24;
            this.button1.Text = "Найти КР";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(580, 150);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(212, 37);
            this.button2.TabIndex = 25;
            this.button2.Text = "Показать все валюты";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // rblCenn
            // 
            this.rblCenn.Appearance = System.Windows.Forms.Appearance.Button;
            this.rblCenn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rblCenn.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rblCenn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rblCenn.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rblCenn.LayoutMethod = EWSoftware.ListControls.LayoutMethod.AcrossThenDown;
            this.rblCenn.Location = new System.Drawing.Point(453, 9);
            this.rblCenn.Name = "rblCenn";
            this.rblCenn.Size = new System.Drawing.Size(339, 44);
            this.rblCenn.TabIndex = 26;
            this.rblCenn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rblCenn.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.rblCenn.SelectedIndexChanged += new System.EventHandler(this.RblCenn_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(580, 247);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(208, 225);
            this.listBox1.TabIndex = 28;
            this.listBox1.Visible = false;
            // 
            // CountingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.rblCenn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnFalse);
            this.Controls.Add(this.rblCondition);
            this.Controls.Add(this.lblSumValue);
            this.Controls.Add(this.lblCountMachine);
            this.Controls.Add(this.lblCountManual);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.cbManualMachine);
            this.Controls.Add(this.lblCountNum);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblClientName);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.dgDenomCount);
            this.Controls.Add(this.lblCard);
            this.Controls.Add(this.rblCurrency);
            this.Controls.Add(this.tbCard);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CountingForm";
            this.Text = "Пересчёт";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CountingForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.rblCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDenomCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rblCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rblCenn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbCard;
        private EWSoftware.ListControls.RadioButtonList rblCurrency;
        private System.Windows.Forms.Label lblCard;
        private System.Windows.Forms.DataGridView dgDenomCount;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblCountNum;
        private System.Windows.Forms.CheckBox cbManualMachine;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblCountManual;
        private System.Windows.Forms.Label lblCountMachine;
        private System.Windows.Forms.Label lblSumValue;
        private EWSoftware.ListControls.RadioButtonList rblCondition;
        private System.Windows.Forms.Button btnFalse;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private EWSoftware.ListControls.RadioButtonList rblCenn;
        private System.Windows.Forms.ListBox listBox1;
    }
}