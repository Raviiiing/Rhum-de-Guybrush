using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PROJET_CSHARP 
{
    /// <summary>
    /// Permet de coder une carte
    /// </summary>
    class CodageCarte
    {

        #region Méthodes
        /// <summary>
        /// Méthode de la classe initalisant la lecture du fichier
        /// </summary>
        public static void CodageCarteFichier(string accesFichier)
        {
            char[,] carteCopy = new char[10, 10];
            try
            {
                StreamReader file = new StreamReader(accesFichier);
                int x = 0;
                int y = 0;
                string str;
                while ((str = file.ReadLine()) != null)
                {
                    foreach (char c in str)
                    {
                        carteCopy[x, y] = c;
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
            CodageDeLaCarte(accesFichier, carteCopy);
        }

        /// <summary>
        /// Méthode codant la carte
        /// </summary>
        private static void CodageDeLaCarte(string fichierPath, char[,] carteCopy)
        {
            string carteUneFoisCodee = "";
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
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

                    //Permet d'ajouter au String "carteUneFoisCodee" la valeur
                    //Exemple : Si on a String x = "test";
                    // Si on fait x = x + " ça va"
                    // Alors x devient "test ça va"
                    carteUneFoisCodee += valeur; 
                    if (y == 9)
                        carteUneFoisCodee += '|';
                    else
                        carteUneFoisCodee += ':';
                }
            }
            CreerFichier(fichierPath, carteUneFoisCodee);
        }

        /// <summary>
        /// Permet de créer le fichier .chiffre de la carte codée
        /// </summary>
        private static void CreerFichier(string fichierPath, string carteUneFoisCodee)
        {
            try
            {
                //Chemin du dossier sans la nom du fichier
                string pathDossier = fichierPath.Remove(fichierPath.LastIndexOf("/") + 1);
                //Nom du fichier avec l'extension .clair
                string nomFichier = Path.GetFileName(fichierPath);
                //On supprime l'extension .clair et on remplace par .chiffre
                string nomFichierAvecExtension = nomFichier.Remove(nomFichier.LastIndexOf(".") + 1) + "chiffre";

                if (File.Exists(pathDossier + nomFichierAvecExtension))
                    File.Delete(pathDossier + nomFichierAvecExtension);

                using (StreamWriter streamWriter = File.AppendText(@pathDossier + nomFichierAvecExtension))
                {
                    streamWriter.WriteLine(carteUneFoisCodee);
                    streamWriter.Close();
                }

                Console.WriteLine("Le fichier {0} à été créé", nomFichierAvecExtension);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Chemin de fichier introuvable : erreur");
                return;
            }
        }
        #endregion
    }
}