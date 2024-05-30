using System;
using System.Configuration;
using Microsoft.Data.Sqlite;
// UNFINISHED
namespace DatabaseManagement
{

    internal class DatabaseManager
    {
        internal void CreateTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS sidik_jari (
                        berkas_citra TEXT,
                        nama VARCHAR(100)
                    );

                    CREATE TABLE IF NOT EXISTS biodata (
                        NIK VARCHAR(16) PRIMARY KEY NOT NULL,
                        nama VARCHAR(100),
                        tempat_lahir VARCHAR(50),
                        tanggal_lahir DATE,
                        jenis_kelamin TEXT CHECK(jenis_kelamin IN ('Laki-Laki', 'Perempuan'))
                    );
                    ";
                    
                    tableCmd.ExecuteNonQuery();
                }
            }
        }
    }

    /*
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            GetUserInput getUserInput = new();
            databaseManager.CreateTable(DatabaseConnector.ConnectionString);
            Console.WriteLine("Tables created successfully.");
            getUserInput.MainMenu();
        }
    }
    */
    class DatabaseConnector
    {
        public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
    }

    class GetUserInput
    {
        public void MainMenu()
        {
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close App");
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\n\nMAIN MENU");
            }
        }
    }
}
