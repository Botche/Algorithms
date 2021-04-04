namespace P03_ThirdProblem
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            LinkedList<int> linkedList = GetLinkedList();

            FindMinPositiveAndMaxNegative(linkedList, out int minPositiveNumber, out int maxNegativeNumber);
            CalculateAverageAndRemoveAllUnderAverage(linkedList, minPositiveNumber, maxNegativeNumber);

            Console.WriteLine(string.Join(", ", linkedList));
        }

        private static void FindMinPositiveAndMaxNegative(LinkedList<int> linkedList, out int minPositiveNumber, out int maxNegativeNumber)
        {
            minPositiveNumber = int.MaxValue;
            maxNegativeNumber = int.MinValue;
            LinkedListNode<int> node = linkedList.First;
            while (node != null)
            {
                if (node.Value > 0)
                {
                    minPositiveNumber = Math.Min(node.Value, minPositiveNumber);
                }
                else
                {
                    maxNegativeNumber = Math.Max(node.Value, maxNegativeNumber);
                }

                node = node.Next;
            }
        }

        private static void CalculateAverageAndRemoveAllUnderAverage(LinkedList<int> linkedList, int minPositiveNumber, int maxNegativeNumber)
        {
            int sum = minPositiveNumber + maxNegativeNumber;
            int average = sum / 2;

            LinkedListNode<int> node = linkedList.First;
            while (node != null)
            {
                LinkedListNode<int> nextNode = node.Next;
                if (node.Value < average)
                {
                    linkedList.Remove(node.Value);
                }

                node = nextNode;
            }
        }

        private static LinkedList<int> GetLinkedList()
        {
            LinkedList<int> linkedList = new LinkedList<int>();
            int numberFromConsole = int.Parse(Console.ReadLine());
            while (numberFromConsole != 0)
            {
                linkedList.AddLast(numberFromConsole);

                numberFromConsole = int.Parse(Console.ReadLine());
            }

            return linkedList;
        }
    }
}
