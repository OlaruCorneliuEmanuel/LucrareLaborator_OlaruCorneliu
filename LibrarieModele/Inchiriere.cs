namespace LibrarieModele

{
    public class Inchiriere
    {
        public Persoana Client { get; set; }
        public Masina Automobil { get; set; }
        public int NumarZile { get; set; }
        public DateTime DataInchiriere { get; set; }

        public Inchiriere(Persoana client, Masina automobil, int numarZile, DateTime dataInchiriere)
        {
            Client = client;
            Automobil = automobil;
            NumarZile = numarZile;
            DataInchiriere = dataInchiriere;
        }
        
    }
}