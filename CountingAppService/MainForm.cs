using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CountingDB;
using CountingDB.Entities;
using CountingForms;
using CountingForms.DictionaryForms;
using CountingForms.Interfaces;
using CountingForms.Services;

namespace CountingAppService
{
    public partial class MainForm : Form
    {
        ToolTip toolTip1;
        ToolTip toolTip2;
        //private String tableName = null;
        IPermisionsManager pm = new PermisionsManager();
        MSDataBaseAsync dataBase = new MSDataBaseAsync();
        List<t_g_role_permisions> perm;       

        OpeningBagsForm openingBags = null;
        PrepCountingForm prepCountingForm = null;
        CountingForm countingForm = null;
        CpsMatchingForm cpsMatchingForm = null;
        ReportsForm reports = null;
        SendCashForm sendcashForm = null;
        SendBagsForm sendBagsForm = null;
        ManagePermisions mp;
        UsersForm usersForm = null;
        ClientGroupForm clientGroupDictForm = null;
        ClientDictForm clientDictForm = null;
        CityDictForm cityDictForm = null;
        CashCentreDictForm cashCentreDictForm = null;
        SpravForm1 zone1 = null;
        SpravForm2 rabmest1 = null;
        CurrencyDictForm currencyDictForm = null;
        ConditionDictForm conditionDictForm = null;
        DenominationDictForm denominationDictForm = null;
        CennostForm сennostForm = null;
        Roles roles = null;

        public MainForm()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;            

            label1.Text = "Код: " + DataExchange.CurrentUser.CurrentUserCode;
            label2.Text = "ФИО: " + DataExchange.CurrentUser.CurrentUserName;            

            toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            toolTip2 = new ToolTip();
            toolTip2.AutoPopDelay = 5000;
            toolTip2.ReshowDelay = 500;
            toolTip2.ShowAlways = true;

            mp = new ManagePermisions();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            perm = await dataBase.GetDataWhere<t_g_role_permisions>("WHERE formName = '" + this.Name + "' AND roleId = " + DataExchange.CurrentUser.CurrentUserRole);
            pm.ChangeControlByRole(this.Controls, perm);

            //if (Login.LoginForm.admin)
            //{
            //    клиентыToolStripMenuItem.Visible = false;
            //    справочникиToolStripMenuItem.Visible = false;
            //    пользователиToolStripMenuItem.Visible = false;
            //    пересчетToolStripMenuItem.Visible = false;
            //    отчётыToolStripMenuItem.Visible = false;
            //    SendToolStripMenuItem.Visible = false;
            //}

            //if (DataExchange.CurrentUser.CurrentUserRole == 0)
            //    adminToolStripMenuItem.Visible = true;
            //else
            //    adminToolStripMenuItem.Visible = false;

            //this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ClientGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ClientGroupForm>().Count() < 1)
            {
                clientGroupDictForm = new ClientGroupForm("Клиентские группы", "name", "Наименование", "t_g_clisubfml");
                clientGroupDictForm.MdiParent = this;
                clientGroupDictForm.Show();                
            }
            else
            {
                clientGroupDictForm.Activate();
            }

        }

        private void ClientsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ClientDictForm>().Count() < 1)
            {
                clientDictForm = new ClientDictForm("Клиент", "name", "Наименование", "t_g_client");
                clientDictForm.MdiParent = this;
                clientDictForm.Show();
            }
            else
            {
                clientDictForm.Activate();
            }
        }

        private void CitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CityDictForm>().Count() < 1)
            {
                cityDictForm = new CityDictForm("Города", "name", "Наименование", "t_g_city");
                cityDictForm.MdiParent = this;
                cityDictForm.Show();
            }
            else
            {
                cityDictForm.Activate();
            }            
        }

        private void CurrencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CurrencyDictForm>().Count() < 1)
            {
                currencyDictForm = new CurrencyDictForm("Валюта", "name", "Наименование", "t_g_currency");
                currencyDictForm.MdiParent = this;
                currencyDictForm.Show();
            }
            else
            {
                currencyDictForm.Activate();
            }
        }

        private void CitesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void ConditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ConditionDictForm>().Count() < 1)
            {
                conditionDictForm = new ConditionDictForm("Состояние", "name", "Наименование", "t_g_condition");
                conditionDictForm.MdiParent = this;
                conditionDictForm.Show();
            }
            else
            {
                conditionDictForm.Activate();
            }         
        }

        private void DenominationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<DenominationDictForm>().Count() < 1)
            {
                denominationDictForm = new DenominationDictForm("Валюта", "name", "Наименование", "t_g_denomination");
                denominationDictForm.MdiParent = this;
                denominationDictForm.Show();
            }
            else
            {
                denominationDictForm.Activate();
            }

        }

        private void CashCentresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CashCentreDictForm>().Count() < 1)
            {
                cashCentreDictForm = new CashCentreDictForm("Кассовый центр", "branch_name", "Наименование", "t_g_cashcentre");
                cashCentreDictForm.MdiParent = this;
                cashCentreDictForm.Show();
            }
            else
            {
                cashCentreDictForm.Activate();
            }
        }

        private void UsersFormToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<UsersForm>().Count() < 1)
            {
                usersForm = new UsersForm("Пользователи", "user_name", "Сотрудник", "t_g_user");
                usersForm.MdiParent = this;
                usersForm.Show();
            }
            else
            {
                usersForm.Activate();
            }

            //usersForm.Close();
            //usersForm = new UsersForm("Пользователи", "user_name", "Сотрудник", "t_g_user");
            //usersForm.Show();
           // usersForm.Close();
        }      

        private void PrepCountingMenuItem_Click(object sender, EventArgs e)
        {
            
            if (Application.OpenForms.OfType<PrepCountingForm>().Count() < 1)
            {
                prepCountingForm = new PrepCountingForm();
                prepCountingForm.MdiParent = this;
                prepCountingForm.Show();
            }
            else
            {   
                prepCountingForm.Activate();
                if (prepCountingForm.WindowState== FormWindowState.Minimized)
                prepCountingForm.WindowState = FormWindowState.Normal;
            }
        }

        private void CountingMenuItem_Click(object sender, EventArgs e)
        {
            //countingForm.Close();
            //countingForm = new CountingForm();
            //countingForm.Show();

            if (Application.OpenForms.OfType<CountingForm>().Count() < 1)
            {
                countingForm = new CountingForm();
                countingForm.MdiParent = this;
                countingForm.Show();
            }
            else
            {
                countingForm.Activate();
                if (countingForm.WindowState == FormWindowState.Minimized)
                    countingForm.WindowState = FormWindowState.Normal;
            }

        }

     

        private void RejectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RejectForm rejectForm = new RejectForm();
            ////10.10.2019
     //       rejectForm.Show();
            ////10.10.2019

        }

        private void DiffsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void ПересчетToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        

        private void tipcenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CennostForm>().Count() < 1)
            {
                сennostForm = new CennostForm("Тип ценностей", "name_zenn", "Наименование", "t_g_tipzenn");
                сennostForm.MdiParent = this;
                сennostForm.Show();
            }
            else
            {
                сennostForm.Activate();
            }
        }

        private void Othetkorr_Click(object sender, EventArgs e)
        {


            Otchetkorr Otchet1 = new Otchetkorr();
            Otchet1.numzapr = 1;
              Otchet1.strzapros = "select t9.name as 'Клиент' , t1.creation as 'Дата создание подготовки' ,t5.lastupdate as 'Дата изменение' ,t12.user_name as 'Оператор' ,t1.date_deposit as 'Дата получения' ,t13.name as 'Номер сумки' ,t15.name as 'Назначение' ,cast(t8.value as int) as 'Заявлено номинал' ,t8.denomcount as 'Заявлено количество' ,cast(t8.value*t8.denomcount as int) as 'Заявлено сумма',cast(t4.value as int) as 'Посчитано номинал' ,t5.count as 'Посчитано количество', cast(t4.value*t5.count as int) as 'Посчитано сумма',cast(isnull(t4.value*t5.count,0)-isnull(t8.value*t8.denomcount,0) as int) as 'Расхождение сумма' ,cast(isnull(t5.count,0)-isnull(t8.denomcount,0) as int) as 'Расхождение количество'  ,t3.curr_code as 'Валюта'  from t_g_counting t1 left join t_g_counting_content t2 on(t1.id=t2.id_counting)    left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom_trace t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination) " +
                //"left join t_g_counting_denom t7  on(t5.id = t7.id) " +
                "left join t_g_client t9 on(t1.id_client=t9.id) left join t_g_user t12 on(t5.last_user_update=t12.id) left join t_g_bags t13 on(t1.id_bag=t13.id) left join t_g_account t14 on (t2.id_account=t14.id) left join t_g_encashpoint t15 on (t14.id_encashpoint=t15.id) left join(select t6.id_counting, t6.denomcount ,t6.id_currency, t7.value from t_g_declared_denom t6 inner join t_g_denomination t7 on (t6.id_denomination=t7.id) )t8 on(t1.id = t8.id_counting) and (t2.id_currency = t8.id_currency) inner join (select t70.id,count(*) col1 from t_g_counting_denom_trace t70  where t70.source<2 {0} group by t70.id having count(*)>1" +
                "union select t70.id,0 col1 from t_g_counting_denom_trace t70  where t70.source=2 and t70.flschet1>1 {0}" +
                ")  t10  on(t5.id = t10.id) where " +
                " ((t5.source<2) or (t5.source=2 and t5.flschet1>1))  {1}" +
               // "t5.source<2 {1}" +

                "order by   t5.lastupdate ";
            // "t7.flschet1>2 " +
            //  "order by  t1.creation, t5.lastupdate ";

            //  Otchet1.strzapros = "select t9.name as  col1, t1.creation as  col2 ,t5.lastupdate as  col3 ,t12.user_name  as  col4 ,t1.date_deposit as  col5 ,t13.name as  col6 ,t15.name as  col7 ,t8.value as  col8 ,t8.denomcount as  col9  ,t4.value as  col10 ,t5.count as  col11  ,t8.value*t8.denomcount-t4.value*t5.count as  col12 ,t8.denomcount-t5.count  as  col13 ,t3.curr_code as  col14  from t_g_counting t1 left join t_g_counting_content t2 on(t1.id=t2.id_counting)    left join t_g_currency t3 on(t2.id_currency = t3.id) left join t_g_denomination t4 on(t3.id = t4.id_currency) left join t_g_counting_denom_trace t5  on(t1.id = t5.id_counting) and(t4.id = t5.id_denomination) left join t_g_counting_denom t7  on(t5.id = t7.id) left join t_g_client t9 on(t1.id_client=t9.id) left join t_g_user t12 on(t5.last_user_update=t12.id) left join t_g_bags t13 on(t1.id_bag=t13.id) left join t_g_account t14 on (t2.id_account=t14.id) left join t_g_encashpoint t15 on (t14.id_encashpoint=t15.id) left join(select t6.id_counting, t6.denomcount ,t6.id_currency, t7.value from t_g_declared_denom t6 inner join t_g_denomination t7 on (t6.id_denomination=t7.id) )t8 on(t1.id = t8.id_counting) and (t2.id_currency = t8.id_currency)where t7.flschet1>2 order by  t1.creation, t5.lastupdate ";

            Otchet1.Show();

        }

        private void подробныйОтчётToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OtchetPodrobn Otchet1 = new OtchetPodrobn();
            Otchet1.vub1 = 2;
            Otchet1.Show();

        }

        private void движениеСредствToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Log1Frm logv1 = new Log1Frm();
            //logv1.Show();
        }

        private void JrndvdenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JrnlDvij1 jrndv1 = new JrnlDvij1();
            jrndv1.Show();
        }

        private void zoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SpravForm1>().Count() < 1)
            {
                zone1 = new SpravForm1("Зоны", "branch_name", "Наименование", "t_g_cashcentre");
                zone1.MdiParent = this;
                zone1.Text = "Зоны";              
                zone1.Show();
            }
            else
            {
                zone1.Activate();
            }

           // zone1.Close();
           // rabmest1.Close();
           // zone1 = new SpravForm1("Зоны", "branch_name", "Наименование", "t_g_cashcentre");
           // zone1.Text = "Зоны";
           //// zone1.tipsp = 1;
           // zone1.Show();
        }

        private void rabmestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SpravForm2>().Count() < 1)
            {
                rabmest1 = new SpravForm2("Рабочие места", "branch_name", "Наименование", "t_g_cashcentre");
                rabmest1.MdiParent = this;
                rabmest1.Text = "Рабочие места";              
                rabmest1.Show();
            }
            else
            {
                rabmest1.Activate();
            }


           
            //rabmest1 = new SpravForm2("Рабочие места", "branch_name", "Наименование", "t_g_cashcentre");
            //rabmest1.Text = "Рабочие места";
               
            //    rabmest1.Show();
        }

        //private void Zon1ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    SpravForm1 zone1 = new SpravForm1("Зоны обмена", "branch_name", "Наименование", "t_g_cashcentre");
        //    zone1.Text = "Зоны обмена";
        //    zone1.tipsp = 3;
        //    zone1.Show();
        //}

        private void SendCashFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SendCashForm sendcashForm = new SendCashForm();//("Пользователи", "user_name", "Сотрудник", "t_g_user");
            //sendcashForm.Show();
            //UsersForm usersForm1= new UsersForm("Передача наличности", "user_name", "Сотрудник", "t_g_user");
            //usersForm1.Show();
        }

        private void наличностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SendCashForm>().Count() < 1)
            {
                sendcashForm = new SendCashForm();
                sendcashForm.MdiParent = this;
                sendcashForm.Show();
            }
            else
            {
                sendcashForm.Activate();
            }

            //sendcashForm.Close();
            //sendcashForm = new SendCashForm();//("Пользователи", "user_name", "Сотрудник", "t_g_user");
            //sendcashForm.Show();
            
        }

        private void сумокИлиКонтейнеровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SendBagsForm>().Count() < 1)
            {
                sendBagsForm = new SendBagsForm();
                sendBagsForm.MdiParent = this;
                sendBagsForm.Show();
            }
            else
            {
                sendBagsForm.Activate();
            }

            //sendBagsForm.Close();
            //sendBagsForm = new SendBagsForm();
            //sendBagsForm.Show();
            
        }

        private void вскрытиеСумкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<OpeningBagsForm>().Count() < 1)
            {
                openingBags = new OpeningBagsForm();
                openingBags.MdiParent = this;
                openingBags.Show();
            }
            else
            {
                openingBags.Activate();
                if (openingBags.WindowState == FormWindowState.Minimized)
                    openingBags.WindowState = FormWindowState.Normal;
            }

            //openingBags.Close();
            //openingBags = new OpeningBagsForm();
            //openingBags.Show();
        }

        private void сервисToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ServiceInstaller
        }

        private void CpsMatchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //cpsMatchingForm.Close();
            //cpsMatchingForm = new CpsMatchingForm();
            //cpsMatchingForm.Show();

            if (Application.OpenForms.OfType<CpsMatchingForm>().Count() < 1)
            {
                cpsMatchingForm = new CpsMatchingForm();
                cpsMatchingForm.MdiParent = this;
                cpsMatchingForm.Show();
            }
            else
            {
                cpsMatchingForm.Activate();
                if (cpsMatchingForm.WindowState == FormWindowState.Minimized)
                    cpsMatchingForm.WindowState = FormWindowState.Normal;
            }
        }

        private void очетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ReportsForm>().Count() < 1)
            {
                reports = new ReportsForm();
                reports.MdiParent = this;
                reports.Show();
            }
            else
            {
                reports.Activate();
                if (reports.WindowState == FormWindowState.Minimized)
                    reports.WindowState = FormWindowState.Normal;
            }

            //reports.Close();
            //reports = new ReportsForm();
            //reports.Show();
        }

        private void открытиеСменыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShiftOpingForm shiftOping = new ShiftOpingForm();
            DialogResult dialogResult = shiftOping.ShowDialog();
        }       

        private void закрытиеСменыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShiftCloseForm shiftCloseForm = new ShiftCloseForm();
            DialogResult dialogResult = shiftCloseForm.ShowDialog();
        }

        private void администрированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //            cashCentreDictForm = new CashCentreDictForm("Кассовый центр", "branch_name", "Наименование", "t_g_cashcentre");

            AdminForm adminForm = new AdminForm("Название полей", "name", "Наименование", "t_g_name_column");
            DialogResult dialogResult = adminForm.ShowDialog();
        }

        private void разрешениеРолейToolStripMenuItem_Click(object sender, EventArgs e)
        {                     
            if(this.MdiChildren.Count() == 1)
            {
                Form chF = this.MdiChildren[0];
                if (Application.OpenForms.OfType<ManagePermisions>().Count() < 1)
                {
                    mp = new ManagePermisions(chF, toolTip1, toolTip2);
                    mp.MdiParent = this;
                    mp.Show();
                }
                else
                {
                    mp.Activate();
                    if (mp.WindowState == FormWindowState.Minimized)
                        mp.WindowState = FormWindowState.Normal;
                }
            }
            else if(this.MdiChildren.Count() == 0)
            {
                if (Application.OpenForms.OfType<ManagePermisions>().Count() < 1)
                {
                    mp = new ManagePermisions(this, toolTip1, toolTip2);
                    mp.MdiParent = this;
                    mp.Show();
                }
                else
                {
                    mp.Activate();
                    if (mp.WindowState == FormWindowState.Minimized)
                        mp.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("Для того что-бы настроить ограничение ролей для меню, все окна должны быть закрыты!\nЕсли Вы хотите настроить для окна, то должно быть открыто только это (одно) окно", 
                                "Предупреждение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void ролиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Roles>().Count() < 1)
            {
                roles = new Roles();
                roles.MdiParent = this;
                roles.Show();
            }
            else
            {
                roles.Activate();
                if (roles.WindowState == FormWindowState.Minimized)
                    roles.WindowState = FormWindowState.Normal;
            }
        }
    }
}
