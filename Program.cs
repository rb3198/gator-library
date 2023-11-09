using Structures.BinaryHeap;

MinBinaryHeap<string> heap = new(null);
heap.Add("patron 1", 1);
heap.Add("patron 2", 2);
heap.Add("patron 4", 4);
heap.Add("patron 6", 6);
heap.Add("patron 5", 5);
heap.Add("patron 3", 3);
Console.WriteLine(heap.GetHeapData());
heap.RemoveMin();
Console.WriteLine(heap.GetHeapData());
heap.RemoveMin();
Console.WriteLine(heap.GetHeapData());
heap.RemoveMin();
Console.WriteLine(heap.GetHeapData());
heap.RemoveMin();

