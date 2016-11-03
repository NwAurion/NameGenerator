using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace NameGenerator.GUI
{
    /// <summary>
    /// Interaktionslogik für OldOptionsWindow.xaml
    /// </summary>
    public partial class OldOptionsWindow : UserControl
    {

        #region global variables

        static int MAX_KANJI = 5;
        static FileInfo languageFile = new FileInfo("languages/languages.txt");
        String[] names;
        String[] lastNames;
        string currentKanji;
       
        public static string languageKey = "de";
        public static string genderKey = "male";
        public static string nameKey = "first";

        Random random = new Random();

        #endregion global variables

        public OldOptionsWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            // if we set it in XAML, it leads to an error due to either one of the keys not being set, or the language file already being in use
            radioButtonGenderMale.Checked += radioButtonGender_Checked;
            radioButtonFirstName.Checked += radioButtonName_Checked;


            if (languageFile.Exists)
            {
                FileStream fs = new FileStream(languageFile.FullName, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                String[] language;

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    language = line.Split(',');
                    LanguageKey.Add(language[0]);
                    LanguageName.Add(language[1]);
                    ListBoxItem lbi = new ListBoxItem();
                    lbi.Content = language[1];
                    lbi.Tag = languageKey;
                    listBoxLanguageSelection.Items.Add(lbi);
                }
                listBoxLanguageSelection.SelectedIndex = 0;
               
            }
        }


        #region methods

        private void generateNewName()
        {
            string name = "";
            int index;
            if (languageKey != "jak")
            {

                if (nameKey == "both")
                {
                    string firstName;
                    index = random.Next(0, names.Length);
                    firstName = names[index];
                    index = random.Next(0, lastNames.Length);
                    string lastName = lastNames[index];

                    name = firstName + " " + lastName;
                }
                else if (nameKey == "first")
                {
                    index = random.Next(0, names.Length);
                    name = names[index];
                }
                else if (nameKey == "last")
                {
                    index = random.Next(0, lastNames.Length);
                    name = lastNames[index];
                }
            }
            else
            {
                if (nameKey == "both")
                {

                }
                else if (nameKey == "first")
                {
                    int kanjiCount = random.Next(2, MAX_KANJI);
                    index = random.Next(0, names.Length);
                    currentKanji = names[index];
                    name = currentKanji;
                    string lastKanji = currentKanji;
                    for (int i = 0; i < kanjiCount; i++)
                    {
                        index = random.Next(0, names.Length);
                        currentKanji = names[index];
                        if (currentKanji != lastKanji)
                        {
                            name += currentKanji.ToLowerInvariant();
                        }
                    }
                }
            }
            listBoxGeneratedNames.Items.Add(name);
        }

        private void loadFirstNames()
        {
            FileInfo nameFile;
            if (genderKey == "unisex")
            {
                List<string> tempNames;
                nameFile = new FileInfo("languages/" + languageKey + "_" + "first" + "_" + "male" + ".txt");
                tempNames = readNames(nameFile);
                nameFile = new FileInfo("languages/" + languageKey + "_" + "first" + "_" + "female" + ".txt");
                readNames(nameFile).ForEach(name => tempNames.Add(name));

                names = tempNames.ToArray();
            }
            else
            {
                nameFile = new FileInfo("languages/" + languageKey + "_" + "first" + "_" + genderKey + ".txt");
                names = readNames(nameFile).ToArray();
            }
        }

        private void loadLastNames()
        {
            int index = random.Next(0, listBoxSelectedLanguages.Items.Count);
            languageKey = LanguageKey[index];
            FileInfo nameFile = new FileInfo("languages/" + languageKey + "_" + "last" + ".txt");
            lastNames = readNames(nameFile).ToArray();
        }

        private List<string> readNames(FileInfo nameFile)
        {
            List<string> tempNames = new List<string>();
            FileStream fs = new FileStream(nameFile.FullName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                tempNames.Add(line);
            }
            return tempNames;
        }

        private void loadNames()
        {
            if (listBoxGeneratedNames != null)
            {
                listBoxGeneratedNames.Items.Clear();
            }
            if (nameKey == "both")
            {
                loadFirstNames();
                loadLastNames();
            }
            else if (nameKey == "first")
            {
                loadFirstNames();
            }
            else if (nameKey == "last")
            {
                loadLastNames();
            }
        }

        #endregion methods

        #region properties

        private List<string> _languageName = new List<string>();
        public List<string> LanguageName
        {
            get { return _languageName; }
            set { _languageName = value; }
        }

        private List<string> _languageKey = new List<string>();
        public List<string> LanguageKey
        {
            get { return _languageKey; }
            set { _languageKey = value; }
        }


        private ObservableCollection<string> _generatedNames = new ObservableCollection<string>();
        public ObservableCollection<string> GeneratedNames
        {
            get { return _generatedNames; }
            set { _generatedNames = value; }
        }


        #endregion properties

        #region UI methods

        private void buttonClearList_Click(object sender, RoutedEventArgs e)
        {
            listBoxGeneratedNames.Items.Clear();
        }

        private void buttonGenerateNames_Click(object sender, RoutedEventArgs e)
        {
            generateNewName();
        }

        private void comboBoxLanguageSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = random.Next(0, listBoxSelectedLanguages.Items.Count);
            languageKey = LanguageKey[index];
            loadNames();
        }

        private void radioButtonGender_Checked(object sender, RoutedEventArgs e)
        {
            genderKey = (string)((RadioButton)sender).Tag;
            loadNames();
        }

        private void radioButtonName_Checked(object sender, RoutedEventArgs e)
        {
            nameKey = (string)((RadioButton)sender).Tag;
            if (nameKey == "last")
            {
                groupBoxGenderSelection.IsEnabled = false;
            }
            else
            {
                groupBoxGenderSelection.IsEnabled = true;
            }
            loadNames();
        }

        #endregion UI methods

        private void buttonAddLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxLanguageSelection.SelectedItem != null)
            {
                ListBoxItem lbi = (ListBoxItem)listBoxLanguageSelection.SelectedItem;
                LanguageKey.RemoveAt(listBoxLanguageSelection.SelectedIndex);
                listBoxLanguageSelection.Items.Remove(lbi);
                listBoxSelectedLanguages.Items.Add(lbi);
                LanguageName.Remove(lbi.Content.ToString());
                listBoxLanguageSelection.Items.Refresh();
            }
        }

        private void buttonRemoveLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxSelectedLanguages.SelectedItem != null)
            {
                ListBoxItem lbi = (ListBoxItem)listBoxSelectedLanguages.SelectedItem;
                listBoxSelectedLanguages.Items.Remove(lbi);
                listBoxLanguageSelection.Items.Add(lbi);
                LanguageName.Add(lbi.Content.ToString());
                LanguageKey.Add(lbi.Tag.ToString());
                listBoxLanguageSelection.Items.Refresh();
            }
        }
    }
}