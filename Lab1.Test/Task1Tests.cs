using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Lab1.Test
{
    [TestFixture] 
    public class Task1Tests
    {
        [Test] 
        public void Test_TaskSolution_Balanced_CorrectSteps()
        {
            
            string[] input = { "3", "2 0 0" };
            File.WriteAllLines("input.txt", input);

            Task1 task = new Task1();

            
            int result = task.TaskSolution();

            
            Assert.AreEqual(2, result);// Очікуємо, що потрібно 2 кроки для балансування кульок
        }

        [Test] 
        public void Test_FindNextEmptyNode()
        {
            
            Task1.arr = new CircularLinkedList<int>();
            Task1.arr.Add(1);
            Task1.arr.Add(0);
            Task1.arr.Add(1);

            int currentIndex = 0;

            
            int nextEmptySteps = Task1.FindNextEmptyNode(currentIndex);

            
            Assert.AreEqual(1, nextEmptySteps); // Наступна порожня коробка знаходиться на відстані 1 крок
        }

        [Test] 
        public void Test_FindPrevEmptyNode()
        {
            // Arrange
            Task1.arr = new CircularLinkedList<int>();
            Task1.arr.Add(1);
            Task1.arr.Add(1);
            Task1.arr.Add(0);

            int currentIndex = 0;

            
            int prevEmptySteps = Task1.FindPrevEmptyNode(currentIndex);

            
            Assert.AreEqual(2, prevEmptySteps); // Попередня порожня коробка знаходиться на відстані 2 кроків
        }

        [Test] 
        public void Test_CircularLinkedList_GetAndSetNodeData()
        {
            // Arrange
            CircularLinkedList<int> list = new CircularLinkedList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            // Act
            list.SetNodeData(1, 25);
            int data = list.GetNodeData(1);

            // Assert
            Assert.AreEqual(25, data); // Очікуємо, що другий елемент матиме значення 25
        }

        [Test] 
        public void Test_CircularLinkedList_Move()
        {
            // Arrange
            Task1.arr = new CircularLinkedList<int>();
            Task1.arr.Add(1);  
            Task1.arr.Add(0);  
            Task1.arr.Add(1);  

            Task1.totalStepsCounter = 0;
            Task1 task = new Task1();
            int initialSteps = Task1.totalStepsCounter;

            // Act
            task.Move(0, 1, true); 

            // Assert
            Assert.AreEqual(0, Task1.arr.GetNodeData(0));
            Assert.AreEqual(1, Task1.arr.GetNodeData(1)); 
            Assert.AreEqual(initialSteps + 1, Task1.totalStepsCounter); // Лічильник кроків має збільшитися на 1
        }
    }
}
