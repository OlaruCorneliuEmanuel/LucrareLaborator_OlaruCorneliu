namespace LibrarieModele
{
    public class Persoana
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string CNP { get; set; }

        // Constructor
        public Persoana(string nume, string prenume, string cnp)
        {
            Nume = nume;
            Prenume = prenume;
            CNP = cnp;
        }
    }
}