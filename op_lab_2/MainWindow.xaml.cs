using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace op_lab_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void studentsButton_Click(object sender, RoutedEventArgs e)
        {
            studentWindow sw = new studentWindow();
        }

        private void meButton_Click(object sender, RoutedEventArgs e)
        {
            aboutmeWindow amw = new aboutmeWindow();
        }

        private void gameButton_Click(object sender, RoutedEventArgs e)
        {
            gameWindow gw = new gameWindow();
        }

        private void calcButton_Click(object sender, RoutedEventArgs e)
        {
            calculatorWindow cw = new calculatorWindow();
        }
    }
}