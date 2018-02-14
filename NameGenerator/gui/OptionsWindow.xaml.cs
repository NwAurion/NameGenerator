using NameGenerator.datamanager;
using NameGenerator.model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NameGenerator.GUI
{
    /// <summary>
    /// Interaktionslogik für OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : UserControl
    {

        static protected int MAX_AT_ONCE = 20;

        public OptionsWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        #region properties

        public Dictionary<string, string> LanguagesAvailable
        {
            get { return Generator.LanguagesAvailable; }
            set { Generator.LanguagesAvailable = value; }
        }

        public Dictionary<string, string> LanguagesSelected
        {
            get { return Generator.LanguagesSelected; }
            set { Generator.LanguagesSelected = value; }
        }

        #endregion properties

        #region UI methods

        private void buttonClearList_Click(object sender, RoutedEventArgs e)
        {
            listBoxGeneratedNames.Items.Clear();
        }

        private void buttonGenerateNames_Click(object sender, RoutedEventArgs e)
        {
            string name = Generator.GenerateNewName();

            listBoxGeneratedNames.Items.Add(name);
        }

        private void radioButtonGender_Checked(object sender, RoutedEventArgs e)
        {
            Generator.UsedGender = (string)((RadioButton)sender).Tag;
            if (listBoxGeneratedNames != null)
            {
                listBoxGeneratedNames.Items.Clear();
            }
        }

        private void radioButtonName_Checked(object sender, RoutedEventArgs e)
        {
            string firstOrLastName = (string)((RadioButton)sender).Tag;
            if (firstOrLastName == "last")
            {
                groupBoxGenderSelection.IsEnabled = false;

                Generator.DoGenerateFirstName = false;
                Generator.DoGenerateLastName = true;
            }
            else
            {
                if (firstOrLastName == "both")
                {
                    Generator.DoGenerateLastName = true;
                }
                else
                {
                    Generator.DoGenerateLastName = false;
                }
                Generator.DoGenerateFirstName = true;
                groupBoxGenderSelection.IsEnabled = true;
            }
            if (listBoxGeneratedNames != null)
            {
                listBoxGeneratedNames.Items.Clear();
            }
        }

        private void buttonAddLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxLanguageSelection.SelectedItem != null)
            {
                KeyValuePair<string, string> selectedItem = (KeyValuePair<string, string>)listBoxLanguageSelection.SelectedItem;

                string languageKey = selectedItem.Key;
                string languageName = selectedItem.Value;

                Generator.LanguagesAvailable.Remove(languageKey);
                Generator.LanguagesSelected.Add(languageKey, languageName);

                listBoxLanguageSelection.Items.Refresh();
                listBoxSelectedLanguages.Items.Refresh();


                if (listBoxSelectedLanguages.Items.Count > 0)
                {
                    buttonGenerateNames.IsEnabled = true;
                    buttonGeneratoreMany.IsEnabled = true;
                }
            }
        }

        private void buttonRemoveLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxSelectedLanguages.SelectedItem != null)
            {
                KeyValuePair<string, string> selectedItem = (KeyValuePair<string, string>)listBoxSelectedLanguages.SelectedItem;

                string languageKey = selectedItem.Key;
                string languageName = selectedItem.Value;

                LanguagesSelected.Remove(languageKey);
                LanguagesAvailable.Add(languageKey, languageName);

                listBoxLanguageSelection.Items.Refresh();
                listBoxSelectedLanguages.Items.Refresh();

                if (listBoxSelectedLanguages.Items.Count == 0)
                {
                    buttonGenerateNames.IsEnabled = false;
                    buttonGeneratoreMany.IsEnabled = false;
                }
            }
        }
        private void buttonGeneratoreMany_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < MAX_AT_ONCE; i++)
            {
                string name = Generator.GenerateNewName();

                listBoxGeneratedNames.Items.Add(name);
            }
        }

        private void listBoxGeneratedNames_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            string text = "";

            foreach (string name in listBoxGeneratedNames.SelectedItems)
            {
                text += name + "\n";
            }

            Clipboard.SetText(text);
        }

        private void buttonManageData_Click(object sender, RoutedEventArgs e)
        {
            DataManager manager = new DataManager();
            manager.Show();
        }


        #endregion UI methods



    }
}