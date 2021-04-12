using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PROJET_CSHARP
{
    /// <summary>
    /// Classe DecodageCarte : Permet de décoder la carte codée
    /// </summary>
    class DecodageCarte
    {

        #region Attributs
        /// <summary>
        /// Associe à chaque lettre la liste des coordonnées
        /// </summary>
        private Dictionary<char, List<string>> parcelle = new Dictionary<char, List<string>>();
        /// <summary>
        /// Permet de mettre la carte codée dans un tableau
        /// </summary>
        private int[,] carteDecodeCopy = new int[10, 10];
        /// <summary>
        /// Permet de garder en memoir la carte décoder
        /// </summary>
        private char[,] carteClair = new char[10, 10];
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="cheminAcces">Chemin d'acces du fichier de la carte</param>
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
        #endregion

        #region Méthodes
        /// <summary>
        /// Décode la carte et retient en memoir la carte clair
        /// </summary>
        public void DecodageDeLaCarte()
        {
            int valeur; // stock en memoir la valeur de la case de carteDecodeCopy[x, y]
            char lettreParcelle = 'a';
            char valeurEnAttente = lettreParcelle;
            bool frontiereNord;
            bool frontiereEst;
            bool valeurTempo = false;
            List<string> charAEcrire = new List<string>();
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (carteDecodeCopy[x, y] >= 0 && carteDecodeCopy[x, y] <= 15) // Terrain
                    {
                        valeur = carteDecodeCopy[x, y];
                        List<int> calculePuissance = new List<int>();

                        for (int i = 0; i < 4; i++)
                        {
                            if (valeur % 2 == 1)
                                calculePuissance.Add((int)System.Math.Pow(2, i));

                            valeur = valeur / 2;
                        }

                        frontiereNord = false;
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
                                    case 8: // Est FRONTIERE
                                        frontiereEst = true;
                                        break;

                                }
                            }

                            if (!frontiereEst)
                            {
                                charAEcrire.Add(x + ":" + y);

                                if (!frontiereNord && !valeurTempo)
                                {
                                    valeurEnAttente = carteClair[x-1, y];
                                    valeurTempo = true;
                                }
                            }
                            else
                            {
                                if (frontiereNord)
                                {
                                    if (valeurTempo)
                                    {

                                        carteClair[x, y] = valeurEnAttente;
                                        if (charAEcrire.Count != 0)
                                        {
                                            foreach (string caractere in charAEcrire)
                                            {
                                                int valX = Convert.ToInt32(caractere.Remove(caractere.IndexOf(":")));
                                                int valY = Convert.ToInt32(caractere.Substring(caractere.LastIndexOf(":") + 1));

                                                carteClair[valX, valY] = valeurEnAttente;
                                            }
                                        }
                                        charAEcrire.Clear();
                                        valeurTempo = false;
                                    }
                                    else
                                    {
                                        carteClair[x, y] = lettreParcelle;
                                        if (charAEcrire.Count != 0)
                                        {
                                            foreach (string caractere in charAEcrire)
                                            {
                                                int valX = Convert.ToInt32(caractere.Remove(caractere.IndexOf(":")));
                                                int valY = Convert.ToInt32(caractere.Substring(caractere.LastIndexOf(":") + 1));

                                                carteClair[valX, valY] = lettreParcelle;
                                            }
                                        }
                                        lettreParcelle++;
                                        charAEcrire.Clear();
                                    }
                                    
                                }
                                else
                                {
                                    carteClair[x, y] = carteClair[x-1, y];
                                    if (charAEcrire.Count != 0)
                                    {
                                        foreach (string caractere in charAEcrire)
                                        {
                                            int valX = Convert.ToInt32(caractere.Remove(caractere.IndexOf(":")));
                                            int valY = Convert.ToInt32(caractere.Substring(caractere.LastIndexOf(":") + 1));

                                            carteClair[valX, valY] = carteClair[x - 1, y];
                                        }
                                    }
                                    charAEcrire.Clear();
                                }
                                valeurTempo = false;

                            }
                        }
                        else
                        {
                            carteClair[x, y] = carteClair[x-1, y];
                        }   
                    }else if (carteDecodeCopy[x, y] >= 32 && carteDecodeCopy[x, y] <= 47) // Forêt
                    {
                        carteClair[x, y] = 'F';
                    }else if (carteDecodeCopy[x, y] >= 64 && carteDecodeCopy[x, y] <= 79) // Mer
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
        #endregion
    }
}