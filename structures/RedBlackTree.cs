namespace GatorLibrary.Structures.RedBlackTree
{
    enum RotationType
    {
        LL = 1,
        LR = 2,
        RR = 3,
        RL = 4,
    }

    public enum NodeColor
    {
        Black = 1,
        Red = 2,
    }

    public class Node<T> where T : IComparable
    {
        public T Data { get; set; }
        public Node<T>? Left { get; set; } = null;
        public Node<T>? Right { get; set; } = null;

        public NodeColor Color { get; set; }
        public Node(T data, NodeColor color = NodeColor.Red)
        {
            Data = data;
            Color = color;
        }
    }
    public class RedBlackTree<T> where T : IComparable
    {
        public Node<T>? Root { get; set; }
        public int FlipCount { get; set; }

        public RedBlackTree(Node<T>? root)
        {
            Root = root ?? null;
            FlipCount = 0;
        }

        #region Primary Methods
        public void Insert(T data)
        {
            if (Root == null)
            {
                Root = new Node<T>(data, NodeColor.Black);
                return;
            }
            Node<T> node = new(data);
            Node<T>? ptr = Root;
            List<Node<T>> path = new();
            while (ptr != null)
            {
                path.Add(ptr);
                if (data.CompareTo(ptr.Data) == 0)
                {
                    throw new InvalidDataException("Data already exists!");
                }
                if (data.CompareTo(ptr.Data) < 0)
                {
                    ptr = ptr.Left;
                }
                else
                {
                    ptr = ptr.Right;
                }
            }
            Node<T> parent = path[path.Count - 1];
            if (parent.Data.CompareTo(data) > 0)
            {
                parent.Left = node;
            }
            else
            {
                parent.Right = node;
            }
            if (parent.Color == NodeColor.Black)
            {
                return;
            }
            path.Add(node);
            BalanceTree(path);
        }

        public Node<T> SearchTree(T searchValue)
        {
            if (Root == null)
            {
                throw new ApplicationException("Tree has not been initialized");
            }
            Node<T>? ptr = Root;
            while (ptr != null)
            {
                if (ptr.Data.CompareTo(searchValue) == 0)
                {
                    return ptr;
                }
                if (searchValue.CompareTo(ptr.Data) < 0)
                {
                    ptr = ptr.Left;
                }
                else
                {
                    ptr = ptr.Right;
                }
            }
            throw new KeyNotFoundException($"Could not find value {searchValue} in the tree");
        }

        public List<Node<T>> SearchTree(T searchValue1, T searchValue2)
        {
            if (Root == null)
            {
                throw new ApplicationException("Tree has not been initialized");
            }
            return InorderTraversal(searchValue1, searchValue2, Root, new());
        }

        public List<T> SearchClosest(T searchValue)
        {
            if (Root == null)
            {
                throw new ApplicationException("Tree has not been initialized");
            }
            if (Root.Left == null && Root.Right == null)
            {
                return new()
                {
                    Root.Data,
                };
            }
            return SearchClosest(Root, new(), searchValue).Select(node => node.Data).ToList();
        }

        #endregion

        #region Helper methods

        private static RotationType GetRotationType(Node<T> grandParent, Node<T> parent, Node<T> node)
        {
            if (parent == grandParent.Left)
            {
                // L
                return node == parent.Left ? RotationType.LL : RotationType.LR;
            }
            else
            {
                // R
                return node == parent.Right ? RotationType.RR : RotationType.RL;
            }
        }

        private static void AssignRotatedNode(Node<T> greatGp, Node<T> grandParent, Node<T> rotatedNode)
        {
            if (greatGp.Left == grandParent)
            {
                greatGp.Left = rotatedNode;
            }
            else
            {
                greatGp.Right = rotatedNode;
            }
        }

        private void BalanceTree(List<Node<T>> path)
        {
            while (path.Count > 2)
            {
                Node<T> node = path[path.Count - 1];
                Node<T> parent = path[path.Count - 2];
                Node<T>? grandParent = path[path.Count - 3];
                Node<T>? parentSibling = parent == grandParent.Right ? grandParent.Left : grandParent.Right;
                if (parentSibling == null || parentSibling.Color == NodeColor.Black)
                {
                    // Rotate + recolor
                    RotationType rotationType = GetRotationType(grandParent, parent, node);
                    switch (rotationType)
                    {
                        case RotationType.LL:
                            Node<T> llRotatedNode = LLRotation(grandParent);
                            if (path.Count > 3)
                            {
                                AssignRotatedNode(path[path.Count - 4], grandParent, llRotatedNode);
                            }
                            break;
                        case RotationType.LR:
                            Node<T> lrRotatedNode = LRRotation(grandParent);
                            if (path.Count > 3)
                            {
                                AssignRotatedNode(path[path.Count - 4], grandParent, lrRotatedNode);
                            }
                            break;
                        case RotationType.RR:
                            Node<T> rrRotatedNode = RRRotation(grandParent);
                            if (path.Count > 3)
                            {
                                AssignRotatedNode(path[path.Count - 4], grandParent, rrRotatedNode);
                            }
                            break;
                        case RotationType.RL:
                            Node<T> rlRotatedNode = RLRotation(grandParent);
                            if (path.Count > 3)
                            {
                                AssignRotatedNode(path[path.Count - 4], grandParent, rlRotatedNode);
                            }
                            break;
                        default:
                            throw new KeyNotFoundException("Unhandled type of rotation!");
                    }
                    break;
                }
                else
                {
                    FlipCount++;
                    // Recolor parent, sibling to black, grandparent to red if it's not the root
                    parentSibling.Color = NodeColor.Black;
                    parent.Color = NodeColor.Black;
                    if (grandParent == Root)
                    {
                        break;
                    }
                    grandParent.Color = NodeColor.Red;
                    path = path.SkipLast(2).ToList();
                }
            }
        }

        private List<Node<T>> SearchClosest(Node<T>? root, List<Node<T>> minDistRoots, T searchValue)
        {
            if (root == null)
            {
                return minDistRoots;
            }
            int curMinDist = minDistRoots.Count == 0 ? int.MaxValue : Math.Abs(minDistRoots[0].Data.CompareTo(searchValue));
            int dist = Math.Abs(root.Data.CompareTo(searchValue));
            if (dist == 0)
            {
                // Exact match found, no other node can match this, since IDs are unique.
                return new()
                {
                    root,
                };
            }
            if (dist == curMinDist)
            {
                // Current distance is equal to the minimum distance found so far. Add current node to the list.
                minDistRoots.Add(root);
            }
            if (dist < curMinDist)
            {
                // New minimum distance found. Reset the list.
                minDistRoots = new()
                {
                    root,
                };
            }
            if (searchValue.CompareTo(root.Data) < 0)
            {
                return SearchClosest(root.Left, minDistRoots, searchValue);
            }
            return SearchClosest(root.Right, minDistRoots, searchValue);
        }

        private List<Node<T>> InorderTraversal(T searchValue1, T searchValue2, Node<T>? root, List<Node<T>> traversedPath)
        {
            if (root == null)
            {
                return traversedPath;
            }
            traversedPath = InorderTraversal(searchValue1, searchValue2, root.Left, traversedPath);
            if (root.Data.CompareTo(searchValue1) >= 0 && root.Data.CompareTo(searchValue2) <= 0)
            {
                traversedPath.Add(root);
            }
            return InorderTraversal(searchValue1, searchValue2, root.Right, traversedPath);
        }

        #region Rotation Methods
        private Node<T> LLRotation(Node<T> node)
        {
            if (node.Left == null)
            {
                throw new InvalidOperationException("Cannot LL rotate without left child");
            }
            Node<T> left = node.Left;
            NodeColor newNodeColor = left.Color;
            Node<T>? temp = left.Right;
            left.Right = node;
            left.Color = node.Color;
            node.Color = newNodeColor;
            if (Root == node)
            {
                Root = node.Left;
            }
            node.Left = temp;
            return left;
        }

        private Node<T> RRRotation(Node<T> node)
        {
            if (node.Right == null)
            {
                throw new InvalidOperationException("Cannot RR rotate without right child");
            }
            Node<T> right = node.Right;
            NodeColor newNodeColor = right.Color;
            Node<T>? temp = right.Left;
            right.Left = node;
            right.Color = node.Color;
            node.Color = newNodeColor;
            if (Root == node)
            {
                Root = node.Right;
            }
            node.Right = temp;
            return right;
        }

        private Node<T> LRRotation(Node<T> node)
        {
            if (node.Left == null)
            {
                throw new InvalidOperationException("Cannot LR rotate without left child");
            }
            if (node.Left.Right == null)
            {
                throw new InvalidOperationException("Cannot LR rotate without left.right");
            }
            Node<T> left = node.Left;
            Node<T> leftRight = node.Left.Right;
            NodeColor newNodeColor = leftRight.Color;
            node.Left = leftRight.Right;
            left.Right = leftRight.Left;
            leftRight.Left = left;
            leftRight.Right = node;
            leftRight.Color = node.Color;
            node.Color = newNodeColor;
            if (Root == node)
            {
                Root = leftRight;
            }
            return leftRight;
        }

        private Node<T> RLRotation(Node<T> node)
        {
            if (node.Right == null)
            {
                throw new InvalidOperationException("Cannot RL rotate without right child");
            }
            if (node.Right.Left == null)
            {
                throw new InvalidOperationException("Cannot RL rotate without right.left");
            }
            Node<T> right = node.Right;
            Node<T> rightLeft = node.Right.Left;
            NodeColor newNodeColor = rightLeft.Color;
            right.Left = rightLeft.Right;
            rightLeft.Right = right;
            node.Right = rightLeft.Left;
            rightLeft.Left = node;
            rightLeft.Color = node.Color;
            node.Color = newNodeColor;
            if (Root == node)
            {
                Root = rightLeft;
            }
            return rightLeft;
        }

        #endregion

        #endregion
    }
}