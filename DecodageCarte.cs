﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PROJET_CSHARP
{
    /// <summary>
    /// Permet de décoder la carte codée
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
        /// Permet de garder en mémoire la carte décodée
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
                // On commence la lecture du fichier
                StreamReader file = new StreamReader(cheminAcces);
                while ((str = file.ReadLine()) != null)
                {
                    // On ajoute chaques ligne dans un tableau de string avec un split sur le char '|'
                    // Le StringSplitOptions.RemoveEmptyEntries permet de supprimer les espaces vides laissé par le split du char '|'
                    string[] tableauLigne = str.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string ligne in tableauLigne)
                    {
                        //Puis on split chaque ligne sur le char ':' et on ajoute les valeurs dans un tableau de valeur.
                        string[] valeur = ligne.Split(':');
                        //Puis chaque valeur est stockée dans un tableau d'entier
                        foreach (string entier in valeur)
                        {
                            carteDecodeCopy[x, y] = Convert.ToInt32(entier);
                            Console.WriteLine("x : {0}, y : {1} et entier : {2}", x, y, entier);
                            y++;
                        }
                        x++; //On descend d'une ligne
                        y = 0; //On recommence au début de la ligne x
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
        /// Décode la carte et retient en mémoire la carte clair
        /// </summary>
        public void DecodageDeLaCarte()
        {
            int valeur; // stock en mémoire la valeur de la case de carteDecodeCopy[hauteur, largeur]
            char lettreParcelle = 'a';
            char lettreEnAttente = lettreParcelle;
            bool frontiereNord;
            bool frontiereEst;
            bool valeurTempo = false;
            List<string> charAEcrire = new List<string>();

            for (int hauteur = 0; hauteur < 10; hauteur++)      // Boucle qui parcours une carte chiffrée
            {                                                   // puis décode cette carte pour  
                for (int largeur = 0; largeur < 10; largeur++)  // écrire une carte clair
                {
                    if (carteDecodeCopy[hauteur, largeur] >= 0 && carteDecodeCopy[hauteur, largeur] <= 15) // Si la parcelle est du Terrain
                    {
                        valeur = carteDecodeCopy[hauteur, largeur]; //On copie la valeur de l'unité dans la variable valeur
                        List<int> calculePuissance = new List<int>(); // Garde en mémoire les puissances (0/1/2/3) de 2 de chaque unité


                        // Mini algorithme permettant de trouver la puissance x (0,1,2 ou 3) de 2 
                        // en divisant successivement la valeur de l'unité par 2
                        // Par exemple pour une parcelle = 2, nous auron une puissance de 1 car 2^1=2 donc fonrtière OUEST.
                        for (int i = 0; i < 4; i++) 
                        {                           
                            if (valeur % 2 == 1) //Si la valeur de l'unité est impaire = frontière
                                calculePuissance.Add((int)Math.Pow(2, i)); //On ajoute la valeur de la puissance pour plus tard connaître la/les frontière(s)

                            valeur = valeur / 2; //On divise la valeur de l'unité
                        }

                        frontiereNord = false;
                        frontiereEst = false;


                        if (calculePuissance.Count != 0) // S'il y a des frontières, on cherche si ce sont des frontières NORD et/ou EST.
                        {
                            foreach (int teste in calculePuissance) // Indication des frontière NORD et/ou EST avec des booléens
                            {
                                switch (teste)
                                {
                                    case 1: // Nord FRONTIERE.
                                        frontiereNord = true;
                                        break;
                                    case 8: // Est FRONTIERE.
                                        frontiereEst = true;
                                        break;
                                }
                            }

                            if (!frontiereEst) // S'il n'y a pas de frontières à l'EST.
                            {
                                // On ajoute les coordonnées des caractères qui seront à écrire
                                // jusqu'à ce qu'il y ait une frontière à l'EST dans la List<string> charAEcrire
                                // sous forme de string "hauteur:largeur".
                                charAEcrire.Add(hauteur + ":" + largeur); 

                                if (!frontiereNord && !valeurTempo) 
                                {
                                    lettreEnAttente = carteClair[hauteur - 1, largeur];
                                    valeurTempo = true;
                                }
                            }
                            else
                            {
                                // S'il y a une frontière à l'EST mais pas de frontière au NORD alors
                                // la valeur hauteur,largeur de la carte prend la valeur en (hauteur-1,largeur) (= la valeur du dessus).
                                if (!frontiereNord && !valeurTempo)
                                    carteClair[hauteur, largeur] = carteClair[hauteur - 1, largeur];
                                else 
                                    //Sinon elle prend la variable lettreEnAttente
                                    carteClair[hauteur, largeur] = lettreEnAttente;

                                // On écrit le caractère qu'il faut sur les coordonnées que l'on a ajouté dans
                                // la liste charAEcrire auparavant (s'il y en a).
                                if (charAEcrire.Count != 0)
                                {
                                    foreach (string caractere in charAEcrire)
                                    {
                                        // On récupère le hauteur du string "hauteur:largeur".
                                        int valHauteur = Convert.ToInt32(caractere.Remove(caractere.IndexOf(":")));
                                        // On récupère le largeur du string "hauteur:largeur".
                                        int valLargeur = Convert.ToInt32(caractere.Substring(caractere.LastIndexOf(":") + 1));

                                        carteClair[valHauteur, valLargeur] = lettreEnAttente;
                                    }
                                }

                                // On incrémente le char dans le cas où il n'y a pas de frontière au Nord et
                                // qu'il n'est pas relié a un morceau de parcelle au dessus (!valeurTempo)
                                if (!valeurTempo && frontiereNord)
                                    lettreParcelle++;

                                charAEcrire.Clear();
                                valeurTempo = false;
                                lettreEnAttente = lettreParcelle;
                            }
                        }
                        else // S'il n'y a pas de frontière(s), la valeur en (hauteur,largeur)
                             // est forcément en dessous de la valeur qu'il lui faudra (hauteur-1,largeur)
                        {
                            carteClair[hauteur, largeur] = carteClair[hauteur - 1, largeur];
                        }
                    }
                    else if (carteDecodeCopy[hauteur, largeur] >= 32 && carteDecodeCopy[hauteur, largeur] <= 47) // Si la parcelle est de la Forêt
                    {
                        carteClair[hauteur, largeur] = 'F';
                    }
                    else if (carteDecodeCopy[hauteur, largeur] >= 64 && carteDecodeCopy[hauteur, largeur] <= 79) // Si la parcelle est de la Mer
                    {
                        carteClair[hauteur, largeur] = 'M';
                    }
                }
            }
            InitParcelle(); // Pour préajouter les coordonées des parcelles en fonction des caractères
        }

        /// <summary>
        /// Permet d'afficher la carte dans la console
        /// </summary>
        public void Affiche()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    //On affiche les caractères en fonction du type avec une couleur qui lui est propre
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
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Permet d'initialiser en mémoire le dictionnaire de données qui va sauvegarder l'ensemble des coordonnées pour chaques type de parcelles
        /// </summary>
        private void InitParcelle()
        {
            int x = 0, y = 0;
            for (x = 0; x < 10; x++)
            {
                for (y = 0; y < 10; y++)
                {
                    //Si on a un caractère autre que Mer ou Forêt
                    if (carteClair[x, y] != 'F' && carteClair[x, y] != 'M')
                    {
                        //Si le caractère n'existe pas dans le dictionnaire de donnée on l'ajoute puis on lui créer une Liste de coordonnée
                        if (!parcelle.ContainsKey(carteClair[x, y]))
                            parcelle.Add(carteClair[x, y], new List<string>());
                        //On ajoute la coordonnée de l'unité dans la liste qui correspond à son caractére.
                        parcelle[carteClair[x, y]].Add(x + "," + y);
                    }
                }
            }
        }

        /// <summary>
        /// Permet d'afficher le nombre parcelles ainsi que leurs coordonnées en fonction de leurs caractères
        /// </summary>
        public void Parcelles()
        {
            foreach (KeyValuePair<char, List<string>> tab in parcelle)
            {
                //On affiche le nombre d'unitée par parcelle
                List<string> coordonne = tab.Value;
                Console.WriteLine("PARCELLE {0} - {1} unites ", tab.Key, coordonne.Count);
                //Puis on affiche les coordonnées de toutes les unités dans la parcelle
                foreach (string co in coordonne)
                    Console.Write("({0}) ", co);

                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// Affiche la taille d'une parcelle en fonction du caractère demandé
        /// </summary>
        /// <param name="parc">Correspond au caractère qui est demandé</param>
        public void TailleParcelle(char parc)
        {
            try
            {
                List<string> coordonne = parcelle[parc]; //On prend la liste de coordonnée du dictionnaire de donnée via la clé qui vaut "parc"
                Console.WriteLine("Taille de la parcelle {0} : {1} ", parc, coordonne.Count);
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur : Parcelle inexistante");
                return;
            }
            Console.WriteLine(" ");
        }

        /// <summary>
        /// Affiche toutes les parcelles qui ont une aire supérieure à la taille demandée nb
        /// </summary>
        /// <param name="nb">Corresponds à l'aire demandée</param>
        public void TailleSupp(int nb)
        {
            foreach (KeyValuePair<char, List<string>> tab in parcelle) // Pour chaque Parcelle faire
            {
                List<string> coordonne = tab.Value; 
                if (coordonne.Count >= nb) //Si le nombre de coordonnée (unités) est supérieur ou egal au nombre nb 
                {
                    Console.WriteLine("Parcelle {0}: {1} unites ", tab.Key, coordonne.Count);
                }
            }
            Console.WriteLine(" ");
        }

        /// <summary>
        /// Permets d'afficher l'aire moyenne de toutes les parcelles de la carte
        /// </summary>
        public void AireMoyenne()
        {
            int total = 0, nbParcelle = 0;
            //Pour chaque liste de coordonnée par parcelle
            foreach (KeyValuePair<char, List<string>> tab in parcelle)
            {
                List<string> coordonne = tab.Value;
                total = total + coordonne.Count; // Compte le nombre de coordonne dans la parcelle
                nbParcelle = nbParcelle + 1; // On incrémente le nombre de parcelle
            }
            //Une fois qu'on a compté le nombre de coordonnées total 
            double aire = (Double)total / nbParcelle; // On divise le nombre de coordonnées total par le nombre de parcelle

            Console.WriteLine("Aire moyenne: {0:0.00}", aire);
            Console.WriteLine(" ");
        }
    }
    #endregion
}