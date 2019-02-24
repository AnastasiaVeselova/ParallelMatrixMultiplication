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
                // чтобы сразу запускать таску, можно воспользоваться Task.Factory.StartNew()
                var multiplication = new Task(state =>
                {
                    // есть ещё один способ передать сюда значение: создать переменную вне этой
                    // лямбды и здесь использовать её значение, тогда не нужно будет елать приведение
                    var j = (int)state;
                    // сейчас потоки запускаются только для строк, но можно сделать ещё и для столбцов
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
