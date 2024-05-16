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
using System.Windows.Shapes;

namespace snake_Game
{
    /// <summary>
    /// Logika interakcji dla klasy Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void SmallMap(object sender, RoutedEventArgs e)
        {
            Window smallMap = new MainWindow();
            smallMap.ShowDialog();
        }

        private void MediumMap(object sender, RoutedEventArgs e)
        {
            Window mediumMap = new MediumMapWindow();
            mediumMap.ShowDialog();
        }

        private void LargeMap(object sender, RoutedEventArgs e)
        {
            Window largeMap = new LargeMapWindow();
            largeMap.ShowDialog();
        }

        
    }
}
