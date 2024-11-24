namespace Lab3
{
    public class DisjointSet
    {
        private int[] parent;
        private int[] rank;

        public DisjointSet(int size)
        {
            parent = new int[size];
            rank = new int[size];
            for (int i = 0; i < size; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int item)
        {
            if (parent[item] != item)
            {
                parent[item] = Find(parent[item]); 
            }
            return parent[item];
        }

        public void Union(int x, int y)
        {
            int xRoot = Find(x);
            int yRoot = Find(y);

            if (xRoot != yRoot)
            {
                if (rank[xRoot] < rank[yRoot])
                {
                    parent[xRoot] = yRoot;
                }
                else if (rank[xRoot] > rank[yRoot])
                {
                    parent[yRoot] = xRoot;
                }
                else
                {
                    parent[yRoot] = xRoot;
                    rank[xRoot]++;
                }
            }
        }
    }

}
