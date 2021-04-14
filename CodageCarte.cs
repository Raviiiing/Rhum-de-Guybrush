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

        #region Attributs
        /// <summary>
        /// Copie des cartes .clair dans un tableau 2D
        /// </summary>
        private char[,] carteCopy = new char[10,10];
        /// <summary>
        /// Chaîne de caractère contenant la carte codée 
        /// Exemple (3:0:0|5:3:2 ..)
        /// </summary>
        private string carteUneFoisCodee;
        /// <summary>
        /// Chemin du fichier .clair (C:/User/Public...)
        /// </summary>
        private string fichierPath;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la classe initalisant la lecture du fichier
        /// </summary>
        /// <param name="accesFichier">Chemin d'accès au fichier</param>
        public CodageCarte(string accesFichier)
        {
            this.fichierPath = accesFichier;

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
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode codant la carte
        /// </summary>
        /// <returns></returns>
        public void CodageDeLaCarte()
        {
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
                    carteUneFoisCodee = carteUneFoisCodee + valeur; 
                    if (y == 9)
                        carteUneFoisCodee = carteUneFoisCodee + '|';
                    else
                        carteUneFoisCodee = carteUneFoisCodee + ':';
                }
            }
        }

        /// <summary>
        /// Permet de créer le fichier .chiffre de la carte codée
        /// </summary>
        public void CreerFichier()
        {
            try
            {
                //Chemin du dossier sans la nom du fichier
                string pathDossier = this.fichierPath.Remove(this.fichierPath.LastIndexOf("/") + 1);
                //Nom du fichier avec l'extension .clair
                string nomFichier = Path.GetFileName(this.fichierPath);
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