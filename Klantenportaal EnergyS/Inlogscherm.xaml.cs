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
using Windows.Storage;
using System.Diagnostics;
using System.Globalization;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Klantenportaal_EnergyS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Inlogscherm : Page
    {
        public Inlogscherm()
        {
            this.InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string Geb_nummer = Gebruikersnummer.Text;
            string WW_gebruiker = Wachtwoord.Text;
            Debug.WriteLine(Geb_nummer);
            Debug.WriteLine(WW_gebruiker);
            //vanuit install directory
            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await installedLocation.GetFileAsync("Login gegevens klant dimensie 1.3.csv");
            using (CSVparse.CsvFileReader csvReader = new CSVparse.CsvFileReader(await file.OpenStreamForReadAsync()))
            {
                CSVparse.CsvRow row = new CSVparse.CsvRow();
                while (csvReader.ReadRow(row))
                {
                    string NieuweRow = "";
                    for(int i = 0; i <row.Count; i++)
                    {
                        NieuweRow += row[i] + ",";
                        Debug.WriteLine(NieuweRow);
                        // een list maken die de rows een voor een opslaat
                        List<string> GebGegevens = new List<string>(new string[] {NieuweRow});
                        Debug.WriteLine(GebGegevens[i]);
                        //gebruikersnummers vergelijken tegen invoer
                        if (Geb_nummer.Substring(0, Geb_nummer.Length) == NieuweRow.Substring(2, Geb_nummer.Length) && WW_gebruiker.Substring(0, WW_gebruiker.Length) == NieuweRow.Substring(1 + Geb_nummer.Length + 1, WW_gebruiker.Length))
                        {
                            Frame.Navigate(typeof(MainPage)); //fix deze check!
                        }
                        else
                        {
                            //display error message
                        }

                    }
                }
            }
        }
    }
}
