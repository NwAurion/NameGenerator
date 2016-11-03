using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NameGenerator.datasource;
using NameGenerator.extension;
using NameGenerator.model;

namespace NameGenerator.datamanager
{
    /// <summary>
    /// Interaktionslogik für DataManager.xaml
    /// </summary>
    public partial class DataManager : Window
    {
        public DataManager()
        {
            InitializeComponent();
        }

        private void ListBoxLanguage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //  if(e.OriginalSource == TextBox){
            // }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ListBoxLanguage.Items.Clear();
            ListBoxGender.Items.Clear();
            ListBoxName.Items.Clear();

            switch ((sender as FrameworkElement).Name)
            {
                case "radioButton_FirstNames":
                    ShowFirstNames();
                    return;
                case "radioButton_LastNames":
                    ShowLastNames();
                    return;
                case "radioButton_Languages":
                    ShowLanguages();
                    return;
            }
        }

        private void ShowLanguages()
        {
            Dictionary<string, string> languages = Generator.NameSource.LoadLanguages();

            foreach (KeyValuePair<string, string> language in languages)
            {
                string languageKey = language.Key;
                string languageName = language.Value;

                TextBox tempBox = new TextBox() { Text = languageKey, IsManipulationEnabled = false, BorderThickness = new Thickness(0), Width = 100 };
                ListBoxLanguage.Items.Add(tempBox);
                tempBox = new TextBox() { Text = languageName, BorderThickness = new Thickness(0) };
                ListBoxName.Items.Add(tempBox);
            }
        }

        private void ShowFirstNames()
        {/*
            HashSet<NameObject> firstNames = Generator.nameSource.LoadFirstNames();
            foreach (NameObject obj in firstNames)
            {
                string language = obj.language;
                string gender = obj.gender;
                string name = obj.name;

                TextBox tempBox = new TextBox() { Text = language, IsManipulationEnabled = false, BorderThickness = new Thickness(0), Width = 100 };
                ListBoxLanguage.Items.Add(tempBox);
                tempBox = new TextBox() { Text = gender, BorderThickness = new Thickness(0) };
                ListBoxGender.Items.Add(tempBox);
                tempBox = new TextBox() { Text = name, BorderThickness = new Thickness(0) };
                ListBoxName.Items.Add(tempBox);
            }*/
        }

        private void ShowLastNames()
        {
            /*HashSet<NameObject> lastNames = Generator.nameSource.LoadLastNames();

            foreach (NameObject obj in lastNames)
            {
                string language = obj.language;
                string name = obj.name;

                TextBox tempBox = new TextBox() { Text = language, IsManipulationEnabled = false, BorderThickness = new Thickness(0), Width = 100 };
                ListBoxLanguage.Items.Add(tempBox);
                tempBox = new TextBox() { Text = name, BorderThickness = new Thickness(0) };
                ListBoxName.Items.Add(tempBox);
            }*/
        }

        private void button_AddLastName_Click(object sender, RoutedEventArgs e)
        {
            panel_AddName.Visibility = Visibility.Visible;
        }

        private void button_AddFirstName_Click(object sender, RoutedEventArgs e)
        {
            panel_AddName.Visibility = Visibility.Visible;

        }

        private void button_SubmitFirstName_Click(object sender, RoutedEventArgs e)
        {
            string language = textBoxLanguage.Text;
            string gender = textBoxGender.Text;
            string name = textBoxName.Text;

            if (!language.NullOrEmpty() && !gender.NullOrEmpty() && !name.NullOrEmpty())
            {
                (Generator.NameSource as SQLiteSource).FillTable_FirstNames(language, gender, name);
            }
        }

        private void button_SubmitLastName_Click(object sender, RoutedEventArgs e)
        {
            string language = textBoxLanguage.Text;
            string name = textBoxName.Text;

            if (!language.NullOrEmpty() && !name.NullOrEmpty())
            {
                (Generator.NameSource as SQLiteSource).FillTable_LastNames(language, name);
            }
        }
    }
}
