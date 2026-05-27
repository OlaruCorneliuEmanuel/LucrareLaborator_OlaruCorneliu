namespace LibrarieModele
{
    public class Persoana
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string CNP { get; set; }

        public Persoana(string nume, string prenume, string cnp)
        {
            Nume = nume;
            Prenume = prenume;
            CNP = cnp;
        }

        // --- Constructor din text---
        public Persoana(string linieFisier)
        {
            try
            {
                string[] date = linieFisier.Split(';');
                Nume = date[0];
                Prenume = date[1];
                CNP = date[2];
            }
            catch (Exception)
            {
                // Valori default în caz de eroare la citirea liniei
                Nume = "Necunoscut";
                Prenume = "Necunoscut";
                CNP = "Necunoscut";
            }
        }

        public string ConversieLaSirPentruFisier()
        {
            return $"{Nume};{Prenume};{CNP}";
        }
    }
}