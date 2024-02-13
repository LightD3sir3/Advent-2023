using System.Diagnostics;
using System.Security.AccessControl;

public class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: dotnet run \"File Path\"");
            return;
        }

        Stopwatch sw = new Stopwatch();

        sw.Start();

        string path = args[0];
        int total = 0;

        try
        {
            foreach (string line in File.ReadLines(path))
            {
                total += GetNumber(line);
            }

            sw.Stop();

            Console.WriteLine($"The total is: {total}");

            Console.WriteLine("Time elapsed: {0}", sw.Elapsed);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Program Encountered an Error: {ex.Message}");
        }
        catch (IOException e)
        {
            Console.WriteLine($"The file could not be read: {e.Message}");
        }
    }

    private static int GetNumber(string line)
    {
        string first = "";
        string second = "";
        bool isFirst = true;

        for (int i = 0; i < line.Length; i++)
        {
            if (Char.IsDigit(line[i]))
            {
                if (isFirst)
                {
                    first = line[i].ToString();
                    isFirst = false;
                }
                else
                {
                    second = line[i].ToString();
                }
            }
        }

        if (second == "")
        {
            second = first;
        }

        return Convert.ToInt32(first + second);
    }
}
