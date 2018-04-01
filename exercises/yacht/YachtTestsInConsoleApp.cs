using System;

/// <summary>
/// C# solution for 'Yacht (dice game) - https://en.wikipedia.org/wiki/Yacht_(dice_game).
/// Requires JSON.NET from https://www.newtonsoft.com/json to run the test cases.
/// </summary>
public class YachtTestsInConsoleApp
{
    public void RunTests()
    {
        string url = @"https://raw.githubusercontent.com/exercism/problem-specifications/master/exercises/yacht/canonical-data.json";
        this.TestJsonData = new System.Net.WebClient().DownloadString(url);

        dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(this.TestJsonData);
        System.Console.WriteLine("Exercise '{0}', Version = '{1}', TestCases.Count = {2}", json.Exercise, json.Version, json.Cases.Count);
        System.Console.WriteLine("---");
        foreach (var comment in json.Comments) System.Console.WriteLine(comment);
        System.Console.WriteLine("---");

        int correctResultsCount = 0;
        for (int i = 0; i < json.Cases.Count; i++)
        {
            dynamic testCase = json.Cases[i];

            int[] diceSet = new int[5];
            for (int j = 0; j < 5; j++)
            {
                diceSet[j] = Int32.Parse(testCase.Input.Dice[j].ToString());
            }

            int score = (new Yacht()).CalculateScore(diceSet, testCase.Input.Category.ToString());
            int expectedScore = Int32.Parse(testCase.Expected.ToString());

            System.Console.WriteLine("{0:00}. {1}: Category = {2}, Dice = {3}, {4} = {5}, ExpectedScore = {6}, Correct? = {7} ",
                    i + 1, testCase.Description, testCase.Input.Category,
                    string.Join(",", testCase.Input.Dice),
                    testCase.Property, score, expectedScore,
                    score == expectedScore ? "YES" : " *** NO ***");
            if (score == expectedScore) correctResultsCount++;
        }

        System.Console.WriteLine("---");
        if (correctResultsCount == json.Cases.Count)
            System.Console.WriteLine("*** All {0} test cases processed correctly ***", json.Cases.Count);
        else
            System.Console.WriteLine("*** Failed {0} test cases of {1} ***", json.Cases.Count - correctResultsCount, json.Cases.Count);

    }

    private string _testJsonData;
    public string TestJsonData
    {
        get
        {
            return _testJsonData
                     .Replace("\"exercise\"", "\"Exercise\"")
                     .Replace("\"version\"", "\"Version\"")
                     .Replace("\"comments\"", "\"Comments\"")
                     .Replace("\"cases\"", "\"Cases\"")
                     .Replace("\"description\"", "\"Description\"")
                     .Replace("\"property\"", "\"Property\"")
                     .Replace("\"input\"", "\"Input\"")
                     .Replace("\"dice\"", "\"Dice\"")
                     .Replace("\"category\"", "\"Category\"")
                     .Replace("\"expected\"", "\"Expected\"");
        }
        private set { _testJsonData = value; }
    }
}

/*
  
    Console app test log
    ---------------------

    Exercise 'yacht', Version = '1.0.0', TestCases.Count = 26
    ---
    The dice are represented always as a list of exactly five integers
    with values between 1 and 6 inclusive. The category is an string.
    the categories are 'ones' to 'sixes',
    Then 'full house',
         'four of a kind'
         'little straight' 1-5
         'big straight' 2-6
         'choice', sometimes called Chance
         'yacht', or five of a kind
    ---
    01. Yacht: Category = yacht, Dice = 5,5,5,5,5, score = 50, ExpectedScore = 50, Correct? = YES
    02. Not Yacht: Category = yacht, Dice = 1,3,3,2,5, score = 0, ExpectedScore = 0, Correct? = YES
    03. Ones: Category = ones, Dice = 1,1,1,3,5, score = 3, ExpectedScore = 3, Correct? = YES
    04. Ones, out of order: Category = ones, Dice = 3,1,1,5,1, score = 3, ExpectedScore = 3, Correct? = YES
    05. No ones: Category = ones, Dice = 4,3,6,5,5, score = 0, ExpectedScore = 0, Correct? = YES
    06. Twos: Category = twos, Dice = 2,3,4,5,6, score = 2, ExpectedScore = 2, Correct? = YES
    07. Fours: Category = fours, Dice = 1,4,1,4,1, score = 8, ExpectedScore = 8, Correct? = YES
    08. Yacht counted as threes: Category = threes, Dice = 3,3,3,3,3, score = 15, ExpectedScore = 15, Correct? = YES
    09. Yacht of 3s counted as fives: Category = fives, Dice = 3,3,3,3,3, score = 0, ExpectedScore = 0, Correct? = YES
    10. Sixes: Category = sixes, Dice = 2,3,4,5,6, score = 6, ExpectedScore = 6, Correct? = YES
    11. Full house two small, three big: Category = full house, Dice = 2,2,4,4,4, score = 16, ExpectedScore = 16, Correct? = YES
    12. Full house three small, two big: Category = full house, Dice = 5,3,3,5,3, score = 19, ExpectedScore = 19, Correct? = YES
    13. Two pair is not a full house: Category = full house, Dice = 2,2,4,4,5, score = 0, ExpectedScore = 0, Correct? = YES
    14. Yacht is not a full house: Category = full house, Dice = 2,2,2,2,2, score = 0, ExpectedScore = 0, Correct? = YES
    15. Four of a Kind: Category = four of a kind, Dice = 6,6,4,6,6, score = 24, ExpectedScore = 24, Correct? = YES
    16. Yacht can be scored as Four of a Kind: Category = four of a kind, Dice = 3,3,3,3,3, score = 12, ExpectedScore = 12, Correct? = YES
    17. Full house is not Four of a Kind: Category = four of a kind, Dice = 3,3,3,5,5, score = 0, ExpectedScore = 0, Correct? = YES
    18. Little Straight: Category = little straight, Dice = 3,5,4,1,2, score = 30, ExpectedScore = 30, Correct? = YES
    19. Little Straight as Big Straight: Category = big straight, Dice = 1,2,3,4,5, score = 0, ExpectedScore = 0, Correct? = YES
    20. Four in order but not a little straight: Category = little straight, Dice = 1,1,2,3,4, score = 0, ExpectedScore = 0, Correct? = YES
    21. No pairs but not a little straight: Category = little straight, Dice = 1,2,3,4,6, score = 0, ExpectedScore = 0, Correct? = YES
    22. Minimum is 1, maximum is 5, but not a little straight: Category = little straight, Dice = 1,1,3,4,5, score = 0, ExpectedScore = 0, Correct? = YES
    23. Big Straight: Category = big straight, Dice = 4,6,2,5,3, score = 30, ExpectedScore = 30, Correct? = YES
    24. Big Straight as little straight: Category = little straight, Dice = 6,5,4,3,2, score = 0, ExpectedScore = 0, Correct? = YES
    25. Choice: Category = choice, Dice = 3,3,5,6,6, score = 23, ExpectedScore = 23, Correct? = YES
    26. Yacht as choice: Category = choice, Dice = 2,2,2,2,2, score = 10, ExpectedScore = 10, Correct? = YES
    ---
    *** All 26 test cases processed correctly ***

 */
