namespace Structures.BinaryHeap
{
    public class Node<T>
    {
        public int PriorityNumber { get; set; }
        public int TimeOfReservation { get; set; }
        public T? Data { get; set; }
        public Node(T data, int priorityNumber, int timeOfReservation)
        {
            Data = data;
            PriorityNumber = priorityNumber;
            TimeOfReservation = timeOfReservation;
        }
    }
    public class MinBinaryHeap<T>
    {
        private List<Node<T>> Nodes { get; set; } = new();
        public MinBinaryHeap(Node<T>? root)
        {
            // Initialize
            if (root != null)
            {
                Nodes.Add(root);
            }
        }
        public void Add(T data, int priority)
        {
            Node<T> node = new(data, priority, (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            Nodes.Add(node);
            if (Nodes.Count == 1)
            {
                return;
            }
            double prevIndex = Nodes.Count - 1;
            double i = Math.Floor(prevIndex / 2);
            while (i >= 0 && Nodes[(int)i].PriorityNumber >= node.PriorityNumber)
            {
                if (Nodes[(int)i].PriorityNumber == node.PriorityNumber)
                {
                    if (Nodes[(int)i].TimeOfReservation > node.TimeOfReservation)
                    {
                        (Nodes[(int)i], Nodes[(int)prevIndex]) = (Nodes[(int)prevIndex], Nodes[(int)i]);
                    }
                    break;
                }
                (Nodes[(int)i], Nodes[(int)prevIndex]) = (Nodes[(int)prevIndex], Nodes[(int)i]);
                prevIndex = i;
                i = Math.Floor((i - 1) / 2);
            }
        }

        public Node<T>? GetRoot()
        {
            return Nodes.FirstOrDefault();
        }

        public string GetHeapData()
        {
            string output = "[";
            foreach (Node<T> node in Nodes)
            {
                output += $"{node.Data?.ToString() ?? string.Empty}, ";
            }
            output += "]";
            return output;
        }

        private int GetMinChild(int leftChild, int rightChild)
        {
            if (Nodes[leftChild].PriorityNumber > Nodes[rightChild].PriorityNumber)
            {
                return rightChild;
            }
            if (Nodes[leftChild].PriorityNumber == Nodes[rightChild].PriorityNumber)
            {
                return Nodes[leftChild].TimeOfReservation < Nodes[rightChild].TimeOfReservation ? leftChild : rightChild;
            }
            return leftChild;
        }

        private void Heapify()
        {
            int i = 0;
            while (true)
            {
                int minChild = i;
                int leftChild = 2 * i + 1;
                int rightChild = 2 * i + 2;
                if (leftChild >= Nodes.Count && rightChild >= Nodes.Count)
                {
                    break;
                }
                if (rightChild >= Nodes.Count)
                {
                    minChild = GetMinChild(minChild, leftChild);
                }
                else
                {
                    int candidate = GetMinChild(leftChild, rightChild);
                    minChild = GetMinChild(minChild, candidate);
                }
                if (minChild != i)
                {
                    (Nodes[i], Nodes[minChild]) = (Nodes[minChild], Nodes[i]);
                    i = minChild;
                }
                else
                {
                    break;
                }
            }
        }

        public void RemoveMin()
        {
            if (Nodes.Count == 0)
            {
                Console.WriteLine("No node exists to delete.");
                return;
            }
            Nodes[0] = Nodes[Nodes.Count - 1];
            Nodes = Nodes.Skip(1).ToList();
            if (Nodes.Count == 1)
            {
                return;
            }
            Heapify();
        }

    }
}