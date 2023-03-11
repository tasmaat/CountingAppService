namespace CountingForms.DictionaryForms
{
    partial class MultiBagsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiBagsForm));
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPlomb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.cbBin = new System.Windows.Forms.ComboBox();
            this.lblClient = new System.Windows.Forms.Label();
            this.cbClient = new System.Windows.Forms.ComboBox();
            this.tbCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgAccountDeclared = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbMarshrut = new System.Windows.Forms.ComboBox();
            this.dgList = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbEncashpoint = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgAccountDeclared)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(203, 24);
            this.tbNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(83, 22);
            this.tbNumber.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Номер мультисумки";
            // 
            // tbPlomb
            // 
            this.tbPlomb.Location = new System.Drawing.Point(512, 32);
            this.tbPlomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPlomb.MaxLength = 10;
            this.tbPlomb.Name = "tbPlomb";
            this.tbPlomb.Size = new System.Drawing.Size(188, 22);
            this.tbPlomb.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(328, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Номер пломбы";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblID.Location = new System.Drawing.Point(13, 58);
            this.lblID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(45, 20);
            this.lblID.TabIndex = 12;
            this.lblID.Text = "БИН";
            // 
            // cbBin
            // 
            this.cbBin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbBin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbBin.Location = new System.Drawing.Point(13, 82);
            this.cbBin.Margin = new System.Windows.Forms.Padding(4);
            this.cbBin.Name = "cbBin";
            this.cbBin.Size = new System.Drawing.Size(209, 28);
            this.cbBin.TabIndex = 15;
            this.cbBin.SelectedIndexChanged += new System.EventHandler(this.cbBin_SelectedIndexChanged);
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblClient.Location = new System.Drawing.Point(239, 58);
            this.lblClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(70, 20);
            this.lblClient.TabIndex = 13;
            this.lblClient.Text = "Клиент";
            // 
            // cbClient
            // 
            this.cbClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbClient.Location = new System.Drawing.Point(243, 82);
            this.cbClient.Margin = new System.Windows.Forms.Padding(4);
            this.cbClient.Name = "cbClient";
            this.cbClient.Size = new System.Drawing.Size(246, 28);
            this.cbClient.TabIndex = 14;
            this.cbClient.SelectedIndexChanged += new System.EventHandler(this.cbClient_SelectedIndexChanged);
            // 
            // tbCount
            // 
            this.tbCount.Location = new System.Drawing.Point(203, 129);
            this.tbCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(83, 22);
            this.tbCount.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Количество сумок";
            // 
            // dgAccountDeclared
            // 
            this.dgAccountDeclared.AllowUserToAddRows = false;
            this.dgAccountDeclared.AllowUserToDeleteRows = false;
            this.dgAccountDeclared.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAccountDeclared.Location = new System.Drawing.Point(285, 241);
            this.dgAccountDeclared.Margin = new System.Windows.Forms.Padding(4);
            this.dgAccountDeclared.MultiSelect = false;
            this.dgAccountDeclared.Name = "dgAccountDeclared";
            this.dgAccountDeclared.RowHeadersWidth = 62;
            this.dgAccountDeclared.Size = new System.Drawing.Size(513, 185);
            this.dgAccountDeclared.TabIndex = 28;
            this.dgAccountDeclared.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAccountDeclared_CellEndEdit);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(642, 193);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(156, 29);
            this.btnDelete.TabIndex = 31;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(421, 193);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(127, 29);
            this.btnInsert.TabIndex = 30;
            this.btnInsert.Text = "Изменить";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(36, 193);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(153, 29);
            this.btnAdd.TabIndex = 29;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(327, 132);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 32;
            this.label4.Text = "Маршрут";
            // 
            // cbMarshrut
            // 
            this.cbMarshrut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarshrut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbMarshrut.Location = new System.Drawing.Point(421, 131);
            this.cbMarshrut.Margin = new System.Windows.Forms.Padding(4);
            this.cbMarshrut.Name = "cbMarshrut";
            this.cbMarshrut.Size = new System.Drawing.Size(279, 28);
            this.cbMarshrut.TabIndex = 33;
            // 
            // dgList
            // 
            this.dgList.AllowUserToAddRows = false;
            this.dgList.AllowUserToDeleteRows = false;
            this.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgList.Location = new System.Drawing.Point(17, 241);
            this.dgList.Margin = new System.Windows.Forms.Padding(4);
            this.dgList.MultiSelect = false;
            this.dgList.Name = "dgList";
            this.dgList.ReadOnly = true;
            this.dgList.RowHeadersWidth = 62;
            this.dgList.Size = new System.Drawing.Size(243, 185);
            this.dgList.TabIndex = 34;
            this.dgList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgList_CellContentClick_1);
            this.dgList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgList_CellContentDoubleClick);
            this.dgList.SelectionChanged += new System.EventHandler(this.dgList_SelectionChanged);
            this.dgList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgList_KeyUp);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(243, 193);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(127, 29);
            this.btnClear.TabIndex = 35;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(508, 58);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "Точка инкасации";
            // 
            // cbEncashpoint
            // 
            this.cbEncashpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEncashpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbEncashpoint.Location = new System.Drawing.Point(512, 82);
            this.cbEncashpoint.Margin = new System.Windows.Forms.Padding(4);
            this.cbEncashpoint.Name = "cbEncashpoint";
            this.cbEncashpoint.Size = new System.Drawing.Size(246, 28);
            this.cbEncashpoint.TabIndex = 37;
            this.cbEncashpoint.SelectedIndexChanged += new System.EventHandler(this.cbEncashpoint_SelectedIndexChanged);
            // 
            // MultiBagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 455);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbEncashpoint);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbMarshrut);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgAccountDeclared);
            this.Controls.Add(this.tbCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.cbBin);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.cbClient);
            this.Controls.Add(this.tbPlomb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultiBagsForm";
            this.Text = "Мультисумка";
            this.Load += new System.EventHandler(this.MultiBagsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAccountDeclared)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPlomb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.ComboBox cbBin;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ComboBox cbClient;
        private System.Windows.Forms.TextBox tbCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgAccountDeclared;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbMarshrut;
        private System.Windows.Forms.DataGridView dgList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbEncashpoint;
    }
}