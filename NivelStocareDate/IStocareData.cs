using System.Collections.Generic;
using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareData
    {
        // Operații pentru Închirieri
        void AdaugaInchiriere(Inchiriere inchiriereNoua);
        List<Inchiriere> GetInchirieri();
        List<Inchiriere> CautaMasiniDupaMarca(string marcaCautata);
        void StergeInchiriere(Inchiriere inchiriere);

        // Operații pentru Flota Auto
        void AdaugaMasina(Masina masinaNoua);
        List<Masina> GetMasini();
        void StergeMasina(Masina masina);
    }
}