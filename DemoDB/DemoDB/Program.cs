using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DemoDB
{
    class Program
    {

        static void Main(string[] args)
        {
            SQLiteConnection sqliteConn;
            sqliteConn = CreateConnection();
            CreateTables(sqliteConn);
            //InsertData(sqliteConn);
            ReadData(sqliteConn);
        }

        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqliteConn;
            sqliteConn = new SQLiteConnection("Data Source=pamyatayka.db;Version=3;New=True;Compress=True;");

            try
            {
                sqliteConn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sqliteConn;
        }

        static void CreateTables(SQLiteConnection conn)
        {
            SQLiteCommand sqliteCmd;
            sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = DatabaseProperties.Default.CreateIntervals;
            sqliteCmd.ExecuteNonQuery();
            sqliteCmd.CommandText = DatabaseProperties.Default.CreateStatuses;
            sqliteCmd.ExecuteNonQuery();
            sqliteCmd.CommandText = DatabaseProperties.Default.CreateCategories;
            sqliteCmd.ExecuteNonQuery();
            sqliteCmd.CommandText = DatabaseProperties.Default.CreateGrades;
            sqliteCmd.ExecuteNonQuery();
            sqliteCmd.CommandText = DatabaseProperties.Default.CreateWords;
            sqliteCmd.ExecuteNonQuery();
        }
        static void InsertData(SQLiteConnection conn)
        {
            RandomGenerator randomGenerator = new RandomGenerator();
            SQLiteCommand sqliteCmd;
            sqliteCmd = conn.CreateCommand();
            for (int i = 0; i < 30; i++)
            {
                string categoryId = randomGenerator.RandomString(7);
                string gradeId = randomGenerator.RandomString(7);
                string intervalsId = randomGenerator.RandomString(7);
                string wordId = randomGenerator.RandomString(7);
                string statusId = randomGenerator.RandomString(7);
                sqliteCmd.CommandText = "INSERT INTO Intervals (interval_id, name) " +
                    "VALUES ('" + intervalsId + "', '" + randomGenerator.RandomString(10) + "');";
                sqliteCmd.ExecuteNonQuery();
                sqliteCmd.CommandText = "INSERT INTO Categories (category_id, name, is_default, interval_fk) " +
                    "VALUES ('" + categoryId + "', '" + randomGenerator.RandomString(10) + "', false, '" + intervalsId + "');";
                sqliteCmd.ExecuteNonQuery();
                sqliteCmd.CommandText = "INSERT INTO Statuses (status_id, status) " +
                    "VALUES ('" + statusId + "', '" + randomGenerator.RandomString(10) + "');";
                sqliteCmd.ExecuteNonQuery();
                sqliteCmd.CommandText = "INSERT INTO Grades (grade_id, name, interval_time, position, interval_fk) " +
                    "VALUES ('" + gradeId + "', '" +
                    randomGenerator.RandomString(10) + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" +
                    + new Random().Next(0, 6) + "', '" + intervalsId + "');";
                sqliteCmd.ExecuteNonQuery();
                sqliteCmd.CommandText = "INSERT INTO Words (word_id, term, definition, status_fk, category_fk, current_grade_fk) " +
                    "VALUES ('" + wordId + "', '" +
                    randomGenerator.RandomString(10) + "', '" + randomGenerator.RandomString(7) + "', '" +
                    statusId + "', '" + categoryId + "', '" + gradeId + "');";
                sqliteCmd.ExecuteNonQuery();
            }

        }
        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqliteDatareader;
            SQLiteCommand sqliteCmd;
            sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = "SELECT * FROM Categories;";
            Console.WriteLine("Categories\n");
            sqliteDatareader = sqliteCmd.ExecuteReader();
            while (sqliteDatareader.Read())
            {
                string myreader = sqliteDatareader.GetString(0) +
                    " " + sqliteDatareader.GetString(1) + " " +
                    " " + sqliteDatareader.GetBoolean(2).ToString() + " " + sqliteDatareader.GetString(3);
                Console.WriteLine(myreader);
            }
            Console.ReadLine();
            sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = "SELECT * FROM Grades;";
            Console.WriteLine("Grades\n");
            sqliteDatareader = sqliteCmd.ExecuteReader();
            while (sqliteDatareader.Read())
            {
                string myreader = sqliteDatareader.GetString(0) +
                    " " + sqliteDatareader.GetString(1) + " " + sqliteDatareader.GetDateTime(2).ToString() + " "
                    + sqliteDatareader.GetInt32(3) + " " + sqliteDatareader.GetString(4);
                Console.WriteLine(myreader);
            }
            Console.ReadLine();
            sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = "SELECT * FROM Intervals;";
            sqliteDatareader.Read();
            Console.WriteLine("Intervals\n");
            sqliteDatareader = sqliteCmd.ExecuteReader();
            while (sqliteDatareader.Read())
            {
                string myreader = sqliteDatareader.GetString(0) +
                    " " + sqliteDatareader.GetString(1);
                Console.WriteLine(myreader);
            }
            Console.ReadLine();
            sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = "SELECT * FROM Words;";
            sqliteDatareader.Read();
            Console.WriteLine("Words\n");
            sqliteDatareader = sqliteCmd.ExecuteReader();
            while (sqliteDatareader.Read())
            {
                string myreader = sqliteDatareader.GetString(0) +
                    " " + sqliteDatareader.GetString(1) + " " + sqliteDatareader.GetString(2) +
                    " " + sqliteDatareader.GetString(3) + " " + sqliteDatareader.GetString(4) + " " + sqliteDatareader.GetString(5);
                Console.WriteLine(myreader);
            }
            Console.ReadLine();
            sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = "SELECT * FROM Statuses;";
            sqliteDatareader.Read();
            Console.WriteLine("Statuses\n");
            sqliteDatareader = sqliteCmd.ExecuteReader();
            while (sqliteDatareader.Read())
            {
                string myreader = sqliteDatareader.GetString(0) +
                    " " + sqliteDatareader.GetString(1);
                Console.WriteLine(myreader);
            }
            Console.ReadLine();
            sqliteCmd.Reset();
            conn.Close();

        }

        public class RandomGenerator
        {
            // Instantiate random number generator.  
            // It is better to keep a single Random instance 
            // and keep using Next on the same instance.  
            private readonly Random _random = new Random();

            // Generates a random string with a given size.    
            public string RandomString(int size, bool lowerCase = false)
            {
                var builder = new StringBuilder(size);

                // Unicode/ASCII Letters are divided into two blocks
                // (Letters 65–90 / 97–122):   
                // The first group containing the uppercase letters and
                // the second group containing the lowercase.  

                // char is a single Unicode character  
                char offset = lowerCase ? 'a' : 'A';
                const int lettersOffset = 26; // A...Z or a..z: length = 26  

                for (var i = 0; i < size; i++)
                {
                    var @char = (char)_random.Next(offset, offset + lettersOffset);
                    builder.Append(@char);
                }

                return lowerCase ? builder.ToString().ToLower() : builder.ToString();
            }
        }
    }
}
