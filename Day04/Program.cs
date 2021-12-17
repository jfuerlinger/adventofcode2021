using Day04;

string[] lines = await File.ReadAllLinesAsync("input.txt");

var bingoNumbers = lines
                    .First()
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(nr => ushort.Parse(nr));

int bingoBlocksSolved = 0;
int bingoBlockCount = -1;

var bingoBlocks = lines
                    .Skip(1)
                    .Where(l => !string.IsNullOrEmpty(l))
                    .Chunk(5)
                    .Select(chunk =>
                        {
                            var bingoBlock = new BingoBlock(chunk);
                            bingoBlock.BingoSolved += OnBingoSolved;
                            return bingoBlock;
                        })
                    .ToList();

bingoBlockCount = bingoBlocks.Count;
foreach (var nr in bingoNumbers)
{
    bingoBlocks.ForEach(bingoBlock =>
    {
        if (!bingoBlock.IsSolved)
        {
            bingoBlock.MarkNumber(nr);
        }
    });
}



void OnBingoSolved(object? sender, int lastNumber)
{
    if (sender == null)
    {
        throw new ArgumentNullException(nameof(sender));
    }

    int sumOfUnmarkedNumbers = ((BingoBlock)sender).GetSumOfUnmarkedNumbers();
    bingoBlocksSolved++;

    if (bingoBlocksSolved == 1)
    {
        Console.WriteLine($"First Bingo Solved: lastNumber={lastNumber}, sumOfUnmarkedNumbers={sumOfUnmarkedNumbers}, solution 1={sumOfUnmarkedNumbers * lastNumber}");
    }

    if (bingoBlocksSolved == bingoBlockCount)
    {
        Console.WriteLine($"Last Bingo Solved: lastNumber={lastNumber}, sumOfUnmarkedNumbers={sumOfUnmarkedNumbers}, solution 2={sumOfUnmarkedNumbers * lastNumber}");
    }
}



