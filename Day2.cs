using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;


public class Program
{
    const UInt16 RED = 12;
    const UInt16 GREEN = 13;
    const UInt16 BLUE = 14;

    private static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: dotnet run \"File Path\"");
            return;
        }

        Stopwatch sw = new Stopwatch();

        sw.Start();

        string[] lines = File.ReadAllLines(args[0]);
        int total = 0;

        try
        {
            for (int i = 1; i <= lines.Length; i++)
            {
                if (IsValidGame(lines[i - 1]))
                {
                    total += i;
                }
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

    private static bool IsValidGame(string line)
    {
        line = line.Substring(line.IndexOf(':') + 1).Trim();

        string[] games = line.Split(';');

        foreach (string game in games)
        {
            string[] values = game.Trim().Split(',');

            foreach (string value in values)
            {
                string[] parts = value.Trim().Split(' ');

                if (parts[1] == "blue")
                {
                    if (Convert.ToInt16(parts[0]) > BLUE)
                    {
                        return false;
                    }
                } else if (parts[1] == "red")
                {
                    if (Convert.ToInt16(parts[0]) > RED)
                    {
                        return false;
                    }
                }
                else if (parts[1] == "green")
                {
                    if (Convert.ToInt16(parts[0]) > GREEN)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
