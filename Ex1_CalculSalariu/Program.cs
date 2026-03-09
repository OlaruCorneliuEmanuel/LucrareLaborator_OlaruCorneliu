using System;

Console.Write("Ore lucrate: ");
int ore = int.Parse(Console.ReadLine());

Console.Write("Tarif pe ora: ");
double tarif = double.Parse(Console.ReadLine());

double salariu = ore * tarif;

Console.WriteLine("Salariu total: " + salariu + " lei");

if (salariu > 3000)
{
    Console.WriteLine("Salariu mare");
}
else
{
    Console.WriteLine("Ati lucrat prea putine ore pentru a avea un salariu mare!");
}