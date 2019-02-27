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

            var tasks = new Task[aRows * bColumns];

            for (var i = 0; i < aRows; i++)
            {
                for (var j = 0; j < bColumns; j++)
                {
                    var k = i;
                    var m = j;

                    //WaitAll(tasks) - принимает массив тасков, как лучше сделать:
                    //List<Task> перевести в массив (зачем лист, если мы знаем количетво элементов точное)
                    //Task[aRows,bColumns] - некрасиво переводить в одномерный, делать Task[aRows][Columns] - тоже нет особого смысла
                    //а сразу одномерный с вычислением смещения  tasks[i * bColumns + j] - как-то "по-школьному" ?

                    tasks[i * bColumns + j] = Task.Factory.StartNew(() =>
                    {
                        for (var n = 0; n < aColumns; n++)
                        {
                            result[k, m] += matrixA[k, n] * matrixB[n, m];
                        }

                    });
                }
            }

            Task.WaitAll(tasks);
            return result;
        }
    }
}
