using NivelStocareDate;
using System.Configuration;
using System.IO;

namespace InterfataWPF
{
    public static class StocareFactory
    {
        private const string FORMAT_SALVARE = "FormatSalvare";
        private const string NUME_FISIER = "NumeFisier";

        public static IStocareData GetAdministratorStocare()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "memorie";
            string numeFisier = ConfigurationManager.AppSettings[NUME_FISIER] ?? "Date";

            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            if (formatSalvare == "txt")
            {
                return new AdministrareInchirieriFisierText(caleCompletaFisier + "_masini.txt", caleCompletaFisier + "_contracte.txt");
            }

            return new AdministrareInchirieriMemorie();
        }
    }
}