
using Day08;
using Figgle;

Console.WriteLine(FiggleFonts.Big.Render("AoC 2021 - Day 8"));

string[] lines = await File.ReadAllLinesAsync("input.txt");

SolvePuzzle1(lines);

static void SolvePuzzle1(string[] lines)
{
    var notes = lines
           .Select(line => line.Split("|", StringSplitOptions.RemoveEmptyEntries))
           .Select(parts => new NoteEntry(
               parts[0].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries),
               parts[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
           ))
           .ToArray();

    int solutionPart1 = notes
        .SelectMany(entry => entry.OutputValues)
        .Count(entry => new[] { 2 /* -> 1 */,
                                4 /* -> 4 */,
                                3 /* -> 7 */,
                                7 /* -> 8 */}.Contains(entry.Length));

    Console.WriteLine($"Solution part 1: {solutionPart1}");
}




