namespace BinaryReflectedGrayCodeRecursive
{
    using System;
    using System.Text;

    public class Engine
    {
        private readonly StringBuilder stringBuilder;

		public Engine()
		{
            this.stringBuilder = new StringBuilder();
        }
         
        public void Run()
        {
            Console.Write("Please enter a number: ");
            int depth = int.Parse(Console.ReadLine());

            Generate(depth);
        }

        private void Generate(int depth)
        {
            int initalValue = 1;
            RecursiveGenerator(initalValue, string.Empty, depth);

            string trimmedResult = this.stringBuilder.ToString().TrimEnd();
            Console.WriteLine(trimmedResult);
        }

        private void RecursiveGenerator(int number, string previousValue, int limit)
        {
            bool isNumberOverLimit = number > limit;
            if (isNumberOverLimit)
            {
                return;
            }

            previousValue += number + previousValue;

            this.stringBuilder.AppendLine(previousValue);
            number++;

            RecursiveGenerator(number, previousValue, limit);
        }
    }
}
