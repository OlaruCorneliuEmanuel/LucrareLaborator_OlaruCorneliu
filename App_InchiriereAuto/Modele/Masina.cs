namespace App_InchiriereAuto
{
    public class Masina
    {
        public string Marca { get; set; }
        public string Model { get; set; }

        public Masina(string marca, string model)
        {
            Marca = marca;
            Model = model;
        }
    }
}