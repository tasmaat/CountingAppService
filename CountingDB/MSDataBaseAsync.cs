using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CountingDB
{
    public class MSDataBaseAsync
    {
        private EventLogger.EventLogger eventLogger = new EventLogger.EventLogger();
        private string connectionString;

        private SqlConnection connection = null;
        private SqlDataAdapter adapter = null;
        private SqlCommandBuilder sqlCommand = null;           
        private SqlTransaction transaction;
        private SqlCommand command;

        public MSDataBaseAsync()
        {
            //connectionString = @"Data Source=localhost;Initial Catalog=CountingDB;Persist Security Info=True;User ID=vadim;Password=123456789";
            connectionString = new StreamReader(@"connectionString.ini", Encoding.Default).ReadToEnd();
            
            
      
            

            //connectionString = ConfigurationManager.ConnectionStrings["ADOContext"].ConnectionString;
        }

        public Task<DataSet> GetDataSetAsync(string sql)
        {
            return Task.Run(() =>
            {
                using (connection = new SqlConnection(connectionString))
                {
                    adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            });
        }

        public Task<bool> UpdateDBfromDatasetAsync(DataTable dt, string tablename)
        {
            return Task.Run(() =>
            {
                string sqlSelect = "SELECT * FROM " + tablename;
                using (connection = new SqlConnection(connectionString))
                {
                    adapter = new SqlDataAdapter(sqlSelect, connection);                    
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    string u = commandBuilder.GetUpdateCommand().CommandText;
                    string i = commandBuilder.GetInsertCommand().CommandText;
                    try
                    {
                        adapter.Update(dt);
                    }
                    catch (Exception ex)
                    {
                        eventLogger.WriteError(ex.Message);
                        return false;
                    }                    
                    return true;
                }
            });
        }

        public async Task<List<T>> GetData<T>()
        {
            Type clT = typeof(T);            
            string expression = $"SELECT * FROM {clT.Name}";
            DataSet ds = await GetDataSetAsync(expression);
            return ds.Tables[0].ToListof<T>();
        }

        public async Task<List<T>> GetDataWhere<T>(string where)
        {
            Type clT = typeof(T);
            string expression = $"SELECT * FROM {clT.Name} " + where;
            DataSet ds = await GetDataSetAsync(expression);
            return ds.Tables[0].ToListof<T>();
        }

        public async Task<int> UpdateDB<T>(List<T> data)
        {
            int count = 0;
            foreach (T item in data)
            {
                bool isNew = false;
                bool updated = false;
                PropertyInfo[] props = typeof(T).GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    if (prop.Name == "updated")
                        updated = (bool)prop.GetValue(item);
                    else if(prop.Name == "isNew")
                        isNew = (bool)prop.GetValue(item);
                }
                if (isNew)
                    if (await InsertAsync(item))
                        count++;
                if (updated)
                    if (await UpdateAsync(item))
                        count++;
            }
            return count;
        }

        public async Task<bool> InsertAsync<T>(T cl)
        {
            string expression = CreateInsertExpression<T>(cl);
            using (connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                command = new SqlCommand(expression, connection);
                command.Parameters.AddRange(CreateParameters<T>(cl));
                int number = await command.ExecuteNonQueryAsync();
                if (number == 1)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> UpdateAsync<T>(T cl)
        {
            string expression = CreateUpdateExpression<T>(cl);
            using (connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                command = new SqlCommand(expression, connection);
                command.Parameters.AddRange(CreateParametersUpd<T>(cl));
                int number = await command.ExecuteNonQueryAsync();
                if (number == 1)
                    return true;
                else
                    return false;
            }
        }

        private string CreateInsertExpression<T>(T cl)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"INSERT INTO {typeof(T).Name} (");            
            PropertyInfo[] props =  typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "id" && prop.Name != "updated" && prop.Name != "isNew")
                    sb.Append($"{prop.Name}, ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(") VALUES (");
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "id" && prop.Name != "updated" && prop.Name != "isNew")
                    sb.Append($"@{prop.Name}, ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(")");
            return sb.ToString();
        }

        private string CreateUpdateExpression<T>(T cl)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE {typeof(T).Name} SET ");
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "id" && prop.Name != "updated" && prop.Name != "isNew")
                    sb.Append($"{prop.Name} = @{prop.Name}, ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append($" WHERE ");
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == "id")
                    sb.Append($"{prop.Name} = @{prop.Name}");
            }
            return sb.ToString();
        }

        private SqlParameter[] CreateParameters<T>(T cl)
        {
            List<SqlParameter> ps = new List<SqlParameter>();
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "id" && prop.Name != "updated" && prop.Name != "isNew")
                {
                    var val = prop.GetValue(cl);
                    if (val == null)
                        val = DBNull.Value;
                    ps.Add(new SqlParameter($"@{prop.Name}", val));
                }                    
            }
            return ps.ToArray();
        }

        private SqlParameter[] CreateParametersUpd<T>(T cl)
        {
            List<SqlParameter> ps = new List<SqlParameter>();
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if(prop.Name != "updated" && prop.Name != "isNew")
                {
                    var val = prop.GetValue(cl);
                    if (val == null)
                        val = DBNull.Value;
                    ps.Add(new SqlParameter($"@{prop.Name}", val));
                }                    
            }
            return ps.ToArray();
        }
    }

    public static class DataTableExt
    {
        public static List<T> ToListof<T>(this DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                {
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                }
                return instanceOfT;
            }).ToList();

            return targetList;
        }
    }

    public static class ListExt
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable(typeof(T).Name);
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
