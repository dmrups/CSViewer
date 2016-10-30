using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CSVOnlineEditor.Interfaces;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Data.Common;
using CSVOnlineEditor.Helpers;

namespace CSVOnlineEditor.Data.DBConnectionProviders
{
    public class DefaultConnectionProvider : IConnectionProvider
    {
        private const string DBName = "CSVOnlineEditor";
        private const string ServerConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB";
        private readonly string DBFilePath = $@"{Directory.GetCurrentDirectory()}\Storage.mdf";
        private readonly string LogFilePath = $@"{Directory.GetCurrentDirectory()}\Storage.ldf";
        private string DBConnectionString => $@"{ServerConnectionString};AttachDbFilename={DBFilePath};Integrated Security=True;Connect Timeout=30";

        //private readonly HashSet<Type> Models;
        //private readonly Dictionary<Type, string> typesMapping = new Dictionary<Type, string>()
        //{
        //    { typeof(string), "nvarchar(50)" },
        //    { typeof(int), "int" },
        //    { typeof(DateTime), "datetime" }
        //};

        public DefaultConnectionProvider()
        {
            //PrepareDB();
            //Models = new HashSet<Type>(ReflectionHelper.GetModels());

            //if (!CheckDB())
            //{
            //    PrepareDB();
            //}
        }

        public DbConnection GetConnection()
        {
            return new SqlConnection(DBConnectionString);
        }
        
        //public void Dispose()
        //{
        //    //Connection.Dispose();
        //}

        //private string GetSelectScript(Type type)
        //{
        //    return $"SELECT * FROM {type.Name}";
        //}

        //private DataTable CreateDataTable(Type type, IEnumerable<PropertyInfo> properties)
        //{
        //    DataTable table = new DataTable(type.Name);

        //    foreach (var property in properties)
        //    {
        //        table.Columns.Add(new DataColumn
        //        {
        //            ColumnName = property.Name,
        //            DataType = property.PropertyType
        //        });
        //    }

        //    return table;
        //}

        //private void FillDataTable<T>(DataTable table, IEnumerable<T> collection, IEnumerable<PropertyInfo> properties)
        //{
        //    foreach (var entity in collection)
        //    {
        //        FillDataTable(table, entity, properties);
        //    }
        //}

        //private void FillDataTable<T>(DataTable table, T entity, IEnumerable<PropertyInfo> properties)
        //{
        //    var dataRow = table.NewRow();

        //    foreach (var property in properties)
        //    {
        //        dataRow[property.Name] = property.GetValue(entity) ?? DBNull.Value;
        //    }

        //    table.Rows.Add(dataRow);
        //}

        //private void Insert<T>(IEnumerable<T> collection)
        //{
        //    var type = typeof(T);
        //    var properties = type.GetRuntimeProperties()
        //        .Where(item => item.Name != nameof(IStorageEntity.Id));

        //    var dataTable = CreateDataTable(type, properties);
        //    FillDataTable(dataTable, collection, properties);

        //    Insert(dataTable);
        //}

        //private void Insert(DataTable table)
        //{
        //    using (var dbConnection = new SqlConnection(DBConnectionString))
        //    {
        //        dbConnection.Open();

        //        using (var bulkCopy = new SqlBulkCopy(dbConnection))
        //        {
        //            bulkCopy.DestinationTableName = table.TableName;

        //            foreach (DataColumn column in table.Columns)
        //            {
        //                bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
        //            }

        //            bulkCopy.WriteToServer(table);
        //        }
        //    }
        //}

        //private bool CheckDB()
        //{
        //    return false;

        //    // TODO: check real table structure instead of trying insert

        //    //try
        //    //{
        //    //    using (var dbConnection = new SqlConnection(DBConnectionString))
        //    //    {
        //    //        dbConnection.Open();

        //    //        using (var transaction = dbConnection.BeginTransaction())
        //    //        {
        //    //            foreach (var model in Models)
        //    //            {
        //    //                var entity = Activator.CreateInstance(model);
        //    //                var properties = model.GetRuntimeProperties()
        //    //                    .Where(item => item.Name != nameof(IStorageEntity.Id));

        //    //                var table = CreateDataTable(model, properties);
        //    //                FillDataTable(table, entity, properties);
        //    //                Insert(table);
        //    //            }
        //    //        }
        //    //    }

        //    //    return true;
        //    //}
        //    //catch(Exception ex)
        //    //{
        //    //    return false;
        //    //}
        //}

        //private List<string> GetTables()
        //{
        //    DataTable schema = Connection.GetSchema("Tables");
        //    List<string> TableNames = new List<string>();

        //    foreach (DataRow row in schema.Rows)
        //    {
        //        TableNames.Add(row[2].ToString());
        //    }

        //    return TableNames;
        //}

        //private List<Tuple<string, Type>> GetColumns()
        //{
        //    using (var cmd = new SqlCommand("SELECT TOP 1 * FROM Applicant", Connection))
        //    {
        //        var reader = cmd.ExecuteReader(CommandBehavior.KeyInfo);
        //        var schema = reader.GetSchemaTable();

        //        return new List<Tuple<string, Type>>();
        //    }
        //}

        private void PrepareDB()
        {
            using (var serverConnection = new SqlConnection(ServerConnectionString))
            {
                serverConnection.Open();

                new SqlCommand(GetDropDatabaseScript(), serverConnection).ExecuteNonQuery();
                RemoveDBFiles();
                new SqlCommand(GetCreateDBScript(), serverConnection).ExecuteNonQuery();
                //new SqlCommand(GetTablesScript(), serverConnection).ExecuteNonQuery();
            }
        }

        //private string GetDropTablesScript()
        //{
        //    var str = new StringBuilder();
        //    foreach(var model in Models)
        //    {
        //        str.Append($@"
        //            IF OBJECT_ID('{model.Name}', 'U') IS NOT NULL 
        //            DROP TABLE {model.Name} 
        //            ");
        //    }

        //    return str.ToString();
        //}

        private string GetDropDatabaseScript()
        {
            return $@"
                USE master
                IF EXISTS(SELECT * from sys.databases where name='{DBName}')
                BEGIN
                    ALTER DATABASE {DBName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE [{DBName}]
                END";
        }

        private void RemoveDBFiles()
        {
            if ((new FileInfo(DBFilePath)).Exists)
            {
                File.Delete(DBFilePath);
            }

            if ((new FileInfo(LogFilePath)).Exists)
            {
                File.Delete(LogFilePath);
            }
        }

        private string GetCreateDBScript()
        {
            return $@"
                CREATE DATABASE
                    [{DBName}]
                ON PRIMARY (
                   NAME=Test_data,
                   FILENAME = '{DBFilePath}'
                )
                LOG ON (
                    NAME=Test_log,
                    FILENAME = '{LogFilePath}'
                )";
        }

        //private string GetTablesScript()
        //{
        //    var str = new StringBuilder();

        //    str.Append($"USE [{DBName}] {Environment.NewLine}");

        //    foreach (var modelType in Models)
        //    {
        //        str.Append(GetTableScript(modelType));
        //    }

        //    return str.ToString();
        //}

        //private string GetTableScript(Type type)
        //{
        //    var str = new StringBuilder();
        //    str.Append($@"
        //        CREATE TABLE {type.Name} ( 
        //        {nameof(IStorageEntity.Id)} int IDENTITY(1,1) PRIMARY KEY, 
        //        ");

        //    foreach(var propertyInfo in type.GetRuntimeProperties().Where(item => item.Name != nameof(IStorageEntity.Id)))
        //    {
        //        str.Append($"{propertyInfo.Name} {typesMapping[propertyInfo.PropertyType]}, {Environment.NewLine}");
        //    }

        //    str.Append(")");

        //    return str.ToString();
        //}
    }
}
