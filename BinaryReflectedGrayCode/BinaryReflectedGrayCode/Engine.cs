namespace BinaryReflectedGrayCode
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

            string previousValue = string.Empty;
            for (int number = initalValue; number <= depth; number++)
            {
                previousValue += number + previousValue;

                this.stringBuilder.AppendLine(previousValue);
                number++;
            }

            string trimmedResult = this.stringBuilder.ToString().TrimEnd();
            Console.WriteLine(trimmedResult);
        }
    }
}
