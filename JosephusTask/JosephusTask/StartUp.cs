namespace JosephusProblem
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            int countOfElements = GetCountOfElements();
            int numberOfElementsToSkip = GetNumberOfElementsToSkip();

            Console.WriteLine(Environment.NewLine);

            int lastElement = JosephusProblem(countOfElements, numberOfElementsToSkip);
            Console.WriteLine($"Last element in the sequense is: {lastElement}");
        }

        private static int JosephusProblem(int n, int k)
        {
            Queue<int> queue = FillQueue(1, n);

            int indexToRemove = k - 1;

            int counter = 0;
            while (queue.Count != 1)
            {
                if (counter < indexToRemove)
                {
                    queue.Enqueue(queue.Dequeue());
                    counter++;

                    continue;
                }

                queue.Dequeue();
                counter = 0;

                if (queue.Count == indexToRemove)
                {
                    string lastElementsKminusOne = string.Join(", ", queue);
                    Console.WriteLine($"Last elements k - 1 -> {indexToRemove}: {lastElementsKminusOne}");
                }
            }

            int lastRemainingNumber = queue.Dequeue();

            return lastRemainingNumber;
        }

        private static Queue<int> FillQueue(int minNumber, int maxNumber)
        {
            Queue<int> queue = new Queue<int>();

            for (int number = minNumber; number <= maxNumber; number++)
            {
                queue.Enqueue(number);
            }

            return queue;
        }

        private static int GetNumberOfElementsToSkip()
        {
            string messageForInput = "Enter the number to skip elements (k): ";
            string errorMessage = "Number must be greater than 1!";

            int numberOfElementsToSkip = (int)default;

            Console.Write(messageForInput);
            string input = Console.ReadLine();
            while (true)
            {
                bool isNumber = int.TryParse(input, out numberOfElementsToSkip);
                bool isGreaterThanOne = numberOfElementsToSkip > 1;

                bool isNumberValid = isNumber && isGreaterThanOne;
                if (isNumberValid)
                {
                    break;
                }

                Console.WriteLine();
                Console.WriteLine(errorMessage);

                Console.Write(messageForInput);
                input = Console.ReadLine();
            }

            return numberOfElementsToSkip;
        }

        private static int GetCountOfElements()
        {
            string messageForInput = "Enter the number of elements in the sequence (n): ";
            string errorMessage = "Number must be greater than 1!";

            int countOfElements = (int)default;

            Console.Write(messageForInput);
            string input = Console.ReadLine();
            while (true)
            {
                bool isNumber = int.TryParse(input, out countOfElements);
                bool isGreaterThanOne = countOfElements > 1;

                bool isNumberValid = isNumber && isGreaterThanOne;
                if (isNumberValid)
                {
                    break;
                }

                Console.WriteLine();
                Console.WriteLine(errorMessage);

                Console.Write(messageForInput);
                input = Console.ReadLine();
            }

            return countOfElements;
        }
    }
}
