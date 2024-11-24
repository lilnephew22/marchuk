namespace Lab1
{
    public class CircularLinkedList<T>
    {

        private class Node
        {
            public T Data;
            public Node Next;

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node head;
        private Node tail;
        private int count;

        public CircularLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void Add(T data)
        {
            Node newNode = new Node(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
                tail.Next = head; // Останній елемент посилається на перший
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
                tail.Next = head; // Оновлюємо посилання останнього елемента
            }

            count++;
        }

        public void Display()
        {
            if (head == null)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            Node current = head;
            do
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            } while (current != head);
            Console.WriteLine();
        }
        public T GetNodeData(int index)
        {
            if (head == null || count == 0)
            {
                throw new ArgumentOutOfRangeException("Список порожній.");
            }

            index = ((index % count) + count) % count; // Коригуємо індекс, щоб він був у межах списку (з врахуванням від'ємних індексів)

            Node current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }

        public void SetNodeData(int index, T newData)
        {
            if (head == null || count == 0)
            {
                throw new ArgumentOutOfRangeException("Список порожній.");
            }

            index = ((index % count) + count) % count; // Коригуємо індекс

            Node current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            current.Data = newData;
        }

        public T GetNext(int index)
        {
            if (head == null || count == 0)
            {
                throw new ArgumentOutOfRangeException("Список порожній.");
            }

            index = ((index % count) + count) % count; // Коригуємо індекс

            Node current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Next.Data; // Повертаємо дані наступного елемента
        }

        public T GetPrevious(int index)
        {
            if (head == null || count == 0) // Перевіряємо, чи список не порожній
            {
                throw new ArgumentOutOfRangeException("Список порожній.");
            }

            index = ((index % count) + count) % count; // Коригуємо індекс

            Node current = head;

            for (int i = 0; i < (index - 1 + count) % count; i++)
            {
                current = current.Next;
            }

            if (index == 0)
                return tail.Data; // Якщо індекс = 0, повертаємо дані останнього елемента

            return current.Data;
        }

        public int Count => count;


        public bool IsEmpty => count == 0;
    }
}
