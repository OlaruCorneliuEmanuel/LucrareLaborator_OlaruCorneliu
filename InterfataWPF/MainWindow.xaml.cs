using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LibrarieModele; // Entitatile tale
using NivelStocareDate; // Logica de salvare

namespace InterfataWPF
{
    public partial class MainWindow : Window
    {
        // Instanțiem clasa de stocare (folosesc in memorie pentru rapiditate, dar merge si FisierText)
        private IStocareData adminInchirieri = new AdministrareInchirieriMemorie();

        public MainWindow()
        {
            InitializeComponent();
        }

        // --- BUTONUL ADAUGĂ ---
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            // 1. Preluarea textului simplu
            string nume = txtNumeClient.Text.Trim();
            string prenume = txtPrenumeClient.Text.Trim();

            int.TryParse(txtZile.Text, out int zile);

            // Validare simplă (Lab 7)
            if (string.IsNullOrEmpty(nume) || string.IsNullOrEmpty(prenume))
            {
                tbEroare.Text = "Numele și prenumele sunt obligatorii!";
                return;
            }

            // 2. Preluarea din lista derulantă (ComboBox - Lab 9)
            string marca = cmbMarca.Text;
            if (marca == "Selectează marca...") marca = "Necunoscut";

            // 3. Preluarea datei din calendar (DatePicker - Lab 9)
            // Folosim operatorul ?? pentru cazul în care utilizatorul nu a ales nimic
            DateTime dataInchi = dpDataInchiriere.SelectedDate ?? DateTime.Today;

            // 4. Preluarea selecției unice (RadioButton - Lab 8)
            Masina.TipCombustibil combustibil = Masina.TipCombustibil.Benzina;
            if (rbMotorina.IsChecked == true) combustibil = Masina.TipCombustibil.Motorina;
            if (rbElectric.IsChecked == true) combustibil = Masina.TipCombustibil.Electric;

            // 5. Crearea obiectelor folosind clasele tale din LibrarieModele
            Persoana clientNou = new Persoana(nume, prenume, "Necunoscut"); // CNP placeholder
            Masina masinaNoua = new Masina(marca, "Standard"); // Model placeholder
            masinaNoua.Combustibil = combustibil;

            Inchiriere contract = new Inchiriere(clientNou, masinaNoua, zile, dataInchi);

            // 6. Salvarea contractului și curățarea interfeței
            adminInchirieri.AdaugaInchiriere(contract);
            tbEroare.Text = ""; // Ștergem mesajul de eroare

            // Golim casetele după adăugare conform cerinței de test
            txtNumeClient.Clear();
            txtPrenumeClient.Clear();
            txtZile.Clear();

            // Afișăm în tabel lista actualizată
            ActualizeazaTabel(adminInchirieri.GetInchirieri());
        }

        // --- BUTONUL AFIȘEAZĂ ---
        private void btnAfiseaza_Click(object sender, RoutedEventArgs e)
        {
            ActualizeazaTabel(adminInchirieri.GetInchirieri());
        }

        // --- BUTONUL FILTREAZĂ ---
        private void btnFiltreaza_Click(object sender, RoutedEventArgs e)
        {
            string marcaSelectata = cmbMarca.Text; // Luăm marca din ComboBox

            // Folosim funcția ta cu LINQ scrisă anterior
            List<Inchiriere> filtrate = adminInchirieri.CautaMasiniDupaMarca(marcaSelectata);
            ActualizeazaTabel(filtrate);
        }

        // --- METODĂ AJUTĂTOARE PENTRU TABEL (DataGrid - Lab 8 & Lab 9) ---
        private void ActualizeazaTabel(List<Inchiriere> lista)
        {
            // Scurt truc din Laboratorul 9: Pentru ca tabelul să își dea seama că 
            // lista s-a modificat, sursa se face null mai întâi, apoi i se dă lista.
            dgInchirieri.ItemsSource = null;
            dgInchirieri.ItemsSource = lista;
        }
    }
}
