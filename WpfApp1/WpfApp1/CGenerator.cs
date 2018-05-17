using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class CGenerator
    {
        public int[,] field;

        public bool isBroken(int x, int y)
        {
            bool res = true;

            if ((x < 0)||(x > field.GetLength(0)-1))
                throw new ArgumentException("ВЫХОД ЗА ГРАНИЦУ");

            if ((y < 0) || (y > field.GetLength(1) - 1))
                throw new ArgumentException("ВЫХОД ЗА ГРАНИЦУ");

            int minx = x - 1;
            if (minx < 0) minx = 0;
            int miny = y - 1;
            if (miny < 0) miny = 0;

            int maxx = x + 1;
            if (maxx > field.GetLength(0) - 1) maxx = field.GetLength(0) - 1;
            int maxy = y + 1;
            if (maxy > field.GetLength(1) - 1) maxy = field.GetLength(1) - 1;

            for (int i = minx; i <= maxx; i++)
            {
                for (int j = miny; j <= maxy; j++)
                {
                    if (field[i, j] == 0)
                    {
                        res = false;
                        break;
                    }
                }
                if (res == false) break;
            }
            return res;
        }


        public void init (int n)
        {
            //не больше
            //не меньше
            field = new int[n, n];
        }

        public void plantMines(int n)
        {
            Random kuku = new Random();
            //int m = kuku.Next();
            if (n > 10) 
                throw new ArgumentException("МНОГО МИН");

            if (n < 5)
                throw new ArgumentException("МАЛО МИН");

            for (int i = 0; i < n; i++)
            {
                int x = kuku.Next(field.GetLength(0));
                int y = kuku.Next(field.GetLength(1));

                if (field[x, y] == -1)
                {
                    i--;
                } else
                    field[x, y] = -1;
               
                for (int i1 = 0; i1 < field.GetLength(0); i1++)
                {
                    for (int j1 = 0; j1 < field.GetLength(1); j1++)
                        if (isBroken(x, y) == true)
                        {
                            field[x, y] = 0;
                            i--;
                            break;
                        }
                    if (field[x, y] == 0) break;
                }
            }
        }

        public void calculate()
        {
            for(int i = 0; i<field.GetLength(0);i++)
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == 0)
                    {
                        int minx = i - 1;
                        if (minx < 0) minx = 0;
                        int miny = j - 1;
                        if (miny < 0) miny = 0;

                        int maxx = i + 1;
                        if (maxx > field.GetLength(0) - 1) maxx = field.GetLength(0) - 1;
                        int maxy = j + 1;
                        if (maxy > field.GetLength(1) - 1) maxy = field.GetLength(1) - 1;

                        int sum = 0;

                        for (int i1 = minx; i1 <= maxx; i1++)
                        {
                            for (int j1 = miny; j1 <= maxy; j1++)
                            {
                                if (field[i1, j1] == -1)
                                {
                                    sum++;
                                }
                            }
                            
                        }
                        field[i, j] = sum;
                    }
                }

        }

        public int getCell(int i, int j)
        {
            //не больше
            //не меньше
            return field[i, j];
        }

        public bool minesCheck()
        {
            bool res = true;

            return res;
        }

        public void reveal(int i, int j)
        {
            if (i >= 0 && j >= 0 && i < (field.GetLength(0) ) && j < (field.GetLength(1)))
                if(field[i, j] == 0)
                {
                    field[i, j] = 10;

                    reveal(i, j-1);
                    reveal(i-1, j);
                    reveal(i, j+1);
                    reveal(i+1, j);
                }
        }

    }
}
