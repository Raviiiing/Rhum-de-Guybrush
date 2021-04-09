using System;

namespace PROJET_CSHARP
{
    class Program
    {
        static void Main(string[] args)
        {
            CarteDecodee laCarte = new CarteDecodee(@"../../../Cartes/Scabb.clair");
            CodageCarte codageCarte = new CodageCarte(laCarte);
            codageCarte.codage();
        }
    }
}