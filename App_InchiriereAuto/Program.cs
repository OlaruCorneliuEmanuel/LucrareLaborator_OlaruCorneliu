using System;

namespace App_InchiriereAuto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistem Gestiune Închirieri Auto ===");
            Console.WriteLine();

            // 1. Citirea datelor pentru Persoană
            Console.Write("Introduceti numele clientului: ");
            string nume = Console.ReadLine();

            Console.Write("Introduceti prenumele clientului: ");
            string prenume = Console.ReadLine();

            Console.Write("Introduceti CNP: ");
            string cnp = Console.ReadLine();

            // Creăm obiectului de tip Persoana
            Persoana clientNou = new Persoana(nume, prenume, cnp);


            // 2. Citirea datelor pentru Mașină
            Console.WriteLine("\n--- Detalii Mașină ---");
            Console.Write("Marca: ");
            string marca = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Masina masinaAleasa = new Masina(marca, model);


            // 3. Detalii închiriere
            Console.Write("\nNumăr de zile pentru închiriere: ");
            int zile = int.Parse(Console.ReadLine());

            // Cream obiectul de tip  Inchiriere
            Inchiriere contract = new Inchiriere(clientNou, masinaAleasa, zile);


            // 4. Afișare confirmare
            Console.WriteLine("\n======================================");
            Console.WriteLine("ÎNCHIRIERE ÎNREGISTRATĂ CU SUCCES!");
            Console.WriteLine($"Client: {contract.Client.Nume} {contract.Client.Prenume}");
            Console.WriteLine($"Mașină: {contract.Automobil.Marca} {contract.Automobil.Model}");
            Console.WriteLine($"Durată: {contract.NumarZile} zile");
            Console.WriteLine("======================================");

            // Menținem consola deschisă
            Console.WriteLine("\nApasă orice tastă pentru a inchide...");
            Console.ReadKey();
        }
    }
}