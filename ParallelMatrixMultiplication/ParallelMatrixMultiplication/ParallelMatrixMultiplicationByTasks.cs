using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelMatrixMultiplication
{
    public class ParallelMatrixMultiplicationByTasks
    {
        public static int[,] MultiplyMatrix(int[,] matrixA, int[,] matrixB)
        {
            if (matrixA == null || matrixB == null)
                throw new ArgumentNullException();

            var aRows = matrixA.GetLength(0);
            var aColumns = matrixA.GetLength(1);

            var bRows = matrixB.GetLength(0);
            var bColumns = matrixB.GetLength(1);

            if (aColumns != bRows)
                throw new ArgumentException("aColumns != bRows");

            var result = new int[aRows, bColumns];

            Task[] tasks = new Task[aRows];
            for (var i = 0; i < aRows; i++)
            {
                var multiplication = new Task(state =>
                {
                    var j = (int)state;

                    for (var k = 0; k < bColumns; k++)
                    {
                        for (var m = 0; m < aColumns; m++)
                        {
                            result[j, k] += matrixA[j, m] * matrixB[m, k];
                        }
                    }
                },
                i);

                tasks[i] = multiplication;
                tasks[i].Start();
            }

            Task.WaitAll(tasks);
            return result;
        }
    }
}
