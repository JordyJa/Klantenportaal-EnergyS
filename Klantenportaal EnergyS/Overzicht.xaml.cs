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
using System.Collections.ObjectModel;

namespace Vragen_en_klachten
{
    /// <summary>
    /// de pagina voor het bekijken van het energieo verzicht
    /// </summary>
    public sealed partial class Overzicht : Page
    {
        //collectie van objecten voor de het binden van de items aan de listvieuw 
        private ObservableCollection<string> OverzichtRegel = new ObservableCollection<string>();

        public ObservableCollection<string> Regels
        {
            get { return this.OverzichtRegel; }
        }
        public string DeGebruiker { get; private set; }
        public string GebruikerID { get; private set; }
        public ObservableCollection<string> OverzichtRegel1 { get => OverzichtRegel; set => OverzichtRegel = value; }

        public Overzicht()
        {
            this.InitializeComponent();
            Regels.Add("eindafrekening ID    betaling per maand    Jaar rekening    betaling uptodate    resterend bedrag    laatste update");
            OverzichtLaden();
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
        private async void OverzichtLaden()
        {
            //een cvs file inladen in een storagefolder en de applicatie doorzoeken voor de data
            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await installedLocation.GetFileAsync("Eindafrekening_klant.csv");
            using (CSVparse.CsvFileReader csvReader = new CSVparse.CsvFileReader(await file.OpenStreamForReadAsync()))
            {
                CSVparse.CsvRow row = new CSVparse.CsvRow();
                while (csvReader.ReadRow(row))
                {
                    string NieuweRow = "";
                    for (int i = 0; i < row.Count; i++)
                    {
                        //een nieuwe regel uit de csv file halen
                        NieuweRow += row[i] + ",";
                        //een list maken die de regels een voor een opslaat
                        List<string> OverzichtGegevens = new List<string>(new string[] { NieuweRow });
                        Debug.WriteLine(OverzichtGegevens[i] + "de list");
                        //id's vergelijken tussen gebruiker en gegevens
                        if (GebruikerID == OverzichtGegevens[i].Substring(0, 1))
                        {
                            string OverzichtRow = OverzichtGegevens[i];
                            string[] result = OverzichtRow.Split(';');
                            OverzichtRow = string.Format("{0,-30}  {1,-30}  {2,-20}  {3,-30}  {4,-30}  {5,-25}",result[0],result[1],result[2],result[3],result[4],result[5]);
                            OverzichtRow = OverzichtRow.Replace(";", "");
                            OverzichtRow = OverzichtRow.Replace(",", "");
                            Regels.Add(OverzichtRow);
                        }
                    }
                }
            }
        }

    }
}
