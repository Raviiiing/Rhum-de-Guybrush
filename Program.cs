using System;

namespace PROJET_CSHARP
{
    class Program
    {
        static void Main(string[] args)
        {
            Carte TEST = new Carte(@"C:\Users\Yann\OneDrive\IUT\Semestre 2\C#\PROJET\Projet\PROJET_CSHARP\Cartes\Scabb.chiffre", true);
            TEST.Affiche();
        }
    }
}