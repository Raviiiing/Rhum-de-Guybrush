using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PROJET_CSHARP 
{
    class CodageCarte
    {
        private char[,] carteCopy = new char[10,10];
        private string carteUneFoisCodee;
        private string fichierPath;
        public CodageCarte(string accesFichier)
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
                string pathDossier = this.fichierPath.Remove(this.fichierPath.LastIndexOf("/") + 1);
                string nomFichier = Path.GetFileName(this.fichierPath);
                string nomFichierSansExtension = nomFichier.Remove(nomFichier.LastIndexOf(".") + 1);
                if (File.Exists(pathDossier + nomFichierSansExtension + "chiffre"))
                    File.Delete(pathDossier + nomFichierSansExtension + "chiffre");
                using (StreamWriter streamWriter = File.AppendText(@pathDossier + nomFichierSansExtension + "chiffre"))
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
