using System;

namespace PROJET_CSHARP
{
    class Program
    {
        static void Main(string[] args)
        {
            CarteDecodee laCarte = new CarteDecodee(@"C:\Users\Yann\OneDrive\IUT\Semestre 2\C#\PROJET\Projet\PROJET_CSHARP\Cartes\Scabb.clair");
            laCarte.Affiche();
        }
    }
}