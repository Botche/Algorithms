namespace GeneratingPermutations
{
    using System;

    public class Engine
    {
        public void Run()
        {
            int number = int.Parse(Console.ReadLine());
            int factorial = CalculateTheFactorialOfN(number);

            Console.WriteLine($"The count of permutations are: {factorial}");
        }

        // Print count of all permutations
        // Could not figured out the algorith for printing permutations not recursively
        private int CalculateTheFactorialOfN(int number)
        {
            int factorial = 1;
            for (int index = 2; index <= number; index++)
            {
                factorial *= index;
            }

            return factorial;
        }
    }
}
