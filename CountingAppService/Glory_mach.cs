﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using CountingDB;

using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.Xml;

using System.Threading;

using System.Collections;
using System.Configuration;

namespace CountingAppService
{
   public class Glory_mach
    { 
    Mutex mutex = new Mutex();
    private EventLogger.EventLogger eventLogger = new EventLogger.EventLogger();
    private MSDataBase dataBase;
    private DataSet denomDataSet = new DataSet();
    private string codeCPS = String.Empty;
    private string codeCashCentre = String.Empty;
    private string linkCashCentre = String.Empty;
    private Stream cardStream;
    private long idCashCentre = -1;

    private String fileName = String.Empty;
    private String defaultPath;// = @"C:\образцы файлов с uw-f\UWFFiles";
    private String readyPath;// = @"C:\образцы файлов с uw-f\Ready";
    private String errorPath;// = @"C:\образцы файлов с uw-f\Error";
    private String UnProcessedPath;// = @"C:\образцы файлов с uw-f\UnProcessed";

    FileSystemWatcher watcher;
    string directoryToWatch;

    public void FileProcesser()
    {



        this.watcher = new FileSystemWatcher();
        this.directoryToWatch = defaultPath;
        try
        {

                /////06.11.2019
                /*
                #region Считывание номиналов из файла XML 
                denomDataSet.Clear();
                Stream denomStream = File.Open(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Denomination.xml"), FileMode.Open, FileAccess.Read);
                if (denomStream != null)
                {
                    denomDataSet.ReadXml(denomStream);
                  //  eventLogger.WriteInfo("Данные по номиналам считаны из файла");
                }
                denomStream.Close();

                    #endregion
                    */
                /////06.11.2019


                /*
                //Считываем из реестра данные о кассовом центре, в котором находимся
                using (RegistryKey key = Registry.CurrentConfig.OpenSubKey(@"Software\Maestro"))
                {
                    if (key != null)
                    {
                        Object code = key.GetValue("CASH_CENTRE_CODE");
                        if (code != null)
                        {
                            codeCashCentre = code.ToString();
                        }
                    }
                }
                */
                //Считывание данных из файла конфигурации 
                codeCashCentre = ConfigurationManager.AppSettings["CASH_CENTRE_CODE"];
            codeCPS = ConfigurationManager.AppSettings["CPS_CODE"];
            defaultPath = ConfigurationManager.AppSettings["defaultPath"];
            errorPath = ConfigurationManager.AppSettings["errorPath"];
            readyPath = ConfigurationManager.AppSettings["readyPath"];
            UnProcessedPath = ConfigurationManager.AppSettings["UnProcessedPath"];
            //dbName = System.Configuration.ConfigurationManager.AppSettings["DBName"];
            idCashCentre = Convert.ToInt64(codeCashCentre);
        }
        catch (Exception ex)
        {
            //Записываем ошибку в журнал
            eventLogger.WriteError("Не удалось считать данные по кассовому центру : " + ex.Message);
        }
        //Создаем переменную для работы с БД
        dataBase = new MSDataBase();

    }

    public void Watch()
    {

            /////


            /////////03.12.2019

           // defaultPath = @"\\10.10.102.223\UWFFiles\In";

            /////////03.12.2019


            ////14.11.2019
            if (!Directory.Exists(defaultPath))
            {
                //Создать такую директорию
                Directory.CreateDirectory(defaultPath);
            }
            ////14.11.2019

            //Запускаем отслеживание изменений в каталоге где сохраняются данные из GLORIA
            watcher.Path = defaultPath;
        watcher.InternalBufferSize = 65536;
        watcher.IncludeSubdirectories = false;
        watcher.Filter = "*.*";
        watcher.NotifyFilter = NotifyFilters.LastWrite;
        watcher.Changed += new FileSystemEventHandler(OnChanged);

            ////20.12.2019
          //  watcher.EnableRaisingEvents = true;
            //   watcher.Created += new FileSystemEventHandler(OnChanged);

            ////20.12.2019

            watcher.Error += new ErrorEventHandler(OnErrorWatcher);
        watcher.EnableRaisingEvents = true;

    }

    private void OnErrorWatcher(object sender, ErrorEventArgs e)
    {
        Watch();
    }

        ////20.12.2019
        public void obrab()
        {
            Thread fileParserThread = new Thread(obrabfile1);
            fileParserThread.Start();
        }

        private void obrabfile1()
        {

         //   mutex.WaitOne();
            //if (!IsFileReady(e.FullPath))
            //{

        //    Thread.Sleep(2000);

            //  DirectoryInfo dir = new DirectoryInfo(defaultPath);

            // var allfiles = DirectoryInfo.GetFiles(defaultPath);
            //   foreach (var e in allfiles)
            //  foreach (var item in dir.GetFiles())
            //   foreach ( e in dir.GetFiles())
            string[] allfiles = Directory.GetFiles(defaultPath);

            //////13.01.2020
            int flosh1 = 0;
            //////13.01.2020

            foreach (string e  in allfiles)
            {

                //////13.01.2020
                flosh1 = 0;
                //////13.01.2020

                //   Console.WriteLine(e);

                //////////////////////////////////


                ////07.11.2019

                ///10.01.2020
                try
                ///10.01.2020

                {
                    ////07.11.2019

                    //////13.12.2019
                    List<string> spisnom1;
                    List<string> spissost1;
                    //////13.12.2019

                    //Если файл уже обрабатывался - не принимать его (особенность компонента, обработчик срабатывает несколько раз)
                    //int seqInc = 1;

                    ////20.12.2019
                    // if (fileName != e.FullPath)
                    // if (fileName != e.FullName)
                    if (fileName != e)
                    ////20.12.2019
                    {
                        //Инициализация переменной для считывания и разбора XML данных
                        DataSet dataSetXML = new DataSet();
                        //Соединяемся с БД
                        dataBase.Connect();
                        DataSet dsCashCentre = dataBase.GetData("t_g_cashcentre", "id", idCashCentre.ToString());

                        //////05.11.2019
                        DataSet d2 = dataBase.GetData("t_g_currency");
                        DataSet d3 = dataBase.GetData("t_g_denomination");
                        DataSet d4 = dataBase.GetData("t_g_condition");
                        //////05.11.2019

                        //Считывание файла для чтения
                        ////20.12.2019
                        // cardStream = File.Open(e.FullPath, FileMode.Open, FileAccess.Read);
                        // cardStream = File.Open(e.FullName, FileMode.Open, FileAccess.Read);
                        cardStream = File.Open(e, FileMode.Open, FileAccess.Read);
                        ////20.12.2019
                        if (cardStream != null)
                        {

                            //////13.12.2019
                            spisnom1 = new List<string>();
                            spissost1 = new List<string>();
                            //////13.12.2019

                            //Запоминание имени файла для предотвращения повторного считывания
                            ////20.12.2019
                            // fileName = e.FullPath;
                            // fileName = e.FullName;
                            fileName = e;
                            ////20.12.2019

                            //Переложение файла по таблицам в набор данных 
                            dataSetXML.ReadXml(cardStream);
                            cardStream.Close();
                            //Считывание имени машины из файла
                            string userName = dataSetXML.Tables["Machine"].Rows[0]["Site"].ToString();
                            //Считывание данных о пользователе из БД
                            DataSet userDataSet = dataBase.GetData("t_g_user", "code", userName);
                            DataTable currency = new DataTable();
                            //Считывание пользователей для кассового центра
                            DataSet UsersInCashCentre = dataBase.GetData("t_g_user", "id_brach", idCashCentre.ToString());

                            DataTable countingContent;
                            //Cчитывание счетчика из БД
                            //DataTable sequenceLINK_SEQ = dataBase.GetSequence("LINK_SEQ");
                            string linkUser;
                            //Текущее состояние счетчика
                            //string currentSeq = sequenceLINK_SEQ.Rows[0]["CURRENTVAL"].ToString();

                            //if (userDataSet.Rows.Count > 0)
                            //{

                            //LINK на пользователя в БД
                            linkUser = userDataSet.Tables[0].Rows[0]["code"].ToString();
                            //}



                            //Если кассовый центр есть в БД
                            if (UsersInCashCentre.Tables[0].Rows.Count > 0)
                            {
                                //Если пользователь есть в данном кассовом центре
                                if (UsersInCashCentre.Tables[0].AsEnumerable().Where(x => x.Field<string>("code").Trim() == userName).Count() > 0)
                                {
                                    string linkCountingConfig = String.Empty;
                                    //Имя рабочей станции
                                    string workStationName = Environment.MachineName;

                                    //Перебираем в XML-файле все карточки, который в ней есть
                                    foreach (DataRow headersCard in dataSetXML.Tables["HeaderCardTransaction"].Rows)
                                    {
                                        string currencyNote = String.Empty;
                                        string aggregateLink = String.Empty;

                                        string countingLink = string.Empty;

                                        //Считаем сумму по данному пересчету
                                        Int64 sumValues = dataSetXML.Tables["Counter"].AsEnumerable().Where(x => x.Field<int>("HeaderCardTransaction_Id") == Convert.ToInt32(headersCard["HeaderCardTransaction_Id"])).Sum(x => Convert.ToInt64(x.Field<string>("Value")) * Convert.ToInt64(x.Field<string>("Number")));

                                        //Считаем количество банкнот по данному пересчету
                                        Int64 countNotes = dataSetXML.Tables["Counter"].AsEnumerable().Where(x => x.Field<int>("HeaderCardTransaction_Id") == Convert.ToInt32(headersCard["HeaderCardTransaction_Id"])).Sum(x => Convert.ToInt64(x.Field<string>("Number")));

                                        //string linkCurrency;
                                        //Считываем данные по карточки из БД
                                        DataTable countingSep = dataBase.GetData("t_g_cards", "name", headersCard["DepositID"].ToString()).Tables[0];//DepositID//HeaderCardID

                                        DataTable counting = new DataTable();
                                        DataTable denomList = new DataTable();
                                        //Если карточка есть в БД
                                        if (countingSep.Rows.Count > 0)
                                        {
                                            //Считываем подготовленные данные из БД для данного пересчета
                                            counting = dataBase.GetData("t_g_counting", "id", countingSep.Rows[0]["id_counting"].ToString()).Tables[0];


                                            //Если данные по пересчету есть в БД
                                            if (counting.Rows.Count > 0)
                                            {
                                                //long lCurrency = Convert.ToInt64(counting.Rows[0]["id"]);
                                                countingContent = dataBase.GetData("t_g_counting_content", "id_counting", counting.Rows[0]["id"].ToString()).Tables[0];
                                                //currencyNote = dataBase.getCurrCode(lCurrency);
                                                //LINK для правила пересчета 
                                                //linkCountingConfig = counting.Rows[0]["LRULE"].ToString();
                                                //Считываем данные по правилу пересчета
                                                //DataTable countingConfig = dataBase.GetCountingConfig(linkCountingConfig);
                                                //Считываем LINK на валюту для пересчета
                                                //linkCurrency = counting.Rows[0]["LCURRENCY"].ToString();
                                                //Считываем список номиналов для валюты из правил пересчета
                                                denomList = dataBase.GetData("t_g_denomination", "id_currency", countingContent.AsEnumerable().Select(x => x.Field<Int64>("id_currency").ToString()).ToList<string>()).Tables[0];//GetDenomList(linkCountingConfig, linkCurrency);
                                                                                                                                                                                                                                 //Считываем валюту из списка валют в БД
                                                                                                                                                                                                                                 //currency = dataBase.GetCurrency(linkCurrency);


                                                //dataBase.CountingUpdate();
                                                //dataBase.UpdateCounting();

                                                //Формируем текущий номер для счетчика
                                                //int currentSequence = Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["CURRENTVAL"]) + Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["INCSIZE"]) > Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["MAXVAL"]) ? Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["SEEDVAL"]) : Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["CURRENTVAL"]) + Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["INCSIZE"]);

                                                //Обновляем счетчик
                                                //dataBase.UpdateSequence("LINK_SEQ", currentSequence);
                                                //Начинаем формировать транзакцию
                                                dataBase.InitTransaction();

                                                //Если в пересчете есть ссылка на агрегат
                                                
                                                //Считывание имени пересчета
                                                string countingName = counting.Rows[0]["NAME"].ToString();
                                                long counting_id = Convert.ToInt64(counting.Rows[0]["id"]);
                                                //Считывание количества изменений по пересчету
                                                ///?????int seqNumber = Convert.ToInt32(counting.Rows[0]["SEQNUMBER"]);
                                                //Считывание даты и времени последних изменнений
                                                DateTime lastUpdate = Convert.ToDateTime(counting.Rows[0]["LASTUPDATE"]);
                                                long user_id = Convert.ToInt64(userDataSet.Tables[0].Rows[0]["id"]);
                                                //Int64 sumCountValue = Convert.ToInt64(counting.Rows[0]["SUMNOTETRUE"]) + sumValues;

                                                //Int64 countCountValue = Convert.ToInt64(counting.Rows[0]["COUNTNOTETRUE"]) + countNotes;

                                                //DateTime createTime = Convert.ToDateTime(counting.Rows[0]["CREATION"]);
                                                //Обновление данных по пересчету
                                                dataBase.CountingUpdateTransaction(countingName, lastUpdate, user_id, counting_id);

                                                ///04.11.2019
                                                //dataBase.DenomCntDeleteTransaction(counting_id);

                                                ////27.11.2019
                                                //     dataBase.DenomCntDeleteTransaction(Convert.ToInt64(countingSep.Rows[0]["id"]));
                                                ////27.11.2019

                                                ///04.11.2019


                                                //(countingName,  lastUpdate, workStationName, linkUser, countCountValue, sumCountValue, createTime);

                                                //string countingSepLink = countingSep.Rows[0]["LINK"].ToString();
                                                //int countingSepSeq = Convert.ToInt32(countingSep.Rows[0]["SEQNUMBER"]);
                                                //DateTime countSepLastUpdate = Convert.ToDateTime(countingSep.Rows[0]["LASTUPDATE"]);
                                                //dataBase.CountingSepUpdateTransaction(countingSepLink, workStationName, linkUser, countingSepSeq, countSepLastUpdate);
                                            }
                                        }
                                        else //Если карточки нет в БД
                                        {
                                            //Формируем следующий номер в счетчике
                                            //int currentSequence = Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["CURRENTVAL"]) + Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["INCSIZE"]) > Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["MAXVAL"]) ? Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["SEEDVAL"]) : Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["CURRENTVAL"]) + Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["INCSIZE"]);

                                            //Обновление счетчика
                                            //dataBase.UpdateSequence("LINK_SEQ", currentSequence);
                                            //Начинаем формировать транзакцию
                                            dataBase.InitTransaction();
                                            //Формирование LINK для пересчета
                                            //countingLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                                            //seqInc++;
                                            //Формирование LINK для КР
                                            //string countingSepLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                                            //seqInc++;
                                            //Формирование имени пересчета
                                            string countingName = headersCard["DepositID"].ToString() + "_" + userDataSet.Tables[0].Rows[0]["CODE"].ToString().Trim() + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmssff", CultureInfo.InvariantCulture);

                                            string currcode = dataSetXML.Tables["Counter"].AsEnumerable().Where(x => x.Field<int>("HeaderCardTransaction_Id") == Convert.ToInt32(headersCard["HeaderCardTransaction_Id"])).Select(x => x.Field<string>("Currency")).First();
                                            //LINK правил пересчета
                                            //string ruleLink = "0225999777_2010-09-18_11:11:22"; //?????

                                            //LINK клиента
                                            //string clientLink = "0099999908_2010-09-16_15:01:17";//?????

                                            //LINK валюты пересчета
                                            //linkCurrency = dataBase.GetCurrencyByCurrCode(currcode);
                                            //long 
                                            long user_id = Convert.ToInt64(userDataSet.Tables[0].Rows[0]["id"]);
                                            long client_id = Convert.ToInt64(codeCPS);
                                            //"0057999945_2010-09-16_11:40:10"; // 5401994601_2017-02-03_16:17:54
                                            //Добавление в БД своего пересчета
                                            dataBase.CountingInsertTransaction(client_id, user_id, countingName);
                                            counting = dataBase.GetCounting(countingName);
                                            string cardName = headersCard["DepositID"].ToString();
                                            long counting_id = Convert.ToInt64(counting.Rows[0]["id"]);
                                            dataBase.CardInsertTransaction(counting_id, cardName, user_id);
                                            countingSep = dataBase.GetCountingSep(cardName, counting_id);
                                            //(countingLink, DateTime.Now, workStationName, linkUser, countingName, sumValues, countNotes, linkCashCentre, linkCurrency, clientLink, ruleLink);
                                            //Добавление в БД своего разделителя 
                                            //dataBase.CountingSepInsertTransaction(countingSepLink, workStationName, linkUser, headersCard["DepositID"].ToString(), countingLink, linkCashCentre, userDataSet.Rows[0]["NAME"].ToString());
                                        }




                                        Dictionary<string, long> counts = new Dictionary<string, long>();
                                        Dictionary<string, long> values = new Dictionary<string, long>();


                                        //Перебор всех номиналов в пересчете
                                        foreach (DataRow counter in dataSetXML.Tables["Counter"].AsEnumerable().Where(x => x.Field<int>("HeaderCardTransaction_Id") == Convert.ToInt32(headersCard["HeaderCardTransaction_Id"])))
                                        {
                                            if (counts.ContainsKey(counter["Currency"].ToString()))
                                            {
                                                counts[counter["Currency"].ToString()] += Convert.ToInt64(counter["Number"]);
                                            }
                                            else
                                            {
                                                counts.Add(counter["Currency"].ToString(), Convert.ToInt64(counter["Number"]));
                                            }

                                            if (values.ContainsKey(counter["Currency"].ToString()))
                                            {
                                                /////04.11.2019
                                                values[counter["Currency"].ToString()] += Convert.ToInt64(counter["Value"]) * Convert.ToInt64(counter["Number"]);
                                                ////values[counter["Currency"].ToString()] += Convert.ToInt64(counter["Value"]);
                                                ////////04.11.2019
                                            }
                                            else
                                            {
                                                /////04.11.2019
                                                values.Add(counter["Currency"].ToString(), Convert.ToInt64(counter["Value"]) * Convert.ToInt64(counter["Number"]));
                                                //values.Add(counter["Currency"].ToString(), Convert.ToInt64(counter["Value"]));
                                                /////04.11.2019
                                            }
                                            //values[counter["Currency"].ToString()] += Convert.ToInt64(counter["Value"]);

                                            //Сопоставление номинала с LINK в БД из файла 
                                            //string linkDenom = denomDataSet.Tables["Denom"].AsEnumerable().Where(x => x.Field<string>("ValueId") == counter["DenomID"].ToString()).Select(x => x.Field<string>("Link")).First<string>();


                                            ///////05.11.2019

                                            //  long idDenom = Convert.ToInt64(denomDataSet.Tables["Denom"].AsEnumerable().Where(x => x.Field<string>("ValueId") == counter["DenomID"].ToString()).Select(x => x.Field<string>("Link")).First<string>());

                                            string selectString = "curr_code= '" + counter["Currency"].ToString() + "'";
                                            DataRow[] searchedRows = ((DataTable)d2.Tables[0]).Select(selectString);
                                            DataRow searchedRows1 = searchedRows[0];
                                            string k1 = searchedRows1[0].ToString();



                                            selectString = "id_currency='" + k1.ToString() + "' and value= '" + counter["Value"].ToString() + "'";
                                            searchedRows = ((DataTable)d3.Tables[0]).Select(selectString);
                                            searchedRows1 = searchedRows[0];
                                            string k2 = searchedRows1[0].ToString();
                                            long idDenom = Convert.ToInt64(k2);


                                            

                                            //  selectString = "upper(code1)='" + counter["Quality"].ToString().ToUpper() + "'";
                                            selectString = "code1='" + counter["Quality"].ToString() + "'";
                                            searchedRows = ((DataTable)d4.Tables[0]).Select(selectString);
                                            searchedRows1 = searchedRows[0];
                                            string k3 = searchedRows1[0].ToString();
                                            long idCondition = Convert.ToInt64(k3);

                                            //     long idCondition = Convert.ToInt64(denomDataSet.Tables["Denom"].AsEnumerable().Where(x => x.Field<string>("ValueId") == counter["DenomID"].ToString()).Select(x => x.Field<string>("Condition")).First<string>());

                                            ///////05.11.2019



                                            //Запрос в базе на существование данного номинала????

                                            DataRow denomination;
                                            if (!String.IsNullOrEmpty(linkCountingConfig) && denomList.Rows.Count > 0)
                                            {
                                                if (denomList.AsEnumerable().Where(x => x.Field<Int64>("LINK") == idDenom).Count() > 0)
                                                {
                                                    denomination = denomList.AsEnumerable().Where(x => x.Field<Int64>("LINK") == idDenom).FirstOrDefault<DataRow>();
                                                }
                                                else
                                                {
                                                    dataBase.TransactionRollback();

                                                    throw new Exception("Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " " + counter["Quality"] + " не учавствует в данном бизнес правиле");
                                                    /*
                                                    ////13.01.2020
                                                    //throw new Exception("Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " " + counter["Quality"] + " не учавствует в данном бизнес правиле");
                                                    eventLogger.WriteError("Файл не обработан: " + e + ". " + "Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " " + counter["Quality"] + " не учавствует в данном бизнес правиле");
                                                    flosh1 = 1;
                                                    
                                                    goto a1;
                                                    ////13.01.2020
                                                    */
                                                }

                                            }
                                            else
                                            {
                                                denomination = dataBase.GetDenomination(idDenom).AsEnumerable().FirstOrDefault<DataRow>();
                                            }

                                            //Если номинал не соответствует указанному номиналу в файле
                                            if (Convert.ToInt32(counter["Value"]) != Convert.ToInt32(denomination["VALUE"]))
                                            {
                                                dataBase.TransactionRollback();

                                                throw new Exception("Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " не соответствует номиналу " + denomination["NAME"].ToString() + " с линком " + denomination["LINK"].ToString());

                                                /*
                                                ////13.01.2020
                                                //  throw new Exception("Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " не соответствует номиналу " + denomination["NAME"].ToString() + " с линком " + denomination["LINK"].ToString());
                                                eventLogger.WriteError("Файл не обработан: " + e + ". " + "Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " не соответствует номиналу " + denomination["NAME"].ToString() + " с линком " + denomination["LINK"].ToString());
                                                flosh1 = 1;
                                                goto a1;
                                                ////13.01.2020
                                                */

                                            }
                                            //Номер карточки
                                            string card = headersCard["DepositID"].ToString();
                                            //Идентификатор номинала в файле
                                            int denom = Convert.ToInt32(counter["DenomID"]);

                                            string linkCounting = String.Empty;

                                            //Количество номинала в файле
                                            Int64 countDenom = Convert.ToInt64(counter["Number"]);
                                            //Расчет суммы для номинала
                                            Int64 sumValue = Convert.ToInt64(counter["Value"]) * countDenom;

                                            //Если пересчет есть в БД
                                            if (counting.Rows.Count > 0)
                                            {
                                                long id_counting = Convert.ToInt64(counting.Rows[0]["id"]);
                                            }
                                            else
                                            {

                                            }
                                           
                                            //LINK для пересчета
                                            //long idCounting = Convert.ToInt64(counting.Rows[0]["id"]);
                                            //Если есть ссылка на агрегат

                                           
                                           

                                            long idCounting = Convert.ToInt64(counting.Rows[0]["id"]);
                                            //Выбор номиналов для пересчета

                                            ////10.12.2019
                                            // DataTable DenomCnt = dataBase.GetDenomCnt(idCounting, idDenom, idCondition);
                                            DataTable DenomCnt = dataBase.GetDenomCnt1(idCounting, idDenom, idCondition, Convert.ToInt64(countingSep.Rows[0]["id"]));
                                            ////10.12.2019

                                            long user_id = Convert.ToInt64(userDataSet.Tables[0].Rows[0]["id"]);
                                            long card_id = Convert.ToInt64(countingSep.Rows[0]["id"]);
                                            //Если номиналы в БД есть
                                            if (DenomCnt.Rows.Count > 0)
                                            {
                                                //LINK для номинала
                                                //string DenomCntLink = DenomCnt.Rows[0]["LINK"].ToString();
                                                long idDenomCnt = Convert.ToInt64(DenomCnt.Rows[0]["id"]);

                                                //Считывание номера изменений в номинале
                                                //int seqNumber = Convert.ToInt32(DenomCnt.Rows[0]["SEQNUMBER"]);
                                                //Расчет количества номинала в пересчете

                                                
                                                countDenom += Convert.ToInt64(DenomCnt.Rows[0]["count"]);
                                                //Расчет суммы номинала в пересчете
                                                sumValue += Convert.ToInt64(DenomCnt.Rows[0]["fact_value"]);

                                                //Обновление данных о номинале о пересчете

                                                //////13.12.2019
                                                string srt1 = "0";

                                                if (spisnom1.Find((x) => x == idDenom.ToString()) != idDenom.ToString())
                                                {
                                                    srt1 = "1";


                                                    spisnom1.Add(idDenom.ToString());
                                                    spissost1.Add(idCondition.ToString());

                                                }
                                                else
                                                {

                                                    int m1 = 0;
                                                    int j1 = 0;

                                                    foreach (string p1 in spisnom1)
                                                    {
                                                        if (p1 == idDenom.ToString())
                                                        {
                                                            if (spissost1[j1] == idCondition.ToString())
                                                            {
                                                                m1 = 1;
                                                                break;

                                                            }

                                                        }
                                                        j1 = j1 + 1;
                                                    }

                                                    if (m1 == 0)
                                                    {

                                                        srt1 = "1";
                                                        spisnom1.Add(idDenom.ToString());
                                                        spissost1.Add(idCondition.ToString());

                                                    }

                                                }
                                                //////13.12.2019

                                                /////06.11.2019
                                                //dataBase.DenomCntUpdateTransaction(user_id, idCounting, idDenom, idCondition, countDenom, sumValue);

                                                //////13.12.2019
                                                //    dataBase.DenomCntUpdateTransaction(user_id, idCounting, idDenom, idCondition, countDenom, sumValue, userName.ToString(), dataSetXML.Tables["Operator"].Rows[0]["Name"].ToString());
                                                dataBase.DenomCntUpdateTransaction(user_id, idCounting, idDenom, idCondition, countDenom, sumValue, userName.ToString(), dataSetXML.Tables["Operator"].Rows[0]["Name"].ToString(), srt1);
                                                //////13.12.2019

                                                /////06.11.2019



                                                //(workStationName, seqNumber, linkUser, linkCounting, linkDenom, countDenom, sumValue, nbMachineCount);
                                            }
                                            else //Если данных о номинале нет в БД
                                            {
                                                //Формирование LINK для номинала
                                                //string denomCntLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                                                //seqInc++;
                                                //Добавление в БД номинала
                                                //long idCondition = Convert.ToInt64(denomList);

                                                /////06.11.2019
                                                //        dataBase.DenomCntInsertTransaction(idCounting, card_id, idDenom, idCondition, user_id, countDenom, sumValue);

                                                //string userName = dataSetXML.Tables["Machine"].Rows[0]["Site"].ToString();

                                                //////13.12.2019
                                                spisnom1.Add(idDenom.ToString());
                                                spissost1.Add(idCondition.ToString());
                                                //////13.12.2019

                                                dataBase.DenomCntInsertTransaction(idCounting, card_id, idDenom, idCondition, user_id, countDenom, sumValue, userName.ToString(), dataSetXML.Tables["Operator"].Rows[0]["Name"].ToString());

                                                /////06.11.2019
                                            }
                                            #region Под вопросом


                                            //Считывание использования данного номинала
                                            int inventory = 1;//dataBase.GetInventoryDenom(linkDenom);
                                                              //Если исползуется
                                            if (inventory == 1)
                                            {
                                                //Считывание данных номиналов по пользователям
                                                DataTable cash = dataBase.GetCash(user_id, idDenom, idCashCentre);

                                                //Если есть данные по данному номиналу
                                                if (cash.Rows.Count > 0)
                                                {
                                                    //Считывыние LINK 
                                                    long cash_id = Convert.ToInt64(cash.Rows[0]["id"]);
                                                    //Считывание номера изменений

                                                    /////10.01.2020

                                                    //  int seqNumber = Convert.ToInt32(cash.Rows[0]["SEQNUMBER"]);
                                                    //Расчет еоличества номиналов
                                                    //   long countCash = Convert.ToInt64(cash.Rows[0]["COUNT"]) + Convert.ToInt64(counter["Number"]);

                                                    int seqNumber = 0;                                                    
                                                    if (cash.Rows[0]["SEQNUMBER"]!=null)
                                                        seqNumber =Convert.ToInt32(cash.Rows[0]["SEQNUMBER"]);

                                                    //Расчет еоличества номиналов
                                                    long countCash = Convert.ToInt64(counter["Number"]);
                                                    if (cash.Rows[0]["COUNT"]!=null)
                                                        countCash= countCash+ Convert.ToInt64(cash.Rows[0]["COUNT"]) ;


                                                    /////10.01.2020

                                                    //Дата и время последних изменений
                                                    DateTime cashLastUpdate = Convert.ToDateTime(cash.Rows[0]["LASTUPDATE"]);
                                                    //Обновление данных номинала по пользователю
                                                    dataBase.UpdateCashTransaction(cash_id, user_id, cashLastUpdate, countCash);
                                                }
                                                else //Если номинала по пользователю в БД 
                                                {
                                                    //Формирование LINK для номинала по пользователю
                                                    //string cashLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                                                    //seqInc++;
                                                    //Добавление данных номинала по пользователю
                                                    dataBase.InsertCashTransaction(user_id, idDenom, idCashCentre, Convert.ToInt32(counter["Number"]));
                                                }
                                            }

                                            #endregion
                                        }
                                        //countingContent = dataBase.GetCountingContent( ();
                                        foreach (var fact_value in values)
                                        {
                                            long idCurrency = dataBase.GetCurrencyByCode(fact_value.Key);
                                            if (idCurrency > 0)
                                            {
                                                long user_id = Convert.ToInt64(userDataSet.Tables[0].Rows[0]["id"]);
                                                long counting_id = Convert.ToInt64(counting.Rows[0]["id"]);
                                                countingContent = dataBase.GetCountingContent(counting_id);
                                                if (countingContent.Rows.Count > 0)
                                                {
                                                    if (countingContent.AsEnumerable().Where(x => x.Field<Int64>("id_currency") == idCurrency).Count() > 0)
                                                    {
                                                        long value = Convert.ToInt64(countingContent.AsEnumerable().FirstOrDefault(x => x.Field<Int64>("id_currency") == idCurrency).Field<Decimal>("fact_value"));
                                                    }
                                                    else
                                                    {
                                                        /////11.11.2019

                                                        DataSet accountsDataSet1 = dataBase.Poisksh("select top 1 * from t_g_account t1 where t1.id_client in (select t1.id from t_g_client t1 inner join t_g_account t2 on (t1.id = t2.id_client) inner join t_g_counting_content t3 on (t2.id = t3.id_account) where t3.id_counting = '" + counting_id.ToString() + "' ) and t1.id_currency = '" + idCurrency.ToString() + "'");
                                                        if (accountsDataSet1 != null)
                                                        {
                                                            if (accountsDataSet1.Tables[0] != null)
                                                            {
                                                                if (accountsDataSet1.Tables[0].Rows.Count > 0)
                                                                {
                                                                    long id_account = Convert.ToInt64(accountsDataSet1.Tables[0].Rows[0]["id"]);
                                                                    dataBase.InsertTransactionCountingContent(id_account, counting_id, fact_value.Value, idCurrency, user_id, workStationName);
                                                                }
                                                                else
                                                                {
                                                                    throw new Exception("Счета с данной валютой у клиента нет.");
                                                                    /*
                                                                    ////13.01.2020
                                                                    //  throw new Exception("Счета с данной валютой у клиента нет.");
                                                                    eventLogger.WriteError("Файл не обработан: " + e + ". " + "Счета с данной валютой у клиента нет.");
                                                                    flosh1 = 1;
                                                                    goto a1;
                                                                    ////13.01.2020
                                                                    */
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception("Счета с данной валютой у клиента нет.");
                                                                /*
                                                                ////13.01.2020
                                                                // throw new Exception("Счета с данной валютой у клиента нет.");
                                                                eventLogger.WriteError("Файл не обработан: " + e + ". " + "Счета с данной валютой у клиента нет.");
                                                                flosh1 = 1;
                                                                goto a1;
                                                                ////13.01.2020
                                                               */
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Счета с данной валютой у клиента нет.");
                                                            /*
                                                            ////13.01.2020
                                                            //  throw new Exception("Счета с данной валютой у клиента нет.");
                                                            eventLogger.WriteError("Файл не обработан: " + e + ". " + "Счета с данной валютой у клиента нет.");
                                                            flosh1 = 1;
                                                            goto a1;
                                                            ////13.01.2020
                                                            */
                                                        }

                                                        //throw new Exception("Счета с данной валютой у клиента нет.");
                                                        /////11.11.2019

                                                    }
                                                    //long value = countingContent.AsEnumerable().Where(x => x.Field<long>("id_currency") == idCurrency).Select(x => x.Field<long>("fact_value")).FirstOrDefault<Int64>() + fact_value.Value;

                                                    /////04.11.2019

                                                    dataBase.UpdateTransactionCountingContent(user_id, fact_value.Value, counting_id, idCurrency);
                                                    //    dataBase.UpdateTransactionCountingContent(user_id, fact_value.Value, counting_id, idCurrency);

                                                    /////04.11.2019
                                                }
                                                else
                                                {
                                                    long client_id = Convert.ToInt64(codeCPS);
                                                    DataTable accountsDataSet = dataBase.GetAccountsByClient(client_id);
                                                    long id_account = accountsDataSet.AsEnumerable().FirstOrDefault(x => x.Field<Int64>("id_currency") == idCurrency).Field<Int64>("id");
                                                    //long value = Convert.ToInt64(countingContent.AsEnumerable().FirstOrDefault(x => x.Field<Int64>("id_currency") == idCurrency).Field<Decimal>("fact_value"));
                                                    dataBase.InsertTransactionCountingContent(id_account, counting_id, fact_value.Value, idCurrency, user_id, workStationName);
                                                }
                                            }
                                        }
                                        //Завершение транзакции
                                        dataBase.TransactionCommit();
                                    }
                                }
                            }



                            //Закрытие потока для чтения файла
                            
                            //Если директория для нормально отработанных файлов не существует
                            try
                            {
                                if (!Directory.Exists(readyPath))
                                {
                                    //Создать такую директорию
                                    Directory.CreateDirectory(readyPath);
                                }

                                //Если данный файл уже существует

                                 if (File.Exists(Path.Combine(readyPath, Path.GetFileName(e))))
                                //if (File.Exists(Path.Combine(readyPath, e)))
                                ////20.12.2019
                                {
                                    //Удалить файл

                                    ////20.12.2019
                                    File.Delete(Path.Combine(readyPath, Path.GetFileName(e)));
                                    //File.Delete(Path.Combine(readyPath, e));
                                    ////20.12.2019
                                }

                                 File.Move(e, Path.Combine(readyPath, Path.GetFileName(e)));
                                // File.Move(e.FullName, Path.Combine(readyPath, e.Name));
                               // File.Move(e, Path.Combine(readyPath, e));

                                ////20.12.2019
                                
                                //Переместить туда файл

                                ////20.12.2019
                                ////20.12.2019

                                //Записать в журнал, что файл обработан
                                   eventLogger.WriteInfo("Файл обработан: " + e );

                            }
                            catch (Exception ex)
                            {
                                //   eventLogger.WriteError("Обработан : " + ex.Message);
                                eventLogger.WriteError("Файл не обработан: " + e +". "+ ex.Message);

                            }

                          //  break;

                        }
                    }


                    ////07.11.2019
                }
                ////07.11.2019
                
                ///10.01.2020
                catch (FileNotFoundException fex)
                {

                    //   eventLogger.WriteError(fex.Message);
                    eventLogger.WriteError("Файл не обработан: " + e + ". "+fex.Message);
                    //dataBase.TransactionRollback();
                    //    break;
                }
                catch (IOException ex) //Ошибка доступа к файлу
                {

                    //  eventLogger.WriteError(ex.Message);
                    eventLogger.WriteError("Файл не обработан: " + e + ". " + ex.Message);
                    //MessageBox.Show(ex.Message);
                    //dataBase.TransactionRollback();
                    continue;
                }
                catch (ArgumentException aex) //Ошибка в структуре элементов
                {


                    try
                    {
                        if (!Directory.Exists(errorPath))
                        {
                            Directory.CreateDirectory(errorPath);
                        }

                        ////20.12.2019
                         if (File.Exists(Path.Combine(errorPath, Path.GetFileName(e))))

                       // if (File.Exists(Path.Combine(errorPath, e)))
                        ////20.12.2019
                        {
                            ////20.12.2019
                            File.Delete(Path.Combine(errorPath, Path.GetFileName(e)));
                           // File.Delete(Path.Combine(errorPath, e));
                            ////20.12.2019
                        }
                        ////20.12.2019


                        ////20.12.2019
                        File.Move(e, Path.Combine(errorPath, Path.GetFileName(e)));
                        // File.Move(e.FullName, Path.Combine(errorPath, e.Name));
                        //File.Move(e, Path.Combine(errorPath, e));

                        

                            eventLogger.WriteError("Файл " + e + " перемещен в папку Error");

                    }
                    catch (Exception ex)
                    {
                        //  eventLogger.WriteError("ArgumentException : " + ex.Message);
                        eventLogger.WriteError("Файл не обработан: " + e + ". " + "ArgumentException : " + ex.Message);

                    }
                    //dataBase.TransactionRollback();
                      eventLogger.WriteError("Не найден нужный элемент XML: " + e + " : " + aex.Message);
                    //MessageBox.Show(e.Name + " : " + ex.Message, "Ошибка передачи данных GLORIA", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                  //  break;

                }
                catch (XmlException xmlex) //Файл не является XML
                {

                    try
                    {
                        if (!Directory.Exists(errorPath))
                        {
                            Directory.CreateDirectory(errorPath);
                        }

                        ////20.12.2019
                         if (File.Exists(Path.Combine(errorPath, Path.GetFileName(e))))
                        //if (File.Exists(Path.Combine(errorPath, e)))
                        ////20.12.2019
                        {
                            ////20.12.2019
                            File.Delete(Path.Combine(errorPath, Path.GetFileName(e)));
                           // File.Delete(Path.Combine(errorPath, e));
                            ////20.12.2019
                        }
                        ////20.12.2019

                        ////20.12.2019
                         File.Move(e, Path.Combine(errorPath, Path.GetFileName(e)));
                        // File.Move(e.FullName, Path.Combine(errorPath, e.Name));
                       // File.Move(e, Path.Combine(errorPath, e));

                       

                        ////20.12.2019
                           eventLogger.WriteError("Файл " + e + " перемещен в папку Error");

                    }
                    catch (Exception ex)
                    {
                        //    eventLogger.WriteError("XmlException : " + ex.Message);
                        eventLogger.WriteError("Файл не обработан: " + e + ". " + "XmlException : " + ex.Message);

                    }
                    //dataBase.TransactionRollback();
                     eventLogger.WriteError("Неверный формат XML: " + e + " : " + xmlex.Message);
                    //MessageBox.Show(e.Name + " : " + xmlex.Message, "Ошибка передачи данных GLORIA", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                 //   break;


                }
                catch (NullReferenceException nre)
                {
                    //  eventLogger.WriteError("NullReferenceException : " + nre.Message);

                    eventLogger.WriteError("Файл не обработан: " + e + ". " + "NullReferenceException : " + nre.Message);

                    dataBase.TransactionRollback();

                }

                ////07.11.2019

                catch (Exception ex)
                {
                    //  eventLogger.WriteError("Exception : " + ex.Message);

                    eventLogger.WriteError("Файл не обработан: " + e + ". " + "Exception : " + ex.Message);

                    

                    try
                    {
                        if (!Directory.Exists(errorPath))
                        {
                            Directory.CreateDirectory(errorPath);
                        }

                        ////20.12.2019
                         if (File.Exists(Path.Combine(errorPath, Path.GetFileName(e))))

                       // if (File.Exists(Path.Combine(errorPath, e)))
                        ////20.12.2019
                        {
                            ////20.12.2019
                            File.Delete(Path.Combine(errorPath, Path.GetFileName(e)));
                            //File.Delete(Path.Combine(errorPath, e));
                            ////20.12.2019
                        }
                        ////20.12.2019

                        ////20.12.2019
                        // File.Move(e.FullPath, Path.Combine(errorPath, e.Name));
                        //File.Move(e.FullName, Path.Combine(errorPath, e.Name));
                        // File.Move(e, Path.Combine(errorPath, e));
                        File.Move(e, Path.Combine(errorPath, Path.GetFileName(e)));


                        ////20.12.2019

                           eventLogger.WriteError("Файл " + e + " перемещен в папку Error");

                    }
                    catch (Exception eex)
                    {
                        //    eventLogger.WriteError("Exception : " + eex.Message);

                        eventLogger.WriteError("Файл не обработан: " + e + ". " + "Exception : " + eex.Message);


                    }
                    //dataBase.TransactionRollback();
                  //  break;
                }



                finally
                {
                    //dataBase.Disconnect();
                }


                /////13.01.2020
                

                dataBase.Disconnect();

                /*
                a1:
                if (flosh1 == 1)
                {

                    try
                    {
                        if (!Directory.Exists(errorPath))
                        {
                            Directory.CreateDirectory(errorPath);
                        }

                        
                        if (File.Exists(Path.Combine(errorPath, Path.GetFileName(e))))

                       
                        {
                            
                            File.Delete(Path.Combine(errorPath, Path.GetFileName(e)));
                            
                        }
                        
                        File.Move(e, Path.Combine(errorPath, Path.GetFileName(e)));


                        

                        eventLogger.WriteError("Файл " + e + " перемещен в папку Error");

                    }
                    catch (Exception eex)
                    {
                        

                        eventLogger.WriteError("Файл не обработан: " + e + ". " + "Exception : " + eex.Message);


                    }


                }
                */

                /////13.01.2020

                ///10.01.2020
                ///////////////////////////////////

            }

            /*
            
            DirectoryInfo dir = new DirectoryInfo(@"D:\Temp");
            Console.WriteLine("============Список каталогов=============");
            foreach (var item in dir.GetDirectories())
            {
                Console.WriteLine(item.Name);
                Console.WriteLine("==Список подкаталогов==");
                foreach (var it in item.GetDirectories())
                    Console.WriteLine(it.Name);
                Console.WriteLine();
            }
            Console.WriteLine("==============Список файлов==============");
            foreach (var item in dir.GetFiles())
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadLine();
             
            */

        }
        ////20.12.2019

        #region Обработчик изменений в каталоге данных GLORIA

        ////20.12.2019
        //private void OnChanged(object sender, FileSystemEventArgs e)
        public void OnChanged(object sender, FileSystemEventArgs e)
        ////20.12.2019
        {

            Thread fileParserThread = new Thread(FileParser);
        fileParserThread.Start(e);

    }
    #endregion

    #region Обработка файла с данными пересчета
    private void FileParser(object eFSW)
    {

        mutex.WaitOne();
        //if (!IsFileReady(e.FullPath))
        //{

        Thread.Sleep(2000);

        FileSystemEventArgs e;
        try
        {
            //dataBase.Connect();
            e = eFSW as FileSystemEventArgs;
        }
        catch (Exception ex)
        {
           eventLogger.WriteError("FileSystemEventArgs : " + ex.Message);
            return;
        }

        //Цикл для количества попыток обработки

        for (int i = 0; i <= 10; i++)
        {

            ////07.11.2019
            try
            {
                    ////07.11.2019

                    //////13.12.2019
                    List<string> spisnom1 ;
                    List<string> spissost1;
                    //////13.12.2019

                    //Если файл уже обрабатывался - не принимать его (особенность компонента, обработчик срабатывает несколько раз)
                    //int seqInc = 1;
                    if (fileName != e.FullPath)
                {
                    //Инициализация переменной для считывания и разбора XML данных
                    DataSet dataSetXML = new DataSet();
                    //Соединяемся с БД
                    dataBase.Connect();
                    DataSet dsCashCentre = dataBase.GetData("t_g_cashcentre", "id", idCashCentre.ToString());

                        //////05.11.2019
                        DataSet d2 = dataBase.GetData("t_g_currency");
                        DataSet d3 = dataBase.GetData("t_g_denomination");
                        DataSet d4 = dataBase.GetData("t_g_condition");
                        //////05.11.2019

                        //Считывание файла для чтения
                        cardStream = File.Open(e.FullPath, FileMode.Open, FileAccess.Read);
                    if (cardStream != null)
                    {

                            //////13.12.2019
                            spisnom1= new List<string>();
                            spissost1 = new List<string>();
                            //////13.12.2019

                            //Запоминание имени файла для предотвращения повторного считывания
                            fileName = e.FullPath;
                        //Переложение файла по таблицам в набор данных 
                        dataSetXML.ReadXml(cardStream);
                        cardStream.Close();
                        //Считывание имени машины из файла
                        string userName = dataSetXML.Tables["Machine"].Rows[0]["Site"].ToString();
                        //Считывание данных о пользователе из БД
                        DataSet userDataSet = dataBase.GetData("t_g_user", "code", userName);
                        DataTable currency = new DataTable();
                        //Считывание пользователей для кассового центра
                        DataSet UsersInCashCentre = dataBase.GetData("t_g_user", "id_brach", idCashCentre.ToString());

                        DataTable countingContent;
                        //Cчитывание счетчика из БД
                        //DataTable sequenceLINK_SEQ = dataBase.GetSequence("LINK_SEQ");
                        string linkUser;
                        //Текущее состояние счетчика
                        //string currentSeq = sequenceLINK_SEQ.Rows[0]["CURRENTVAL"].ToString();

                        //if (userDataSet.Rows.Count > 0)
                        //{

                        //LINK на пользователя в БД
                        linkUser = userDataSet.Tables[0].Rows[0]["code"].ToString();
                        //}



                        //Если кассовый центр есть в БД
                        if (UsersInCashCentre.Tables[0].Rows.Count > 0)
                        {
                            //Если пользователь есть в данном кассовом центре
                            if (UsersInCashCentre.Tables[0].AsEnumerable().Where(x => x.Field<string>("code").Trim() == userName).Count() > 0)
                            {
                                string linkCountingConfig = String.Empty;
                                //Имя рабочей станции
                                string workStationName = Environment.MachineName;

                                //Перебираем в XML-файле все карточки, который в ней есть
                                foreach (DataRow headersCard in dataSetXML.Tables["HeaderCardTransaction"].Rows)
                                {
                                    string currencyNote = String.Empty;
                                    string aggregateLink = String.Empty;

                                    string countingLink = string.Empty;

                                    //Считаем сумму по данному пересчету
                                    Int64 sumValues = dataSetXML.Tables["Counter"].AsEnumerable().Where(x => x.Field<int>("HeaderCardTransaction_Id") == Convert.ToInt32(headersCard["HeaderCardTransaction_Id"])).Sum(x => Convert.ToInt64(x.Field<string>("Value")) * Convert.ToInt64(x.Field<string>("Number")));

                                    //Считаем количество банкнот по данному пересчету
                                    Int64 countNotes = dataSetXML.Tables["Counter"].AsEnumerable().Where(x => x.Field<int>("HeaderCardTransaction_Id") == Convert.ToInt32(headersCard["HeaderCardTransaction_Id"])).Sum(x => Convert.ToInt64(x.Field<string>("Number")));

                                    //string linkCurrency;
                                    //Считываем данные по карточки из БД
                                    DataTable countingSep = dataBase.GetData("t_g_cards", "name", headersCard["DepositID"].ToString()).Tables[0];//DepositID//HeaderCardID

                                    DataTable counting = new DataTable();
                                    DataTable denomList = new DataTable();
                                    //Если карточка есть в БД
                                    if (countingSep.Rows.Count > 0)
                                    {
                                        //Считываем подготовленные данные из БД для данного пересчета
                                        counting = dataBase.GetData("t_g_counting", "id", countingSep.Rows[0]["id_counting"].ToString()).Tables[0];


                                        //Если данные по пересчету есть в БД
                                        if (counting.Rows.Count > 0)
                                        {
                                            //long lCurrency = Convert.ToInt64(counting.Rows[0]["id"]);
                                            countingContent = dataBase.GetData("t_g_counting_content", "id_counting", counting.Rows[0]["id"].ToString()).Tables[0];
                                            //currencyNote = dataBase.getCurrCode(lCurrency);
                                            //LINK для правила пересчета 
                                            //linkCountingConfig = counting.Rows[0]["LRULE"].ToString();
                                            //Считываем данные по правилу пересчета
                                            //DataTable countingConfig = dataBase.GetCountingConfig(linkCountingConfig);
                                            //Считываем LINK на валюту для пересчета
                                            //linkCurrency = counting.Rows[0]["LCURRENCY"].ToString();
                                            //Считываем список номиналов для валюты из правил пересчета
                                            denomList = dataBase.GetData("t_g_denomination", "id_currency", countingContent.AsEnumerable().Select(x => x.Field<Int64>("id_currency").ToString()).ToList<string>()).Tables[0];//GetDenomList(linkCountingConfig, linkCurrency);
                                                                                                                                                                                                                             //Считываем валюту из списка валют в БД
                                                                                                                                                                                                                             //currency = dataBase.GetCurrency(linkCurrency);


                                            //dataBase.CountingUpdate();
                                            //dataBase.UpdateCounting();

                                            //Формируем текущий номер для счетчика
                                            //int currentSequence = Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["CURRENTVAL"]) + Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["INCSIZE"]) > Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["MAXVAL"]) ? Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["SEEDVAL"]) : Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["CURRENTVAL"]) + Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["INCSIZE"]);

                                            //Обновляем счетчик
                                            //dataBase.UpdateSequence("LINK_SEQ", currentSequence);
                                            //Начинаем формировать транзакцию
                                            dataBase.InitTransaction();

                                            //Если в пересчете есть ссылка на агрегат
                                            /*
                                            if (counting.Rows[0]["LAGGREGATE"] != DBNull.Value)
                                            {
                                                //Записываем LINK на него
                                                aggregateLink = counting.Rows[0]["LAGGREGATE"].ToString();

                                                //Считываем данные по агрегату
                                                DataTable countingAggregate = dataBase.GetAggregate(aggregateLink);
                                                //Cчитываем количество изменений по агрегату
                                                int seqAggr = Convert.ToInt32(countingAggregate.Rows[0]["SEQNUMBER"]);
                                                //Считываем дату и время последнего изменения
                                                DateTime lastUpdateAggr = Convert.ToDateTime(countingAggregate.Rows[0]["LASTUPDATE"]);

                                                Int64 sumAggrValue = Convert.ToInt64(countingAggregate.Rows[0]["SUMNOTETRUE"]) + sumValues;

                                                Int64 countAggrValue = Convert.ToInt64(countingAggregate.Rows[0]["COUNTNOTETRUE"]) + countNotes;

                                                //Обновление данных по агрегату для пересчета
                                                dataBase.AggregateUpdateTransaction(aggregateLink, seqAggr, lastUpdateAggr, workStationName, linkUser, countAggrValue, sumAggrValue);
                                            }
*/
                                            //Считывание имени пересчета
                                            string countingName = counting.Rows[0]["NAME"].ToString();
                                            long counting_id = Convert.ToInt64(counting.Rows[0]["id"]);
                                            //Считывание количества изменений по пересчету
                                            ///?????int seqNumber = Convert.ToInt32(counting.Rows[0]["SEQNUMBER"]);
                                            //Считывание даты и времени последних изменнений
                                            DateTime lastUpdate = Convert.ToDateTime(counting.Rows[0]["LASTUPDATE"]);
                                            long user_id = Convert.ToInt64(userDataSet.Tables[0].Rows[0]["id"]);
                                            //Int64 sumCountValue = Convert.ToInt64(counting.Rows[0]["SUMNOTETRUE"]) + sumValues;

                                            //Int64 countCountValue = Convert.ToInt64(counting.Rows[0]["COUNTNOTETRUE"]) + countNotes;

                                            //DateTime createTime = Convert.ToDateTime(counting.Rows[0]["CREATION"]);
                                            //Обновление данных по пересчету
                                            dataBase.CountingUpdateTransaction(countingName, lastUpdate, user_id, counting_id);

                                                ///04.11.2019
                                                //dataBase.DenomCntDeleteTransaction(counting_id);

                                                ////27.11.2019
                                                //     dataBase.DenomCntDeleteTransaction(Convert.ToInt64(countingSep.Rows[0]["id"]));
                                                ////27.11.2019
                                                
                                                ///04.11.2019


                                                //(countingName,  lastUpdate, workStationName, linkUser, countCountValue, sumCountValue, createTime);

                                                //string countingSepLink = countingSep.Rows[0]["LINK"].ToString();
                                                //int countingSepSeq = Convert.ToInt32(countingSep.Rows[0]["SEQNUMBER"]);
                                                //DateTime countSepLastUpdate = Convert.ToDateTime(countingSep.Rows[0]["LASTUPDATE"]);
                                                //dataBase.CountingSepUpdateTransaction(countingSepLink, workStationName, linkUser, countingSepSeq, countSepLastUpdate);
                                            }
                                        }
                                    else //Если карточки нет в БД
                                    {
                                        //Формируем следующий номер в счетчике
                                        //int currentSequence = Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["CURRENTVAL"]) + Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["INCSIZE"]) > Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["MAXVAL"]) ? Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["SEEDVAL"]) : Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["CURRENTVAL"]) + Convert.ToInt32(sequenceLINK_SEQ.Rows[0]["INCSIZE"]);

                                        //Обновление счетчика
                                        //dataBase.UpdateSequence("LINK_SEQ", currentSequence);
                                        //Начинаем формировать транзакцию
                                        dataBase.InitTransaction();
                                        //Формирование LINK для пересчета
                                        //countingLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                                        //seqInc++;
                                        //Формирование LINK для КР
                                        //string countingSepLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                                        //seqInc++;
                                        //Формирование имени пересчета
                                        string countingName = headersCard["DepositID"].ToString() + "_" + userDataSet.Tables[0].Rows[0]["CODE"].ToString().Trim() + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmssff", CultureInfo.InvariantCulture);

                                        string currcode = dataSetXML.Tables["Counter"].AsEnumerable().Where(x => x.Field<int>("HeaderCardTransaction_Id") == Convert.ToInt32(headersCard["HeaderCardTransaction_Id"])).Select(x => x.Field<string>("Currency")).First();
                                        //LINK правил пересчета
                                        //string ruleLink = "0225999777_2010-09-18_11:11:22"; //?????

                                        //LINK клиента
                                        //string clientLink = "0099999908_2010-09-16_15:01:17";//?????

                                        //LINK валюты пересчета
                                        //linkCurrency = dataBase.GetCurrencyByCurrCode(currcode);
                                        //long 
                                        long user_id = Convert.ToInt64(userDataSet.Tables[0].Rows[0]["id"]);
                                        long client_id = Convert.ToInt64(codeCPS);
                                        //"0057999945_2010-09-16_11:40:10"; // 5401994601_2017-02-03_16:17:54
                                        //Добавление в БД своего пересчета
                                        dataBase.CountingInsertTransaction(client_id, user_id, countingName);
                                        counting = dataBase.GetCounting(countingName);
                                        string cardName = headersCard["DepositID"].ToString();
                                        long counting_id = Convert.ToInt64(counting.Rows[0]["id"]);
                                        dataBase.CardInsertTransaction(counting_id, cardName, user_id);
                                        countingSep = dataBase.GetCountingSep(cardName, counting_id);
                                        //(countingLink, DateTime.Now, workStationName, linkUser, countingName, sumValues, countNotes, linkCashCentre, linkCurrency, clientLink, ruleLink);
                                        //Добавление в БД своего разделителя 
                                        //dataBase.CountingSepInsertTransaction(countingSepLink, workStationName, linkUser, headersCard["DepositID"].ToString(), countingLink, linkCashCentre, userDataSet.Rows[0]["NAME"].ToString());
                                    }




                                    Dictionary<string, long> counts = new Dictionary<string, long>();
                                    Dictionary<string, long> values = new Dictionary<string, long>();


                                    //Перебор всех номиналов в пересчете
                                    foreach (DataRow counter in dataSetXML.Tables["Counter"].AsEnumerable().Where(x => x.Field<int>("HeaderCardTransaction_Id") == Convert.ToInt32(headersCard["HeaderCardTransaction_Id"])))
                                    {
                                        if (counts.ContainsKey(counter["Currency"].ToString()))
                                        {
                                            counts[counter["Currency"].ToString()] += Convert.ToInt64(counter["Number"]);
                                        }
                                        else
                                        {
                                            counts.Add(counter["Currency"].ToString(), Convert.ToInt64(counter["Number"]));
                                        }

                                        if (values.ContainsKey(counter["Currency"].ToString()))
                                        {
                                                /////04.11.2019
                                                values[counter["Currency"].ToString()] += Convert.ToInt64(counter["Value"])* Convert.ToInt64(counter["Number"]);
                                                ////values[counter["Currency"].ToString()] += Convert.ToInt64(counter["Value"]);
                                                ////////04.11.2019
                                            }
                                            else
                                        {
                                                /////04.11.2019
                                                values.Add(counter["Currency"].ToString(), Convert.ToInt64(counter["Value"]) *Convert.ToInt64(counter["Number"]));
                                                //values.Add(counter["Currency"].ToString(), Convert.ToInt64(counter["Value"]));
                                                /////04.11.2019
                                            }
                                            //values[counter["Currency"].ToString()] += Convert.ToInt64(counter["Value"]);

                                            //Сопоставление номинала с LINK в БД из файла 
                                            //string linkDenom = denomDataSet.Tables["Denom"].AsEnumerable().Where(x => x.Field<string>("ValueId") == counter["DenomID"].ToString()).Select(x => x.Field<string>("Link")).First<string>();


                                            ///////05.11.2019

                                          //  long idDenom = Convert.ToInt64(denomDataSet.Tables["Denom"].AsEnumerable().Where(x => x.Field<string>("ValueId") == counter["DenomID"].ToString()).Select(x => x.Field<string>("Link")).First<string>());

                                            string selectString = "curr_code= '" + counter["Currency"].ToString() + "'";
                                            DataRow[] searchedRows = ((DataTable)d2.Tables[0]).Select(selectString);
                                            DataRow searchedRows1 = searchedRows[0];
                                            string k1 = searchedRows1[0].ToString();

                                          

                                            selectString = "id_currency='"+ k1.ToString() + "' and value= '" + counter["Value"].ToString() + "'";
                                            searchedRows = ((DataTable)d3.Tables[0]).Select(selectString);
                                            searchedRows1 = searchedRows[0];
                                            string k2 = searchedRows1[0].ToString();
                                            long idDenom = Convert.ToInt64(k2);


                                            /*
                                            int i55 = -1;

                                            if (counter["Quality"].ToString()== "ATM")
                                                i55 = 5;
                                            if (counter["Quality"].ToString() == "Unfit")
                                                i55 = 5;
                                            if (counter["Quality"].ToString() == "ATM")
                                                i55 = 5;
                                            */

                                            //  selectString = "upper(code1)='" + counter["Quality"].ToString().ToUpper() + "'";
                                            selectString = "code1='" + counter["Quality"].ToString() + "'";
                                            searchedRows = ((DataTable)d4.Tables[0]).Select(selectString);
                                            searchedRows1 = searchedRows[0];
                                            string k3 = searchedRows1[0].ToString();
                                            long idCondition = Convert.ToInt64(k3);

                                            //     long idCondition = Convert.ToInt64(denomDataSet.Tables["Denom"].AsEnumerable().Where(x => x.Field<string>("ValueId") == counter["DenomID"].ToString()).Select(x => x.Field<string>("Condition")).First<string>());

                                            ///////05.11.2019



                                            //Запрос в базе на существование данного номинала????

                                            DataRow denomination;
                                        if (!String.IsNullOrEmpty(linkCountingConfig) && denomList.Rows.Count > 0)
                                        {
                                            if (denomList.AsEnumerable().Where(x => x.Field<Int64>("LINK") == idDenom).Count() > 0)
                                            {
                                                denomination = denomList.AsEnumerable().Where(x => x.Field<Int64>("LINK") == idDenom).FirstOrDefault<DataRow>();
                                            }
                                            else
                                            {
                                                dataBase.TransactionRollback();
                                                    throw new Exception("Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " " + counter["Quality"] + " не учавствует в данном бизнес правиле");
                                                    /*
                                                    ////13.01.2020
                                                    //  throw new Exception("Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " " + counter["Quality"] + " не учавствует в данном бизнес правиле");
                                                    eventLogger.WriteError("Файл не обработан: " + e + ". " + "Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " " + counter["Quality"] + " не учавствует в данном бизнес правиле");
                                                    break;
                                                    ////13.01.2020
                                                    */
                                                }

                                            }
                                        else
                                        {
                                            denomination = dataBase.GetDenomination(idDenom).AsEnumerable().FirstOrDefault<DataRow>();
                                        }

                                        //Если номинал не соответствует указанному номиналу в файле
                                        if (Convert.ToInt32(counter["Value"]) != Convert.ToInt32(denomination["VALUE"]))
                                        {
                                            dataBase.TransactionRollback();
                                                throw new Exception("Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " не соответствует номиналу " + denomination["NAME"].ToString() + " с линком " + denomination["LINK"].ToString());
                                                /*
                                                ////13.01.2020
                                                //  throw new Exception("Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " не соответствует номиналу " + denomination["NAME"].ToString() + " с линком " + denomination["LINK"].ToString());
                                                eventLogger.WriteError("Файл не обработан: " + e + ". " + "Указанный номинал " + counter["DenomID"] + " со значением " + counter["Value"] + " " + counter["Currency"] + " не соответствует номиналу " + denomination["NAME"].ToString() + " с линком " + denomination["LINK"].ToString());
                                                break;
                                                ////13.01.2020
                                                */

                                            }
                                            //Номер карточки
                                            string card = headersCard["DepositID"].ToString();
                                        //Идентификатор номинала в файле
                                        int denom = Convert.ToInt32(counter["DenomID"]);

                                        string linkCounting = String.Empty;

                                        //Количество номинала в файле
                                        Int64 countDenom = Convert.ToInt64(counter["Number"]);
                                        //Расчет суммы для номинала
                                        Int64 sumValue = Convert.ToInt64(counter["Value"]) * countDenom;

                                        //Если пересчет есть в БД
                                        if (counting.Rows.Count > 0)
                                        {
                                            long id_counting = Convert.ToInt64(counting.Rows[0]["id"]);
                                        }
                                        else
                                        {

                                        }
                                        /*
                                        if (currencyNote == String.Empty || currencyNote != counter["Currency"].ToString())
                                        {
                                            if (counter["Currency"].ToString() == "GB1" && currencyNote == "GBP")
                                            {
                                                currencyNote = counter["Currency"].ToString();

                                            }
                                            else
                                            {
                                                dataBase.TransactionRollback();
                                                throw new Exception("Такая валюта : " + counter["Currency"].ToString() + " не должна учавствовать в данном пересчете");
                                            }

                                        }
                                        else
                                        {

                                        }
                                        */
                                        //LINK для пересчета
                                        //long idCounting = Convert.ToInt64(counting.Rows[0]["id"]);
                                        //Если есть ссылка на агрегат

                                        /*
                                        if (counting.Rows[0]["LAGGREGATE"] != DBNull.Value)
                                        {


                                            //Запрос данных об агрегате
                                            DataTable aggregateDenomCnt = dataBase.GetAggDenomCnt(counting.Rows[0]["LAGGREGATE"].ToString(), linkDenom);
                                            //Если номинал есть в БД
                                            if (aggregateDenomCnt.Rows.Count > 0)
                                            {
                                                //Последние ищменения
                                                int seqNumber = Convert.ToInt32(aggregateDenomCnt.Rows[0]["SEQNUMBER"]);
                                                //Расчет количества номинала в агрегате
                                                Int64 aggNBMachineCount = 0;
                                                if (Convert.ToInt64(aggregateDenomCnt.Rows[0]["NBMACHINE"]) == 0)
                                                {
                                                    aggNBMachineCount = countDenom;
                                                }
                                                else
                                                {
                                                    aggNBMachineCount = countDenom + Convert.ToInt64(aggregateDenomCnt.Rows[0]["NBMACHINE"]);
                                                }
                                                Int64 aggCountDenom = countDenom + Convert.ToInt64(aggregateDenomCnt.Rows[0]["NBDENOM"]);
                                                //Расчет суммы номинала в агрегате
                                                Int64 aggSumValue = sumValue + Convert.ToInt64(aggregateDenomCnt.Rows[0]["VALUECOUNTING"]);
                                                //Обновление номинала в БД
                                                dataBase.aggDenomCntUpdateTransaction(workStationName, seqNumber, linkUser, aggregateLink, linkDenom, aggCountDenom, aggSumValue, aggNBMachineCount);

                                            }
                                            else //Если номинала нет в БД
                                            {
                                                //Формирование LINK для таблицы номиналов 
                                                string aggDenomCntLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                                                seqInc++;
                                                //Добавление номинала в БД
                                                dataBase.AggDenomCntInsertTransaction(aggDenomCntLink, workStationName, linkUser, aggregateLink, linkDenom, countDenom, sumValue);
                                            }

                                        }
                                        else
                                        {

                                        }*/
                                        /*
                                                                                   }

                                                                                   else
                                                                                   {
                                                                                       linkCounting = countingLink;
                                                                                       if (currencyNote == String.Empty)
                                                                                       {
                                                                                           currencyNote = counter["Currency"].ToString();
                                                                                       }
                                                                                       else if (currencyNote != counter["Currency"].ToString())
                                                                                       {
                                                                                           dataBase.TransactionRollback();
                                                                                           throw new Exception("В одном пересчете не может быть нескольких валют!");
                                                                                       }
                                                                                   }
                                       */

                                        long idCounting = Convert.ToInt64(counting.Rows[0]["id"]);
                                            //Выбор номиналов для пересчета

                                            ////10.12.2019
                                            // DataTable DenomCnt = dataBase.GetDenomCnt(idCounting, idDenom, idCondition);
                                            DataTable DenomCnt = dataBase.GetDenomCnt1(idCounting, idDenom, idCondition, Convert.ToInt64(countingSep.Rows[0]["id"]));
                                            ////10.12.2019

                                            long user_id = Convert.ToInt64(userDataSet.Tables[0].Rows[0]["id"]);
                                        long card_id = Convert.ToInt64(countingSep.Rows[0]["id"]);
                                        //Если номиналы в БД есть
                                        if (DenomCnt.Rows.Count > 0)
                                        {
                                            //LINK для номинала
                                            //string DenomCntLink = DenomCnt.Rows[0]["LINK"].ToString();
                                            long idDenomCnt = Convert.ToInt64(DenomCnt.Rows[0]["id"]);

                                            //Считывание номера изменений в номинале
                                            //int seqNumber = Convert.ToInt32(DenomCnt.Rows[0]["SEQNUMBER"]);
                                            //Расчет количества номинала в пересчете

                                            /*Int64 nbMachineCount = 0;
                                            //if (Convert.ToInt32(DenomCnt.Rows[0]["NBMACHINE"]) == 0)
                                            {
                                                nbMachineCount = countDenom;
                                            }
                                            else
                                            {
                                                nbMachineCount = countDenom + Convert.ToInt64(DenomCnt.Rows[0]["NBMACHINE"]);
                                            }
                                            */
                                            countDenom += Convert.ToInt64(DenomCnt.Rows[0]["count"]);
                                            //Расчет суммы номинала в пересчете
                                            sumValue += Convert.ToInt64(DenomCnt.Rows[0]["fact_value"]);

                                                //Обновление данных о номинале о пересчете

                                                //////13.12.2019
                                                string srt1 = "0";

                                                if (spisnom1.Find((x) => x == idDenom.ToString()) != idDenom.ToString())
                                                {
                                                    srt1 = "1";


                                                    spisnom1.Add(idDenom.ToString());
                                                    spissost1.Add(idCondition.ToString());

                                                }
                                                else
                                                {

                                                    int m1 = 0;
                                                    int j1 = 0;

                                                    foreach (string p1 in spisnom1)
                                                    {
                                                        if (p1 == idDenom.ToString())
                                                        {
                                                            if (spissost1[j1] == idCondition.ToString())
                                                            {
                                                                m1 = 1;
                                                                break;

                                                            }

                                                        }
                                                        j1 = j1 + 1;
                                                    }

                                                    if (m1==0)
                                                    {

                                                        srt1 = "1";
                                                        spisnom1.Add(idDenom.ToString());
                                                        spissost1.Add(idCondition.ToString());

                                                    }

                                                }
                                                //////13.12.2019

                                                /////06.11.2019
                                                //dataBase.DenomCntUpdateTransaction(user_id, idCounting, idDenom, idCondition, countDenom, sumValue);

                                                //////13.12.2019
                                                //    dataBase.DenomCntUpdateTransaction(user_id, idCounting, idDenom, idCondition, countDenom, sumValue, userName.ToString(), dataSetXML.Tables["Operator"].Rows[0]["Name"].ToString());
                                                dataBase.DenomCntUpdateTransaction(user_id, idCounting, idDenom, idCondition, countDenom, sumValue, userName.ToString(), dataSetXML.Tables["Operator"].Rows[0]["Name"].ToString(), srt1);
                                                //////13.12.2019

                                                /////06.11.2019

                                                

                                                //(workStationName, seqNumber, linkUser, linkCounting, linkDenom, countDenom, sumValue, nbMachineCount);
                                            }
                                            else //Если данных о номинале нет в БД
                                        {
                                                //Формирование LINK для номинала
                                                //string denomCntLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                                                //seqInc++;
                                                //Добавление в БД номинала
                                                //long idCondition = Convert.ToInt64(denomList);

                                                /////06.11.2019
                                                //        dataBase.DenomCntInsertTransaction(idCounting, card_id, idDenom, idCondition, user_id, countDenom, sumValue);

                                                //string userName = dataSetXML.Tables["Machine"].Rows[0]["Site"].ToString();

                                                //////13.12.2019
                                                spisnom1.Add(idDenom.ToString());
                                                spissost1.Add(idCondition.ToString());
                                                //////13.12.2019

                                                dataBase.DenomCntInsertTransaction(idCounting, card_id, idDenom, idCondition, user_id, countDenom, sumValue,  userName.ToString(), dataSetXML.Tables["Operator"].Rows[0]["Name"].ToString());

                                                /////06.11.2019
                                            }
                                            #region Под вопросом


                                            //Считывание использования данного номинала
                                            int inventory = 1;//dataBase.GetInventoryDenom(linkDenom);
                                                          //Если исползуется
                                        if (inventory == 1)
                                        {
                                            //Считывание данных номиналов по пользователям
                                            DataTable cash = dataBase.GetCash(user_id, idDenom, idCashCentre);

                                            //Если есть данные по данному номиналу
                                            if (cash.Rows.Count > 0)
                                            {
                                                //Считывыние LINK 
                                                long cash_id = Convert.ToInt64(cash.Rows[0]["id"]);
                                                
                                                    /////10.01.2020
                                                 //Считывание номера изменений
                                               // int seqNumber = Convert.ToInt32(cash.Rows[0]["SEQNUMBER"]);
                                                //Расчет еоличества номиналов
                                              //  long countCash = Convert.ToInt64(cash.Rows[0]["COUNT"]) + Convert.ToInt64(counter["Number"]);

                                                    int seqNumber = 0;
                                                    if (cash.Rows[0]["SEQNUMBER"] != null)
                                                        seqNumber = Convert.ToInt32(cash.Rows[0]["SEQNUMBER"]);
                                                    /////10.01.2020

                                                    //Расчет еоличества номиналов
                                                    long countCash = Convert.ToInt64(counter["Number"]);
                                                    if (cash.Rows[0]["COUNT"] != null)
                                                        countCash = countCash + Convert.ToInt64(cash.Rows[0]["COUNT"]);

                                                    //Дата и время последних изменений
                                                    DateTime cashLastUpdate = Convert.ToDateTime(cash.Rows[0]["LASTUPDATE"]);
                                                //Обновление данных номинала по пользователю
                                                dataBase.UpdateCashTransaction(cash_id, user_id, cashLastUpdate, countCash);
                                            }
                                            else //Если номинала по пользователю в БД 
                                            {
                                                //Формирование LINK для номинала по пользователю
                                                //string cashLink = currentSeq + (Convert.ToInt32(currentSeq) - seqInc).ToString("D4") + DateTime.Now.ToString("ff") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                                                //seqInc++;
                                                //Добавление данных номинала по пользователю
                                                dataBase.InsertCashTransaction(user_id, idDenom, idCashCentre, Convert.ToInt32(counter["Number"]));
                                            }
                                        }

                                        #endregion
                                    }
                                    //countingContent = dataBase.GetCountingContent( ();
                                    foreach (var fact_value in values)
                                    {
                                        long idCurrency = dataBase.GetCurrencyByCode(fact_value.Key);
                                        if (idCurrency > 0)
                                        {
                                            long user_id = Convert.ToInt64(userDataSet.Tables[0].Rows[0]["id"]);
                                            long counting_id = Convert.ToInt64(counting.Rows[0]["id"]);
                                            countingContent = dataBase.GetCountingContent(counting_id);
                                            if (countingContent.Rows.Count > 0)
                                            {
                                                if (countingContent.AsEnumerable().Where(x => x.Field<Int64>("id_currency") == idCurrency).Count() > 0)
                                                {
                                                    long value = Convert.ToInt64(countingContent.AsEnumerable().FirstOrDefault(x => x.Field<Int64>("id_currency") == idCurrency).Field<Decimal>("fact_value"));
                                                }
                                                else
                                                {
                                                        /////11.11.2019

                                                        DataSet accountsDataSet1 = dataBase.Poisksh("select top 1 * from t_g_account t1 where t1.id_client in (select t1.id from t_g_client t1 inner join t_g_account t2 on (t1.id = t2.id_client) inner join t_g_counting_content t3 on (t2.id = t3.id_account) where t3.id_counting = '"+ counting_id.ToString()+ "' ) and t1.id_currency = '"+ idCurrency.ToString()+ "'");
                                                        if (accountsDataSet1 != null)
                                                        {
                                                            if (accountsDataSet1.Tables[0] != null)
                                                            {
                                                                if (accountsDataSet1.Tables[0].Rows.Count > 0)
                                                                {
                                                                    long id_account = Convert.ToInt64(accountsDataSet1.Tables[0].Rows[0]["id"]);
                                                                    dataBase.InsertTransactionCountingContent(id_account, counting_id, fact_value.Value, idCurrency, user_id, workStationName);
                                                                }
                                                                else
                                                                {
                                                                    throw new Exception("Счета с данной валютой у клиента нет.");
                                                                    /*
                                                                    ////13.01.2020
                                                                    // throw new Exception("Счета с данной валютой у клиента нет.");
                                                                    eventLogger.WriteError("Файл не обработан: " + e + ". " + "Счета с данной валютой у клиента нет.");
                                                                    break;
                                                                    ////13.01.2020
                                                                    */
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception("Счета с данной валютой у клиента нет.");
                                                                /*
                                                                ////13.01.2020
                                                                // throw new Exception("Счета с данной валютой у клиента нет.");
                                                                eventLogger.WriteError("Файл не обработан: " + e + ". " + "Счета с данной валютой у клиента нет.");
                                                                break;
                                                                ////13.01.2020
                                                                */
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Счета с данной валютой у клиента нет.");
                                                            /*
                                                            ////13.01.2020
                                                            //  throw new Exception("Счета с данной валютой у клиента нет.");
                                                            eventLogger.WriteError("Файл не обработан: " + e + ". " + "Счета с данной валютой у клиента нет.");
                                                            break;
                                                            ////13.01.2020
                                                            */
                                                        }

                                                        //throw new Exception("Счета с данной валютой у клиента нет.");
                                                        /////11.11.2019

                                                    }
                                                    //long value = countingContent.AsEnumerable().Where(x => x.Field<long>("id_currency") == idCurrency).Select(x => x.Field<long>("fact_value")).FirstOrDefault<Int64>() + fact_value.Value;

                                                    /////04.11.2019

                                                    dataBase.UpdateTransactionCountingContent(user_id, fact_value.Value , counting_id, idCurrency);
                                                    //    dataBase.UpdateTransactionCountingContent(user_id, fact_value.Value, counting_id, idCurrency);

                                                    /////04.11.2019
                                                }
                                                else
                                            {
                                                long client_id = Convert.ToInt64(codeCPS);
                                                DataTable accountsDataSet = dataBase.GetAccountsByClient(client_id);
                                                long id_account = accountsDataSet.AsEnumerable().FirstOrDefault(x => x.Field<Int64>("id_currency") == idCurrency).Field<Int64>("id");
                                                //long value = Convert.ToInt64(countingContent.AsEnumerable().FirstOrDefault(x => x.Field<Int64>("id_currency") == idCurrency).Field<Decimal>("fact_value"));
                                                dataBase.InsertTransactionCountingContent(id_account, counting_id, fact_value.Value, idCurrency, user_id, workStationName);
                                            }
                                        }
                                    }
                                    //Завершение транзакции
                                    dataBase.TransactionCommit();
                                }
                            }
                        }



                        //Закрытие потока для чтения файла
                        /*
                        if (cardStream != null)
                        {
                            cardStream.Close();
                        }*/
                        //Если директория для нормально отработанных файлов не существует
                        try
                        {
                            if (!Directory.Exists(readyPath))
                            {
                                //Создать такую директорию
                                Directory.CreateDirectory(readyPath);
                            }

                            //Если данный файл уже существует
                            if (File.Exists(Path.Combine(readyPath, e.Name)))
                            {
                                //Удалить файл
                                File.Delete(Path.Combine(readyPath, e.Name));
                            }
                            //Переместить туда файл
                            File.Move(e.FullPath, Path.Combine(readyPath, e.Name));
                            //Записать в журнал, что файл обработан
                            eventLogger.WriteInfo("Файл обработан: " + e.Name + " с " + (i + 1).ToString() + " попытки ");

                        }
                        catch (Exception ex)
                        {
                                //   eventLogger.WriteError("Обработан : " + ex.Message);
                                eventLogger.WriteError("Файл не обработан: " + e.Name + ". " + ex.Message);

                            }

                        break;

                    }
                         }


                    ////07.11.2019
                      }
                  ////07.11.2019

                      catch (FileNotFoundException fex)
                      {

                    //   eventLogger.WriteError(fex.Message);

                    eventLogger.WriteError("Файл не обработан: " + e.Name + ". " + fex.Message);

                    //dataBase.TransactionRollback();
                    break;
                      }
                      catch (IOException ex) //Ошибка доступа к файлу
                      {

                    //  eventLogger.WriteError(ex.Message);

                    eventLogger.WriteError("Файл не обработан: " + e.Name + ". " + ex.Message);

                    //MessageBox.Show(ex.Message);
                    //dataBase.TransactionRollback();
                    continue;
                      }
                      catch (ArgumentException aex) //Ошибка в структуре элементов
                      {


                          try
                          {
                              if (!Directory.Exists(errorPath))
                              {
                                  Directory.CreateDirectory(errorPath);
                              }

                              if (File.Exists(Path.Combine(errorPath, e.Name)))
                              {
                                  File.Delete(Path.Combine(errorPath, e.Name));
                              }
                              File.Move(e.FullPath, Path.Combine(errorPath, e.Name));
                              eventLogger.WriteError("Файл " + e.Name + " перемещен в папку Error");

                          }
                          catch (Exception ex)
                          {
                        //  eventLogger.WriteError("ArgumentException : " + ex.Message);
                        eventLogger.WriteError("Файл не обработан: " + e.Name + ". " + "ArgumentException : " + ex.Message);

                    }
                          //dataBase.TransactionRollback();
                          eventLogger.WriteError("Не найден нужный элемент XML: " + e.Name + " : " + aex.Message);
                          //MessageBox.Show(e.Name + " : " + ex.Message, "Ошибка передачи данных GLORIA", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                          break;

                      }
                      catch (XmlException xmlex) //Файл не является XML
                      {

                          try
                          {
                              if (!Directory.Exists(errorPath))
                              {
                                  Directory.CreateDirectory(errorPath);
                              }

                              if (File.Exists(Path.Combine(errorPath, e.Name)))
                              {
                                  File.Delete(Path.Combine(errorPath, e.Name));
                              }
                              File.Move(e.FullPath, Path.Combine(errorPath, e.Name));
                              eventLogger.WriteError("Файл " + e.Name + " перемещен в папку Error");

                          }
                          catch (Exception ex)
                          {
                        //    eventLogger.WriteError("XmlException : " + ex.Message);

                        eventLogger.WriteError("Файл не обработан: " + e.Name + ". " + "XmlException : " + ex.Message);

                    }
                          //dataBase.TransactionRollback();
                          eventLogger.WriteError("Неверный формат XML: " + e.Name + " : " + xmlex.Message);
                          //MessageBox.Show(e.Name + " : " + xmlex.Message, "Ошибка передачи данных GLORIA", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                          break;


                      }
                      catch (NullReferenceException nre)
                      {
                    //  eventLogger.WriteError("NullReferenceException : " + nre.Message);

                    eventLogger.WriteError("Файл не обработан: " + e.Name + ". " + "NullReferenceException : " + nre.Message);

                    dataBase.TransactionRollback();

                      }

                          ////07.11.2019

                          catch (Exception ex)
                      {
                    //  eventLogger.WriteError("Exception : " + ex.Message);

                    eventLogger.WriteError("Файл не обработан: " + e.Name + ". " + "Exception : " + ex.Message);

                    try
                          {
                              if (!Directory.Exists(errorPath))
                              {
                                  Directory.CreateDirectory(errorPath);
                              }

                              if (File.Exists(Path.Combine(errorPath, e.Name)))
                              {
                                  File.Delete(Path.Combine(errorPath, e.Name));
                              }
                              File.Move(e.FullPath, Path.Combine(errorPath, e.Name));
                              eventLogger.WriteError("Файл " + e.Name + " перемещен в папку Error");

                          }
                          catch (Exception eex)
                          {
                        //    eventLogger.WriteError("Exception : " + eex.Message);

                        eventLogger.WriteError("Файл не обработан: " + e.Name + ". " + "Exception : " + eex.Message);

                    }
                          //dataBase.TransactionRollback();
                          break;
                      }



                          finally
                          {
                          //dataBase.Disconnect();
                      }
                      ////07.11.2019
                              
                    ////07.11.2019
                    Thread.Sleep(100);




        }


        //Если файл не удалось обработать  - перемещаем его в ошибочные файлы
        try
        {
            if (File.Exists(e.FullPath))
            {
                if (File.Exists(Path.Combine(errorPath, e.Name)))
                {
                    File.Delete(Path.Combine(errorPath, e.Name));
                }
                File.Move(e.FullPath, Path.Combine(errorPath, e.Name));

               // eventLogger.WriteError("Данные не обработаны после 10 попыток : " + e.Name);
              //  eventLogger.WriteError("Файл " + e.Name + " перемещен в папку Error");

                //MessageBox.Show(e.Name + " : Данные не обработаны после 3 попыток", "Ошибка передачи данных GLORIA", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        catch (Exception ex)
        {
          //  eventLogger.WriteError("После 10 попыток : " + ex.Message);

        }
        dataBase.Disconnect();

        mutex.ReleaseMutex();

        //}
    }

    #endregion

    #region Первоначальная обработка файлов в папке с файлами пересчета
    public void PathStartMonitor()
    {

        DataTable cashCentreCounter = new DataTable();
        DateTime endOperDay = DateTime.MinValue;
        try
        {
            //Соединяемся с БД
            dataBase.Connect();
            //linkCashCentre = dataBase.GetCasheCentreLinkByCode(codeCashCentre);
            //cashCentreCounter = dataBase.GetCashCentreCounter(linkCashCentre);
            //Отключаемся от БД
            dataBase.Disconnect();
        }
        catch (Exception ex)
        {
           // eventLogger.WriteError("Не удалось считать : " + ex.Message);
        }

        if (cashCentreCounter.Rows.Count > 0)
        {
            endOperDay = Convert.ToDateTime(cashCentreCounter.Rows[0]["LASTUPDATE"]);
        }
        //string[] files = Directory.GetFiles(defaultPath);

        foreach (string file in Directory.GetFiles(defaultPath))
        {
            var fileCountDate = file.Substring(file.LastIndexOf('_') + 1);
            fileCountDate = fileCountDate.TrimEnd('.', 'd', 'a', 't');
            fileCountDate = fileCountDate.TrimEnd('.', 'x', 'm', 'l');
            string shortName = file.Substring(file.LastIndexOf('\\') + 1);
            try
            {
                DateTime dtCountEnd = DateTime.ParseExact(fileCountDate, "yyyyMMdd-HHmmss", CultureInfo.InvariantCulture);
                if (dtCountEnd > endOperDay)
                {

                    FileSystemEventArgs e = new FileSystemEventArgs(WatcherChangeTypes.Changed, defaultPath, shortName);
                    Thread fileParserThread = new Thread(FileParser);
                    fileParserThread.Start(e);
                    //FileParser(e);

                }
                else
                {
                    if (!Directory.Exists(UnProcessedPath))
                    {
                        //Создать такую директорию
                        Directory.CreateDirectory(UnProcessedPath);
                    }

                    //Если данный файл уже существует
                    if (File.Exists(Path.Combine(UnProcessedPath, shortName)))
                    {
                        //Удалить файл
                        File.Delete(Path.Combine(UnProcessedPath, shortName));
                    }
                    //Переместить туда файл
                    File.Move(file, Path.Combine(UnProcessedPath, shortName));

                    //Записать в журнал, что файл обработан
                 //   eventLogger.WriteInfo("Файл " + shortName + " перемещен в необработанные ");

                }
            }
            catch (Exception ex)
            {
                if (!Directory.Exists(errorPath))
                {
                    //Создать такую директорию
                    Directory.CreateDirectory(errorPath);
                }

                //Если данный файл уже существует
                if (File.Exists(Path.Combine(errorPath, shortName)))
                {
                    //Удалить файл
                    File.Delete(Path.Combine(errorPath, shortName));
                }
                //Переместить туда файл
                File.Move(file, Path.Combine(errorPath, shortName));
                //Записать в журнал, что файл обработан
             //   eventLogger.WriteInfo("Файл " + shortName + " перемещен в необработанные ");
             //   eventLogger.WriteError(ex.Message);

            }



        }
    }
    #endregion


    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                watcher = null;
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
   }
}
