using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using DataExchange;
using System.Windows;
using System.Threading;
using System.IO;
using System.Data.OleDb;

namespace CountingDB
{
    public class MSDataBase
    {

        private EventLogger.EventLogger eventLogger = new EventLogger.EventLogger();



        public string log = "vadim";
        public string par = "123456789";

        //public string connectionString = @"Data Source=localhost;Initial Catalog=CountingDB;Persist Security Info=True;User ID=vadim;Password=123456789";
        public string connectionString = new StreamReader(@"connectionString.ini", Encoding.Default).ReadToEnd()+ "Password=123456789";
        //public string connectionString = new StreamReader(@"connectionString.ini", Encoding.Default).ReadToEnd();



        private SqlConnection connection = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlCommandBuilder sqlCommand = null;
        private DataSet dataSet = null;
        //private Int64 counting_id = -1;
        //private Int64 bag_id = -1;
        private SqlTransaction transaction;
        private SqlCommand command;


        public MSDataBase()
        {

        }


        public bool Status()
        {
            if ((connection != null) && (connection.State == ConnectionState.Open))
                return true;
            else
                return false;
        }


        public void Connect()
        {

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    connection = new SqlConnection(connectionString);
                    //connection.ConnectionString = connectionString;
                    connection.Open();
                    break;
                }
                catch (SqlException sqlEx)
                {
                    System.Windows.Forms.MessageBox.Show(sqlEx.Message);
                    Thread.Sleep(3000);
                    if (i == 2)
                    {
                        throw;
                    }
                    continue;
                }
            }
        }

        public void Disconnect()
        {
            if (connection!= null)
            {
                connection.Close();
                connection = null;
            }
            
        }

        /*
        public ~MSDataBase()
        {

        }
*/

        /// <summary>
        /// Возвращает только структуру таблицы
        /// </summary>
        /// <param name="tblName">Имя таблицы</param>
        /// <returns></returns>
        public DataSet GetSchema(string tableName)
        {
            dataSet = new DataSet();
            sqlCommand = new SqlCommandBuilder(sqlDataAdapter);


            if ((connection != null) && (connection.State == ConnectionState.Open))
            {

                String sqlSelect = "SELECT TOP 0 * FROM " + tableName;
                sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);


                //string str = sqlDataAdapter.SelectCommand.CommandText;
                sqlDataAdapter.Fill(dataSet);


            }
            else
            {
                dataSet = null;
            }
            return dataSet;
        }


        /// <summary>
        /// Запрос данных со связями
        /// </summary>
        /// <returns>Набор данных из нескольких таблиц</returns>
        public DataSet GetData()
        {
            DataSet dataSet = new DataSet();
            String query = "SELECT * FROM t_g_client; SELECT * FROM t_g_ClientTOCC; SELECT * FROM t_g_cashcentre; SELECT * FROM t_g_encashpoint; SELECT * FROM t_g_account";
            sqlDataAdapter = new SqlDataAdapter(query, connection);


            sqlDataAdapter.Fill(dataSet);
            DataRelation dataRelationCTOC_Client
                = new DataRelation("clientToc_Client", dataSet.Tables[0].Columns["id"], dataSet.Tables[1].Columns["id_client"]);
            DataRelation dataRetaionCTOC_CashCentre
                = new DataRelation("clientToc_Client", dataSet.Tables[2].Columns["id"], dataSet.Tables[1].Columns["id_cashcentre"]);
            DataRelation dataRelationCTOC_enCashPoint
                = new DataRelation("clientToc_Client", dataSet.Tables[1].Columns["id"], dataSet.Tables[3].Columns["id_client"]);
            //dataSet.Relations.Add(dataRelation);
            return dataSet;
        }

        /// <summary>
        /// Запрос данных из таблицы с несколькими значениями параметра
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="fieldName">Имя поля</param>
        /// <param name="paramValues">Набор значений параметра</param>
        /// <returns>Набор данных</returns>
        public DataSet GetData(string tableName, string fieldName, List<string> paramValues)
        {
            dataSet = new DataSet();
            sqlCommand = new SqlCommandBuilder(sqlDataAdapter);

            if ((connection != null) && (connection.State == ConnectionState.Open) && (paramValues.Count > 0))
            {

                String sqlSelect = "SELECT * FROM " + tableName + " WHERE " + fieldName + " IN (" + String.Join(",", paramValues) + ")";
                sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);

                string str = sqlDataAdapter.SelectCommand.CommandText;
                sqlDataAdapter.Fill(dataSet);

            }

            return dataSet;
        }




        /// <summary>
        /// Запрос данных из таблицы по значению поля
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="fieldName">Имя поля</param>
        /// <param name="paramValue">Значение поля</param>
        /// <returns>Набор данных</returns>
        public DataSet GetData(string tableName, string fieldName, string paramValue)
        {
            dataSet = new DataSet();
            sqlCommand = new SqlCommandBuilder(sqlDataAdapter);

            ////
         //   sqlCommand.ConflictOption = ConflictOption.OverwriteChanges;
            ////

            if ((connection != null) && (connection.State == ConnectionState.Open))
            {

                String sqlSelect = "SELECT * FROM " + tableName + " WHERE " + fieldName + " = \'" + paramValue + "\'";
                sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);


                string str = sqlDataAdapter.SelectCommand.CommandText;
                sqlDataAdapter.Fill(dataSet);

            }
            else
            {
                dataSet = null;
            }
            return dataSet;
        }


        /// <summary>
        /// Запрос всех данных из таблицы
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Набор данных</returns>
        public DataSet GetData(string tableName)
        {
            dataSet = new DataSet();
            sqlCommand = new SqlCommandBuilder(sqlDataAdapter);


            if ((connection != null) && (connection.State == ConnectionState.Open))
            {

                String sqlSelect = "SELECT * FROM " + tableName;
                sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);


                string str = sqlDataAdapter.SelectCommand.CommandText;
                sqlDataAdapter.Fill(dataSet);


            }
            else
            {
                dataSet = null;
            }
            return dataSet;
        }


        /// <summary>
        /// Запрос значения идентификатора в БД
        /// </summary>
        /// <returns>Идентификатор в БД</returns>
        public Int64 GetIdentity()
        {
            Int64 result = -1;
            if ((connection != null) && (connection.State == ConnectionState.Open))
            {

                String sqlSelect = "SELECT SCOPE_IDENTITY()";
                sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                //sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
                //sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                //sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                //sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);


                //string str = sqlDataAdapter.SelectCommand.CommandText;
                result = Convert.ToInt64(sqlDataAdapter.SelectCommand.ExecuteScalar());// (dataSet);


            }
            return result;
        }


        /// <summary>
        /// Обновление данных в БД
        /// </summary>
        /// <param name="dataSetForm">Набор данных</param>
        public void UpdateData(DataSet dataSetForm)
        {
            sqlDataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(SqlAdapterRowUpdated);
            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                try
                {
                    sqlDataAdapter.Update(dataSetForm);

                  //  dataSetForm.AcceptChanges();
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Невозможно выполнить операцию: " + ex.Message);
                }
                

            }
        }

        /// <summary>
        /// Обновление данных в БД
        /// </summary>
        /// <param name="dataSetForm">Набор данных</param>
        /// <param name="tableName">Имя таблицы</param>
        public void UpdateData(DataSet dataSetForm, string tableName)
        {
            
            String sqlSelect = "SELECT TOP 0 * FROM " + tableName;
            sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
            sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
                        
            sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
            sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
            sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);
            //var row = dataSet.Tables[0].New
            if (transaction != null)
            {
                sqlCommand.GetInsertCommand(true).Transaction = transaction;
                sqlCommand.GetUpdateCommand(true).Transaction = transaction;
                sqlCommand.GetDeleteCommand(true).Transaction = transaction;
            }
            sqlDataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(SqlAdapterRowUpdated);
            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                sqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                try
                {   
                    sqlDataAdapter.Update(dataSetForm);
                    dataSetForm.AcceptChanges();

                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Невозможно выполнить операцию: " + ex.Message);

                    

                }


            }
        }


       


       /// <summary>
       /// Обрабочик обновления данных в БД
       /// </summary>
       /// <param name="sender">Объект DataAdapter</param>
       /// <param name="e">Аргументы объетка</param>
        private void SqlAdapterRowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            //SqlCommand IdentityCommand = new SqlCommand("SELECT SCOPE_IDENTITY()", e.Command.Connection);
            if (e.Row.RowState == DataRowState.Added)
            {
                SqlCommand IdentityCommand = new SqlCommand("SELECT @@IDENTITY", e.Command.Connection);
                e.Row["id"] = IdentityCommand.ExecuteScalar();
                e.Row.AcceptChanges();
            }
            
        }



        /// <summary>
        /// Обновление данных в БД через транзакцию
        /// </summary>
        /// <param name="dataSets">Список наборов данных</param>
        /// <param name="tableNames">Список имен таблиц</param>
        public void UpdateDataTransaction(List<DataSet> dataSets, List<string> tableNames)
        {
            List<SqlDataAdapter> sqlDataAdapters = new List<SqlDataAdapter>();


            foreach (var dataSetTables in dataSets.Zip(tableNames, (x, y) => new { dataset = x, tablename = y }))
            {
                String sqlSelect = "SELECT TOP 0 * FROM " + dataSetTables.tablename;
                sqlDataAdapters.Add(new SqlDataAdapter(sqlSelect, connection));
                sqlCommand = new SqlCommandBuilder(sqlDataAdapters[sqlDataAdapters.Count - 1]);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].DeleteCommand = sqlCommand.GetDeleteCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                //sqlDataAdapters[sqlDataAdapters.Count - 1].RowUpdated += new SqlRowUpdatedEventHandler(SqlAdapterRowUpdated);
            }

            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (var sqlAdapter in sqlDataAdapters.Zip(dataSets, (x, y) => new { adapter = x, dataset = y }))
                    {
                        sqlAdapter.adapter.InsertCommand.Transaction = transaction;
                        //sqlAdapter.adapter.InsertCommand.Transaction.
                        sqlAdapter.adapter.UpdateCommand.Transaction = transaction;
                        sqlAdapter.adapter.DeleteCommand.Transaction = transaction;

                        sqlAdapter.adapter.Update(sqlAdapter.dataset);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    eventLogger.WriteError("UpdateDataTransaction : " + ex.Message);
                    transaction.Rollback();
                }
            }
        }


        /// <summary>
        /// Обновление данных в БД через транзакцию
        /// </summary>
        /// <param name="dataSets">Список наборов данных</param>
        /// <param name="tableNames">Список имен таблиц</param>
        public void UpdateDataTransaction(DataSet commonDataSet)
            {
                List<SqlDataAdapter> sqlDataAdapters = new List<SqlDataAdapter>();


                foreach (DataTable dataSetTable in commonDataSet.Tables)
                {
                    String sqlSelect = "SELECT TOP 0 * FROM " + dataSetTable.TableName;
                    sqlDataAdapters.Add(new SqlDataAdapter(sqlSelect, connection));
                    sqlCommand = new SqlCommandBuilder(sqlDataAdapters[sqlDataAdapters.Count - 1]);
                    sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand = sqlCommand.GetInsertCommand(true);
                    sqlDataAdapters[sqlDataAdapters.Count - 1].UpdateCommand = sqlCommand.GetUpdateCommand(true);
                    sqlDataAdapters[sqlDataAdapters.Count - 1].DeleteCommand = sqlCommand.GetDeleteCommand(true);
                    sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                //sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.E
                //sqlDataAdapters[sqlDataAdapters.Count - 1].RowUpdated += new SqlRowUpdatedEventHandler(SqlAdapterRowUpdated);
                }

                if ((connection != null) && (connection.State == ConnectionState.Open))
                {
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        int i = 0;
                        foreach (var sqlAdapter in sqlDataAdapters)
                        {
                            sqlAdapter.InsertCommand.Transaction = transaction;
                            //sqlAdapter.adapter.InsertCommand.Transaction.
                            sqlAdapter.UpdateCommand.Transaction = transaction;
                            sqlAdapter.DeleteCommand.Transaction = transaction;

                            sqlAdapter.Update(commonDataSet.Tables[i]);
                            i++;
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        eventLogger.WriteError("UpdateDataTransaction : " + ex.Message);
                        transaction.Rollback();
                    }
                }


                



            }


        /// <summary>
        /// Инициализация транзакции по пересчету
        /// </summary>
        public void InitTransaction()
        {
            //command = null;
            //transaction = null;
            for (int i = 0; i <= 3; i++)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    try
                    {
                        /*
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }
                        */
                        //transaction.
                        command = connection.CreateCommand();

                        transaction = connection.BeginTransaction();// .Serializable);
                        command.Transaction = transaction;
                        break;
                    }
                    catch (Exception ex)
                    {
                        eventLogger.WriteError("InitTransaction : Проблема с инициализацией транзакции");
                        eventLogger.WriteError(ex.Message);

                        continue;
                    }

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("InitTransaction : Нет подключения к БД");

                    Connect();
                    continue;
                }
            }

            

        }

        /// <summary>
        /// Применение транзакции по пересчету
        /// </summary>
        public void TransactionCommit()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                try
                {
                    transaction.Commit();
                    transaction = null;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    eventLogger.WriteError("TransactionCommit : транзакция отменена");
                    eventLogger.WriteError("TransactionCommit : " + ex.Message);

                    throw;
                }
            }
            else
            {
                eventLogger.WriteError("TransactionCommit : Нет подключения к БД");

            }
        }

        /// <summary>
        /// Отмена транзакции по пересчету
        /// </summary>
        public void TransactionRollback()
        {
            if (connection != null && connection.State == ConnectionState.Open && transaction != null)
            {
                try
                {
                    transaction.Rollback();
                    eventLogger.WriteError("TransactionRollback : транзакция отменена");
                    transaction = null;
                }
                catch (Exception ex)
                {

                    eventLogger.WriteError("TransactionRollback : транзакция отменена");
                    eventLogger.WriteError("TransactionRollback : " + ex.Message);

                    throw;
                }
            }
            else
            {
                eventLogger.WriteError("TransactionCommit : Нет подключения к БД");

            }
        }

        /// <summary>
        /// Дабавление данных по пересчету с машины
        /// </summary>
        /// <param name="countingName">Наименование пересчета</param>
        /// <param name="lastUpdate">Дата последнего обновления пересчета</param>
        /// <param name="user_id">Идентификатор пользователя</param>
        /// <param name="counting_id"></param>
        public void CountingUpdateTransaction(string countingName, DateTime lastUpdate, long user_id, long counting_id)
        {   

            command.CommandText = "UPDATE t_g_counting SET " +
                "lastupdate = @dtupdate, " +
                "Last_user_update = @id_user, " +
                "status = 1 " +
            "WHERE NAME = @name_counting AND " +
            "id =  @id_counting " +
            "AND LASTUPDATE = @last_update";
            
            command.CommandType = CommandType.Text;
                       
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("@dtupdate", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@id_user", user_id));
            command.Parameters.Add(new SqlParameter("@name_counting", countingName));
            command.Parameters.Add(new SqlParameter("@id_counting", counting_id));
            command.Parameters.Add(new SqlParameter("@last_update", lastUpdate));
            




            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception oex)
            {
                eventLogger.WriteError("CountingUpdateTransaction : " + command.CommandText);
                eventLogger.WriteError("CountingUpdateTransaction : " + oex.Message);

                throw;
            }

        }

        /// <summary>
        /// Вставка пересчета, которого нет в базе
        /// </summary>
        /// <param name="client_id">Идентификатор клиента</param>
        /// <param name="user_id">Идентификатор пользователя</param>
        /// <param name="counting_name">Наименование пересчета</param>
        public void CountingInsertTransaction(long client_id, long user_id, string counting_name)
        {
            command.CommandText = "INSERT INTO t_g_counting " +

            //////04.11.2019 

            //   "(id_client, id_bag, creation, last_user_update, lastupdate, name, date_deposit, date_collect, date_reception, date_value, datetime1, datetime2, datetime3, datetime4, deposit1, deposit2, deposit3, deposit4, deposit5, depoit6, deposit7, deleted, status, source) " +

            "(id_client, id_bag, creation, last_user_update, lastupdate, name, date_deposit, date_collect, date_reception, date_value, datetime1, datetime2, datetime3, datetime4, deposit1, deposit2, deposit3, deposit4, deposit5, deposit6, deposit7, deleted, status, source) " +

            //////04.11.2019 

            "VALUES " +
            "(@id_client, NULL, @dt_create, @id_user, @dt_lastupdate, @name_counting, @dt_deposit, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1)";
            
            command.Parameters.Clear();

            command.Parameters.Add(new SqlParameter("@id_client", client_id));
            command.Parameters.Add(new SqlParameter("@dt_create", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@id_user", user_id));
            command.Parameters.Add(new SqlParameter("@dt_lastupdate", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@name_counting", counting_name));
            command.Parameters.Add(new SqlParameter("@dt_deposit", DateTime.Now));
            
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                eventLogger.WriteError("CountingInsertTransaction : " + command.CommandText);
                eventLogger.WriteError("CountingInsertTransaction : " + ex.Message);


                transaction.Rollback();
                throw;
            }
        }


       
        /// <summary>
        /// Нахождение идентификатора валюты по его коду
        /// </summary>
        /// <param name="currcode">Литерный код валюты</param>
        /// <returns></returns>
        public long GetCurrencyByCurrCode(string currcode)
        {
            long linkCurrence = -1;
            for (int i = 0; i <= 3; i++)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT id FROM t_g_currency WHERE CURRCODE=@currcode";
                    command.Parameters.Add(new SqlParameter("@currcode", currcode));
                    linkCurrence = Convert.ToInt64(command.ExecuteScalar());
                    break;

                }
                else
                {
                    eventLogger.WriteError("GetCurrencyByCurrCode : Нет подключения к БД");
                    Connect();
                    continue;

                }
            }

            return linkCurrence;
        }

        /// <summary>
        /// Считывание данных по номиналу
        /// </summary>
        /// <param name="idDenom">Идентификатор номинала</param>
        /// <returns></returns>
        public DataTable GetDenomination(long idDenom)
        {
            DataTable denomination = new DataTable();
            for (int i = 0; i <= 3; i++)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM t_g_denomination WHERE id=@id_denom";
                    command.Parameters.Add(new SqlParameter("@id_denom", idDenom));
                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(denomination);
                    break;

                }
                else
                {
                    eventLogger.WriteError("GetDenomination : Нет подключения к БД");

                    Connect();
                    continue;
                }
            }

            return denomination;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkCounting"></param>
        /// <param name="linkDenom"></param>
        /// <returns></returns>
        public DataTable GetDenomCnt(long id_counting, long id_denom, long condition_id)
        {
            DataTable denomCnt = new DataTable();
            for (int i = 0; i <= 3; i++)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM t_g_counting_denom WHERE id_counting=@id_counting AND id_denomination=@id_denom AND  id_condition=@condition_id";
                    command.Parameters.Add(new SqlParameter("@id_counting", id_counting));
                    command.Parameters.Add(new SqlParameter("@id_denom", id_denom));
                    command.Parameters.Add(new SqlParameter("@condition_id", condition_id));
                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(denomCnt);
                    break;

                }
                else
                {
                    eventLogger.WriteError("GetDenomCnt : Нет подключения к БД ");

                    Connect();
                    continue;
                }
            }
            return denomCnt;
        }

        public DataTable GetDenomCnt1(long id_counting, long id_denom, long condition_id, long card_id)
        {
            DataTable denomCnt = new DataTable();
            for (int i = 0; i <= 3; i++)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM t_g_counting_denom WHERE id_counting=@id_counting AND id_denomination=@id_denom AND  id_condition=@condition_id and id_card=@card_id";
                    command.Parameters.Add(new SqlParameter("@id_counting", id_counting));
                    command.Parameters.Add(new SqlParameter("@id_denom", id_denom));
                    command.Parameters.Add(new SqlParameter("@condition_id", condition_id));
                    command.Parameters.Add(new SqlParameter("@card_id", card_id));
                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(denomCnt);
                    break;

                }
                else
                {
                    eventLogger.WriteError("GetDenomCnt : Нет подключения к БД ");

                    Connect();
                    continue;
                }
            }
            return denomCnt;
        }

        /// <summary>
        /// Обновление данных по номиналам в пересчете
        /// </summary>
        /// <param name="id_user">Идентификатор пользозователя</param>
        /// <param name="counting_id">Идентификатор пересчета</param>
        /// <param name="denom_id">Идентификатор номинала</param>
        /// <param name="condition_id">Идентификатор состояния</param>
        /// <param name="count">Количество номиналов</param>
        /// <param name="value">Сумма номиналов</param>
        /// 

        /////06.11.2019
        // public void DenomCntUpdateTransaction(long id_user, long counting_id, long denom_id, long condition_id, Int64 count, Int64 value)

        //////13.12.2019
       // public void DenomCntUpdateTransaction(long id_user, long counting_id, long denom_id, long condition_id, Int64 count, Int64 value,string mach1, string oper1)

        public void DenomCntUpdateTransaction(long id_user, long counting_id, long denom_id, long condition_id, Int64 count, Int64 value, string mach1, string oper1,string srt1)
        //////13.12.2019

        /////06.11.2019
        {

            command.CommandText = "UPDATE t_g_counting_denom SET " +
                "LASTUPDATE=@lastupdate, " +
                "LAST_USER_UPDATE = @user_id, " +
                "COUNT = @count_value, " +
                "FACT_VALUE = @sum_value, " +

                /////06.11.2019
                /////"SOURCE = 1 " +
                "SOURCE = 2 " +

                ",workstation = @mach1, " +
                "description1 = @oper1 " +
             /////06.11.2019

             ///13.12.2019
             ",flschet1 = isnull(flschet1, 0) + "+ srt1.ToString()+
             ///13.12.2019
             " WHERE " +
                "id_denomination = @denom_id " +
                "AND id_counting = @counting_id " +
                "AND id_condition = @condition_id ";

            
            command.Parameters.Clear();

            command.Parameters.Add(new SqlParameter("@lastupdate", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@user_id", id_user));
            command.Parameters.Add(new SqlParameter("@counting_id", counting_id));
            command.Parameters.Add(new SqlParameter("@denom_id", denom_id));
            command.Parameters.Add(new SqlParameter("@count_value", count));
            command.Parameters.Add(new SqlParameter("@sum_value", value));
            command.Parameters.Add(new SqlParameter("@condition_id", condition_id));

            /////06.11.2019
            command.Parameters.Add(new SqlParameter("@mach1", mach1));
            command.Parameters.Add(new SqlParameter("@oper1", oper1));
            /////06.11.2019

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception sqlex)
            {
                eventLogger.WriteError("DenomCntUpdateTransaction : " + command.CommandText);
                eventLogger.WriteError("DenomCntUpdateTransaction : " + sqlex.Message);

                transaction.Rollback();
                throw;
            }


        }

        /// <summary>
        /// Добавление данных по номиналам
        /// </summary>
        /// <param name="counting_id">Идентификатор пересчета</param>
        /// <param name="card_id">Идентификатор разделительной карточки</param>
        /// <param name="denom_id">Идентификатор номинала</param>
        /// <param name="condition_id">Идентификатор состояния</param>
        /// <param name="user_id">Идентификатор пользователя</param>
        /// <param name="count">Количество номиналов</param>
        /// <param name="value">Сумма номиналов</param>

        /////06.11.2019
        //public void DenomCntInsertTransaction(long counting_id, long card_id, long denom_id, long condition_id, long user_id, long count, long value)
        public void DenomCntInsertTransaction(long counting_id, long card_id, long denom_id, long condition_id, long user_id, long count, long value, string mach1, string oper1)
        /////06.11.2019
        {

          

           command.CommandText = "INSERT INTO t_g_counting_denom " +

              /////06.11.2019
              // "(id_counting, id_card, id_denomination, id_condition, creation, lastupdate, last_user_update, count, reject_count, fact_value, source, serial_number, serial_number2, description) VALUES " +

              ///13.12.2019
              // "(id_counting, id_card, id_denomination, id_condition, creation, lastupdate, last_user_update, count, reject_count, fact_value, source, serial_number, serial_number2, description,workstation,description1) VALUES " +
              "(id_counting, id_card, id_denomination, id_condition, creation, lastupdate, last_user_update, count, reject_count, fact_value, source, serial_number, serial_number2, description,workstation,description1,flschet1) VALUES " +
              ///13.12.2019

              /////06.11.2019

              
              "(@id_countiong, @id_card, @id_denom, @id_condition, @dt_create, @dt_lastupdate, @id_user, @count_denom, NULL, @sum_denom, " +


              /////06.11.2019
              // "1 , NULL, NULL, NULL)";

              ///13.12.2019
              // "2 , NULL, NULL, NULL,@mach1, @oper1)";
              "2 , NULL, NULL, NULL,@mach1, @oper1,1)";
            ///13.12.2019

            /////06.11.2019

            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("@id_countiong", counting_id));
            command.Parameters.Add(new SqlParameter("@id_card", card_id));
            command.Parameters.Add(new SqlParameter("@id_denom", denom_id));
            command.Parameters.Add(new SqlParameter("@id_condition", condition_id));
            command.Parameters.Add(new SqlParameter("@dt_create", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@dt_lastupdate", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@id_user", user_id));
            command.Parameters.Add(new SqlParameter("@count_denom", count));
            command.Parameters.Add(new SqlParameter("@sum_denom", value));

            /////06.11.2019
            command.Parameters.Add(new SqlParameter("@mach1", mach1));
            command.Parameters.Add(new SqlParameter("@oper1", oper1));
            /////06.11.2019

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                eventLogger.WriteError("DenomCntInsertTransaction : " + command.CommandText);
                eventLogger.WriteError("DenomCntInsertTransaction : " + ex.Message);

                transaction.Rollback();
                throw;
            }

        }

        /// <summary>
        /// Получение данных о наличных по пользователю
        /// </summary>
        /// <param name="user_id">Идентификатор пользователя</param>
        /// <param name="denom_id">Идентификатор номинала</param>
        /// <param name="cashcentre_id">Идентификатор кассового центра</param>
        /// <returns></returns>
        public DataTable GetCash(long user_id, long denom_id, long cashcentre_id)
        {
            DataTable cash = new DataTable();
            if (connection != null && connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM t_g_cash WHERE id_user=@user_id AND id_denomination=@denom_id AND  id_cashcentre=@cashcentre_id";
                command.Parameters.Add(new SqlParameter("@user_id", user_id));
                command.Parameters.Add(new SqlParameter("@denom_id", denom_id));
                command.Parameters.Add(new SqlParameter("@cashcentre_id", cashcentre_id));
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(cash);


            }
            else
            {
                eventLogger.WriteError("GetCash : Нет подключения к БД ");

            }
            return cash;
        }

        public DataTable GetCounting(string countingName)
        {
            DataTable counting = new DataTable();
            if (connection != null && connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM t_g_counting WHERE name=@counting_name";

                command.Parameters.Add(new SqlParameter("@counting_name", countingName));
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(counting);


            }
            else
            {
                eventLogger.WriteError("GetCountingContent : Нет подключения к БД ");

            }
            return counting;

        }

        public DataTable GetCountingSep(string cardName, long counting_id)
        {
            DataTable counting = new DataTable();
            if (connection != null && connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM t_g_cards WHERE name=@card_name AND id_counting = @counting_id" ;

                command.Parameters.Add(new SqlParameter("@card_name", cardName));
                command.Parameters.Add(new SqlParameter("@counting_id", counting_id));
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(counting);


            }
            else
            {
                eventLogger.WriteError("GetCountingSep : Нет подключения к БД ");

            }
            return counting;

        }
        public DataTable GetCountingContent(long user_id, long denom_id, long cashcentre_id)
        {
            DataTable content = new DataTable();
            if (connection != null && connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM t_g_counting_content WHERE id_user=@user_id AND id_denomination=@denom_id AND  id_cashcentre=@cashcentre_id";
                command.Parameters.Add(new SqlParameter("@user_id", user_id));
                command.Parameters.Add(new SqlParameter("@denom_id", denom_id));
                command.Parameters.Add(new SqlParameter("@cashcentre_id", cashcentre_id));
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(content);


            }
            else
            {
                eventLogger.WriteError("GetCountingContent : Нет подключения к БД ");

            }
            return content;
        }

        public DataTable GetCountingContent(long counting_id)
        {
            DataTable content = new DataTable();
            if (connection != null && connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM t_g_counting_content WHERE id_counting=@counting_id";
                
                command.Parameters.Add(new SqlParameter("@counting_id", counting_id));
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(content);


            }
            else
            {
                eventLogger.WriteError("GetCountingContent : Нет подключения к БД ");

            }
            return content;
        }

        public void CardInsertTransaction(long counting_id, string cardName, long user_id)
        {
            //string dateStr = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            command.CommandText = "INSERT INTO t_g_cards " +
                "(NAME, ID_BAG, TYPE, CREATION, LASTUPDATE, LAST_USER_UPDATE, ID_COUNTING)" +
                " VALUES " +
                "(@card_name, NULL, 0, @dt_now, @dt_now, @user_id, @counting_id) ";


            command.Parameters.Clear();


            command.Parameters.Add(new SqlParameter("@card_name", cardName));
            //command.Parameters.Add(new OracleParameter("workstation", workstation));
            command.Parameters.Add(new SqlParameter("@dt_now", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@user_id", user_id));
            command.Parameters.Add(new SqlParameter("@counting_id", counting_id));
            

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception oex)
            {
                eventLogger.WriteError("CardInsertTransaction : " + command.CommandText);
                eventLogger.WriteError("CardInsertTransaction : " + oex.Message);

                transaction.Rollback();
                throw;
            }

        }

        public void InsertCashTransaction(long user_id, long denom_id, long cashcentre_id, int count)
        {
            //string dateStr = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            command.CommandText = "INSERT INTO t_g_cash " +
                "(CREATION, LASTUPDATE, WORKSTATION, LAST_USER_UPDATE, SEQNUMBER, ID_USER, COUNT, ID_DENOMINATION, ID_CASHCENTRE)" +
                " VALUES " +
                "(@last_update, @last_update, NULL, @user_id, 0, @user_id, @count_value, @denom_id, @cash_centre) ";
            

            command.Parameters.Clear();

            
            command.Parameters.Add(new SqlParameter("@last_update", DateTime.Now));
            //command.Parameters.Add(new OracleParameter("workstation", workstation));
            command.Parameters.Add(new SqlParameter("@user_id", user_id));
            command.Parameters.Add(new SqlParameter("@count_value", count));
            command.Parameters.Add(new SqlParameter("@denom_id", denom_id));
            command.Parameters.Add(new SqlParameter("@cash_centre", cashcentre_id));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception oex)
            {
                eventLogger.WriteError("InsertCashTransaction : " + command.CommandText);
                eventLogger.WriteError("InsertCashTransaction : " + oex.Message);

                transaction.Rollback();
                throw;
            }

        }


        public void UpdateCashTransaction(long cash_id, long user_id, DateTime cashLastUpdate, long count)
        {
            //int seqNumber_inc = seqNumber + 1;
            command.CommandText = "UPDATE t_g_cash SET " +
                "LASTUPDATE =@last_update, " +
                //"WORKSTATIONID = :workstation, " +
                "last_user_update = @user_id, " +
                //"SEQNUMBER = :seq_number_inc, " +
                "id_user = @user_id, " +
                "count = @count_value " +
            "WHERE " +
            "id = @cash_id " +
            "AND LASTUPDATE = @cash_lastupdate";
            //"AND SEQNUMBER = :seq_number ";

            //command.BindByName = true;
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("@last_update", DateTime.Now));
            //command.Parameters.Add(new OracleParameter("workstation", workstation));
            command.Parameters.Add(new SqlParameter("@user_id", user_id));
            //command.Parameters.Add(new OracleParameter("seq_number_inc", seqNumber_inc));
            command.Parameters.Add(new SqlParameter("@count_value", count));
            command.Parameters.Add(new SqlParameter("@cash_id", cash_id));
            command.Parameters.Add(new SqlParameter("@cash_lastupdate", cashLastUpdate));

            //command.Parameters.Add(new OracleParameter("seq_number", seqNumber));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception oex)
            {
                eventLogger.WriteError("UpdateCashTransaction : " + command.CommandText);
                eventLogger.WriteError("UpdateCashTransaction : " + oex.Message);

                transaction.Rollback();
                throw;
            }

        }

        public long GetCurrencyByCode(string curr_code)
        {
            long id_currency = -1;
            if (connection != null && connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT id FROM t_g_currency WHERE curr_code=@curr_code";
                command.Parameters.Add(new SqlParameter("@curr_code", curr_code));
                
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                //dataAdapter.Fill(cash);
                id_currency = Convert.ToInt64(command.ExecuteScalar());

            }
            else
            {
                eventLogger.WriteError("GetCountingContent : Нет подключения к БД ");

            }
            return id_currency;

        }

        public DataTable GetAccountsByClient(long client_id)
        {
            DataTable accounts = new DataTable();
            if (connection != null && connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM t_g_account WHERE id_client=@client_id";
                command.Parameters.Add(new SqlParameter("@client_id", client_id));

                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(accounts);
                //id_currency = Convert.ToInt64(command.ExecuteScalar());

            }
            else
            {
                eventLogger.WriteError("GetAccountsByClient : Нет подключения к БД ");

            }
            return accounts;

        }

        public void UpdateTransactionCountingContent(long user_id, long fact_value, long counting_id, long currency_id)
        {
            command.CommandText = "UPDATE t_g_counting_content SET " +
                "LASTUPDATE =@last_update, " +
                //"WORKSTATIONID = :workstation, " +
                
            //"SEQNUMBER = :seq_number_inc, " +

            ////24.01.2020
             "last_user_update = @user_id " +
            /// "last_user_update = @user_id, " +
            //   "fact_value = @fact_value " +
            ////24.01.2020

            "WHERE " +
            "id_counting = @counting_id " +
            "AND id_currency = @currency_id";
            //"AND SEQNUMBER = :seq_number ";

            //command.BindByName = true;
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("@last_update", DateTime.Now));
            //command.Parameters.Add(new OracleParameter("workstation", workstation));
            command.Parameters.Add(new SqlParameter("@user_id", user_id));
            //command.Parameters.Add(new OracleParameter("seq_number_inc", seqNumber_inc));

            ////24.01.2020
            //command.Parameters.Add(new SqlParameter("@fact_value", fact_value));
            ////24.01.2020

            command.Parameters.Add(new SqlParameter("@counting_id", counting_id));
            command.Parameters.Add(new SqlParameter("@currency_id", currency_id));

            //command.Parameters.Add(new OracleParameter("seq_number", seqNumber));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception oex)
            {
                eventLogger.WriteError("UpdateTransactionCountingContent : " + command.CommandText);
                eventLogger.WriteError("UpdateTransactionCountingContent : " + oex.Message);

                transaction.Rollback();
                throw;
            }
        }

        /// 22.10.2019

        public void Zapros(string zapros,string ID
            //,string table1
            )
        {
           // bool result = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
               // try
                {
                    con.Open();
                    string query1 = "";
                    if (ID.Trim()!="")
                    query1 = String.Format(zapros, ID);
                    else
                    query1 = zapros;
                    SqlCommand com = new SqlCommand(query1, con);
                    com.ExecuteNonQuery();

                    

                    // if (com.ExecuteNonQuery() != 0)
                    //     result = true;
                }
               // catch { }
               // return result;
            }

           

        }

        public Int32 Zapros1(string zapros, string ID
           //,string table1
           )
        {
            Int32 result = -1;
            // bool result = false;
            //  using (
            SqlConnection con = new SqlConnection(connectionString);
                //)
            {
                // try
                {
                    con.Open();
                    string query1 = "";
                    if (ID.Trim() != "")
                        query1 = String.Format(zapros, ID);
                    else
                        query1 = zapros;
                    SqlCommand com = new SqlCommand(query1, con);
                   // transaction=connection.BeginTransaction();
                  //  com.Transaction = transaction;
                    
                        ;
                  //  com.ExecuteNonQuery();
                    result = Convert.ToInt32(com.ExecuteScalar());



                    // if (com.ExecuteNonQuery() != 0)
                    //     result = true;
                }
                // catch { }
              //  return result;
            }

            return result;

        }

        public Int32 Zapros2(List<string> zapros,int k1)
        {
            Int32 result = 1;

             if ((connection != null) && (connection.State == ConnectionState.Open))
             {

            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand com;
            int i1 = 0, id_bag = -1, id_coun = -1;





                try
                {


                    foreach (string s in zapros)
                    {


                        string s1 = String.Format(s, id_bag, id_coun);
                        com = new SqlCommand(s1, connection);
                        com.Transaction = transaction;

                        if (k1 == 1)
                        {
                            com.ExecuteNonQuery();
                        }
                        else
                        {

                            if ((i1 == 0) | (i1 == 1))
                            {

                                if (i1 == 0)
                                    id_bag = Convert.ToInt32(com.ExecuteScalar());
                                if (i1 == 1)
                                    id_coun = Convert.ToInt32(com.ExecuteScalar());


                            }

                            else
                            {
                                //       int i22 = Convert.ToInt32("gfhf");
                                com.ExecuteNonQuery();
                            }

                        }
                        i1 = i1 + 1;
                    }




                    transaction.Commit();


                }
                catch (Exception ex)
                {
                    ///31.10.2019
                    eventLogger.WriteError("UpdateDataTransaction : " + ex.Message);
                    ///31.10.2019
                    transaction.Rollback();
                    result = 0;
                }
            

                
            }
            return result;
           


        }
            /// <summary>
            /// Обновление данных в БД через транзакцию
            /// </summary>
            /// <param name="dataSets">Список наборов данных</param>
            /// <param name="tableNames">Список имен таблиц</param>
            public void UpdateData1(DataSet commonDataSet)
        {
            List<SqlDataAdapter> sqlDataAdapters = new List<SqlDataAdapter>();


            foreach (DataTable dataSetTable in commonDataSet.Tables)
            {
                String sqlSelect = "SELECT TOP 0 * FROM " + dataSetTable.TableName;
                sqlDataAdapters.Add(new SqlDataAdapter(sqlSelect, connection));
                sqlCommand = new SqlCommandBuilder(sqlDataAdapters[sqlDataAdapters.Count - 1]);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].DeleteCommand = sqlCommand.GetDeleteCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                //sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.E
                //sqlDataAdapters[sqlDataAdapters.Count - 1].RowUpdated += new SqlRowUpdatedEventHandler(SqlAdapterRowUpdated);
            }

            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
              //  SqlTransaction transaction = connection.BeginTransaction();

               // try
               // {
                    int i = 0;
                    foreach (var sqlAdapter in sqlDataAdapters)
                    {
                        sqlAdapter.InsertCommand.Transaction = transaction;
                        //sqlAdapter.adapter.InsertCommand.Transaction.
                        sqlAdapter.UpdateCommand.Transaction = transaction;
                        sqlAdapter.DeleteCommand.Transaction = transaction;

                        sqlAdapter.Update(commonDataSet.Tables[i]);
                        i++;
                    }
                  //  transaction.Commit();
               // }
               // catch (Exception ex)
              //  {
               //     eventLogger.WriteError("UpdateDataTransaction : " + ex.Message);
                 //   transaction.Rollback();
               // }
            }






        }

        public Int32 UpdateData2(DataSet commonDataSet)
        {
            List<SqlDataAdapter> sqlDataAdapters = new List<SqlDataAdapter>();
            int result = 1;

            foreach (DataTable dataSetTable in commonDataSet.Tables)
            {
                String sqlSelect = "SELECT TOP 0 * FROM " + dataSetTable.TableName;
                sqlDataAdapters.Add(new SqlDataAdapter(sqlSelect, connection));
                sqlCommand = new SqlCommandBuilder(sqlDataAdapters[sqlDataAdapters.Count - 1]);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].DeleteCommand = sqlCommand.GetDeleteCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                //sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.E
                //sqlDataAdapters[sqlDataAdapters.Count - 1].RowUpdated += new SqlRowUpdatedEventHandler(SqlAdapterRowUpdated);
            }

            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    int i = 0;
                    foreach (var sqlAdapter in sqlDataAdapters)
                    {
                        sqlAdapter.InsertCommand.Transaction = transaction;
                        //sqlAdapter.adapter.InsertCommand.Transaction.
                        sqlAdapter.UpdateCommand.Transaction = transaction;
                        sqlAdapter.DeleteCommand.Transaction = transaction;

                        sqlAdapter.Update(commonDataSet.Tables[i]);
                        i++;
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    eventLogger.WriteError("UpdateDataTransaction : " + ex.Message);
                    transaction.Rollback();
                    result= 0;
                }
            }

            return result;





        }

        public void UpdateData3(DataSet commonDataSet)
        {
            List<SqlDataAdapter> sqlDataAdapters = new List<SqlDataAdapter>();
           // int result = 1;

            foreach (DataTable dataSetTable in commonDataSet.Tables)
            {
                String sqlSelect = "SELECT TOP 0 * FROM " + dataSetTable.TableName;
                sqlDataAdapters.Add(new SqlDataAdapter(sqlSelect, connection));
                sqlCommand = new SqlCommandBuilder(sqlDataAdapters[sqlDataAdapters.Count - 1]);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].DeleteCommand = sqlCommand.GetDeleteCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                //sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.E
                //sqlDataAdapters[sqlDataAdapters.Count - 1].RowUpdated += new SqlRowUpdatedEventHandler(SqlAdapterRowUpdated);
            }

            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    int i = 0;
                    foreach (var sqlAdapter in sqlDataAdapters)
                    {
                        sqlAdapter.InsertCommand.Transaction = transaction;
                        //sqlAdapter.adapter.InsertCommand.Transaction.
                        sqlAdapter.UpdateCommand.Transaction = transaction;
                        sqlAdapter.DeleteCommand.Transaction = transaction;

                        sqlAdapter.Update(commonDataSet.Tables[i]);

                        //////28.10.2019
                        if (commonDataSet.Tables[i].TableName == "t_g_cards")
                        {
                            int i1 = Convert.ToInt32( commonDataSet.Tables[i].Rows[0]["ID"]);

                        }


                        //////28.10.2019

                        i++;
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    eventLogger.WriteError("UpdateDataTransaction : " + ex.Message);
                    transaction.Rollback();
                   // result = 0;
                }
            }

           





        }
        /// 22.10.2019

        //////12.12.2019  
        public int UpdateData80( List<string> zapros)
        {

            int result = 1;
            if ((connection != null) && (connection.State == ConnectionState.Open))
            {



                



                SqlTransaction transaction1 = null;




                



                try
                {

                    transaction1 = connection.BeginTransaction();
                    SqlCommand com;


                    

                    

                    foreach (string s in zapros)
                    {


                        if (s.Trim().ToString() != "")
                        {
                            string s1 = s;
                            com = new SqlCommand(s1, connection);
                            com.Transaction = transaction1;

                            com.ExecuteNonQuery();
                        }

                    }

                    



                    transaction1.Commit();

                }
                catch (Exception ex)
                {
                    transaction1.Rollback();
                    result = 0;
                    System.Windows.Forms.MessageBox.Show("Невозможно выполнить операцию: " + ex.Message);



                }



            }
            return result;
        }
        //////12.12.2019 

        /// <summary>
        /// Update Table counting_denom and notes FOR counterfietTable
        /// </summary>
        /// <param name="counterfiet"></param>
        /// <param name="counting_deon and notes"></param>
        /// <param name="zapros"></param>
        public int UpdateDataCounterfiet(DataTable dataSet)
        {
            int i=0;

            return  i;
        }
        ///////01.11.2019

        public int UpdateData8(DataSet dataSetForm, string tableName, List<string> zapros)
        {
            {
                /*
                Int32 result = 1;

                if ((connection != null) && (connection.State == ConnectionState.Open))
                {

                    SqlTransaction transaction = connection.BeginTransaction();
                    SqlCommand com;
                    int i1 = 0, id_bag = -1, id_coun = -1;





                    try
                    {


                        foreach (string s in zapros)
                        {


                            string s1 = String.Format(s, id_bag, id_coun);
                            com = new SqlCommand(s1, connection);
                            com.Transaction = transaction;


                            com.ExecuteNonQuery();


                            i1 = i1 + 1;
                        }




                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        ///31.10.2019
                        eventLogger.WriteError("UpdateDataTransaction : " + ex.Message);
                        ///31.10.2019
                        transaction.Rollback();
                        result = 0;
                    }



                }
                return result;
    */
            }
            
            int result = 1;
            //TestDat

            if ((connection != null) && (connection.State == ConnectionState.Open))
            {



                //  SqlCommand com;



                SqlTransaction transaction1 = null;




                String sqlSelect = "SELECT TOP 0 * FROM " + tableName;
                sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
                


               


                sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);
                sqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                
                

                try
                    {

                         transaction1 = connection.BeginTransaction();
                    SqlCommand com;
                   

                    sqlDataAdapter.InsertCommand.Transaction = transaction1;
                    sqlDataAdapter.UpdateCommand.Transaction = transaction1;
                    sqlDataAdapter.DeleteCommand.Transaction = transaction1;



                     sqlDataAdapter.Update(dataSetForm);
                     dataSetForm.AcceptChanges();

                    //////12.12.2019
                    
                    /////

                    foreach (string s in zapros)
                        {


                            if (s.Trim().ToString() != "")
                            {
                            string s1 = s;
                            com = new SqlCommand(s1, connection);
                                com.Transaction = transaction1;

                                com.ExecuteNonQuery();
                            }
                           
                        }

                    /////
                   
                    //////12.12.2019

                    transaction1.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction1.Rollback();
                        result = 0;
                        System.Windows.Forms.MessageBox.Show("Невозможно выполнить операцию: " + ex.Message);



                    }


                
            }
            return result;
            


        }
        ///////01.11.2019

        ///04.11.2019
        public void DenomCntDeleteTransaction(long 
           // counting_id
            card_id)
        {

            //command.CommandText = "delete  from t_g_counting_denom where id_counting=@id_countiong" ;
            command.CommandText = "delete  from t_g_counting_denom where id_card=@card_id";

            command.Parameters.Clear();
            // command.Parameters.Add(new SqlParameter("@id_countiong", counting_id));
            command.Parameters.Add(new SqlParameter("@card_id", card_id));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                eventLogger.WriteError("DenomCntDeleteTransaction : " + command.CommandText);
                eventLogger.WriteError("DenomCntDeleteTransaction : " + ex.Message);

                transaction.Rollback();
                throw;
            }

        }
        ///04.11.2019

        ////05.11.2019
        public DataSet GetData_Cards(string id_contin)
        {
            dataSet = new DataSet();
            sqlCommand = new SqlCommandBuilder(sqlDataAdapter);


            if ((connection != null) && (connection.State == ConnectionState.Open))
            {

                String sqlSelect = "select * from t_g_cards where id_counting = '"+ id_contin.ToString()+ "' and not exists(select 1 from t_g_counting_denom where id_counting = '" + id_contin.ToString() + "' )";
                sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
                


                string str = sqlDataAdapter.SelectCommand.CommandText;
                sqlDataAdapter.Fill(dataSet);


            }
            else
            {
                dataSet = null;
            }
            return dataSet;
        }

        public DataSet GetData_Cards1(string id_contin)
        {
            dataSet = new DataSet();
            sqlCommand = new SqlCommandBuilder(sqlDataAdapter);


            if ((connection != null) && (connection.State == ConnectionState.Open))
            {

                String sqlSelect = "select * from t_g_cards where id_counting = '" + id_contin.ToString()  + "' ";
                sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                sqlCommand = new SqlCommandBuilder(sqlDataAdapter);



                string str = sqlDataAdapter.SelectCommand.CommandText;
                sqlDataAdapter.Fill(dataSet);


            }
            else
            {
                dataSet = null;
            }
            return dataSet;
        }

        /////29.01.2020

        public string Dvden1(string p1, long p2, int p3, long p4)
        {
            string str1 = "1Выполнено!";

            if (connection != null && connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("PROC_DVSRD", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter RetVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                RetVal.Direction = ParameterDirection.ReturnValue;
                SqlParameter r1 = command.Parameters.Add("@P1", SqlDbType.BigInt);
                r1.Direction = ParameterDirection.Input;
                SqlParameter r2 = command.Parameters.Add("@P2", SqlDbType.NVarChar);
                r2.Direction = ParameterDirection.Input;
                SqlParameter r3 = command.Parameters.Add("@P3", SqlDbType.Int);
                r1.Direction = ParameterDirection.Input;
                SqlParameter r4 = command.Parameters.Add("@P4", SqlDbType.BigInt);
                r1.Direction = ParameterDirection.Input;
                r1.Value = p2;
                r2.Value = p1;
                r3.Value = p3;
                r4.Value = p4;

                if (p3 != 2)
                    command.ExecuteNonQuery();
                else
                {

                    using (SqlDataReader reader = command.ExecuteReader())

                    {
                        while (reader.Read())
                        {

                            str1 = reader[0].ToString()+ reader[1].ToString();



                        }
                    }

                }
            }

            return str1;


        }

        /////29.01.2020

        public DataSet Poisksh(string sql1)
           
        {
            DataSet accounts = new DataSet();
            if (connection != null && connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = sql1;
                

                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(accounts);
                

            }
            else
            {
                eventLogger.WriteError("GetAccountsByClient : Нет подключения к БД ");

            }
            return accounts;

            
        }

        public DataSet GetData8(string tableName)
        {
            dataSet = new DataSet();
            //sqlCommand = new SqlCommandBuilder(sqlDataAdapter);


            if ((connection != null) && (connection.State == ConnectionState.Open))
            {

                String sqlSelect = "SELECT * FROM " + tableName;

                SqlDataAdapter adapter = new SqlDataAdapter(sqlSelect, connection);

                
                adapter.Fill(dataSet);
                                
            }
            else
            {
                dataSet = null;
            }
            return dataSet;
        }

        ////05.11.2019

        ///06.11.2019 
        public DataSet GetData9(string sqlsel1)
        {          
            dataSet = new DataSet();          
            
            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                //System.Diagnostics.Debug.WriteLine("Connect!!!!!!!!!!!!!!!!!!!!!!!");
                SqlDataAdapter adapter = new SqlDataAdapter(sqlsel1, connection);

                adapter.Fill(dataSet);
                
            }
            else
            {                
                dataSet = null;
            }
            return dataSet;
        }
        ///06.11.2019 

        ///08.11.2019 
        public DataSet GetData10(String sqlSelect)
        {
            dataSet = new DataSet();
            sqlCommand = new SqlCommandBuilder(sqlDataAdapter);

            

            if ((connection != null) && (connection.State == ConnectionState.Open))
            {

                
                sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);


                string str = sqlDataAdapter.SelectCommand.CommandText;
                sqlDataAdapter.Fill(dataSet);

            }
            else
            {
                dataSet = null;
            }
            return dataSet;
        }
        ///08.11.2019 

        /////14.11.2019
        public Int32 Zapros33(MemoryStream ms,string zapros, string ID )
        {
            Int32 result = -1;
            
            SqlConnection con = new SqlConnection(connectionString);
           
           
                    con.Open();
                    string query1 = "";
                    if (ID.Trim() != "")
                        query1 = String.Format(zapros, ID);
                    else
                        query1 = zapros;
                    SqlCommand com = new SqlCommand(query1, con);

            com.Parameters.Add("@Scheme", SqlDbType.Image).Value = ms.ToArray();


            result = Convert.ToInt32(com.ExecuteScalar());


            /*
             string query = "Insert into TableScheme values(Scheme =@Scheme)";
SqlConnection conn ... 
SqlCommand comm = new SqlCommand(query, conn);
comm.Parameters.Add("@Scheme", SqlDbType.Image).Value = ms.ToArray();
conn.Open();
comm.ExecuteNonQuery();
            */


            return result;

        }
        /////14.11.2019

        /////16.01.2020
        public void GetDataset11(List<DataSet> dataSets,int ipr1, int ipech1)
        {


            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                int i1 = 0;
                long icl1 = 0;

                  SqlTransaction transaction1 = null;
                //SqlTransaction transaction1 = null;
                //   transaction1 = connection.BeginTransaction();

             //   OleDbTransaction transaction1 = null;
                //SqlDataAdapter sqlDataAdapter1 = null;
                //SqlCommandBuilder sqlCommand1 = null;

                DataSet d1pr = dataSets[1];
                DataSet d2pr = dataSets[2];
                DataSet d3pr = dataSets[3];
                DataSet d4pr = dataSets[4];

                DataSet d1t = null;
                DataSet d2t = null;
                DataSet d3t = null;
                DataSet d4t = null;

                string sud1 = "";

                /////20.01.2020

                if (ipr1 > 0)
                {

                    d1t = GetData9("SELECT id  FROM t_g_pechatclien where id_client="+ipr1.ToString());
                    d2t = GetData9("SELECT id  FROM t_g_clienttocc where id_client=" + ipr1.ToString());
                    d3t = GetData9("SELECT  t1.id  FROM t_g_encashpoint t1 inner join t_g_clienttocc t2 on(t1.id_clienttocc=t2.id) where t2.id_client=" + ipr1.ToString());
                    d4t = GetData9("SELECT id  FROM t_g_account where id_client=" + ipr1.ToString());


                }

                /////20.01.2020

                    foreach (var dataSetTables in dataSets)
                {


                    if (i1 == 0)
                    {




                       


                        
                        String sqlSelect = "SELECT TOP 0 * FROM t_g_client" ;
                        
                        sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                        sqlCommand = new SqlCommandBuilder(sqlDataAdapter);




                       
                       

                        
                        sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                        sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                        sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);
                        sqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                       
                        


                        try
                        {
                            /*
                            OleDbConnection connection1 =
               new OleDbConnection("Provider=SQLOLEDB;" + connection.ConnectionString);

                            connection1.Open();
                            transaction1 = connection1.BeginTransaction();
                            // SqlCommand com;

                            OleDbCommand com;


                            OleDbDataAdapter adapter =
          new OleDbDataAdapter();
                            adapter.SelectCommand =
                                new OleDbCommand(sqlSelect, connection1);
                            OleDbCommandBuilder builder =
                                new OleDbCommandBuilder(adapter);

                            adapter.Fill(dataSetTables);

                            // Code to modify data in the DataSet here.

                            // Without the OleDbCommandBuilder, this line would fail.
                            adapter.UpdateCommand = builder.GetUpdateCommand();

                            adapter.UpdateCommand.Transaction = transaction1;

                            adapter.Update(dataSetTables);
                            */

                            transaction1 = connection.BeginTransaction();

                            sqlDataAdapter.InsertCommand.Transaction = transaction1;
                            sqlDataAdapter.UpdateCommand.Transaction = transaction1;
                            sqlDataAdapter.DeleteCommand.Transaction = transaction1;



                            sqlDataAdapter.Update(dataSetTables);
                            //  dataSetTables.AcceptChanges();

                             SqlCommand com;
                            com = new SqlCommand("select max(id) from t_g_client", connection);
                            //com = new OleDbCommand("select max(id) from t_g_client", connection1);

                            com.Transaction = transaction1;

                                using (SqlDataReader reader = com.ExecuteReader())
                           // using (OleDbDataReader reader = com.ExecuteReader())
                            //  SqlDataReader reader = com.ExecuteReader();
                            {
                                while (reader.Read())
                                {

                                    icl1 = Convert.ToInt64(reader[0]);
                            
                                    //       System.Windows.Forms.MessageBox.Show("ID " + reader[0].ToString());
                                    //   Console.WriteLine(String.Format("{0}, {1}",
                                    //      reader[0], reader[1]));


                                }
                            }

                            // com.ExecuteNonQuery();
                            //  int i1 = Convert.ToInt32(dataSetTables.Tables[i].Rows[0]["ID"]);
                            //  System.Windows.Forms.MessageBox.Show("ID " + dataSetTables.Tables[0].Rows[0]["ID"].ToString());


                            /*
                            foreach (string s in zapros)
                            {


                                if (s.Trim().ToString() != "")
                                {
                                    string s1 = s;
                                    com = new SqlCommand(s1, connection);
                                    com.Transaction = transaction1;

                                    com.ExecuteNonQuery();
                                }

                            }
                            */


                       //     transaction1.Commit();

                        }
                        catch (Exception ex)
                        {
                            transaction1.Rollback();
                            System.Windows.Forms.MessageBox.Show("Невозможно выполнить операцию: " + ex.Message);



                        }

                    }
                  
                    /////21.01.2020
                    if (ipech1==1)
                    /////21.01.2020
                        if (i1 == 1)
                    {
                        if (dataSetTables != null)
                        {
                            foreach (DataRow dr in dataSetTables.Tables[0].Rows)
                            {

                                //   dr["id_client"] = icl1;

                                string s1 = "";
                                if (dr["id"] != null) 
                                { 
                                   
                                    if (dr["id"].ToString().Trim()!="")
                                    s1 = "update t_g_pechatclien set img1=@Scheme,img2=@Scheme1,last_update_user='" + DataExchange.CurrentUser.CurrentUserId.ToString() + "',lastupdate='" + DateTime.Now.ToString() + "' where id='" + dr["id"].ToString() + "'";
                                    else
                                    s1 = "insert into t_g_pechatclien(id_client,name_pechat,img1,img2,creation,last_update_user,lastupdate) values('" + icl1.ToString() + "','" + dr["name_pechat"].ToString().Trim() + "',@Scheme,@Scheme1,'" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DateTime.Now.ToString() + "')";



                                }
                                else
                                    s1 = "insert into t_g_pechatclien(id_client,name_pechat,img1,img2,creation,last_update_user,lastupdate) values('" + icl1.ToString() + "','" + dr["name_pechat"].ToString().Trim() + "',@Scheme,@Scheme1,'" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DateTime.Now.ToString() + "')";



                                SqlCommand com;
                                com = new SqlCommand(s1, connection);
                                com.Parameters.Add("@Scheme", SqlDbType.Image).Value = dr["img1"];
                                com.Parameters.Add("@Scheme1", SqlDbType.Image).Value = dr["img2"];
                                com.Transaction = transaction1;

                                com.ExecuteNonQuery();

                            }

                            /*
                            String sqlSelect = "SELECT TOP 0 * FROM t_g_pechatclien";
                            sqlDataAdapter1 = new SqlDataAdapter(sqlSelect, connection);
                            sqlCommand1 = new SqlCommandBuilder(sqlDataAdapter1);






                            sqlDataAdapter1.InsertCommand = sqlCommand1.GetInsertCommand();
                            sqlDataAdapter1.UpdateCommand = sqlCommand1.GetUpdateCommand();
                            sqlDataAdapter1.DeleteCommand = sqlCommand1.GetDeleteCommand();
                            //   sqlDataAdapter1.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;

                            sqlDataAdapter1.InsertCommand.Transaction = transaction1;
                            sqlDataAdapter1.UpdateCommand.Transaction = transaction1;
                            sqlDataAdapter1.DeleteCommand.Transaction = transaction1;



                            sqlDataAdapter1.Update(dataSetTables);
                            //  dataSetTables.AcceptChanges();
                            */

                        }

                    }

                    if (i1 == 2)
                    {
                        if (dataSetTables != null)
                        {
                            foreach (DataRow dr in dataSetTables.Tables[0].Rows)
                            {

                                //   dr["id_client"] = icl1;

                                string s1 = "";
                                long idtoch = 0;
                                //int idtoch = 0;
                                long idtochpr = 0;

                                long iden = 0;
                                long idenpr = 0;

                                idtochpr = Convert.ToInt64(dr["id"]);

                                if (Convert.ToInt64(dr["id"]) < 0)
                                {


                                    s1 = "insert into t_g_clienttocc(id_client,id_cashcentre,last_update_user,lastupdate) values('" + icl1.ToString() + "','" + dr["id_cashcentre"].ToString().Trim() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DateTime.Now.ToString() + "')" + " SELECT CAST(scope_identity() AS int)";


                                    SqlCommand com2;
                                    com2 = new SqlCommand(s1, connection);

                                    com2.Transaction = transaction1;

                                    //com.ExecuteNonQuery();

                                    idtoch = Convert.ToInt64(com2.ExecuteScalar());

                                    //   idtoch = Convert.ToInt32(com.ExecuteScalar());

                                }
                                else
                                    //   idtoch = Convert.ToInt32(dr["id"]);
                                    idtoch = Convert.ToInt64(dr["id"]);

                                /////перебор точек инкасации

                                /////20.01.2020
                               if (dataSets[3] != null)
                               {



                                    //  foreach (DataRow dr1 in dataSets[2].Tables[0].Rows)

                                    //for (int i2 = dataSets[3].Tables[0].Rows.Count - 1; i2 >= 0; i2--)
                                    for (int i2 =0 ; i2 < dataSets[3].Tables[0].Rows.Count ; i2++)
                                    /////20.01.2020
                                    {

                                        idenpr = Convert.ToInt64(dataSets[3].Tables[0].Rows[i2]["id"]);

                                        if (dataSets[3].Tables[0].Rows[i2]["id_clienttocc"].ToString() == idtochpr.ToString())
                                        {

                                            if (Convert.ToInt64(dataSets[3].Tables[0].Rows[i2]["id"]) < 0)
                                                s1 = "insert into t_g_encashpoint(id_clienttocc,name,address,INF1,INF2,creation,last_update_user,lastupdate) values('" + idtoch.ToString() + "','" + dataSets[3].Tables[0].Rows[i2]["name"].ToString().Trim() + "','" + dataSets[3].Tables[0].Rows[i2]["address"].ToString().Trim() + "','" + dataSets[3].Tables[0].Rows[i2]["INF1"].ToString().Trim() + "','" + dataSets[3].Tables[0].Rows[i2]["INF2"].ToString().Trim() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DateTime.Now.ToString() + "')" + "SELECT CAST(scope_identity() AS int)";
                                            else
                                                s1 = "update t_g_encashpoint set name='" + dataSets[3].Tables[0].Rows[i2]["name"].ToString().Trim() + "', address='" + dataSets[3].Tables[0].Rows[i2]["address"].ToString().Trim() + "', INF1='" + dataSets[3].Tables[0].Rows[i2]["INF1"].ToString().Trim() + "', INF2='" + dataSets[3].Tables[0].Rows[i2]["INF2"].ToString().Trim() + "', last_update_user='" + DataExchange.CurrentUser.CurrentUserId.ToString() + "',lastupdate='" + DateTime.Now.ToString() + "' where id=" + Convert.ToInt64(dataSets[3].Tables[0].Rows[i2]["id"]);



                                            SqlCommand com3;
                                            com3 = new SqlCommand(s1, connection);

                                            com3.Transaction = transaction1;

                                            if (Convert.ToInt64(dataSets[3].Tables[0].Rows[i2]["id"]) < 0)

                                                iden = Convert.ToInt64(com3.ExecuteScalar());
                                            else
                                            {
                                                com3.ExecuteNonQuery();
                                                iden = Convert.ToInt64(dataSets[3].Tables[0].Rows[i2]["id"]);

                                            }

                                            /////20.01.2020
                                            if (dataSets[4] != null)
                                            {
                                             /////20.01.2020

                                            //    for (int i3 = dataSets[4].Tables[0].Rows.Count - 1; i3 >= 0; i3--)

                                                  for (int i3 = 0; i3 < dataSets[4].Tables[0].Rows.Count ; i3++)


                                                    /////20.01.2020
                                                    {

                                                        if ((dataSets[4].Tables[0].Rows[i3]["id_clienttocc"].ToString() == idtochpr.ToString()) & (dataSets[4].Tables[0].Rows[i3]["id_encashpoint"].ToString() == idenpr.ToString()))
                                                {

                                                        if (Convert.ToInt64(dataSets[4].Tables[0].Rows[i3]["id"]) < 0)
                                                            if (ipr1 != 0)
                                                                s1 = "insert into t_g_account(id_clienttocc,id_encashpoint,id_client,name,id_currency,creation,last_update_user,lastupdate) values('" + idtoch.ToString() + "','" + iden.ToString() + "','" + ipr1.ToString() + "','" + dataSets[4].Tables[0].Rows[i3]["name"].ToString().Trim() + "','" + dataSets[4].Tables[0].Rows[i3]["id_currency"].ToString().Trim() + "','" + DateTime.Now.ToString() + "','" + DataExchange.CurrentUser.CurrentUserId.ToString() + "','" + DateTime.Now.ToString() + "')";
                                                            else s1 = "insert into t_g_account(id_clienttocc, id_encashpoint, id_client, name, id_currency, creation, last_update_user, lastupdate) values('" + idtoch.ToString() + "', '" + iden.ToString() + "', '" + icl1.ToString() + "', '" + dataSets[4].Tables[0].Rows[i3]["name"].ToString().Trim() + "', '" + dataSets[4].Tables[0].Rows[i3]["id_currency"].ToString().Trim() + "', '" + DateTime.Now.ToString() + "', '" + DataExchange.CurrentUser.CurrentUserId.ToString() + "', '" + DateTime.Now.ToString() + "')";
                                                        else
                                                            s1 = "update t_g_account set name='" + dataSets[4].Tables[0].Rows[i3]["name"].ToString().Trim() + "', id_currency='" + dataSets[4].Tables[0].Rows[i3]["id_currency"].ToString().Trim() + "', last_update_user='" + DataExchange.CurrentUser.CurrentUserId.ToString() + "',lastupdate='" + DateTime.Now.ToString() + "' where id=" + Convert.ToInt64(dataSets[4].Tables[0].Rows[i3]["id"]);



                                                    SqlCommand com1;
                                                    com1 = new SqlCommand(s1, connection);

                                                    com1.Transaction = transaction1;

                                                    // iden = Convert.ToInt64(com.ExecuteScalar());

                                                    com1.ExecuteNonQuery();

                                                    ///20.01.2020
                                                    //     dataSets[4].Tables[0].Rows[i3].Delete();
                                                    ///20.01.2020
                                                }


                                            }

                                            /////20.01.2020
                                        }
                                            /////20.01.2020


                                            ///20.01.2020
                                            // dataSets[3].Tables[0].Rows[i2].Delete();
                                            ///20.01.2020

                                        }


                                    }

                                /////20.01.2020
                                }
                                /////20.01.2020

                                /////



                            }
                        }
                    }

                            i1 = i1 + 1;
                }

                /////20.01.2020

                ////удаление старых id


                if (ipr1 >0)
                {

                    int ifl1 = 0;
                    int i33=0;
                    SqlCommand com1;
                    ////1

                    /////21.01.2020
                    if (ipech1 == 1)
                    {
                    /////21.01.2020

                        sud1 = "";

                        if (d1t != null)
                            foreach (DataRow dr in d1t.Tables[0].Rows)
                            {
                                ifl1 = 0;
                                if (d1pr != null)
                                    foreach (DataRow dr1 in d1pr.Tables[0].Rows)
                                    {

                                        if (dr["id"].ToString() == dr1["id"].ToString())
                                        {
                                            ifl1 = 1;
                                            break;
                                        }

                                    }

                                if (ifl1 == 0)
                                    sud1 = sud1 + dr["id"] + ",";


                            }

                        
                         i33 = sud1.Length;
                        if (i33 > 1)
                        {
                            sud1 = sud1.Substring(0, sud1.Length - 1);

                            com1 = new SqlCommand("delete from t_g_pechatclien where id in (" + sud1.ToString() + ")", connection);

                            com1.Transaction = transaction1;
                            com1.ExecuteNonQuery();
                        }

                    /////21.01.2020
                    }
                    /////21.01.2020

                            /////4
                            sud1 = "";
                    if (d4t != null)
                        foreach (DataRow dr in d4t.Tables[0].Rows)
                    {
                        ifl1 = 0;
                            if (d4pr != null)
                                foreach (DataRow dr1 in d4pr.Tables[0].Rows)
                        {

                            if (dr["id"].ToString() == dr1["id"].ToString())
                            {
                                ifl1 = 1;
                                break;
                            }

                        }

                        if (ifl1 == 0)
                            sud1 = sud1 + dr["id"] + ",";


                    }

                    i33 = sud1.Length;
                    if (i33 > 1)
                    {
                        sud1 = sud1.Substring(0,sud1.Length - 1);
                        com1 = new SqlCommand("delete from t_g_account  where  id in (" + sud1.ToString() + ")", connection);
                        com1.Transaction = transaction1;
                        com1.ExecuteNonQuery();
                    }

                    /////3
                    sud1 = "";

                    if (d3t != null)
                        foreach (DataRow dr in d3t.Tables[0].Rows)
                    {
                        ifl1 = 0;

                            if (d3pr != null)
                                foreach (DataRow dr1 in d3pr.Tables[0].Rows)
                        {

                            if (dr["id"].ToString() == dr1["id"].ToString())
                            {
                                ifl1 = 1;
                                break;
                            }

                        }

                       
                            if (ifl1 == 0)
                            sud1 = sud1 + dr["id"] + ",";



                    }

                    i33 = sud1.Length;
                    if (i33 > 1)
                    {
                        sud1 = sud1.Substring(0,sud1.Length - 1);
                        com1 = new SqlCommand("delete from t_g_encashpoint  where  id in (" + sud1.ToString() + ")", connection);
                        com1.Transaction = transaction1;
                        com1.ExecuteNonQuery();
                    }

                    /////2
                    sud1 = "";

                    if (d2t != null)
                        foreach (DataRow dr in d2t.Tables[0].Rows)
                    {
                        ifl1 = 0;

                            if (d2pr != null)
                                foreach (DataRow dr1 in d2pr.Tables[0].Rows)
                        {

                            if (dr["id"].ToString() == dr1["id"].ToString())
                            {
                                ifl1 = 1;
                                break;
                            }

                        }

                        if (ifl1 == 0)
                            sud1 = sud1 + dr["id"] + ",";


                    }

                    i33 = sud1.Length;
                    if (i33 > 1)
                    {
                        sud1 = sud1.Substring(0,sud1.Length - 1);
                        com1 = new SqlCommand("delete from t_g_clienttocc  where  id in (" + sud1.ToString() + ")", connection);
                        com1.Transaction = transaction1;
                        com1.ExecuteNonQuery();
                    }
                }
                    /////20.01.2020


                    //  System.Windows.Forms.MessageBox.Show("ID " + icl1.ToString());
                    transaction1.Commit();
            }


            /*
            SqlTransaction transaction1 = null;

            int i1 = 0;
            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                try
                {

                    transaction1 = connection.BeginTransaction();
                    SqlCommand com;
                    foreach (var dataSetTables in dataSets)
                    {


                        if (i1 == 0)
                        {
                            String sqlSelect = "SELECT TOP 0 * FROM t_g_client";
                            sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                            sqlCommand = new SqlCommandBuilder(sqlDataAdapter);



                            sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                            sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                            sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);
                            sqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;




                            sqlDataAdapter.InsertCommand.Transaction = transaction1;
                            sqlDataAdapter.UpdateCommand.Transaction = transaction1;
                            sqlDataAdapter.DeleteCommand.Transaction = transaction1;



                            sqlDataAdapter.Update(dataSetTables);
                            dataSetTables.AcceptChanges();
                        }

                       



                    }

                    i1 = i1 + 1;


                    transaction1.Commit();

                }
                catch (Exception ex)
                {
                    transaction1.Rollback();

                    System.Windows.Forms.MessageBox.Show("Невозможно выполнить операцию: " + ex.Message);



                }
            }
            */

            /*
            int i1 = 0;

            try
            {
             //   SqlTransaction transaction = connection.BeginTransaction();
                foreach (var dataSetTables in dataSets)
                {
                    if (i1 == 0)
                    {
                        String sqlSelect = "SELECT TOP 0 * FROM t_g_client";
                        sqlDataAdapter = new SqlDataAdapter(sqlSelect, connection);
                        sqlCommand = new SqlCommandBuilder(sqlDataAdapter);





                        sqlDataAdapter.InsertCommand = sqlCommand.GetInsertCommand(true);
                        sqlDataAdapter.UpdateCommand = sqlCommand.GetUpdateCommand(true);
                        sqlDataAdapter.DeleteCommand = sqlCommand.GetDeleteCommand(true);

                        if (transaction != null)
                        {
                            sqlCommand.GetInsertCommand(true).Transaction = transaction;
                            sqlCommand.GetUpdateCommand(true).Transaction = transaction;
                            sqlCommand.GetDeleteCommand(true).Transaction = transaction;
                        }
                        sqlDataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(SqlAdapterRowUpdated);
                        if ((connection != null) && (connection.State == ConnectionState.Open))
                        {


                            sqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;



                                sqlDataAdapter.Update(dataSetTables);
                                dataSetTables.AcceptChanges();




                        }
                    }
                    i1 = i1 + 1;
                }
              //  transaction.Commit();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Невозможно выполнить операцию: " + ex.Message);
        //        transaction.Rollback();


            }
            */

            /*
            List<SqlDataAdapter> sqlDataAdapters = new List<SqlDataAdapter>();


            foreach (var dataSetTables in dataSets.Zip(tableNames, (x, y) => new { dataset = x, tablename = y }))
            {
                String sqlSelect = "SELECT TOP 0 * FROM " + dataSetTables.tablename;
                sqlDataAdapters.Add(new SqlDataAdapter(sqlSelect, connection));
                sqlCommand = new SqlCommandBuilder(sqlDataAdapters[sqlDataAdapters.Count - 1]);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand = sqlCommand.GetInsertCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].UpdateCommand = sqlCommand.GetUpdateCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].DeleteCommand = sqlCommand.GetDeleteCommand(true);
                sqlDataAdapters[sqlDataAdapters.Count - 1].InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                   }

            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (var sqlAdapter in sqlDataAdapters.Zip(dataSets, (x, y) => new { adapter = x, dataset = y }))
                    {
                        sqlAdapter.adapter.InsertCommand.Transaction = transaction;
                        //sqlAdapter.adapter.InsertCommand.Transaction.
                        sqlAdapter.adapter.UpdateCommand.Transaction = transaction;
                        sqlAdapter.adapter.DeleteCommand.Transaction = transaction;

                        sqlAdapter.adapter.Update(sqlAdapter.dataset);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    eventLogger.WriteError("UpdateDataTransaction : " + ex.Message);
                    transaction.Rollback();
                }
            }
            */

            /*
             * 
             Int32 result = 1;

             if ((connection != null) && (connection.State == ConnectionState.Open))
             {

            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand com;
            int i1 = 0, id_bag = -1, id_coun = -1;





                 try
                {


                foreach (string s in zapros)
                {


                    string s1 = String.Format(s, id_bag, id_coun);
                    com = new SqlCommand(s1, connection);
                    com.Transaction = transaction;

                        if (k1 == 1)
                        {
                            com.ExecuteNonQuery();
                        }
                        else
                        {

                            if ((i1 == 0) | (i1 == 1))
                            {

                                if (i1 == 0)
                                    id_bag = Convert.ToInt32(com.ExecuteScalar());
                                if (i1 == 1)
                                    id_coun = Convert.ToInt32(com.ExecuteScalar());


                            }

                            else
                            {
                                //       int i22 = Convert.ToInt32("gfhf");
                                com.ExecuteNonQuery();
                            }

                        }
                    i1 = i1 + 1; 
                }




                    transaction.Commit();


              }
                     catch (Exception ex)
                      {
                    ///31.10.2019
                    eventLogger.WriteError("UpdateDataTransaction : " + ex.Message);
                    ///31.10.2019
                    transaction.Rollback();
                    result = 0;
                      }



            }
            return result;



            */


        }
        /////16.01.2020

        public void InsertTransactionCountingContent(long account_id, long counting_id, long fact_value, long currency_id, long user_id, string workstation)
        {
            command.CommandText = "INSERT INTO t_g_counting_content " +
                "(id_account, id_counting, id_bag, declared_value, fact_value, id_currency, creation, lastupdate, last_user_update, workstation) " +
                //"WORKSTATIONID = :workstation, " +
                "VALUES " +
                //"SEQNUMBER = :seq_number_inc, " +

                "(@account_id, @counting_id, NULL, 0, @fact_value, @currency_id, @dt_update, @dt_update, @user_id, @workstation) ";
            //"AND SEQNUMBER = :seq_number ";

            //command.BindByName = true;
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("@account_id", account_id));
            command.Parameters.Add(new SqlParameter("@counting_id", counting_id));
            
            //command.Parameters.Add(new OracleParameter("workstation", workstation));
            
            //command.Parameters.Add(new OracleParameter("seq_number_inc", seqNumber_inc));
            command.Parameters.Add(new SqlParameter("@fact_value", fact_value));
            
            command.Parameters.Add(new SqlParameter("@currency_id", currency_id));
            command.Parameters.Add(new SqlParameter("@dt_update", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@user_id", user_id));
            command.Parameters.Add(new SqlParameter("@workstation", workstation));

            //command.Parameters.Add(new OracleParameter("seq_number", seqNumber));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception oex)
            {
                eventLogger.WriteError("UpdateTransactionCountingContent : " + command.CommandText);
                eventLogger.WriteError("UpdateTransactionCountingContent : " + oex.Message);

                transaction.Rollback();
                throw;
            }
        }

    }
}



