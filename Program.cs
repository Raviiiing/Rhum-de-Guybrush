using System;

namespace PROJET_CSHARP
{
    class Program
    {
        static void Main(string[] args)
        {
            CodageCarte nouvcarte = new CodageCarte(@"../../../Cartes/ScabbEdit.clair");
            nouvcarte.CodageDeLaCarte();
            nouvcarte.creerFichier();

            DecodageCarte lacarte = new DecodageCarte(@"../../../Cartes/ScabbEdit.chiffre");
            lacarte.DecodageDeLaCarte();
            lacarte.Affiche();
            lacarte.AireMoyenne();
            lacarte.Parcelles();
            lacarte.TailleSupp(4);
            lacarte.TailleParcelle('a');
        }
    }
}