﻿namespace BinaryTree
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            HardAlgorithm();
        }

        public static void HardAlgorithm()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(83);

            while (queue.Count != 0)
            {
                int oldNumberFromQueue = queue.Dequeue();

                if (oldNumberFromQueue % 3 == 0)
                {
                    break;
                }

                int mutlipliedNumberByTwo = oldNumberFromQueue * 2;
                queue.Enqueue(mutlipliedNumberByTwo);

                int oldNumberMinusOne = oldNumberFromQueue - 1;

                if (oldNumberMinusOne % 3 == 0)
                {
                    int numberDividedByThree = oldNumberMinusOne / 3;
                    queue.Enqueue(numberDividedByThree);
                }
            }

        }



    }


    public class Node
    {
        public int value;
        public Node left;
        public Node right;
    }

    public class Tree
    {
        #region First problem
        private const string ERROR_MESSAGE_ALREADY_EXIST = "The key {0} is already registered in the tree!";

        //рекурсивна реализация на добавяне на елемент        
        public void Add(ref Node root, int v)
        {
            if (root == null)
            {
                root = new Node();
                root.value = v;

                return;
            }

            bool isAlreadyExistsInTree = this.SearchNR(root, v);
            if (isAlreadyExistsInTree)
            {
                string errorMessage = string.Format(ERROR_MESSAGE_ALREADY_EXIST, v);
                Console.WriteLine(errorMessage);
                return;
            }

            if (v < root.value)
            {
                Add(ref root.left, v);
            }
            else
            {
                Add(ref root.right, v);
            }
        }

        //нерекурсивна реализация на добавяне на елемент        
        public void AddNR(ref Node root, int v)
        {
            if (root == null)
            {
                root = new Node();
                root.value = v;

                return;
            }


            bool isAlreadyExistsInTree = this.Search(root, v);
            if (isAlreadyExistsInTree)
            {
                string errorMessage = string.Format(ERROR_MESSAGE_ALREADY_EXIST, v);
                Console.WriteLine(errorMessage);
                return;
            }

            Node newNode = new Node();
            newNode.value = v;

            Node prior = root;
            Node next;
            if (v < root.value)
            {
                next = root.left;
            }
            else
            {
                next = root.right;
            }

            while (next != null)
            {
                prior = next;
                if (v < prior.value)
                {
                    next = prior.left;
                }
                else
                {
                    next = prior.right;
                }
            }

            if (v < prior.value)
            {
                prior.left = newNode;
            }
            else
            {
                prior.right = newNode;
            }
        }
        #endregion

        //рекурсивна реализация на търсене на елемент
        public bool Search(Node root, int key)
        {
            if (root == null)
                return false;
            if (root.value == key)
                return true;
            if (root.value > key)
                return Search(root.left, key);
            else
                return Search(root.right, key);
        }

        #region Second problem
        public bool SearchNR(Node root, int key)
        {
            bool isFound = false;

            while (root != null)
            {
                if (root.value == key)
                {
                    isFound = true;
                    break;
                }

                if (root.value > key)
                {
                    root = root.left;
                    continue;
                }

                root = root.right;
            }

            return isFound;
        }
        #endregion

        //намиране на минимален елемент в дясно поддърво
        Node FindMinRight(Node root)
        {
            Node p = root.right;
            while (p.left != null)
            {
                p = p.left;
            }
            return p;
        }

        //рекурсивна реализация на изтриване на елемент
        public void RemoveNode(ref Node root, int key)
        {
            if (root == null)
            {
                Console.WriteLine("No such key");
            }
            else
                if (key < root.value)
                RemoveNode(ref root.left, key);
            else
            {
                if (key > root.value)
                    RemoveNode(ref root.right, key);
                else
                {
                    if (root.left != null && root.right != null)
                    {
                        Node replace = FindMinRight(root);
                        root.value = replace.value;
                        RemoveNode(ref root.right, root.value);
                    }
                    else
                    {
                        Node temp = root;
                        if (root.left != null) root = root.left;
                        else root = root.right;
                        temp = null;
                    }
                }
            }

        }

        #region Third problem
        public void RemoveNodeNR(ref Node root, int key)
        {
            Node parent = null;
            Node current = root;
            while (current != null && current.value != key)
            {
                parent = current;
                if (current.value > key)
                {
                    current = current.left;
                    continue;
                }

                current = current.right;
            }

            if (current == null)
            {
                string message = "The key is not found in the tree!";
                Console.WriteLine(message);
                return;
            }

            if (current.left == null && current.right == null)
            {
                if (current == root)
                {
                    root = null;
                    return;
                }

                if (parent.left == current)
                {
                    parent.left = null;
                    return;
                }

                parent.right = null;

                return;
            }

            if (current.right == null)
            {
                parent.left = current.left;
                return;
            }

            parent = current;
            Node leftLowestNodeFromRightSubTree = current.right;
            while (leftLowestNodeFromRightSubTree != null && leftLowestNodeFromRightSubTree.left != null)
            {
                parent = leftLowestNodeFromRightSubTree;
                leftLowestNodeFromRightSubTree = leftLowestNodeFromRightSubTree.left;
            }

            current.value = leftLowestNodeFromRightSubTree.value;
            if (parent.right == leftLowestNodeFromRightSubTree)
            {
                parent.right = leftLowestNodeFromRightSubTree.right;
                return;
            }

            if (parent.left != null)
            {
                parent.left = leftLowestNodeFromRightSubTree.right;
                return;
            }

            parent.right = leftLowestNodeFromRightSubTree.right;
        }
        #endregion

        //ЛКД обхождане на дървото
        public void Traverse(Node root)
        {
            if (root.left != null)
                Traverse(root.left);
            Console.Write("{0} ", root.value);
            if (root.right != null)
                Traverse(root.right);
        }
    }
}
