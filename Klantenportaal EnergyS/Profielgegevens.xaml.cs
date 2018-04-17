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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Vragen_en_klachten
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profielgegevens : Page
    {
        public Profielgegevens()
        {
            this.InitializeComponent();
            GegevensLaden();
        }
        public string DeGebruiker { get; private set; }
        public string GebruikerID { get; private set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Adres_wijziging));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GebruikerID = (Application.Current as App).GebruikerID;
            DeGebruiker = e.Parameter as string;
        }
        private async void GegevensLaden()
        {
            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await installedLocation.GetFileAsync("Klantgegevens.csv");
            using (CSVparse.CsvFileReader csvReader = new CSVparse.CsvFileReader(await file.OpenStreamForReadAsync()))
            {
                CSVparse.CsvRow row = new CSVparse.CsvRow();
                while (csvReader.ReadRow(row))
                {
                    string NieuweRow = "";
                    string[] Nieuwestraat = {"Arendsstraat","Dorpslaan","frederiksweg","Hendrikusweg","Schilderslaan","Davidsweg" };
                    string[] Nieuwepostcode = { "2346 BA", "1876 CD", "4571 KJ", "5012 LI", "4225 HB", "0912 GT" };
                    string[] Nieuwehuis = { "12", "55", "104", "98", "7", "23" };
                    string[] NieuweWoon = { "Genk", "Maastricht", "Echt", "Volendam", "Amsterdam", "Sittard" };
                    for (int i = 0; i < row.Count; i++)
                    {
                        //een nieuwe regel uit de csv file halen
                        NieuweRow += row[i] + ",";
                        // een list maken die de regels een voor een opslaat
                        List<string> GebGegevens = new List<string>(new string[] { NieuweRow });
                        Debug.WriteLine(GebGegevens[i] + "de list");
                        // check of de er door de gebruiker niks is ingevuld
                        GebruikerID = "1";
                        if (GebruikerID == GebGegevens[i].Substring(0, 1))
                        {
                            string OverzichtRow = GebGegevens[i];
                            string[] result = OverzichtRow.Split(';');
                            for(int Gebcount = 0; Gebcount < result.Length; Gebcount++)
                            {
                                Geb.Text = result[0];
                                Naam.Text = result[1];
                                Achternaam.Text = result[2];
                                Geslacht.Text = result[4];
                                int x = Int32.Parse(GebruikerID);
                                Straat.Text = Nieuwestraat[x];
                                Postcode.Text = Nieuwepostcode[x];
                                Email.Text = result[6];
                                Mobiel.Text = result[5];
                                Huisnummer.Text = Nieuwehuis[x];
                                Woonplaats.Text = NieuweWoon[x];
                            }
                            
                        }
                    }
                }
            }
        }
    }
}
