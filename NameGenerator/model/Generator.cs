using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NameGenerator.datasource;

namespace NameGenerator.model
{
    class Generator
    {
        public static NameSource NameSource;

        public static HashSet<NameObject> FirstNames;
        public static HashSet<NameObject> LastNames;

        public static bool DoGenerateFirstName;
        public static bool DoGenerateLastName;
        public static string UsedGender;

        static Random random = new Random();

        private readonly BackgroundWorker worker = new BackgroundWorker();

        public Generator()
        {
            //nameSource = new SQLiteSource();
            NameSource = new TextFileSource();

            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            LanguagesAvailable = NameSource.LoadLanguages();
            LanguagesSelected = new Dictionary<string, string>();

            LanguagesSelected.Add(LanguagesAvailable.First().Key, LanguagesAvailable.First().Value);
            LanguagesAvailable.Remove(LanguagesAvailable.First().Key);

            worker.RunWorkerAsync();
        }

        #region methods

        public static string GenerateNewName()
        {
            string firstName = "";
            string lastName = "";

            if (DoGenerateFirstName)
            {
                string language = ChooseLanguage();
                firstName = GenerateFirstName(language, UsedGender);
            }
            if (DoGenerateLastName)
            {
                string language = ChooseLanguage();
                lastName = GenerateLastName(language);
            }

            string name = firstName + " " + lastName;

            return name;

        }

        private static string ChooseLanguage()
        {
            int languageIndex = random.Next(0, LanguagesSelected.Count);
            IEnumerator<KeyValuePair<string, string>> e = LanguagesSelected.GetEnumerator();

            int runningIndex = 0;

            e.MoveNext();
            while (runningIndex < languageIndex && e.MoveNext())
            {
                runningIndex++;
            }

            string languageKey = e.Current.Key;

            return languageKey;

        }

        private static string GenerateFirstName(string language, string gender)
        {
            IEnumerable<NameObject> firstNamesFilteredByLanguage = FirstNames.Where(elem => elem.language == language);
            IEnumerable<NameObject> firstNamesFilteredByLanguageAndGender;

            if (gender != "u")
            {
                firstNamesFilteredByLanguageAndGender = firstNamesFilteredByLanguage.Where(elem => elem.gender == gender);
            }
            else
            {
                firstNamesFilteredByLanguageAndGender = firstNamesFilteredByLanguage;
            }

            int elementCount = firstNamesFilteredByLanguageAndGender.Count();
            int index = random.Next(0, elementCount);
            string name = firstNamesFilteredByLanguageAndGender.ElementAt(index).name;

            return name;
        }

        private static string GenerateLastName(string language)
        {
            IEnumerable<NameObject> lastNamesFilteredByLanguage = LastNames.Where(elem => elem.language == language);

            int elementCount = lastNamesFilteredByLanguage.Count();
            int index = random.Next(0, elementCount);

            string name = lastNamesFilteredByLanguage.ElementAt(index).name;

            return name;
        }

        #endregion methods

        #region properties

        public static string LatestName { get; set; }

        public static Dictionary<string, string> LanguagesAvailable { get; set; }

        public static Dictionary<string, string> LanguagesSelected { get; set; }

        #endregion properties


        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            NameSource.LoadFirstNames();
            NameSource.LoadLastNames();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FirstNames = NameSource.GetFirstNames();
            LastNames = NameSource.GetLastNames();
        }
    }
}
