using System.Collections.Generic;
using System.Data.SQLite;
using NameGenerator.model;

namespace NameGenerator.datasource
{
    class SQLiteSource : NameSource
    {
        static protected string connectionString = "Data Source=names.sqlite;Version=3;";
        static SQLiteConnection m_dbConnection;
        public static HashSet<NameObject> FirstNames = new HashSet<NameObject>();
        public static HashSet<NameObject> LastNames = new HashSet<NameObject>();

        public SQLiteSource()
        {
            m_dbConnection = new SQLiteConnection(connectionString);
        }


        public Dictionary<string, string> LoadLanguages()
        {
            string sql = "SELECT * FROM language";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            m_dbConnection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            Dictionary<string, string> languages = new Dictionary<string, string>();


            while (reader.Read())
            {
                string languageKey = reader.GetString(0);
                string languageName = reader.GetString(1);
                languages.Add(languageKey, languageName);
            }
            m_dbConnection.Close();

            return languages;

        }

        public void LoadFirstNames()
        {
            string sql = "SELECT * FROM FIRSTNAMES";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            m_dbConnection.Open();
            SQLiteDataReader reader = command.ExecuteReader();

            HashSet<NameObject> names = new HashSet<NameObject>();
            while (reader.Read())
            {
                string language = reader.GetString(0);
                string gender = reader.GetString(1);
                string name = reader.GetString(2);

                NameObject obj = new NameObject(language, gender, name);
                names.Add(obj);

            }
            m_dbConnection.Close();

            FirstNames = names;
        }

        public void LoadLastNames()
        {
            string sql = "SELECT * FROM LASTNAMES";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            m_dbConnection.Open();
            SQLiteDataReader reader = command.ExecuteReader();

            HashSet<NameObject> names = new HashSet<NameObject>();
            while (reader.Read())
            {
                string language = reader.GetString(0);
                string name = reader.GetString(1);

                NameObject obj = new NameObject(language, null, name);
                names.Add(obj);

            }
            m_dbConnection.Close();

            LastNames = names;
        }


        public void FillTable_Languages(string languageKey, string languageName)
        {
            string sql = "insert into language (key, name) values (\'" + languageKey + "\',\'" + languageName + "\')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            m_dbConnection.Open();
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        public void FillTable_FirstNames(string language, string gender, string name)
        {
            string sql = "insert into FirstNames (language, gender, name) values (\'" + language + "\',\'" + gender + "\',\'" + name + "\')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            m_dbConnection.Open();
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        public void FillTable_LastNames(string language, string name)
        {
            string sql = "insert into LastNames (language, name) values (\'" + language + "\',\'" + name + "\')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            m_dbConnection.Open();
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        private void CreateTable_Language()
        {
            string sql = "CREATE TABLE language(key varchar(3), name varchar(20))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            m_dbConnection.Open();
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        private void CreateTable_FirstNames()
        {
            string sql = "CREATE TABLE FirstNames (language varchar(3), gender varchar(1), name varchar(30))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            m_dbConnection.Open();
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        private void CreateTable_LastNames()
        {
            string sql = "CREATE TABLE LastNames (language varchar(3), name varchar(30))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            m_dbConnection.Open();
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        public HashSet<NameObject> GetFirstNames()
        {
            return FirstNames;
        }

        public HashSet<NameObject> GetLastNames()
        {
            return LastNames;
        }

    }
}
