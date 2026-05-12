using System.Collections.Generic;
using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareData
    {
        void AdaugaInchiriere(Inchiriere inchiriereNoua);
        List<Inchiriere> GetInchirieri();
        List<Inchiriere> CautaMasiniDupaMarca(string marcaCautata);
    }
}