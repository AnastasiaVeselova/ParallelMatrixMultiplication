using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelMatrixMultiplication
{
    class Program
    {
        private static Random rnd = new Random();

        static void Main(string[] args)
        {
            var matrixA = CreateRamdomMatrix(3, 3);
            PrintMatrix(matrixA);
            var matrixB = CreateRamdomMatrix(3, 3);
            PrintMatrix(matrixB);

            var result = ParallelMatrixMultiplicationByTasks.MultiplyMatrix(matrixA, matrixB);
            PrintMatrix(result);

            Console.ReadKey();
        }

        private static int[,] CreateRamdomMatrix(int rows, int columns)
        {
            var result = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = rnd.Next(1, 4);
                }
            }

            return result;
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}