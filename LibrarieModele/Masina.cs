namespace LibrarieModele
{
    public class Masina
    {
        public string Marca { get; set; }
        public string Model { get; set; }
        public enum Culoare { 
            Alb, 
            Negru, 
            Rosu, 
            Albastru, 
            Gri 
        }
        public enum TipCombustibil { 
            Benzina, 
            Motorina, 
            Electric, 
            Hibrid
        }
        [Flags]
        public enum optiuniMasina
        {
            AerConditionat,
            Navigatie,
            Bluetooth,
            SenzoriParcare,
            CameraMarsarier
        }
        public Masina(string marca, string model)
        {
            Marca = marca;
            Model = model;
        }

    }
}