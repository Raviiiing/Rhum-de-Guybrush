using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PROJET_CSHARP
{
    class Carte
    {
        private char[,] cartes = new char[10,10];
        private int[,] cartesCode = new int[10, 10];
        private bool estCoder;
        public Carte(string accesFichier, bool coder)
        {
            string str;
            int x = 0;
            int y = 0;
            this.estCoder = coder;
            if (coder == false)
            {
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
            else
            {
                try
                {
                    StreamReader file = new StreamReader(accesFichier);
                    while ((str = file.ReadLine()) != null)
                    {
                        string[] tableauLigne = str.Split('|', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string ligne in tableauLigne)
                        {
                            string[] valeur = ligne.Split(':');
                            foreach(string entier in valeur)
                            {
                                cartesCode[x,y] = Convert.ToInt32(entier);
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
                    Console.WriteLine(e);
                    return;
                }
            }
        }
        public void Affiche()
        {
            int x, y;
            for(x=0;x<10;x++)
            {
                for(y=0;y<10;y++)
                {
                    if (estCoder == false)
                    {
                        Console.Write("{0} ", cartes[x, y]);
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
