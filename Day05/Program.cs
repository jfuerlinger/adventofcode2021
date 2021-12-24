
using Day05;
using Figgle;

Console.WriteLine(FiggleFonts.Big.Render("AoC 2021 - Day 5"));

string[] lines = await File.ReadAllLinesAsync("input.txt");

var navigationLines = BuildNavigationLines(lines);

SolvePuzzle(
    "part 1",
    navigationLines
        .Where(line => line.Start.X == line.End.X || line.Start.Y == line.End.Y));

SolvePuzzle(
    "part 2",
    navigationLines);



void SolvePuzzle(string puzzleName, IEnumerable<NavigationLine> lines)
{
    Field fieldPart = new(GetDimension(lines));
    lines.ToList()
        .ForEach(line => fieldPart.AddNavigationLine(line));

    Console.WriteLine($"Solution '{puzzleName}' -> fields of danger: {fieldPart.GetDangerousPositions()}");
}


int GetDimension(IEnumerable<NavigationLine> lines) =>
    new int[] {
            lines.Max(line => line.Start.X),
            lines.Max(line => line.Start.Y),
            lines.Max(line => line.End.X),
            lines.Max(line => line.End.Y)
    }.Max() + 1;

IEnumerable<NavigationLine> BuildNavigationLines(string[] lines) =>
    lines.Select(line =>
    {
        var points = line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
        var point1Information = points[0].Split(",");
        var point2Information = points[1].Split(",");

        return new NavigationLine(
            new Position(ushort.Parse(point1Information[0]), ushort.Parse(point1Information[1])),
            new Position(ushort.Parse(point2Information[0]), ushort.Parse(point2Information[1])));
    });

