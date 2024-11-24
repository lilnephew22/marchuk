using NUnit.Framework;

namespace Lab2.Test
{
    [TestFixture]
    public class Task2Tests
    {
        [Test]
        public void Test_Minimax_1ElemOnDiagonal()
        {
            int N = 1;
            int[] diagonal = { 5 };
            var task = new Task2(N, diagonal);
            int result = task.Minimax(N - 1, 0, true);
            Assert.That(result, Is.EqualTo(5)); // Оскільки на дошці тільки один елемент на діагоналі
        }

        [Test]
        public void Test_Minimax_When2ElemOnDiagonal()
        {
            int N = 2;
            int[] diagonal = { 3, 7 };
            var task = new Task2(N, diagonal);
            int result = task.Minimax(N - 1, 0, true);
            Assert.That(result, Is.EqualTo(7));
        }



        [Test]
        public void Test_Minimax_MinEdgeCase()
        {
            int N = 1;
            int[] diagonal = { 100 };
            var task = new Task2(N, diagonal);
            int result = task.Minimax(N - 1, 0, true);
            Assert.That(result, Is.EqualTo(100));
        }
        [Test]
        public void Test_Minimax_When3ElemOnDiagonal()
        {
            int N = 3;
            int[] diagonal = { 5, 3, 1 };
            var task = new Task2(N, diagonal);
            int result = task.Minimax(N - 1, 0, true);
            Assert.That(result, Is.EqualTo(3)); 
        }

        [Test]
        public void Test_Minimax_When4ElemOnDiagonal()
        {
            int N = 4;
            int[] diagonal = { 4, 7, 2, 9 };
            var task = new Task2(N, diagonal);
            int result = task.Minimax(N - 1, 0, true);
            Assert.That(result, Is.EqualTo(7));
        }
    }
}
