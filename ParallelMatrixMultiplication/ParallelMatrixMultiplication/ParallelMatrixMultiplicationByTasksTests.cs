using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ParallelMatrixMultiplication
{
    [TestFixture]
    public class ParallelMatrixMultiplicationByTasksTests
    {
        [Test]
        public void MultiplyMatrixWithFirstNullArgument_Fails()
        {
            Assert.Throws<ArgumentNullException>(
                () => ParallelMatrixMultiplicationByTasks.MultiplyMatrix(null, new int[1, 1]));
        }

        [Test]
        public void MultiplyMatrixWithSecondNullArgument_Fails()
        {
            Assert.Throws<ArgumentNullException>(
                () => ParallelMatrixMultiplicationByTasks.MultiplyMatrix(new int[1, 1], null));
        }

        [Test]
        public void MultiplyMatrixWithInappropriateDimensions_Fails()
        {
            Assert.Throws<ArgumentException>(
                () => ParallelMatrixMultiplicationByTasks.MultiplyMatrix(new int[,] { { 1, 2 }, { 3, 4 } }, new int[,] { { 10, 20 } }));
        }

        [Test]
        public void MultiplySquareMatrix()
        {
            var matrixA = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            var matrixB = new int[,] { { 2, 4, 6 }, { 9, 7, 5 }, { 8, 1, 3 } };

            var result = new int[,] { { 44, 21, 25 }, { 101, 57, 67 }, { 158, 93, 109 } };

            Assert.That(ParallelMatrixMultiplicationByTasks.MultiplyMatrix(matrixA, matrixB), Is.EqualTo(result));
        }

        [Test]
        public void MultiplRectangularMatrix()
        {
            var matrixA = new int[,] { { 7, -3, 3, 8 }, { 1, 3, 2, -4 } };

            var matrixB = new int[,] { { -4, 5, 1 }, { -2, 7, 9 }, { 3, 7, 5 }, { 1, 2, -7 } };

            var result = new int[,] { { -5, 51, -61 }, { -8, 32, 66 } };

            Assert.That(ParallelMatrixMultiplicationByTasks.MultiplyMatrix(matrixA, matrixB), Is.EqualTo(result));
        }
    }
}