using Lab3;

namespace LabsLibrary
{
    internal class Lab3
    {

        public int ExecuteTask(string[] inputLines)
        {
            string[] firstLine = inputLines[0].Split();
            int N = int.Parse(firstLine[0]); // Кількість вершин
            int M = int.Parse(firstLine[1]); // Кількість ребер

            List<Edge> edges = new List<Edge>();

            for (int i = 1; i <= M; i++)
            {
                string[] edgeInfo = inputLines[i].Split();
                int A = int.Parse(edgeInfo[0]) - 1;
                int B = int.Parse(edgeInfo[1]) - 1;
                int C = int.Parse(edgeInfo[2]);
                edges.Add(new Edge { Source = A, Destination = B, Weight = C });
            }

            // Сортування ребр по весу
            edges.Sort();

            DisjointSet ds = new DisjointSet(N);
            int totalWeight = 0;

            foreach (Edge edge in edges)
            {
                if (ds.Find(edge.Source) != ds.Find(edge.Destination))
                {
                    ds.Union(edge.Source, edge.Destination);
                    totalWeight += edge.Weight;
                }
            }
            return totalWeight;
        }
    }
}
