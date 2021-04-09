﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PROJET_CSHARP 
{
    class CodageCarte
    {
        private CarteDecodee carteADecodee;
        private char[,] carteCopy;
        string carteUneFoisCodee;

        public CodageCarte(CarteDecodee carte)
        {
            this.carteADecodee = carte;
            this.carteCopy = carte.GetCartes;
        }

        public string CodageDeLaCarte()
        {
            int x, y;
            for (x = 0; x < 10; x++)
            {
                for (y = 0; y < 10; y++)
                {
                    int valeur = 0;
                    if(x == 0)
                        valeur += 1;

                    if (y == 0)
                        valeur += 2;

                    if (y == 9)
                        valeur += 8;

                    if (x == 9)
                        valeur += 4;

                    if(x != 9 && carteCopy[x+1,y] != carteCopy[x, y])
                        valeur += 4;

                    if (y != 9 && carteCopy[x, y + 1] != carteCopy[x, y])
                        valeur += 8;

                    if (y != 0 && carteCopy[x, y - 1] != carteCopy[x, y])
                        valeur += 2;

                    if (x != 0 && carteCopy[x - 1, y] != carteCopy[x, y])
                        valeur +=  1;

                    if(carteCopy[x,y] == 70)
                        valeur += 32;
                    else if (carteCopy[x,y] == 77)
                        valeur += 64;

                    carteUneFoisCodee = carteUneFoisCodee + valeur;
                    if(y == 9)
                        carteUneFoisCodee = carteUneFoisCodee + '|';
                    else
                        carteUneFoisCodee = carteUneFoisCodee + ':';
                }
            }
            return carteUneFoisCodee;
        }

        public void creerFichier()
        {
            try
            {
                string pathDossier = carteADecodee.GetFichierPath.Remove(carteADecodee.GetFichierPath.LastIndexOf("/") + 1);
                string nomFichier = Path.GetFileName(carteADecodee.GetFichierPath);
                string nomFichierSansExtension = nomFichier.Remove(nomFichier.LastIndexOf(".") + 1);

                using(StreamWriter streamWriter = File.AppendText(@pathDossier + "/" + nomFichierSansExtension + "chiffre"))
                {
                    streamWriter.WriteLine(carteUneFoisCodee);
                    streamWriter.Close();
                }

                Console.WriteLine("Le fichier {0}chiffre à été créé", nomFichierSansExtension);

            }
            catch (Exception e)
            {
                Console.WriteLine("Chemin de fichier introuvable : erreur");
                return;
            }
        }
    }
}
