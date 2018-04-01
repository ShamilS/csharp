
using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // System.Console.WriteLine("For 'Yacht' dice game solution see 'Yacht.cs' file.");
            (new YachtTestsInConsoleApp()).RunTests();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"*** Runtime Error = {ex.Message}");
        }
    }
}

