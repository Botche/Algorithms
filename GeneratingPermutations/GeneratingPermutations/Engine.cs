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

            List<string> permutations = this.GenerateAllPermutations(number);

            Console.WriteLine(string.Join(Environment.NewLine, permutations));
        }

        // Generating half of the permutations and then repeats them
        // It is correct for n = 3
        // but n = 4 it is not
        private List<string> GenerateAllPermutations(int lengthOfPermutation)
        {
            int[] numbers = this.GenerateArrayWithNumber(lengthOfPermutation);
            int countOfIterations = this.CalculateCountOfIterations(lengthOfPermutation);

            List<string> permutations = new List<string>();
            permutations = this.AddPermutation(numbers, permutations);

            for (int iteration = 1; iteration < countOfIterations; iteration++)
            {
                int indexOfNumberOne = (iteration - 1) % lengthOfPermutation;
                int nextIndexOfNumber = indexOfNumberOne + 1;

                if (nextIndexOfNumber == lengthOfPermutation)
                {
                    nextIndexOfNumber = 0;
                }

                numbers = this.SwapTwoNumbersInArray(numbers, indexOfNumberOne, nextIndexOfNumber);

                permutations = this.AddPermutation(numbers, permutations);
            }

            return permutations;
        }

        private List<string> AddPermutation(int[] numbers, List<string> permutations)
        {
            List<string> copiedPermutations = this.DeepCopy(permutations);

            string separatorOfElementsInPermutation = " ";
            string permutation = string.Join(separatorOfElementsInPermutation, numbers);
            copiedPermutations.Add(permutation);

            return copiedPermutations;
        }

        private int CalculateCountOfIterations(int lengthOfPermutation)
        {
            int countOfIterations = 1;
            for (int iterator = 1; iterator <= lengthOfPermutation; iterator++)
            {
                countOfIterations *= iterator;
            }

            return countOfIterations;
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
