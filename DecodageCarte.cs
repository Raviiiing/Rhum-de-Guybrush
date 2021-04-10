using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PROJET_CSHARP
{
    class DecodageCarte
    {

        // Attributs
        private Dictionary<char, List<string>> parcelle = new Dictionary<char, List<string>>();
        private int[,] carteDecodeCopy = new int[10, 10];
        private char[,] carteClair = new char[10, 10];
        // Constructeur
        public DecodageCarte(string cheminAcces)
        {
            string str;
            int x = 0;
            int y = 0;
            try
            {
                StreamReader file = new StreamReader(cheminAcces);
                while ((str = file.ReadLine()) != null)
                {
                    string[] tableauLigne = str.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string ligne in tableauLigne)
                    {
                        string[] valeur = ligne.Split(':');
                        foreach (string entier in valeur)
                        {
                            carteDecodeCopy[x, y] = Convert.ToInt32(entier);
                            y++;
                        }
                        x++;
                        y = 0;
                    }
                }
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        // Méthodes
        public void DecodageDeLaCarte()
        {
            int x = 0;
            int y = 0;
            char lettreTerrain = 'a';
            bool frontiereNord;
            bool frontiereOuest;
            bool frontiereEst;
            for (x = 0; x < 10; x++)
            {
                for (y = 0; y < 10; y++)
                {
                    // Terrain
                    if (carteDecodeCopy[x, y] >= 0 && carteDecodeCopy[x, y] <= 15)
                    {
                        int valeur = carteDecodeCopy[x, y];
                        List<int> puissance = new List<int>();
                        List<int> calculePuissance = new List<int>();
                        for (int i = 0; i < 4; i++)
                        {
                            //puissance.Add(valeur % 2);
                            if (valeur % 2 == 1)
                                calculePuissance.Add((int)System.Math.Pow(2, i));

                            valeur = valeur / 2;
                        }
                        frontiereNord = false;
                        frontiereOuest = false;
                        frontiereEst= false;
                        if (calculePuissance.Count != 0)
                        {
                            foreach (int teste in calculePuissance)
                            {
                                switch (teste)
                                {
                                    case 1: // Nord FRONTIERE
                                        frontiereNord = true;
                                        break;
                                    case 2: // Ouest FRONTIERE
                                        frontiereOuest = true;
                                        break;
                                    case 8:// Est FRONTIERE
                                        frontiereEst = true;
                                        break;

                                }
                                if (frontiereNord && frontiereOuest)
                                    if (x != 0)
                                    {
                                        if(carteClair[x,y-1]!='M' && carteClair[x,y-1]!='F' || carteClair[x-1, y] != 'M' && carteClair[x-1, y] != 'F')
                                        {
                                        //lettreTerrain++;
                                        }
                                        carteClair[x, y] = lettreTerrain;
                                         
                                    }
                                    else
                                        carteClair[x, y] = lettreTerrain;
                                else if (frontiereNord && !frontiereOuest)
                                {
                                    if (y != 0)
                                        carteClair[x, y] = carteClair[x, y - 1];
                                    else
                                        carteClair[x, y] = lettreTerrain;
                                }
                                else if (!frontiereNord && frontiereOuest)
                                {
                                    if (x != 0)
                                        carteClair[x, y] = carteClair[x - 1, y];
                                    else
                                        carteClair[x, y] = lettreTerrain;
                                }
                                else if(frontiereEst && !frontiereNord)
                                {
                                    if(y!=0)
                                    {
                                        carteClair[x, y] = carteClair[x, y - 1];
                                    }
                                }
                            }
                        }
                        else
                            if (x != 0)
                            carteClair[x, y] = carteClair[x - 1, y];
                        else if (y != 0)
                            carteClair[x, y] = carteClair[x, y - 1];
                    }
                    // Forêt
                    if (carteDecodeCopy[x, y] >= 32 && carteDecodeCopy[x, y] <= 47)
                    {
                        carteClair[x, y] = 'F';
                    }
                    // Mer
                    if (carteDecodeCopy[x, y] >= 64 && carteDecodeCopy[x, y] <= 79)
                    {
                        carteClair[x, y] = 'M';
                    }
                }
            }
            InitParcelle();
        }

        public void Affiche()
        {
            int x, y;
            for (x = 0; x < 10; x++)
            {
                for (y = 0; y < 10; y++)
                {
                    if (carteClair[x, y] == 'M')
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (carteClair[x, y] == 'F')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("{0} ", carteClair[x, y]);
                }
                Console.WriteLine("");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void InitParcelle()
        {
            int x = 0, y = 0;
            for (x = 0; x < 10; x++)
            {
                for (y = 0; y < 10; y++)
                {
                    if (carteClair[x, y] != 'F' && carteClair[x, y] != 'M')
                    {
                        if (!parcelle.ContainsKey(carteClair[x, y]))
                            parcelle.Add(carteClair[x, y], new List<string>());

                        parcelle[carteClair[x, y]].Add(x + "," + y);
                    }
                }
            }
        }

        public void Parcelles()
        {
            foreach (KeyValuePair<char, List<string>> tab in parcelle)
            {
                List<string> coordonne = tab.Value;
                Console.WriteLine("PARCELLE {0} - {1} unites ", tab.Key, coordonne.Count);
                foreach (string co in coordonne)
                    Console.Write("({0}) ", co);

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
            int total = 0, nbParcelle = 0;
            foreach (KeyValuePair<char, List<string>> tab in parcelle)
            {
                List<string> coordonne = tab.Value;
                total = total + coordonne.Count;
                nbParcelle = nbParcelle + 1;
            }
            double aire = (Double)total / nbParcelle;

            Console.WriteLine("Aire moyenne: {0:0.00}", aire);
        }
    }
}