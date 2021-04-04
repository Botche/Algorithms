namespace P01_FirstProblem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            const int MIN_NUMBER = 0;
            const int MAX_NUMBER = 99;
            const int COUNT_OF_NUMBERS_IN_LIST = 20;

            List<int> A = GenerateListWithRandomNumbers(COUNT_OF_NUMBERS_IN_LIST, MIN_NUMBER, MAX_NUMBER); 
            List<int> B = GenerateListWithRandomNumbers(COUNT_OF_NUMBERS_IN_LIST, MIN_NUMBER, MAX_NUMBER);

            MergeSort(A, 0, A.Count - 1);
            MergeSort(B, 0, B.Count - 1);

            Console.WriteLine($"The first sorted array is: {string.Join(", ", A)}");
            Console.WriteLine($"The second sorted array is: {string.Join(", ", B)}");

            List<int> C = A.Concat(B)
                .ToList();

            Console.WriteLine($"The combined array is: {string.Join(", ", C)}");

            C = C.Where(number => (number & 1) == 1)
                .ToList();

            Console.WriteLine($"The list without all even number is: {string.Join(", ", C)}");
        }

        private static List<int> GenerateListWithRandomNumbers(int countOfNumbers, int minNumber, int maxNumber)
        {
            List<int> list = new List<int>(countOfNumbers);

            Random random = new Random(); 
            for (int index = 0; index < countOfNumbers; index++)
            {
                int randomNumber = random.Next(minNumber, maxNumber + 1);

                list.Add(randomNumber);
            }

            return list;
        }

        private static void Merge(List<int> list, int rightBorderIndex, int middleIndex, int leftBorderIndex)
        {
            rightBorderIndex++;
            int leftSubarrayRightBorder = middleIndex;
            int rightSubarrayIndex = middleIndex;
            int leftSubarrayIndex = leftBorderIndex;

            int arrayLength = rightBorderIndex - leftBorderIndex;
            int[] helperArray = new int[arrayLength];
            int index = 0;

            while (index < arrayLength && leftBorderIndex != leftSubarrayRightBorder && rightSubarrayIndex != rightBorderIndex)
            {
                if (list[leftBorderIndex] < list[rightSubarrayIndex])
                {
                    helperArray[index] = list[leftBorderIndex++];
                }
                else
                {
                    helperArray[index] = list[rightSubarrayIndex++];
                }

                index++;
            }

            if (leftBorderIndex == leftSubarrayRightBorder)
            {
                while (index < arrayLength)
                {
                    helperArray[index++] = list[rightSubarrayIndex++];
                }
            }

            if (rightSubarrayIndex == rightBorderIndex)
            {
                while (index < arrayLength)
                {
                    helperArray[index++] = list[leftBorderIndex++];
                }
            }

            for (index = 0; index < arrayLength; index++)
                list[leftSubarrayIndex++] = helperArray[index];
        }

        private static void MergeSort(List<int> list, int leftMostIndex, int rightMostIndex)
        {
            if (leftMostIndex < rightMostIndex)
            {
                int median = (leftMostIndex + rightMostIndex) / 2;
                MergeSort(list, leftMostIndex, median++);
                MergeSort(list, median, rightMostIndex);
                Merge(list, rightMostIndex, median, leftMostIndex);
            }
        }
    }
}
