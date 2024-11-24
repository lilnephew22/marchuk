using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace Lab3.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            if (File.Exists("output.txt"))
            {
                File.Delete("output.txt");
            }
        }
        [Test]
        public void Find_WithPathCompression()
        {
            var ds = new DisjointSet(5);
            ds.Union(1, 2);
            ds.Union(2, 3);
            ds.Find(3);
            int result1 = ds.Find(1);
            int result3 = ds.Find(3);

            Assert.That(result3, Is.EqualTo(result1)); // Елементи повинні бути в одному наборі після стиснення шляху
        }
        [Test]
        public void Union_WithRank()
        {
            var ds = new DisjointSet(5);
            ds.Union(1, 2); 
            ds.Union(3, 4); 
            ds.Union(1, 4); 

            int root1 = ds.Find(1);
            int root2 = ds.Find(3);

            Assert.That(root2, Is.EqualTo(root1)); // Усі елементи повинні бути в одному наборі
        }
        [Test]
        public void Test_CompareTo_EqualWeight()
        {
            var edge1 = new Edge { Source = 0, Destination = 1, Weight = 10 };
            var edge2 = new Edge { Source = 1, Destination = 2, Weight = 10 };
            int result = edge1.CompareTo(edge2);

            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void Test_Find_SameElement()
        {
            var ds = new DisjointSet(5);
            int result = ds.Find(3);

            Assert.That(result, Is.EqualTo(3));
        }
        public void Test_Union_TwoDifferentSets()
        {
            var ds = new DisjointSet(5);
            ds.Union(1, 2);
            int result1 = ds.Find(1);
            int result2 = ds.Find(2);

            Assert.That(result2, Is.EqualTo(result1)); // Елементи 1 і 2 повинні бути в одному наборі
        }
    }
}