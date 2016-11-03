using System;
using System.Collections.Generic;
using System.IO;
using NameGenerator.model;

namespace NameGenerator.datasource
{
    class TextFileSource : NameSource
    {
        public static FileInfo LANGUAGE_FILE = new FileInfo("data/languages.txt");
        public static FileInfo FIRST_NAME_FILE = new FileInfo("data/firstNames.txt");
        public static FileInfo LAST_NAME_FILE = new FileInfo("data/lastNames.txt");

        public static HashSet<NameObject> FirstNames = new HashSet<NameObject>();
        public static HashSet<NameObject> LastNames = new HashSet<NameObject>();

        private HashSet<NameObject> readNames(FileInfo nameFile)
        {
            HashSet<NameObject> names = new HashSet<NameObject>();
            FileStream fs = new FileStream(nameFile.FullName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                String[] tempName = line.Split(',');
                string language = tempName[0];
                string gender = tempName[1];
                string name = tempName[2];

                NameObject obj = new NameObject(language, gender, name);
                names.Add(obj);
            }

            sr.Close();
            fs.Close();

            return names;
        }

        public Dictionary<string, string> LoadLanguages()
        {
            FileStream fs = new FileStream(LANGUAGE_FILE.FullName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            String[] language;
            Dictionary<string, string> languages = new Dictionary<string, string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                language = line.Split(',');
                string languageKey = language[0];
                string languageName = language[1];
                languages.Add(languageKey, languageName);
            }

            sr.Close();
            fs.Close();

            return languages;
        }

        public void LoadFirstNames()
        {
            FirstNames = readNames(FIRST_NAME_FILE);
        }

        public void LoadLastNames()
        {
            LastNames = readNames(LAST_NAME_FILE);
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
