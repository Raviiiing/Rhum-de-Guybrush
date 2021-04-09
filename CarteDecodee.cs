using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PROJET_CSHARP
{
    class CarteDecodee
    {
        private string fichierPath;
        private char[,] cartes = new char[10, 10];
        private Dictionary<char, List<string>> parcelle = new Dictionary<char, List<string>>();
        public char[,] GetCartes { get => cartes; }
        public string GetFichierPath { get => fichierPath; }

        public CarteDecodee(string accesFichier)
        {
            string str;
            this.fichierPath = accesFichier;
            int x = 0;
            int y = 0;
            try
            {
                StreamReader file = new StreamReader(accesFichier);
                while ((str = file.ReadLine()) != null)
                {
                    foreach (char c in str)
                    {
                        cartes[x, y] = c;
                        y++;
                    }
                    x++;
                    y = 0;
                }
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }

        public void Affiche()
        {
            int x, y;
            for (x = 0; x < 10; x++)
            {
                for (y = 0; y < 10; y++)
                {
                    if (cartes[x, y] == 'M')
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (cartes[x, y] == 'F')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("{0} ", cartes[x, y]);
                }
                Console.WriteLine("");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void InitParcelle()
        {
            int x = 0, y = 0;
            for (x = 0; x < 10; x++)
            {
                for (y = 0; y < 10; y++)
                {
                    if (cartes[x, y] != 'F' && cartes[x, y] != 'M')
                    {
                        if (!parcelle.ContainsKey(cartes[x, y]))
                            parcelle.Add(cartes[x, y], new List<string>());

                        parcelle[cartes[x, y]].Add(x + "," + y);
                    }
                }
            }
        }

        public void parcelles()
        {
            foreach (KeyValuePair<char, List<string>> tab in parcelle)
            {
                List<string> coordonne = tab.Value;
                Console.WriteLine("PARCELLE {0} - {1} unites ", tab.Key, coordonne.Count);
                foreach (string co in coordonne)
                    Console.Write("({0}) ",co);

                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }
        }
        public void TailleParcelle(char parc)
        {
            try
            {
                List<string> coordonne = parcelle[parc];
                Console.WriteLine("Taille de la parcelle {0} : {1} ", parc, coordonne.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : Parcelle inexistante");
                return;
            }
            Console.WriteLine(" ");
        }

        public void TailleSupp(int nb)
        {
            foreach (KeyValuePair<char, List<string>> tab in parcelle)
            {
                List<string> coordonne = tab.Value;
                if (coordonne.Count >= nb)
                {
                    Console.WriteLine("Parcelle {0}: {1} unites ", tab.Key, coordonne.Count);
                }
            }
        }

        public void AireMoyenne()
        {
            int total=0, nbParcelle=0;
            foreach (KeyValuePair<char, List<string>> tab in parcelle)
            {
                List<string> coordonne = tab.Value;
                total = total + coordonne.Count;
                nbParcelle = nbParcelle + 1;
            }
            double aire =(Double)total / nbParcelle;
            
            Console.WriteLine("Aire moyenne: {0:0.00}",aire);
        }
    }
}
