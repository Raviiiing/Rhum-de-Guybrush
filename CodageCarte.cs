using System;
using System.Collections.Generic;
using System.Text;

namespace PROJET_CSHARP 
{
    class CodageCarte
    {
        private char[,] carteCopy;
        private int[,] cartesCode = new int[10, 10];

        public CodageCarte(CarteDecodee carte)
        {
            carteCopy = carte.GetCartes;
        }

        public int[,] CodageDeLaCarte()
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

                    Console.WriteLine(valeur);
                }
            }
            return cartesCode;
        }
    }
}
