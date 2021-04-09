using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PROJET_CSHARP
{
    class CarteCodee
    {
        private int[,] cartesCode = new int[10, 10];

        public int[,] GetCarteCodee { get => cartesCode; }

        public CarteCodee(string cheminAcces)
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
                            cartesCode[x, y] = Convert.ToInt32(entier);
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
    }
}
