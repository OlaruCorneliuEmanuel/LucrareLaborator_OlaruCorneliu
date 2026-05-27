using System.Collections.Generic;
using System.Linq;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareInchirieriMemorie : IStocareData
    {
        private List<Inchiriere> listaInchirieri;
        private List<Masina> listaMasini;

        public AdministrareInchirieriMemorie()
        {
            listaInchirieri = new List<Inchiriere>();
            listaMasini = new List<Masina>();

            // Populăm flota cu 7 mașini diverse
            listaMasini.Add(new Masina("Dacia", "Logan") { CuloareMasina = Masina.Culoare.Alb, Combustibil = Masina.TipCombustibil.Benzina, Optiuni = Masina.optiuniMasina.AerConditionat });
            listaMasini.Add(new Masina("Renault", "Clio") { CuloareMasina = Masina.Culoare.Rosu, Combustibil = Masina.TipCombustibil.Motorina, Optiuni = Masina.optiuniMasina.Navigatie | Masina.optiuniMasina.SenzoriParcare });
            listaMasini.Add(new Masina("BMW", "Seria 3") { CuloareMasina = Masina.Culoare.Negru, Combustibil = Masina.TipCombustibil.Motorina, Optiuni = Masina.optiuniMasina.FaruriLED | Masina.optiuniMasina.IncalzireScaune });
            listaMasini.Add(new Masina("Tesla", "Model 3") { CuloareMasina = Masina.Culoare.Albastru, Combustibil = Masina.TipCombustibil.Electric, Optiuni = Masina.optiuniMasina.PilotAutomat | Masina.optiuniMasina.CameraMarsarier | Masina.optiuniMasina.Navigatie });
            listaMasini.Add(new Masina("Toyota", "Corolla") { CuloareMasina = Masina.Culoare.Gri, Combustibil = Masina.TipCombustibil.Hibrid, Optiuni = Masina.optiuniMasina.Navigatie | Masina.optiuniMasina.Bluetooth });
            listaMasini.Add(new Masina("Ford", "Puma") { CuloareMasina = Masina.Culoare.Verde, Combustibil = Masina.TipCombustibil.Hibrid, Optiuni = Masina.optiuniMasina.AerConditionat | Masina.optiuniMasina.Bluetooth | Masina.optiuniMasina.FaruriLED });
            listaMasini.Add(new Masina("Audi", "A4") { CuloareMasina = Masina.Culoare.Maro, Combustibil = Masina.TipCombustibil.Benzina, Optiuni = Masina.optiuniMasina.IncalzireScaune | Masina.optiuniMasina.SenzoriParcare | Masina.optiuniMasina.Bluetooth });
        }

        public void AdaugaInchiriere(Inchiriere inchiriereNoua) { listaInchirieri.Add(inchiriereNoua); }
        public List<Inchiriere> CautaMasiniDupaMarca(string marca) { return listaInchirieri.Where(i => i.Automobil.Marca == marca).ToList(); }
        public List<Inchiriere> GetInchirieri() { return listaInchirieri; }
        public void StergeInchiriere(Inchiriere inchiriere) { listaInchirieri.Remove(inchiriere); }

        public void AdaugaMasina(Masina masinaNoua) { listaMasini.Add(masinaNoua); }
        public List<Masina> GetMasini() { return listaMasini; }
        public void StergeMasina(Masina masina) { listaMasini.Remove(masina); }
    }
}