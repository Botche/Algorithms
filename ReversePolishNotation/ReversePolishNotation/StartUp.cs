namespace ReversePolishNotation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Enter mathematical expression: ");
            string input = Console.ReadLine();

            string[] tokens = SplitInput(input);

            Queue<string> outputQueue = GenerateReversePolishNotation(tokens);

            Console.WriteLine($"Reverse polish notation expression: {string.Join(' ', outputQueue)}");

            Console.WriteLine($"Result: {CalculateReversePolishNotation(outputQueue)}");
        }

        private static string[] SplitInput(string input)
        {
            string regexPattern = @"(([0-9]+[.]*)?[0-9]+)|\(|\)|\+|\/|\*|-";
            Regex regex = new Regex(regexPattern);

            string[] tokens = regex.Matches(input)
                .Select(x => x.ToString())
                .ToArray();

            return tokens;
        }

        private static Queue<string> GenerateReversePolishNotation(string[] symbols)
        {
            Stack<string> operatorStack = new Stack<string>();
            Queue<string> outputQueue = new Queue<string>();

            for (int index = 0; index < symbols.Length; index++)
            {
                string symbol = symbols[index];

                FillOutputQueue(operatorStack, outputQueue, symbol);
            }

            // Add last operators 
            while (operatorStack.Count != 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            return outputQueue;
        }

        private static void FillOutputQueue(Stack<string> operatorStack, Queue<string> outputQueue, string symbol)
        {
            if (IsNumber(symbol))
            {
                outputQueue.Enqueue(symbol);
            }
            else if (symbol == "^")
            {
                operatorStack.Push(symbol);
            }
            else if (OperatorPrecedence(symbol) != -1)
            {

                while (operatorStack.Count != 0 && OperatorPrecedence(operatorStack.Peek()) >= OperatorPrecedence(symbol))
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }

                operatorStack.Push(symbol);
            }
            else if (symbol == "(")
            {
                operatorStack.Push(symbol);
            }
            else if (symbol == ")")
            {
                while (!(operatorStack.Peek() == "("))
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }

                operatorStack.Pop();
            }
        }

        private static bool IsNumber(string input)
        {
            bool isNumeric = double.TryParse(input, out _);

            return isNumeric;
        }

        private static int OperatorPrecedence(string mathOperator)
        {
            int precedence = -1;
            char mathOperatorAsChar = char.Parse(mathOperator);

            switch (mathOperatorAsChar)
            {
                case '+':
                case '-':
                    precedence = 2;
                    break;
                case '*':
                case '/':
                    precedence = 3;
                    break;
                case '^':
                    precedence = 4;
                    break;
            }

            return precedence;
        }

        private static double SubtractValues(double firstOperand, double secondOperand)
        {
            double difference = secondOperand - firstOperand;

            return difference;
        }

        private static double AddValues(double firstOperand, double secondOperand)
        {
            double sum = firstOperand + secondOperand;

            return sum;
        }

        private static double DivideValues(double firstOperand, double secondOperand)
        {
            double quotient = secondOperand / firstOperand;

            return quotient;
        }

        private static double MultiplyValues(double firstOperand, double secondOperand)
        {
            double product = firstOperand * secondOperand;

            return product;
        }

        private static double ExponentValues(double firstOperand, double secondOperand)
        {
            double product = Math.Pow(secondOperand, firstOperand);

            return product;
        }

        private static double CalculateReversePolishNotation(Queue<string> inputQueue)
        {
            Stack<string> outputStack = new Stack<string>();

            while (inputQueue.Count != 0)
            {
                string symbol = inputQueue.Dequeue();

                if (IsNumber(symbol))
                {
                    outputStack.Push(symbol);
                    continue;
                }

                double firstOperand = double.Parse(outputStack.Pop());
                double secondOperand = double.Parse(outputStack.Pop());
                double result = 0.0;

                if (symbol == "+")
                {
                    result = AddValues(firstOperand, secondOperand);
                }
                else if (symbol == "-")
                {
                    result = SubtractValues(firstOperand, secondOperand);
                }
                else if (symbol == "*")
                {
                    result = MultiplyValues(firstOperand, secondOperand);
                }
                else if (symbol == "/")
                {
                    result = DivideValues(firstOperand, secondOperand);
                }
                else if (symbol == "^")
                {
                    result = ExponentValues(firstOperand, secondOperand);
                }

                outputStack.Push(result.ToString());
            }

            double outcome = double.Parse(outputStack.Pop());

            return outcome;
        }
    }
}
