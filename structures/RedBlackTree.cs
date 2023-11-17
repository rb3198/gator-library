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
        public int ColorFlipCount { get; set; }

        public RedBlackTree(Node<T>? root)
        {
            Root = root ?? null;
            ColorFlipCount = 0;
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

        public T Delete(T data)
        {
            List<Node<T>> vPath = SearchTreeWithPath(data);
            Node<T> v = vPath[vPath.Count - 1];
            List<Node<T>>? uPath = BstReplace(v);
            Node<T>? u = uPath.Count == 0 ? null : uPath[uPath.Count - 1];
            // True when both u & v are black
            bool uvBlack = (u == null || u.Color == NodeColor.Black) && v.Color == NodeColor.Black;
            T deletedData = v.Data;
            Node<T>? vSibling = GetSibling(vPath);
            Node<T>? vParent = GetParent(vPath);
            if (u == null)
            {
                // u  is null, i.e. v is a leaf.
                if (v == Root)
                {
                    Root = null;
                }
                else
                {
                    if (uvBlack)
                    {
                        FixDoubleBlack(vPath);
                    }
                    else if (vSibling != null)
                    {
                        // Make sibling red
                        IncrementColorFlipCount(vSibling.Color, NodeColor.Red);
                        vSibling.Color = NodeColor.Red;
                    }
                    if (vParent != null)
                    {
                        if (v == vParent.Left)
                        {
                            vParent.Left = null;
                        }
                        else
                        {
                            vParent.Right = null;
                        }
                    }
                }
                return v.Data;
            }

            if (v.Left == null || v.Right == null)
            {
                // v has 1 child
                if (v == Root)
                {
                    // assign u to v, delete v
                    v.Data = u.Data;
                    v.Left = v.Right = null;
                    // delete u
                }
                else
                {
                    // Detach v from tree and move u up
                    if (vParent != null)
                    {
                        if (vParent.Left == v)
                            vParent.Left = u;
                        else
                            vParent.Right = u;
                        uPath = SearchTreeWithPath(u.Data);
                    }

                    if (uvBlack)
                    {
                        // u and v both black, fix double black at u
                        FixDoubleBlack(uPath);
                    }
                    else
                    {
                        // u or v red, color u black
                        IncrementColorFlipCount(u.Color, NodeColor.Black);
                        u.Color = NodeColor.Black;
                    }
                }
                return deletedData;
            }
            SwapValues(u, v);
            var ptr = v.Right;
            while (ptr != null)
            {
                vPath.Add(ptr);
                ptr = ptr.Left;
            }
            Delete(vPath);
            return deletedData;
        }
        #endregion

        #region Helper methods

        private T Delete(List<Node<T>> vPath)
        {
            Node<T> v = vPath[vPath.Count - 1];
            List<Node<T>>? uPath = BstReplace(v);
            Node<T>? u = uPath.Count == 0 ? null : uPath[uPath.Count - 1];
            // True when both u & v are black
            bool uvBlack = (u == null || u.Color == NodeColor.Black) && v.Color == NodeColor.Black;
            T deletedData = v.Data;
            Node<T>? vSibling = GetSibling(vPath);
            Node<T>? vParent = GetParent(vPath);
            if (u == null)
            {
                // u  is null, i.e. v is a leaf.
                if (v == Root)
                {
                    Root = null;
                }
                else
                {
                    if (uvBlack)
                    {
                        FixDoubleBlack(vPath);
                    }
                    else if (vSibling != null)
                    {
                        // Make sibling red
                        IncrementColorFlipCount(vSibling.Color, NodeColor.Red);
                        vSibling.Color = NodeColor.Red;
                    }
                    if (vParent != null)
                    {
                        if (v == vParent.Left)
                        {
                            vParent.Left = null;
                        }
                        else
                        {
                            vParent.Right = null;
                        }
                    }
                }
                return v.Data;
            }

            if (v.Left == null || v.Right == null)
            {
                // v has 1 child
                if (v == Root)
                {
                    // assign u to v, delete v
                    v.Data = u.Data;
                    v.Left = v.Right = null;
                    // delete u
                }
                else
                {
                    // Detach v from tree and move u up
                    if (vParent != null)
                    {
                        if (vParent.Left == v)
                            vParent.Left = u;
                        else
                            vParent.Right = u;
                        uPath = SearchTreeWithPath(u.Data);
                    }

                    if (uvBlack)
                    {
                        // u and v both black, fix double black at u
                        FixDoubleBlack(uPath);
                    }
                    else
                    {
                        // u or v red, color u black
                        IncrementColorFlipCount(u.Color, NodeColor.Black);
                        u.Color = NodeColor.Black;
                    }
                }
                return deletedData;
            }
            SwapValues(u, v);
            Delete(u.Data);
            return deletedData;
        }

        private void FixDoubleBlack(List<Node<T>> pathToNode)
        {
            // Reached root
            if (pathToNode.Count == 1)
                return;
            Node<T>? sibling = GetSibling(pathToNode), parent = GetParent(pathToNode), grandparent = GetGrandparent(pathToNode);
            if (parent == null)
            {
                return;
            }

            Node<T> newParent;
            if (sibling == null)
            {
                // No sibling, double black pushed up
                FixDoubleBlack(pathToNode.SkipLast(1).ToList());
            }
            else
            {
                if (sibling.Color == NodeColor.Red)
                {
                    if (sibling == parent.Left)
                    {
                        // left case
                        newParent = LLRotation(parent);
                    }
                    else
                    {
                        // right case
                        newParent = RRRotation(parent);
                    }
                    if (grandparent != null)
                    {
                        AssignRotatedNode(grandparent, parent, newParent);
                    }
                    FixDoubleBlack(pathToNode);
                }
                else
                {
                    // Sibling black
                    if (HasRedChild(sibling))
                    {
                        // at least 1 red child
                        if (sibling.Left != null && sibling.Left.Color == NodeColor.Red)
                        {
                            if (sibling == parent.Left)
                            {
                                // left left
                                newParent = LLRotation(parent);
                            }
                            else
                            {
                                // right left
                                newParent = RLRotation(parent);
                            }
                        }
                        else
                        {
                            if (sibling == parent.Left)
                            {
                                // left right
                                newParent = LRRotation(parent);
                            }
                            else
                            {
                                // Right Right
                                newParent = RRRotation(parent);
                            }
                        }
                        IncrementColorFlipCount(parent.Color, NodeColor.Black);
                        parent.Color = NodeColor.Black;
                        if (grandparent != null)
                        {
                            AssignRotatedNode(grandparent, parent, newParent);
                        }
                    }
                    else
                    {
                        // 2 black children
                        IncrementColorFlipCount(sibling.Color, NodeColor.Red);
                        sibling.Color = NodeColor.Red;
                        if (parent.Color == NodeColor.Black)
                            FixDoubleBlack(pathToNode.SkipLast(1).ToList());
                        else
                        {
                            IncrementColorFlipCount(parent.Color, NodeColor.Black);
                            parent.Color = NodeColor.Black;
                        }
                    }
                }
            }
        }

        private static bool HasRedChild(Node<T> node)
        {
            return node.Left?.Color == NodeColor.Red || node.Right?.Color == NodeColor.Red;
        }
        private static void SwapValues(Node<T> u, Node<T> v)
        {
            (v.Data, u.Data) = (u.Data, v.Data);
        }

        // Given a path to node, return its sibling
        private static Node<T>? GetSibling(List<Node<T>> pathToNode)
        {
            if (pathToNode.Count <= 1)
            {
                return null;
            }
            Node<T> node = pathToNode[pathToNode.Count - 1];
            Node<T> parent = pathToNode[pathToNode.Count - 2];
            return node == parent.Right ? parent.Left : parent.Right;
        }

        // Given a path to node, return its parent
        private static Node<T>? GetParent(List<Node<T>> pathToNode)
        {
            if (pathToNode.Count <= 1)
            {
                return null;
            }
            return pathToNode[pathToNode.Count - 2];
        }

        private static Node<T>? GetGrandparent(List<Node<T>> pathToNode)
        {
            if (pathToNode.Count <= 2)
            {
                return null;
            }
            return pathToNode[pathToNode.Count - 3];
        }

        // Returns the entire path to the node with search value.
        private List<Node<T>> SearchTreeWithPath(T searchValue)
        {
            if (Root == null)
            {
                throw new ApplicationException("Tree has not been initialized");
            }
            List<Node<T>> path = new();
            Node<T>? ptr = Root;
            while (ptr != null)
            {
                path.Add(ptr);
                if (ptr.Data.CompareTo(searchValue) == 0)
                {
                    return path;
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

        private static List<Node<T>?> FindMinChildPath(Node<T> root)
        {
            // Find the child of root with minimum value
            List<Node<T>?> path = new();
            Node<T> ptr = root;
            while (ptr.Left != null)
            {
                path.Add(ptr);
                ptr = ptr.Left;
            }
            path.Add(ptr);
            return path;
        }

        private static List<Node<T>?> BstReplace(Node<T> node)
        {
            // When node has 2 children, replace the node with the min node in its right subtree
            if (node.Left != null && node.Right != null)
                return FindMinChildPath(node.Right);

            // When node is a leaf node, return null
            if (node.Left == null && node.Right == null)
                return new();

            // when single child
            if (node.Left != null)
            {
                return new()
                {
                    node,
                    node.Left,
                };
            }
            else
            {
                return new()
                {
                    node,
                    node.Right,
                };
            }
        }
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
                    // Recolor parent, sibling to black, grandparent to red if it's not the root
                    IncrementColorFlipCount(parentSibling.Color, NodeColor.Black);
                    IncrementColorFlipCount(parent.Color, NodeColor.Black);
                    parentSibling.Color = NodeColor.Black;
                    parent.Color = NodeColor.Black;
                    if (grandParent == Root)
                    {
                        break;
                    }
                    IncrementColorFlipCount(grandParent.Color, NodeColor.Red);
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

        private void IncrementColorFlipCount(NodeColor oldNodeColor, NodeColor newNodeColor)
        {
            if (newNodeColor != oldNodeColor)
            {
                ColorFlipCount++;
            }
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
            IncrementColorFlipCount(left.Color, node.Color);
            IncrementColorFlipCount(node.Color, newNodeColor);
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
            IncrementColorFlipCount(right.Color, node.Color);
            IncrementColorFlipCount(node.Color, newNodeColor);
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
            IncrementColorFlipCount(leftRight.Color, node.Color);
            IncrementColorFlipCount(node.Color, newNodeColor);
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
            IncrementColorFlipCount(rightLeft.Color, node.Color);
            IncrementColorFlipCount(node.Color, newNodeColor);
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