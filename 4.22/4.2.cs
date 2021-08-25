using System;
using System.Linq;


namespace _4._22
{
    static class MatrixExt
    {
        public static int RowsCount(this double[,] matrix)
        {
            return matrix.GetLength(0);
        }


        public static int ColumnsCount(this double[,] matrix)
        {
            return matrix.GetLength(1);
        }
    }
    class Task
    {
        public double[,] tetta(double[,] a, double[,] Y, double[,] b)
        {
            double[,] multi;
            double[,] tetta;
            multi = Multiplication(Y,b);
            tetta = MatrixSubstract(a,multi);
            return tetta;
        }
        public double[,] Multiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
        static double[,] MatrixSubstract(double[,] matrixA, double[,] matrixB)
        {
            if ((matrixA.ColumnsCount() != matrixB.ColumnsCount()) || (matrixA.RowsCount() != matrixB.RowsCount()))
            {
                throw new Exception("Для матриц с разным размером вычитание не возможно");
            }

            var matrixC = new double[matrixA.RowsCount(), matrixB.ColumnsCount()];

            for (var i = 0; i < matrixA.RowsCount(); i++)
            {
                for (var j = 0; j < matrixB.ColumnsCount(); j++)
                {
                    matrixC[i, j] = matrixA[i, j] - matrixB[i, j];
                }
            }

            return matrixC;
        }
        public double[,] MatrixSum(double[,] matrixA, double[,] matrixB)
        {
            if ((matrixA.ColumnsCount() != matrixB.ColumnsCount()) || (matrixA.RowsCount() != matrixB.RowsCount()))
            {
                throw new Exception("Для матриц с разным размером вычитание не возможно");
            }

            var matrixC = new double[matrixA.RowsCount(), matrixB.ColumnsCount()];

            for (var i = 0; i < matrixA.RowsCount(); i++)
            {
                for (var j = 0; j < matrixB.ColumnsCount(); j++)
                {
                    matrixC[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }

            return matrixC;
        }
        public double[,] Coefs(string[] input)
        {
            double[,] coefs = new double[input.Length, input[0].Split(' ').Select(double.Parse).ToArray().Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Split(' ').Select(double.Parse).ToArray().Length; j++)
                {
                    coefs[i, j] = Convert.ToDouble(input[i].Split(' ').Select(double.Parse).ToArray()[j]);
                }
            }

            return coefs;
        }
        public double[][] CopyRow(double[,] coefs)
        {
            int n = coefs.GetLength(0);
            double[][] a = new double[n][];
            for (int i = 0; i < n; i++)
            {
                a[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    a[i][j] = coefs[i, j];
                }
            }
            return a;
        }
        public int dim(double[,] coefs)
        {
            int n = coefs.GetLength(0);
            return n;
        }
        public double[,] reverse1(double[,] coefs)
        {
            double[,] ansver = new double[2,2];
            double b11, b12, b21, b22;
            b12 = -Math.Pow(coefs[0, 0], -1) * coefs[0, 1] * Math.Pow((coefs[1, 1] - coefs[1, 0] * Math.Pow(coefs[0, 0], -1) * coefs[0, 1]), -1);
            b11 = Math.Pow(coefs[0, 0], -1) - b12 * coefs[1, 0] * Math.Pow(coefs[0, 0], -1);
            b22 = Math.Pow((coefs[1, 1] - coefs[1, 0] * Math.Pow(coefs[0, 0], -1) * coefs[0, 1]), -1);
            b21 = -b22 * coefs[1, 0] * Math.Pow(coefs[0, 0], -1);
            ansver[0, 0] = b11;
            ansver[0, 1] = b12;
            ansver[1, 0] = b21;
            ansver[1, 1] = b22;
            return ansver;

        }
        public double[,] a21(double[,] coefs, int now, int dim)
        {
            double[,] ansver = new double[1,now-1];
            for (int i = 0; i < now - 1; i++)
            {
                ansver[0, i] = coefs[now-1,i];
            }
            return ansver;
        }
        public double[,] a22(double[,] coefs, int now, int dim)
        {
            double[,] ansver = new double[1, 1];
            ansver[0, 0] = coefs[now -1 , now -1];
            return ansver;
        }
        public double[,] a12(double[,] coefs, int now, int dim)
        {
            double[,] ansver = new double[now-1, 1];
            for (int i = 0; i < now - 1; i++)
            {
                ansver[i, 0] = coefs[i, now - 1];
            }
            return ansver;

        }
        public double[,] transformback(double[,] center,double[,] down,double[,] angel,double[,] up, int now)
        {
            double[,] otvet = new double[now,now];
            for (int i = 0; i < now-1; i++)
            {
                for (int j = 0;j< now - 1; j++)
                {
                    otvet[i, j] = center[i, j];
                }

            }
            otvet[now - 1, now - 1] = angel[0, 0];
            for (int i = 0; i < now - 1; i++)
            {
                otvet[now - 1, i] = down[0, i];
            }
            for (int i =0;i < now - 1; i++)
            {
                otvet[i, now - 1] = up[i, 0];
            }
            return otvet;

        }
        public void det(double[,] c)
        {
            int n = dim(c);
            double det = 1;
            const double EPS = 1E-9;
            double[][] a = CopyRow(c);
            double[][] b = new double[1][];
            b[0] = new double[n];
            for (int i = 0; i < n; ++i)
            {
                int k = i;
                for (int j = i + 1; j < n; ++j)

                    if (Math.Abs(a[j][i]) > Math.Abs(a[k][i]))
                        k = j;
                if (Math.Abs(a[k][i]) < EPS)
                {
                    det = 0;
                    break;
                }
                b[0] = a[i];
                a[i] = a[k];
                a[k] = b[0];
                if (i != k)
                    det = -det;
                det *= a[i][i];
                for (int j = i + 1; j < n; ++j)
                    a[i][j] /= a[i][i];
                for (int j = 0; j < n; ++j)
                    //проверяем
                    if ((j != i) && (Math.Abs(a[j][i]) > EPS))
                        for (k = i + 1; k < n; ++k)
                            a[j][k] -= a[i][k] * a[j][i];
            }
            if (det == 0)
            {
                Console.WriteLine("Определитель не существует");
                Environment.Exit(0);
            }
        }
        public void watch(double[,] c)
        {
            if (c.GetLength(0) != c.GetLength(1))
            {
                Console.WriteLine("Матрица не квадратная");
                Environment.Exit(0);
            }
        }
        public void vivod(double[,] ansver, int n)
        {
            int i = 0;
            Console.WriteLine();
            while (i < n)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(ansver[i, j].ToString("0.00") + " ");
                }
                Console.WriteLine();
                i++;
                
            }
        }

    }

    class Program
    {

        static void Main(string[] args)
        {
            Task task = new Task();
            Console.WriteLine("Введите элементы матрицы");
            string[] input = Console.ReadLine().Split(';');
            var coefs = task.Coefs(input);
            task.watch(coefs);
            task.det(coefs);
            double[,] a11 = null;
            double[,] a12;
            double[,] a21;
            double[,] a22;
            double[,] X;
            double[,] Y;
            double[,] tetta;
            double[,] tetta1 = new double[1,1];
            double[,] tettam;
            double[,] ansver;
            double[,] between;
            double[,] tettaX;
            double[,] otvet1;
            double[,] otvet2;
            double[,] otvet3;
            double[,] otvet4;
            double[,] minus = new double[1,1];
            minus[0, 0] = -1;
            int dim = task.dim(coefs);
            int i = 2;
            if (dim <= 2)
            {
                if (dim == 1)
                {
                    Console.WriteLine(coefs[0, 0]);
                    Environment.Exit(0);
                }
                else
                {
                    ansver = task.reverse1(coefs); 
                    task.vivod(ansver, dim);
                    Environment.Exit(0);
                }
            }
            else
            {

                while (i <= dim)
                {
                    if (i == 2)
                    {
                        ansver = task.reverse1(coefs);
                        a11 = ansver;
                    }
                    else
                    {
                        a12 = task.a12(coefs, i, dim);
                        a22 = task.a22(coefs, i, dim);
                        a21 = task.a21(coefs, i, dim);
                        X = task.Multiplication(a11, a12);
                        Y = task.Multiplication(a21, a11);
                        tetta = task.tetta(a22, Y, a12);
                        tetta1[0, 0] = tetta[0, 0];
                        tetta1[0, 0] = Math.Pow(tetta1[0, 0], -1);
                        tettam = task.Multiplication(tetta1,minus);
                        tettaX = task.Multiplication(X, tetta1);
                        between = task.Multiplication(tettaX, Y);
                        otvet1 = task.MatrixSum(a11, between); //11
                        otvet2 = task.Multiplication(tettam,Y); // 21
                        otvet3 = tetta1; //22
                        otvet4 = task.Multiplication(X,tettam); //12
                        a11 = task.transformback(otvet1,otvet2,otvet3,otvet4,i);

                    }
                    i++;
                    
                }
                task.vivod(a11, dim);
                Console.ReadKey();
            }
        }
    }
}
