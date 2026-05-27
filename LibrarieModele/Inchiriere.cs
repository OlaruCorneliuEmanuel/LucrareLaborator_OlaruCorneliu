using System;

namespace LibrarieModele
{
    public class Inchiriere
    {
        public Persoana Client { get; set; }
        public Masina Automobil { get; set; }
        public int NumarZile { get; set; }
        public DateTime DataInchiriere { get; set; }

        public Inchiriere(Persoana client, Masina automobil, int numarZile, DateTime data)
        {
            Client = client;
            Automobil = automobil;
            NumarZile = numarZile;
            DataInchiriere = data;
        }

        public Inchiriere(string linieFisier)
        {
            try
            {
                string[] date = linieFisier.Split(';');

                Client = new Persoana(date[0], date[1], date[2]);

                Automobil = new Masina(date[3], date[4]);
                Automobil.EsteInchiriata = bool.Parse(date[5]);
                Automobil.CuloareMasina = (Masina.Culoare)Enum.Parse(typeof(Masina.Culoare), date[6]);
                Automobil.Combustibil = (Masina.TipCombustibil)Enum.Parse(typeof(Masina.TipCombustibil), date[7]);
                Automobil.Optiuni = (Masina.optiuniMasina)Enum.Parse(typeof(Masina.optiuniMasina), date[8]);

                NumarZile = int.Parse(date[9]);
                DataInchiriere = DateTime.Parse(date[10]);
            }
            catch (Exception)
            {
                NumarZile = 0;
                DataInchiriere = DateTime.MinValue;
            }
        }

        public string ConversieLaSirPentruFisier()
        {
            string clientSir = Client.ConversieLaSirPentruFisier();
            string masinaSir = Automobil.ConversieLaSirPentruFisier();

            return $"{clientSir};{masinaSir};{NumarZile};{DataInchiriere}";
        }
    }
}