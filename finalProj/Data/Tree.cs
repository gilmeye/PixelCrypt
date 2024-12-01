using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    internal class Tree<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public Tree<T> Left { get; set; }
        public Tree<T> Right { get; set; }


        // BinaryTreeNode constructor initializes the data
        public Tree(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }



        public void Add(T data)
        {
            // If the data is less than the node, go left
            if (data.CompareTo(Data) < 0)
            {
                // If no left child exists, insert data here
                if (Left == null)
                {
                    Left = new Tree<T>(data);
                }
                else
                {
                    // Recursively call the Add method
                    Left.Add(data);
                }
            }
            // If the data is greater than or equal to the node, go right
            else
            {
                // If no right child exists, insert data here
                if (Right == null)
                {
                    Right = new Tree<T>(data);
                }
                else
                {
                    // Recursively call the Add method
                    Right.Add(data);
                }
            }
        }


        public Tree<T> Find(T data)
        {
            // If node found or tree traversal ends
            if (data.CompareTo(Data) == 0 || this == null)
            {
                return this;
            }
            else if (data.CompareTo(Data) < 0 && Left != null)
            {
                // If the data is less than the node, search left
                return Left.Find(data);
            }
            else if (Right != null)
            {
                // If the data is greater than the node, search right
                return Right.Find(data);
            }

            // Data was not found in the tree
            return null;
        }


        
    }
}
