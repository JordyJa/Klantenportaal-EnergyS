using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Vragen_en_klachten
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string HuidigeGebruiker { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Uitlogknop_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Inlogscherm));
        }

        private void Profielknop_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Profielgegevens));
        }

        private void Wachtwoordknop_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Instellingen),HuidigeGebruiker);
        }

        private void Contactknop_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Klantenportaal_EnergyS.VragenKlachten));
        }

        private void Abonnementknop_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage1));
        }

        private void Overzichtknop_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Overzicht), HuidigeGebruiker);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           HuidigeGebruiker = (Application.Current as App).GebruikerString;
        }
    }
}
