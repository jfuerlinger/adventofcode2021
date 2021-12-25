
using Figgle;

const int DaysToLive = 256;
const int MaxAgeOfFish = 8;

Console.WriteLine(FiggleFonts.Big.Render("AoC 2021 - Day 7"));

string[] lines = await File.ReadAllLinesAsync("input.txt");

SolvePuzzle(ExtractInformation(lines.First()));

void SolvePuzzle(int[] crabPositions)
{
    int maxPosition = crabPositions.Max();
    int optimalPosition = -1;
    int shortestWay = int.MaxValue;

    for (int targetPosition = 0; targetPosition <= maxPosition; targetPosition++)
    {
        int movementsToTargetPos = 0;
        bool isInefficient = false;
        for (int i = 0; i < crabPositions.Length; i++)
        {
            movementsToTargetPos += Math.Abs(crabPositions[i] - targetPosition);
            if (movementsToTargetPos > shortestWay)
            {
                isInefficient = true;
                break;
            }
        }

        if (!isInefficient)
        {
            shortestWay = movementsToTargetPos;
            optimalPosition = targetPosition;
        }
    }
    
    Console.WriteLine($"Solution: The crabs have to move {shortestWay} to position {optimalPosition}");

}

int[] ExtractInformation(string input) =>
    input
        .Split(",")
        .Select(entry => int.Parse(entry))
        .ToArray();



