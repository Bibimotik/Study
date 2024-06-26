using Npgsql;
using System;
using System.Configuration;
using System.IO;

namespace lab08
{
    public static class SQLClass
    {
        internal static NpgsqlConnection conn = new NpgsqlConnection
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString
        };
        
        internal static NpgsqlConnection mainconn = new NpgsqlConnection
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString
        };

        public static void OpenConnection()
        {
            if (!test_connection())
            {
                if (!DatabaseExists(mainconn))
                {
                    mainconn.Close();
                    CreateDatabase(mainconn);
                    Console.WriteLine($"База данных lab08 успешно создана.");
                    mainconn.Close();
                    mainconn.Open();
                    open_conn();
                    ExecuteScriptFromFile();
                }
                else
                {
                    Console.WriteLine($"База данных lab08 уже существует.");
                }
            }
            else
            {
                Console.WriteLine("ALL GOOD");
            }
        }

        public static void CloseConnection()
        {
            conn.Close();
            mainconn.Close();
        }

        public static bool test_connection()
        {
            try
            {
                conn.Open();
                Console.WriteLine("Соединение установлено успешно.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при установке соединения: {ex.Message}");
                return false;
            }
        }

        static bool DatabaseExists(NpgsqlConnection connection)
        {
            mainconn.Open();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"SELECT 1 FROM pg_database WHERE datname = 'lab08'";
                return command.ExecuteScalar() != null;
            }
        }

        static void CreateDatabase(NpgsqlConnection connection)
        {
            mainconn.Open();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"CREATE DATABASE lab08";
                command.ExecuteNonQuery();
            }
        }

        private static void open_conn()
        {
            conn.Open();
        }
        
        public static void ExecuteScriptFromFile()
        {
            try
            {
                string script = File.ReadAllText("D:\\Учеба\\4_семестр\\ООП\\lab08\\lab08.sql");

                using (var command = new NpgsqlCommand(script, conn))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Скрипт успешно выполнен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении скрипта: {ex.Message}");
            }
        }
    }
}