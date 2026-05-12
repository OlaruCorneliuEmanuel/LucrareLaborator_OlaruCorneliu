using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Necesar pentru ObservableCollection (Lab 10)
using System.ComponentModel;          // Necesar pentru INotifyPropertyChanged (Lab 10)
using System.Runtime.CompilerServices;
using System.Windows;
using LibrarieModele;
using NivelStocareDate;

namespace InterfataWPF
{
    // 1. Clasa ferestrei implementează INotifyPropertyChanged pentru Data Binding modern
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Instanțiem sistemul tău de salvare a datelor
        private IStocareData adminInchirieri = new AdministrareInchirieriMemorie();

        // 2. Colectia inteligentă care va trimite notificări automate către DataGrid
        public ObservableCollection<Inchiriere> ListaInchirieri { get; set; }

        // 3. Cerință Laboratorul 10: Mecanismul de notificare a modificărilor
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // --- CONSTRUCTORUL FERESTREI ---
        public MainWindow()
        {
            InitializeComponent();

            // Setăm fereastra curentă ca sursă de date (Data Binding)
            DataContext = this;

            // Inițializăm colecția vizuală cu datele deja existente în memorie/fișier
            ListaInchirieri = new ObservableCollection<Inchiriere>(adminInchirieri.GetInchirieri());

            // Conectăm tabelul DataGrid direct la această colecție
            dgInchirieri.ItemsSource = ListaInchirieri;
        }

        // --- BUTONUL ADAUGĂ ---
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtNumeClient.Text.Trim();
            string prenume = txtPrenumeClient.Text.Trim();
            int.TryParse(txtZile.Text, out int zile);

            // Validarea datelor (Lab 7)
            if (string.IsNullOrEmpty(nume) || string.IsNullOrEmpty(prenume))
            {
                tbEroare.Text = "Numele și prenumele sunt obligatorii!";
                return;
            }

            // Preluare element din lista derulantă (Lab 9)
            string marca = cmbMarca.Text;
            if (marca == "Selectează marca...") marca = "Necunoscut";

            // Preluare dată din calendar (Lab 9)
            DateTime dataInchi = dpDataInchiriere.SelectedDate ?? DateTime.Today;

            // Crearea obiectelor pe baza claselor tale
            Persoana clientNou = new Persoana(nume, prenume, "Necunoscut");
            Masina masinaNoua = new Masina(marca, "Standard");

            // Generarea contractului (folosind constructorul modificat cu DateTime anterior)
            Inchiriere contractNou = new Inchiriere(clientNou, masinaNoua, zile, dataInchi);

            // 1. Salvăm contractul în spate (NivelStocareDate)
            adminInchirieri.AdaugaInchiriere(contractNou);

            // 2. Adăugăm contractul în colecția vizuală
            // Aici intervine magia din Lab 10: XAML-ul simte adăugarea și actualizează DataGrid-ul instant!
            ListaInchirieri.Add(contractNou);

            // Resetarea interfeței după o salvare de succes (Cerință Test 2)
            tbEroare.Text = "";
            txtNumeClient.Clear();
            txtPrenumeClient.Clear();
            txtZile.Clear();
        }

        // --- BUTONUL AFIȘEAZĂ (Toate) ---
        private void btnAfiseaza_Click(object sender, RoutedEventArgs e)
        {
            // Golește lista vizuală și o reumple cu datele complete din administrare
            ListaInchirieri.Clear();
            foreach (var contract in adminInchirieri.GetInchirieri())
            {
                ListaInchirieri.Add(contract);
            }
        }

        // --- BUTONUL FILTREAZĂ ---
        private void btnFiltreaza_Click(object sender, RoutedEventArgs e)
        {
            string marcaSelectata = cmbMarca.Text;

            // Apelăm metoda ta de filtrare cu LINQ din NivelStocareDate
            List<Inchiriere> filtrate = adminInchirieri.CautaMasiniDupaMarca(marcaSelectata);

            // Reîncărcăm colecția vizuală doar cu rezultatele găsite
            ListaInchirieri.Clear();
            foreach (var contract in filtrate)
            {
                ListaInchirieri.Add(contract);
            }
        }
    }
}