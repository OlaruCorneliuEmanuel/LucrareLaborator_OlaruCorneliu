using System.Collections.Generic;
using System.Linq; 
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareInchirieriMemorie
    {
        private List<Inchiriere> listaInchirieri;

        public AdministrareInchirieriMemorie()
        {
            listaInchirieri = new List<Inchiriere>();
        }
        public void AdaugaInchiriere(Inchiriere inchiriereNoua)
        {
            listaInchirieri.Add(inchiriereNoua);
        }

       
        public List<Inchiriere> CautaMasiniDupaMarca(string marcaCautata)
        {
            var rezultate = listaInchirieri.Where(inchiriere => inchiriere.Automobil.Marca == marcaCautata).ToList();

            return rezultate;
        }
        public List<Inchiriere> GetInchirieri()
        {
            return listaInchirieri;
        }
    }
}