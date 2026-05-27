using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareInchirieriFisierText : IStocareData
    {
        private string numeFisierMasini;
        private string numeFisierInchirieri;

        public AdministrareInchirieriFisierText(string numeFisierMasini, string numeFisierInchirieri)
        {
            this.numeFisierMasini = numeFisierMasini;
            this.numeFisierInchirieri = numeFisierInchirieri;

            // Se incearca deschiderea fisierelor in modul OpenOrCreate pentru a fi create daca nu exista
            Stream streamMasini = File.Open(numeFisierMasini, FileMode.OpenOrCreate);
            streamMasini.Close();

            Stream streamInchirieri = File.Open(numeFisierInchirieri, FileMode.OpenOrCreate);
            streamInchirieri.Close();
        }

        public void AdaugaMasina(Masina masinaNoua)
        {
            using (StreamWriter sw = new StreamWriter(numeFisierMasini, true))
            {
                sw.WriteLine(masinaNoua.ConversieLaSirPentruFisier());
            }
        }

        public List<Masina> GetMasini()
        {
            List<Masina> masini = new List<Masina>();
            using (StreamReader sr = new StreamReader(numeFisierMasini))
            {
                string linieFisier;
                while ((linieFisier = sr.ReadLine()) != null)
                {
                    masini.Add(new Masina(linieFisier));
                }
            }
            return masini;
        }

        public void StergeMasina(Masina masina)
        {
            List<Masina> masini = GetMasini();
            masini.RemoveAll(m => m.Marca == masina.Marca && m.Model == masina.Model); // Filtrare cu LINQ

            // Rescriem fisierul cu elementele ramase
            using (StreamWriter sw = new StreamWriter(numeFisierMasini, false))
            {
                foreach (var m in masini)
                    sw.WriteLine(m.ConversieLaSirPentruFisier());
            }
        }

        // --- ACEEASI LOGICA PENTRU CONTRACTE ---
        public void AdaugaInchiriere(Inchiriere contractNou)
        {
            using (StreamWriter sw = new StreamWriter(numeFisierInchirieri, true))
            {
                sw.WriteLine(contractNou.ConversieLaSirPentruFisier());
            }
        }

        public List<Inchiriere> GetInchirieri()
        {
            List<Inchiriere> contracte = new List<Inchiriere>();
            using (StreamReader sr = new StreamReader(numeFisierInchirieri))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    contracte.Add(new Inchiriere(linie));
                }
            }
            return contracte;
        }

        public void StergeInchiriere(Inchiriere contract)
        {
            List<Inchiriere> contracte = GetInchirieri();
            contracte.RemoveAll(c => c.Client.Nume == contract.Client.Nume && c.Automobil.Marca == contract.Automobil.Marca);
            using (StreamWriter sw = new StreamWriter(numeFisierInchirieri, false))
            {
                foreach (var c in contracte)
                    sw.WriteLine(c.ConversieLaSirPentruFisier());
            }
        }

        public List<Inchiriere> CautaMasiniDupaMarca(string marcaCautata)
        {
            return GetInchirieri().Where(i => i.Automobil.Marca == marcaCautata).ToList();
        }
    }
}