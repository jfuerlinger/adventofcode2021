
using Figgle;

const int DaysToLive = 256;
const int MaxAgeOfFish = 8;

Console.WriteLine(FiggleFonts.Big.Render("AoC 2021 - Day 6"));

string[] lines = await File.ReadAllLinesAsync("input.txt");

SolvePuzzle(ExtractInformation(lines.First()));

void SolvePuzzle(long[] fishesByAge)
{
    for (int day = 0; day < DaysToLive; day++)
    {
        long[] currentFishesByAge = new long[MaxAgeOfFish + 1];
        for (int i = 8; i >= 1; i--)
        {
            currentFishesByAge[i - 1] = fishesByAge[i];
        }

        currentFishesByAge[8] = fishesByAge[0];
        currentFishesByAge[6] += fishesByAge[0];

        Console.WriteLine($"After {day + 1} days: {currentFishesByAge.Sum()}");

        fishesByAge = currentFishesByAge;
    }

    Console.WriteLine($"Solution after {DaysToLive}: {fishesByAge.Sum()} fishes");

}

long[] ExtractInformation(string input)
{
    long[] result = new long[MaxAgeOfFish + 1];
    input
        .Split(",")
        .Select(entry => byte.Parse(entry))
        .GroupBy(_ => _)
        .OrderBy(entry => entry.Key)
        .Select(g => new { Age = g.Key, NrOfFishes = g.Count() })
        .ToList()
        .ForEach(entry => result[entry.Age] = entry.NrOfFishes);

    return result;
}


