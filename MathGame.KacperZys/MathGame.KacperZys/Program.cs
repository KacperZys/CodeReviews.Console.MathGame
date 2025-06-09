using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int operation;
            int num1 = 0, num2 = 0;
            int points = 0;
            List<string> previousGames = new List<string>();
            Random rnd = new Random();
            Stopwatch stopwatch = new Stopwatch();
            char[] signs = { '+', '-', '*', '/' };

            int diffLevel = ChooseDifficultyLevel();

            while (true)
            {
                operation = MenuChoice();

                if (operation != 6 && operation != 7 && operation != 8)
                {

                    if (operation == 4)
                    {

                        (num1, num2) = GenerateNumberInDifficultyRange(diffLevel, true);
                    }
                    else
                    {
                        (num1, num2) = GenerateNumberInDifficultyRange(diffLevel, false);
                    }

                    Console.WriteLine();
                }



                switch (operation)
                {
                    case 1:
                        CheckAnswer(num1, num2, signs[0]);
                        break;
                    case 2:
                        CheckAnswer(num1, num2, signs[1]);
                        break;
                    case 3:
                        CheckAnswer(num1, num2, signs[2]);
                        break;
                    case 4:
                        CheckAnswer(num1, num2, signs[3]);
                        break;
                    case 5:
                        CheckAnswer(num1, num2, signs[rnd.Next(signs.Length)]);
                        break;
                    case 6:
                        if (previousGames.Count == 0)
                        {
                            Console.WriteLine("It's your first game!");
                        }
                        else
                        {
                            foreach (string game in previousGames)
                            {
                                Console.WriteLine(game);
                            }
                            Console.WriteLine($"\nTotal Points: {points}");
                        }
                        Console.WriteLine();
                        break;
                    case 7:
                        diffLevel = ChooseDifficultyLevel();
                        break;
                    case 8:
                        return;
                }
            }

            void CheckAnswer(int num1, int num2, char sign)
            {
                var operations = new Dictionary<char, Func<int, int, int>>
            {
                { '+', (x, y) => x + y },
                { '-', (x, y) => x - y },
                { '*', (x, y) => x * y },
                { '/', (x, y) => x / y },
            };

                if (operations.TryGetValue(sign, out var operation))
                {
                    int result = operation(num1, num2);

                    stopwatch.Restart();

                    Console.WriteLine($"{num1} {sign} {num2} = ?");
                    int input = Convert.ToInt32(Console.ReadLine());

                    if (input == result)
                    {
                        Console.WriteLine("Good Answer!");
                        Console.WriteLine();
                        previousGames.Add($"{num1} {sign} {num2} = {result}    +1 Point");
                        points++;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Answer!");
                        Console.WriteLine($"The correct answer is: {result}");
                        Console.WriteLine();
                        previousGames.Add($"{num1} {sign} {num2} = {result}");
                    }

                    stopwatch.Stop();
                    Console.WriteLine($"Time needed for answer: {Math.Round(stopwatch.Elapsed.TotalSeconds, 2)} s");
                }
            }

            int MenuChoice()
            {
                Console.WriteLine("1. Addition");
                Console.WriteLine("2. Substraction");
                Console.WriteLine("3. Multiplication");
                Console.WriteLine("4. Division");
                Console.WriteLine("5. Random");
                Console.WriteLine("6. History");
                Console.WriteLine("7. Change difficulty level");
                Console.WriteLine("8. Exit");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                return choice;
            }

            int ChooseDifficultyLevel()
            {
                Console.WriteLine("Choose difficulty level");
                Console.WriteLine("1. Easy");
                Console.WriteLine("2. Hard");
                int diff = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                return diff;
            }

            (int, int) GenerateNumberInDifficultyRange(int diffLevel, bool isDivision)
            {
                Random rnd = new Random();
                int num1 = 0;
                int num2 = 0;

                if (!isDivision)
                {
                    if (diffLevel == 1)
                    {
                        num1 = rnd.Next(1, 10);
                        num2 = rnd.Next(1, 10);
                    }
                    else if (diffLevel == 2)
                    {
                        num1 = rnd.Next(1, 101);
                        num2 = rnd.Next(1, 101);
                    }
                }
                else
                {
                    if (diffLevel == 1)
                    {
                        num1 = rnd.Next(1, 10);
                        num2 = rnd.Next(1, 10);

                        while ((float)num1 % num2 != 0)
                        {
                            num2 = rnd.Next(1, 10);
                        }
                    }
                    else if (diffLevel == 2)
                    {
                        num1 = rnd.Next(1, 101);
                        num2 = rnd.Next(1, 101);

                        while ((float)num1 % num2 != 0)
                        {
                            num2 = rnd.Next(1, 101);
                        }
                    }
                }

                return (num1, num2);
            }
        }


    }
}
