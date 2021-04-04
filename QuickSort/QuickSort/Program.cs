using System;

namespace QuickSort
{
    class Program
    {
        public static int[] array = new int[] { 6, 2, 9, 3, 7, 1, 8, 5, 4 };
		public static int[] C = new int[array.Length];

        static void Main(string[] args)
        {
            MergeSort(0, array.Length - 1);

            Console.WriteLine(string.Join(", ", array));
        }

		public static void Merge(int rightBorderIndex, int middleIndex, int leftBorderIndex)
		{
            rightBorderIndex++;
            int leftSubarrayRightBorder = middleIndex;
            int rightSubarrayIndex = middleIndex;
            int leftSubarrayIndex = leftBorderIndex;

            // The length of the array formed by merging the two subarrays
            int arrayLength = rightBorderIndex - leftBorderIndex;
            int[] helperArray = new int[arrayLength];
            int i = 0;

            // Stop when all of the elements from one of the subarrays are used
            while (i < arrayLength && leftBorderIndex != leftSubarrayRightBorder && rightSubarrayIndex != rightBorderIndex)
            {
                if (array[leftBorderIndex] < array[rightSubarrayIndex])
                {
                    helperArray[i] = array[leftBorderIndex++];
                }
                else
                {
                    helperArray[i] = array[rightSubarrayIndex++];
                }
                i++;
            }

            // Set the elements left from the right subarray at the end of the
            // helperArray if there are any
            if (leftBorderIndex == leftSubarrayRightBorder)
            {
                while (i < arrayLength)
                {
                    helperArray[i++] = array[rightSubarrayIndex++];
                }
            }

            // Set the elements left from the left subarray at the end of the
            // helperArray if there are any
            if (rightSubarrayIndex == rightBorderIndex)
            {
                while (i < arrayLength)
                {
                    helperArray[i++] = array[leftBorderIndex++];
                }
            }

            // Set the sorted subarray's values as values in the original array
            for (i = 0; i < arrayLength; i++)
                array[leftSubarrayIndex++] = helperArray[i];
        }


        public static void MergeSort(int a, int b)
        {
            if (a < b)
            {
                int c = (a + b) / 2;
                MergeSort(a, c++);
                MergeSort(c, b);
                Merge(b, c, a);
            }
        }

    }
}
