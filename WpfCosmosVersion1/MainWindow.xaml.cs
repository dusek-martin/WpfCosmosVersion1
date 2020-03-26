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
using System.Windows.Threading;

namespace WpfCosmosVersion1
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CosmosEventHandler cosmosEventHandler;

        public MainWindow()
        {
            InitializeComponent();
            
            cosmosEventHandler = new CosmosEventHandler(canvasGame, this);
        }

        private void buttonAddPlanet_Click(object sender, RoutedEventArgs e)
        {
            cosmosEventHandler.buttonAddPlanet_Click(sender, e);
        }

        private void buttonAddAsteroid_Click(object sender, RoutedEventArgs e)
        {
            cosmosEventHandler.buttonAddAsteroid_Click(sender, e);
        }

        private void buttonGravity_Click(object sender, RoutedEventArgs e)
        {
            cosmosEventHandler.buttonGravity_Click(sender, e);
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            cosmosEventHandler.buttonStart_Click(sender, e);
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            cosmosEventHandler.buttonStop_Click(sender, e);
        }

        private void buttonRemovePlanets_Click(object sender, RoutedEventArgs e)
        {
            cosmosEventHandler.buttonRemovePlanets_Click(sender, e);
        }
        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            cosmosEventHandler.buttonUpdate_Click(sender, e);
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            cosmosEventHandler.buttonClose_Click(sender, e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            cosmosEventHandler.OnKeyDownHandler(sender, e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            cosmosEventHandler.OnKeyUpHandler(sender, e);
        }
    }
}
