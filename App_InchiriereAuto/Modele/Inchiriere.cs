namespace App_InchiriereAuto
{
    public class Inchiriere
    {
        public Persoana Client { get; set; }
        public Masina Automobil { get; set; }
        public int NumarZile { get; set; }

        public Inchiriere(Persoana client, Masina automobil, int numarZile)
        {
            Client = client;
            Automobil = automobil;
            NumarZile = numarZile;
        }
    }
}