using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CountingDB;

namespace CountingForms.ParentForms
{
    public partial class DictionaryTabForm : DictionaryForm
    {
        public DictionaryTabForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Конструктор создания форм отчетов
        /// </summary>
        /// <param name="formName">Название справочника</param>
        /// <param name="gridFieldName">Наименование поля</param>
        /// <param name="strCaption">Подпись поля</param>
        /// <param name="tableName">Название таблицы</param>
        /// <param name="tabNames">подписи ко вкладкам</param>
        public DictionaryTabForm(String formName, String gridFieldName, string strCaption, string tableName)//, List<string> tabNames)
        {
            InitializeComponent();
            
            dataSet = new DataSet();
            dataBase = new MSDataBase();
            dataBase.Connect();
            this.tableName = tableName;
            this.dataSet = dataBase.GetData(tableName);
            this.gridFieldName = gridFieldName;
            this.tableName = tableName;

            dgList.AutoGenerateColumns = false;
            dgList.DataSource = null;

            dgList.DataSource = dataSet.Tables[0];
            dgList.Columns.Add(gridFieldName, gridFieldName);
            dgList.Columns[gridFieldName].DataPropertyName = gridFieldName;
            dgList.ColumnHeadersVisible = true;
            dgList.Columns[gridFieldName].HeaderText = strCaption;
            dgList.RowHeadersVisible = true;
            dgList.RowHeadersWidth = 30;
            dgList.Columns[gridFieldName].Visible = true;
            //dgList.Columns[gridFieldName].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Text = formName;
            lblDictName.Text = formName;
            lblName.Text = strCaption;
           
            
        }

       
    }
}
