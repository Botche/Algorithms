namespace GeneratingPermutations
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Engine
    {
        public void Run()
        {
            int number = int.Parse(Console.ReadLine());

            this.GenerateAllPermutations(number);
        }

        private List<string> GenerateAllPermutations(int lengthOfPermutation)
        {
            int[] numbers = this.GenerateArrayWithNumber(lengthOfPermutation);

            List<string> permutations = new List<string>();
            permutations = this.AddPermutation(numbers, permutations);

            int startingIndex = 0;
            this.GeneratePermutationRecursively(
                numbers,
                startingIndex,
                numbers.Length - 1
           );

            return permutations;
        }

        private void GeneratePermutationRecursively(
            int[] numbers,
            int startingIndex,
            int lastIndex
        )
        {
            int[] copiedNumbers = this.DeepCopy(numbers);

            if (startingIndex == lastIndex)
            {
                Console.WriteLine(string.Join(string.Empty, numbers));
                return;
            }

            for (int index = startingIndex; index <= lastIndex; index++)
            {
                copiedNumbers = this.SwapTwoNumbersInArray(copiedNumbers, startingIndex, index);

                this.GeneratePermutationRecursively(copiedNumbers, startingIndex + 1, lastIndex);
            }
        }

        private List<string> AddPermutation(int[] numbers, List<string> permutations)
        {
            List<string> copiedPermutations = this.DeepCopy(permutations);

            string permutation = string.Join(string.Empty, numbers);
            copiedPermutations.Add(permutation);

            return copiedPermutations;
        }

        private int[] GenerateArrayWithNumber(int number)
        {
            int[] numbers = new int[number];
            for (int index = 1; index <= number; index++)
            {
                numbers[index - 1] = index;
            }

            return numbers;
        }

        private T[] SwapTwoNumbersInArray<T>(T[] numbers, int indexOfFirstElement, int indexOfSecondElement)
        {
            T[] copiedNumbers = this.DeepCopy(numbers);

            T temp = copiedNumbers[indexOfFirstElement];
            copiedNumbers[indexOfFirstElement] = copiedNumbers[indexOfSecondElement];
            copiedNumbers[indexOfSecondElement] = temp;

            return copiedNumbers;
        }

        private T DeepCopy<T>(T objectToCopy)
        {
            string serializedObject = JsonConvert.SerializeObject(objectToCopy);
            T copiedObject = JsonConvert.DeserializeObject<T>(serializedObject);

            return copiedObject;
        }
    }
}
