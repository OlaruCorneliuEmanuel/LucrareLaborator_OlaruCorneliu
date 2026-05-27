using System;

namespace LibrarieModele
{
    public class Masina
    {
        public string Marca { get; set; }
        public string Model { get; set; }
        public bool EsteInchiriata { get; set; }

        public Culoare CuloareMasina { get; set; }
        public TipCombustibil Combustibil { get; set; }
        public optiuniMasina Optiuni { get; set; }

        public enum Culoare { Alb, Negru, Rosu, Albastru, Gri, Verde, Galben, Portocaliu, Maro }
        public enum TipCombustibil { Benzina, Motorina, Electric, Hibrid }

        [Flags]
        public enum optiuniMasina
        {
            Niciuna = 0, AerConditionat = 1, Navigatie = 2, Bluetooth = 4,
            SenzoriParcare = 8, CameraMarsarier = 16, IncalzireScaune = 32,
            FaruriLED = 64, PilotAutomat = 128
        }

        public string DetaliiAfisare => $"{Marca} {Model} ({CuloareMasina})";

        public Masina(string marca, string model)
        {
            Marca = marca;
            Model = model;
            EsteInchiriata = false;
            CuloareMasina = Culoare.Alb;
        }

        // Constructor din fișier
        public Masina(string linieFisier)
        {
            try
            {
                string[] date = linieFisier.Split(';');

                Marca = date[0];
                Model = date[1];

                EsteInchiriata = bool.Parse(date[2]);
                CuloareMasina = (Culoare)Enum.Parse(typeof(Culoare), date[3]);
                Combustibil = (TipCombustibil)Enum.Parse(typeof(TipCombustibil), date[4]);
                Optiuni = (optiuniMasina)Enum.Parse(typeof(optiuniMasina), date[5]);
            }
            catch (Exception)
            {
                Marca = "Eroare";
                Model = "Citire";
                EsteInchiriata = false;
                CuloareMasina = Culoare.Alb;
                Combustibil = TipCombustibil.Benzina;
            }
        }

        public string ConversieLaSirPentruFisier()
        {
            return $"{Marca};{Model};{EsteInchiriata};{CuloareMasina};{Combustibil};{Optiuni}";
        }
    }
}