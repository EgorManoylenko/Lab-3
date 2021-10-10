using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    class Calculator
    {
        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }

        public decimal Substract(decimal a, decimal b)
        {
            return a - b;
        }

        public decimal Multiply(decimal a, decimal b)
        {
            return a * b;
        }

        public decimal Division(decimal a, decimal b)
        {
            return a / b;
        }
    }

    class Parser
    {
        private string input;
        private char[] arrInput;

        private decimal leftOperand;
        private decimal rightOperand;
        private char operat;

        private bool stop = false;
        private int countOperat = 0;

        private void Input()
        {
            input = Console.ReadLine();
            input = input
                .Replace(" ", "")
                .Replace(",", ".")
                .Replace("=", "");
            arrInput = input.ToCharArray();

            foreach (var t in arrInput)
            {
                if (t == '-' || t == '+' || t == '*' || t == '/')
                {
                    countOperat++;
                }
            }
        }
        private static decimal ParseDecimal(string s)
        {
            return decimal.Parse(
                s,
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture
            );
        }

        private bool OperatorFind()
        {
            if (arrInput.Length != 0)
            {
                for (int i = 0; i < arrInput.Length; i++)
                {
                    if (arrInput[i] == '+' || arrInput[i] == '-' || arrInput[i] == '*' || arrInput[i] == '/')
                    {
                        operat = arrInput[i];
                        return true;
                    }
                }
            }

            return false;
        }

        private void ParseLeftOperand()
        {
            string firstOperand = "";
            OperatorFind();

            if (arrInput.Length != 0)
            {
                for (int i = 0; i < arrInput.Length; i++)
                {
                    if (arrInput[i] == operat)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            firstOperand += arrInput[j];
                        }
                    }
                }

                if (OperatorFind() == false)
                {
                    for (int i = 0; i < arrInput.Length; i++)
                    {
                        firstOperand += arrInput[i];
                    }
                }
            }

            if (firstOperand != "")
            {
                try
                {
                    leftOperand = ParseDecimal(firstOperand);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Перше число введено неправильно");
                    stop = true;
                }
            }
        }

        private void ParseRightOperand()
        {
            string secondOperand = "";
            bool o = false;
            if (OperatorFind() == false)  
            {
                Input();
                OperatorFind();
                for (int i = 0; i < arrInput.Length; i++)
                {
                    if (arrInput[i] == operat)
                    {
                        o = true;
                    }
                }
                if (OperatorFind() == true && o == true)
                {
                    for (int i = 0; i < arrInput.Length; i++)
                    {
                        if (arrInput[i] == operat)
                        {
                            for (int j = i + 1; j < arrInput.Length; j++)
                            {
                                secondOperand += arrInput[j];
                            }
                        }
                    }
                }

                if (secondOperand == "" && OperatorFind() == true)
                {
                    Input();
                    for (int i = 0; i < arrInput.Length; i++)
                    {
                        secondOperand += arrInput[i];
                    }
                }
            }
            else if (OperatorFind() == true)
            {
                for (int i = 0; i < arrInput.Length; i++)
                {
                    if (arrInput[i] == operat)
                    {
                        o = true;
                    }
                }

                if (o == true)
                {
                    for (int i = 0; i < arrInput.Length; i++)
                    {
                        if (arrInput[i] == operat)
                        {
                            for (int j = i + 1; j < arrInput.Length; j++)
                            {
                                secondOperand += arrInput[j];
                            }
                        }
                    }
                }

                if (secondOperand == "")
                {
                    Input();
                    for (int i = 0; i < arrInput.Length; i++)
                    {
                        secondOperand += arrInput[i];
                    }
                }
            }


            if (secondOperand != "")
            {
                try
                {
                    rightOperand = ParseDecimal(secondOperand);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Друге число введено неправильно");
                    stop = true;
                }
            }

        }

        public void Parse()
        {
            Input();
            ParseLeftOperand();
            ParseRightOperand();
        }

        public void GetResult()
        {
            decimal result;
            if (countOperat > 1)
            {
                Console.WriteLine("Більше одного знака дії.");
                stop = true;
            }

            if (stop == false)
            {
                if (operat == '+')
                {
                    Calculator calculator = new Calculator();
                    result = calculator.Add(leftOperand, rightOperand);
                }
                else if (operat == '-')
                {
                    Calculator calculator = new Calculator();
                    result = calculator.Substract(leftOperand, rightOperand);
                }
                else if (operat == '*')
                {
                    Calculator calculator = new Calculator();
                    result = calculator.Multiply(leftOperand, rightOperand);
                }
                else
                {
                    Calculator calculator = new Calculator();
                    result = calculator.Division(leftOperand, rightOperand);
                }

                Console.WriteLine($"{leftOperand} {operat} {rightOperand} = {result}");
            }
        }

        public void Instruction()
        {
            Console.WriteLine("Інструкція: ");
            Console.WriteLine();
            Console.WriteLine("Введіть математичний приклад, який складається з двух чисел і знаком дії між введеними числами числами.");
            Console.WriteLine("Математичний приклад може бути введений частинами.");
            Console.WriteLine();
            Console.WriteLine("Можливі операції над числами: \nМноження (*), \nДілення (/), \nДодавання (+), \nВіднімання (-).");
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = System.Text.Encoding.Unicode;

            Parser parser = new Parser();
            parser.Instruction();
            parser.Parse();
            parser.GetResult();

            Console.ReadKey();
        }
    }
}
