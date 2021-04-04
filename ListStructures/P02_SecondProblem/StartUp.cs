namespace P02_SecondProblem
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            int[] array = new int[] { 1, -2, 5, 3, -2, 6, 3, 10, 42, -4, 5 };

            LinkedList<int> linkedList = new LinkedList<int>(array);
            int firstRepeatingNumber = ReturnFirstRepeatingNumberInLinkedList(linkedList);

            RemoveAllBiggerThan(linkedList, firstRepeatingNumber);
            Console.WriteLine(string.Join(", ", linkedList));
        }

        private static int ReturnFirstRepeatingNumberInLinkedList(LinkedList<int> linkedList)
        {
            LinkedListNode<int> node = linkedList.First;
            int firstRepeatingNumber = 0;
            bool isRepeated = false;
            while (node != linkedList.Last)
            {
                LinkedListNode<int> innerNode = node;
                do
                {
                    innerNode = innerNode.Next;

                    if (innerNode.Value == node.Value)
                    {
                        firstRepeatingNumber = node.Value;
                        isRepeated = true;
                        break;
                    }
                } while (innerNode != linkedList.Last);

                if (isRepeated)
                {
                    break;
                }

                node = node.Next;
            }

            return firstRepeatingNumber;
        }

        private static void RemoveAllBiggerThan(LinkedList<int> linkedList, int number)
        {
            LinkedListNode<int>node = linkedList.First;
            while (node != null)
            {
                LinkedListNode<int> nextNode = node.Next;
                if (node.Value > number)
                {
                    linkedList.Remove(node);
                }

                node = nextNode;
            }
        }
    }
}
