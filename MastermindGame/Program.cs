using System;
using System.Collections.Generic;
using System.Linq;

namespace MastermindGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = null;
            int attempts = 10;
            // Parse arguments
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-c" && i + 1 < args.Length)
                {
                    code = args[i + 1];
                }
                if (args[i] == "-t" && i + 1 < args.Length && int.TryParse(args[i + 1], out int t))
                {
                    attempts = t;
                }
            }
            MastermindGame game = new MastermindGame(code, attempts);
            game.Play();
        }
    }

    public class MastermindGame
    {
        private string SecretCode;
        private int MaxAttempts;
        private int CurrentRound = 0;
        private readonly CodeValidator Validator = new CodeValidator();

        public MastermindGame(string code, int attempts)
        {
            if (code != null && Validator.IsValidCode(code))
            {
                SecretCode = code;
            }
            else
            {
                SecretCode = CodeGenerator.GenerateRandomCode();
            }
            MaxAttempts = attempts;
        }

        public void Play()
        {
            Console.WriteLine("Will you find the secret code?");
            Console.WriteLine("Please enter a valid guess");

            while (CurrentRound < MaxAttempts)
            {
                Console.WriteLine($"--- Round {CurrentRound}");
                Console.Write(">");
                string input = Console.ReadLine();

                if (input == null) // Handle EOF
                {
                    Console.WriteLine("Input closed.");
                    break;
                }

                if (!Validator.IsValidCode(input))
                {
                    Console.WriteLine("Wrong input!");
                    continue;
                }


                if (input == SecretCode)
                {
                    Console.WriteLine("Congratz! You did it!");
                    break;
                }

                (int wellPlaced, int misplaced) = FeedbackCalculator.CalculateFeedback(SecretCode, input);
                Console.WriteLine($"Well placed pieces: {wellPlaced}");
                Console.WriteLine($"Misplaced pieces: {misplaced}");

                CurrentRound++;
            }

            if (CurrentRound >= MaxAttempts)
            {
                Console.WriteLine("You have used all attempts. Game over.");
            }
        }
    }

    public static class CodeGenerator
    {
        public static string GenerateRandomCode()
        {
            Random rand = new Random();
            List<char> digits = Enumerable.Range(0, 9).Select(x => x.ToString()[0]).ToList();
            string code = "";
            for (int i = 0; i < 4; i++)
            {
                int index = rand.Next(digits.Count);
                code += digits[index];
                digits.RemoveAt(index);
            }
            return code;
        }
    }

    public class CodeValidator
    {
        public bool IsValidCode(string code)
        {
            if (code.Length != 4) return false;
            if (!code.All(c => c >= '0' && c <= '8')) return false;
            if (code.Distinct().Count() != 4) return false;
            return true;
        }
    }

    public static class FeedbackCalculator
    {
        public static (int wellPlaced, int misplaced) CalculateFeedback(string secret, string guess)
        {
            int wellPlaced = 0;
            int misplaced = 0;

            List<char> secretList = secret.ToList();
            List<char> guessList = guess.ToList();

            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == secret[i])
                {
                    wellPlaced++;
                    secretList[i] = 'x';
                    guessList[i] = 'y';
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (guessList[i] != 'y')
                {
                    int index = secretList.IndexOf(guessList[i]);
                    if (index != -1)
                    {
                        misplaced++;
                        secretList[index] = 'z';
                    }
                }
            }

            return (wellPlaced, misplaced);
        }
    }
}
