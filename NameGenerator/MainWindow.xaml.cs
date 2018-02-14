using NameGenerator.model;
using System.Windows;

namespace NameGenerator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            // Generator generator = new Generator();
            InitializeComponent();

            Generator generator = new Generator();
        }
    }
}
