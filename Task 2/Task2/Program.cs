using System;

class GradeCalculator
{
    static int ReadValidScore(int scoreNumber)
    {
        while (true)
        {
            Console.Write($"Enter score {scoreNumber} (0-100): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int score) && score >= 0 && score <= 100)
                return score;

            Console.WriteLine("  Invalid score! Please enter a value between 0 and 100.");
        }
    }

    static double CalculateAverage(int[] scores)
    {
        double sum = 0;
        foreach (int s in scores)
            sum += s;
        return sum / scores.Length;
    }

    static string GetLetterGrade(double avg)
    {
        return avg switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };
    }

    static void PrintReport(int[] scores, double avg, string grade)
    {
        int highest = scores[0], lowest = scores[0];
        foreach (int s in scores)
        {
            if (s > highest) highest = s;
            if (s < lowest)  lowest  = s;
        }

        string gradeLabel = grade switch
        {
            "A" => "Excellent",
            "B" => "Good",
            "C" => "Above Average",
            "D" => "Pass",
            _   => "Fail"
        };

        string status = avg >= 60 ? "PASS" : "FAIL";

        Console.WriteLine("\n--- Report ---");
        Console.WriteLine($"Scores   : {string.Join(", ", scores)}");
        Console.WriteLine($"Average  : {avg:F2}");
        Console.WriteLine($"Highest  : {highest}  |  Lowest: {lowest}");
        Console.WriteLine($"Grade    : {grade}  ({gradeLabel})");
        Console.WriteLine($"Status   : {status}");
    }

    static void Main()
    {
        int[] scores = new int[5];

        Console.WriteLine("=== Grade Calculator ===\n");

        for (int i = 0; i < scores.Length; i++)
            scores[i] = ReadValidScore(i + 1);

        double avg   = CalculateAverage(scores);
        string grade = GetLetterGrade(avg);

        PrintReport(scores, avg, grade);
    }
}