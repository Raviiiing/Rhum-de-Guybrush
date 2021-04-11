using System;

namespace PROJET_CSHARP
{
    class Program
    {
        static void Main(string[] args)
        {
            DecodageCarte lacarte = new DecodageCarte(@"../../../Cartes/Scabb.chiffre");
            lacarte.DecodageDeLaCarte();
            lacarte.Affiche();
            lacarte.AireMoyenne();
            lacarte.Parcelles();
            lacarte.TailleSupp(4);
            lacarte.TailleParcelle('a');

            CodageCarte nouvcarte = new CodageCarte(@"../../../Cartes/Scabb.clair");
            nouvcarte.CodageDeLaCarte();
            nouvcarte.creerFichier();
        }
    }
}