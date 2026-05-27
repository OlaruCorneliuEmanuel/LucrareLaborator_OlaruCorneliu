using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibrarieModele;
using NivelStocareDate;

namespace InterfataWPF
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // === MODIFICAREA CHEIE (Lab 5) PENTRU A FOLOSI FISIERELE ===
        private IStocareData adminStocare = StocareFactory.GetAdministratorStocare();

        public ObservableCollection<Inchiriere> ListaInchirieri { get; set; }
        public ObservableCollection<Masina> ListaMasini { get; set; }
        public ObservableCollection<Masina> MasiniDisponibile { get; set; } = new ObservableCollection<Masina>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            ListaInchirieri = new ObservableCollection<Inchiriere>(adminStocare.GetInchirieri());
            ListaMasini = new ObservableCollection<Masina>(adminStocare.GetMasini());

            dgInchirieri.ItemsSource = ListaInchirieri;
            dgMasini.ItemsSource = ListaMasini;

            ActualizeazaInterfataSiStatistici();
        }

        private void ActualizeazaInterfataSiStatistici()
        {
            MasiniDisponibile.Clear();
            var filtrare = ListaMasini.Where(m => m.EsteInchiriata == false).ToList();
            foreach (var m in filtrare)
            {
                MasiniDisponibile.Add(m);
            }

            int totalMasini = ListaMasini.Count;
            int masiniInchiriate = ListaMasini.Count(m => m.EsteInchiriata);
            int contracteActive = ListaInchirieri.Count;

            tbStatisticiLive.Text = $"📊 Total Flotă: {totalMasini} | Libere: {filtrare.Count} | Închiriate: {masiniInchiriate} | Contracte emise: {contracteActive}";
        }

        private void txtCautare_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtCautare.Text.ToLower();

            var rezultate = adminStocare.GetInchirieri()
                                        .Where(c => c.Client.Nume.ToLower().Contains(keyword) ||
                                                    c.Client.Prenume.ToLower().Contains(keyword))
                                        .ToList();

            ListaInchirieri.Clear();
            foreach (var r in rezultate)
            {
                ListaInchirieri.Add(r);
            }
        }

        private bool ValideazaFormular()
        {
            bool isValid = true;
            tbEroare.Text = "";

            txtNumeClient.Background = Brushes.White;
            txtPrenumeClient.Background = Brushes.White;
            cmbMasinaSelectata.Background = Brushes.White;

            if (string.IsNullOrWhiteSpace(txtNumeClient.Text))
            {
                txtNumeClient.Background = new SolidColorBrush(Color.FromRgb(255, 230, 230));
                tbEroare.Text += "Numele este obligatoriu!\n";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPrenumeClient.Text))
            {
                txtPrenumeClient.Background = new SolidColorBrush(Color.FromRgb(255, 230, 230));
                tbEroare.Text += "Prenumele este obligatoriu!\n";
                isValid = false;
            }

            if (cmbMasinaSelectata.SelectedItem == null)
            {
                cmbMasinaSelectata.Background = new SolidColorBrush(Color.FromRgb(255, 230, 230));
                tbEroare.Text += "Alege o mașină din listă!\n";
                isValid = false;
            }

            return isValid;
        }

        private void btnMeniuInchirieri_Click(object sender, RoutedEventArgs e)
        {
            panelInchirieri.Visibility = Visibility.Visible;
            panelMasini.Visibility = Visibility.Collapsed;
        }

        private void btnMeniuFlota_Click(object sender, RoutedEventArgs e)
        {
            panelInchirieri.Visibility = Visibility.Collapsed;
            panelMasini.Visibility = Visibility.Visible;
        }

        private void MeniuIesire_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (!ValideazaFormular()) return;

            int.TryParse(txtZile.Text, out int zile);
            DateTime data = dpDataInchiriere.SelectedDate ?? DateTime.Today;

            Masina masinaAleasa = cmbMasinaSelectata.SelectedItem as Masina;
            Persoana client = new Persoana(txtNumeClient.Text, txtPrenumeClient.Text, "Necunoscut");
            Inchiriere contract = new Inchiriere(client, masinaAleasa, zile, data);

            masinaAleasa.EsteInchiriata = true;
            adminStocare.AdaugaInchiriere(contract);

            // Reîmprospătăm fișierul mașinilor pentru a salva starea de "EsteInchiriata=true"
            adminStocare.StergeMasina(masinaAleasa);
            adminStocare.AdaugaMasina(masinaAleasa);

            if (string.IsNullOrWhiteSpace(txtCautare.Text)) ListaInchirieri.Add(contract);

            tbEroare.Text = "Contract adăugat cu succes!";
            tbEroare.Foreground = Brushes.Green;

            txtNumeClient.Clear();
            txtPrenumeClient.Clear();
            txtZile.Clear();

            ActualizeazaInterfataSiStatistici();
            dgMasini.Items.Refresh();
        }

        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (dgInchirieri.SelectedItem is Inchiriere selectata)
            {
                selectata.Automobil.EsteInchiriata = false;

                var masinaDinFlota = ListaMasini.FirstOrDefault(m => m.Marca == selectata.Automobil.Marca && m.Model == selectata.Automobil.Model);
                if (masinaDinFlota != null)
                {
                    masinaDinFlota.EsteInchiriata = false;
                }
                ListaInchirieri.Remove(selectata);
                adminStocare.StergeInchiriere(selectata);

                adminStocare.StergeMasina(selectata.Automobil);
                adminStocare.AdaugaMasina(selectata.Automobil);

                ActualizeazaInterfataSiStatistici();
                dgMasini.Items.Refresh();

                MessageBox.Show("Mașina a fost returnată și contractul șters!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAdaugaMasina_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMarca.Text) && !string.IsNullOrEmpty(txtModel.Text))
            {
                Masina m = new Masina(txtMarca.Text, txtModel.Text);

                m.CuloareMasina = (Masina.Culoare)Enum.Parse(typeof(Masina.Culoare), cmbCuloare.Text);
                m.Combustibil = (Masina.TipCombustibil)Enum.Parse(typeof(Masina.TipCombustibil), cmbCombustibil.Text);

                Masina.optiuniMasina optiuniSelectate = Masina.optiuniMasina.Niciuna;
                if (chkAer.IsChecked == true) optiuniSelectate |= Masina.optiuniMasina.AerConditionat;
                if (chkNavigatie.IsChecked == true) optiuniSelectate |= Masina.optiuniMasina.Navigatie;
                if (chkSenzori.IsChecked == true) optiuniSelectate |= Masina.optiuniMasina.SenzoriParcare;
                if (chkCamera.IsChecked == true) optiuniSelectate |= Masina.optiuniMasina.CameraMarsarier;
                if (chkBluetooth.IsChecked == true) optiuniSelectate |= Masina.optiuniMasina.Bluetooth;
                if (chkIncalzire.IsChecked == true) optiuniSelectate |= Masina.optiuniMasina.IncalzireScaune;
                if (chkFaruri.IsChecked == true) optiuniSelectate |= Masina.optiuniMasina.FaruriLED;
                if (chkPilot.IsChecked == true) optiuniSelectate |= Masina.optiuniMasina.PilotAutomat;
                m.Optiuni = optiuniSelectate;

                adminStocare.AdaugaMasina(m);
                ListaMasini.Add(m);

                txtMarca.Clear();
                txtModel.Clear();
                chkAer.IsChecked = false; chkNavigatie.IsChecked = false; chkSenzori.IsChecked = false;
                chkCamera.IsChecked = false; chkBluetooth.IsChecked = false; chkIncalzire.IsChecked = false;
                chkFaruri.IsChecked = false; chkPilot.IsChecked = false;

                ActualizeazaInterfataSiStatistici();
            }
        }

        private void btnStergeMasina_Click(object sender, RoutedEventArgs e)
        {
            if (dgMasini.SelectedItem is Masina selectata)
            {
                if (selectata.EsteInchiriata)
                {
                    MessageBox.Show("Nu poți radia o mașină care este într-un contract activ!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ListaMasini.Remove(selectata);
                adminStocare.StergeMasina(selectata);
                ActualizeazaInterfataSiStatistici();
            }
        }
    }
}