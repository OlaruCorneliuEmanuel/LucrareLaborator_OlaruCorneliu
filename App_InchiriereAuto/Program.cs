using System;
using LibrarieModele;
using NivelStocareDate; 

namespace App_InchiriereAuto
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            AdministrareInchirieriMemorie admin = new AdministrareInchirieriMemorie();
            bool ruleaza = true;

            while (ruleaza)
            {
                Console.WriteLine("\n=== Sistem Gestiune Închirieri Auto ===");
                Console.WriteLine("1. Adauga o inchiriere noua");
                Console.WriteLine("2. Afiseaza toate inchirierile");
                Console.WriteLine("3. Cauta masini dupa marca (LINQ)");
                Console.WriteLine("0. Iesire");
                Console.Write("Alege o optiune: ");
                string optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        Console.WriteLine();
                        // Citirea datelor pentru Persoană
                        Console.Write("Introduceti numele clientului: ");
                        string nume = Console.ReadLine();

                        Console.Write("Introduceti prenumele clientului: ");
                        string prenume = Console.ReadLine();

                        Console.Write("Introduceti CNP: ");
                        string cnp = Console.ReadLine();

                        // Crearea obiectului de tip Persoana
                        Persoana clientNou = new Persoana(nume, prenume, cnp);


                        // Citirea datelor pentru Masina
                        Console.WriteLine("\n--- Detalii Masina ---");
                        Console.Write("Marca: ");
                        string marca = Console.ReadLine();

                        Console.Write("Model: ");
                        string model = Console.ReadLine();

                        Masina masinaAleasa = new Masina(marca, model);


                        // Detalii închiriere
                        Console.Write("\nNumar de zile pentru inchiriere: ");
                        int zile = int.Parse(Console.ReadLine());

                        // Cream obiectul de tip  Inchiriere
                        Inchiriere contract = new Inchiriere(clientNou, masinaAleasa, zile);

                        Console.WriteLine();
                        Console.WriteLine("Se inregistreaza inchirierea, va rugam asteapta!");
                        System.Threading.Thread.Sleep(2000);


                        // Afișare confirmare
                        Console.WriteLine("\n======================================");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("INCHIRIERE SALVATA CU SUCCES!");
                        Console.ResetColor();
                        Console.WriteLine($"Client: {contract.Client.Nume} {contract.Client.Prenume}");
                        Console.WriteLine($"Masina: {contract.Automobil.Marca} {contract.Automobil.Model}");
                        Console.WriteLine($"Durata: {contract.NumarZile} zile");
                        Console.WriteLine("======================================");
                        Persoana clientNouEX = new Persoana("Ion", "Popescu", "123456789"); // exemplu
                        Masina masinaAleasaEX = new Masina("Dacia", "Logan"); // exemplu
                        Inchiriere contractEX = new Inchiriere(clientNou, masinaAleasa, 5); // exemplu

                        admin.AdaugaInchiriere(contract);

                        Console.WriteLine("\nApasa orice tasta pentru a reveni la meniu...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("\n--- Lista Inchirieri ---");
                        var toateInchirierile = admin.GetInchirieri();
                        foreach (var inch in toateInchirierile)
                        {
                            Console.WriteLine($"Client: {inch.Client.Nume}, Masina: {inch.Automobil.Marca}");
                        }
                        Console.WriteLine("\nApasa orice tasta pentru a reveni la meniu...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Write("Introdu marca cautata: ");
                        string marcaCautata = Console.ReadLine();
                        var rezultate = admin.CautaMasiniDupaMarca(marcaCautata);
                        foreach (var rez in rezultate)
                        {
                            Console.WriteLine($"Gasit: Client {rez.Client.Nume} cu masina {rez.Automobil.Marca}");
                        }
                        Console.WriteLine("\nApasa orice tasta pentru a reveni la meniu...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "0":
                        ruleaza = false;
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
        }
    }
}