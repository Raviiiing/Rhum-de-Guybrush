using System;

namespace PROJET_CSHARP
{
    class Program
    {
        /// <summary>
        /// Permet de lancer le programme Rhum de Guybrush
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            //Permet de créer l'objet codant une carte .clair
            CodageCarte.CodageCarteFichier(@"../../../Cartes/Phatt.clair");

            //Permet de créer l'objet décodant une carte .chiffre
            DecodageCarte lacarte = new DecodageCarte(@"../../../Cartes/Phatt.chiffre");
            lacarte.DecodageDeLaCarte();
            lacarte.Affiche();
            lacarte.AireMoyenne();
            lacarte.Parcelles();
            lacarte.TailleSupp(4);
            lacarte.TailleParcelle('a');
        }
    }
}