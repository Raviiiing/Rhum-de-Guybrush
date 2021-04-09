using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PROJET_CSHARP
{
    class CarteDecodee
    {
        private char[,] cartes = new char[10, 10];
        public char[,] GetCartes { get => cartes; }

        public CarteDecodee(string accesFichier)
        {
            string str;
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
    }
}
