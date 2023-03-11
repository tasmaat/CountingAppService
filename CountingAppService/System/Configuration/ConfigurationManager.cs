using System.Collections.Specialized;
using System.Configuration;

namespace System.Configuration
{
    //
    // Сводка:
    //     Предоставляет доступ к файлам конфигурации для клиентских приложений. Этот класс
    //     не наследуется.
    public static class ConfigurationManager1
    {
        //
        // Сводка:
        //     Получает данные System.Configuration.AppSettingsSection для конфигурации текущего
        //     приложения по умолчанию.
        //
        // Возврат:
        //     Возвращает объект System.Collections.Specialized.NameValueCollection, в который
        //     помещено содержимое объекта System.Configuration.AppSettingsSection для конфигурации
        //     текущего приложения по умолчанию.
        //
        // Исключения:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Не удалось извлечь объект System.Collections.Specialized.NameValueCollection
        //     с параметрами приложения.
        public static NameValueCollection AppSettings { get; }
        //
        // Сводка:
        //     Получает данные System.Configuration.ConnectionStringsSection для конфигурации
        //     текущего приложения по умолчанию.
        //
        // Возврат:
        //     Возвращает объект System.Configuration.ConnectionStringSettingsCollection, в
        //     который помещено содержимое объекта System.Configuration.ConnectionStringsSection
        //     для конфигурации текущего приложения по умолчанию.
        //
        // Исключения:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Не удалось получить объект System.Configuration.ConnectionStringSettingsCollection.
        public static ConnectionStringSettingsCollection ConnectionStrings { get; }

        //
        // Сводка:
        //     Извлекает указанный раздел конфигурации для конфигурации по умолчанию текущего
        //     приложения.
        //
        // Параметры:
        //   sectionName:
        //     Путь и имя раздела конфигурации.
        //
        // Возврат:
        //     Указанный объект System.Configuration.ConfigurationSection или значение null,
        //     если раздел не существует.
        //
        // Исключения:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Не удалось загрузить файл конфигурации.
      //  public static object GetSection(string sectionName);
        //
        // Сводка:
        //     Открывает файл конфигурации для текущего приложения в качестве объекта System.Configuration.Configuration.
        //
        // Параметры:
        //   userLevel:
        //     Объект System.Configuration.ConfigurationUserLevel, для которого открывается
        //     конфигурация.
        //
        // Возврат:
        //     Объект System.Configuration.Configuration.
        //
        // Исключения:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Не удалось загрузить файл конфигурации.
      //  public static Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel);
        //
        // Сводка:
        //     Открывает указанный файл конфигурации клиента в качестве объекта System.Configuration.Configuration.
        //
        // Параметры:
        //   exePath:
        //     Путь к исполняемому файлу (EXE-файлу).
        //
        // Возврат:
        //     Объект System.Configuration.Configuration.
        //
        // Исключения:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Не удалось загрузить файл конфигурации.
      //  public static Configuration OpenExeConfiguration(string exePath);
        //
        // Сводка:
        //     Открывает файл конфигурации компьютера на текущем компьютере в качестве объекта
        //     System.Configuration.Configuration.
        //
        // Возврат:
        //     Объект System.Configuration.Configuration.
        //
        // Исключения:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Не удалось загрузить файл конфигурации.
     //   public static Configuration OpenMachineConfiguration();
        //
        // Сводка:
        //     Открывает указанный файл конфигурации клиента в качестве объекта System.Configuration.Configuration,
        //     который использует указанные сопоставление файлов и уровень пользователя.
        //
        // Параметры:
        //   fileMap:
        //     Объект System.Configuration.ExeConfigurationFileMap, который ссылается на файл
        //     конфигурации, используемый вместо файла конфигурации приложения по умолчанию.
        //
        //   userLevel:
        //     Объект System.Configuration.ConfigurationUserLevel, для которого открывается
        //     конфигурация.
        //
        // Возврат:
        //     Объект конфигурации.
        //
        // Исключения:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Не удалось загрузить файл конфигурации.
     //   public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);
        //
        // Сводка:
        //     Открывает указанный файл конфигурации клиента в качестве объекта System.Configuration.Configuration,
        //     который использует указанное сопоставление файлов, уровень пользователя и параметр
        //     предварительной загрузки.
        //
        // Параметры:
        //   fileMap:
        //     Объект System.Configuration.ExeConfigurationFileMap, который ссылается на файл
        //     конфигурации, используемый вместо файла конфигурации приложения по умолчанию.
        //
        //   userLevel:
        //     Объект System.Configuration.ConfigurationUserLevel, для которого открывается
        //     конфигурация.
        //
        //   preLoad:
        //     Значение true для предварительной загрузки всех групп разделов и разделов; в
        //     противном случае — значение false.
        //
        // Возврат:
        //     Объект конфигурации.
        //
        // Исключения:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Не удалось загрузить файл конфигурации.
      //  public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel, bool preLoad);
        //
        // Сводка:
        //     Открывает файл конфигурации компьютера в качестве объекта System.Configuration.Configuration,
        //     который использует указанное сопоставление файлов.
        //
        // Параметры:
        //   fileMap:
        //     Объект System.Configuration.ExeConfigurationFileMap, который ссылается на файл
        //     конфигурации, используемый вместо файла конфигурации приложения по умолчанию.
        //
        // Возврат:
        //     Объект System.Configuration.Configuration.
        //
        // Исключения:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     Не удалось загрузить файл конфигурации.
      //  public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap);
        //
        // Сводка:
        //     Обновляет раздел с заданным именем, чтобы при следующем извлечении он повторно
        //     считывался с диска.
        //
        // Параметры:
        //   sectionName:
        //     Имя раздела конфигурации, или имя пути и раздела конфигурации того раздела, который
        //     необходимо обновить.
      //  public static void RefreshSection(string sectionName);
    }
}