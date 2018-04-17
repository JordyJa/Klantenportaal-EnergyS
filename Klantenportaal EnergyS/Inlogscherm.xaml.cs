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


namespace Vragen_en_klachten
{
    /// <summary>
    /// The login page
    /// </summary>
    public sealed partial class Inlogscherm : Page
    {
        public Inlogscherm()
        {
            this.InitializeComponent();
        }
        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            string Geb_nummer = Gebruikersnummer.Text;
            string WW_gebruiker = Wachtwoord.Text;
            string Gebruiker_ID;
            Debug.WriteLine(Geb_nummer);
            Debug.WriteLine(WW_gebruiker);
            //CSV file wordt ingeladen vanuit de install directory
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
                        //een nieuwe regel uit de csv file halen
                        NieuweRow += row[i] + ",";
                        // een list maken die de regels een voor een opslaat
                        List<string> GebGegevens = new List<string>(new string[] {NieuweRow});
                        Debug.WriteLine(GebGegevens[i] + "de list");
                        // check of de er door de gebruiker niks is ingevuld
                        if(Geb_nummer != "" || WW_gebruiker != "")
                        {
                            // check of wat er is ingevuld wel overeenkomt met de inloggegevens
                            if (Geb_nummer.Substring(0, Geb_nummer.Length) == GebGegevens[i].Substring(2, Geb_nummer.Length) && WW_gebruiker.Substring(0, WW_gebruiker.Length) == GebGegevens[i].Substring(2 + Geb_nummer.Length + 1, WW_gebruiker.Length))
                            {
                                Gebruiker_ID = GebGegevens[i].Substring(0, 1);
                                Debug.WriteLine(Gebruiker_ID);
                                (Application.Current as App).GebruikerString = Geb_nummer;
                                (Application.Current as App).GebruikerID = Gebruiker_ID;
                                Frame.Navigate(typeof(MainPage));
                            }
                            else
                            {
                                LoginStatus.Text = "Geef een geldige naam en/of gebruikersnummer";
                            }
                        }
                        else
                        {
                            LoginStatus.Text = "voer een wachtwoord en gebruikernummer in";
                        }

                    }
                }
            }
        }
    }
}
