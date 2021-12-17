namespace Day04
{
    internal class BingoBlock
    {
        struct BingoEntry
        {
            public ushort Nr;
            public bool Marked;
        }

        private readonly BingoEntry[,] _data;

        public bool IsSolved { get; private set; }

        public event EventHandler<int>? BingoSolved;

        public BingoBlock(string[] input)
        {
            _data = new BingoEntry[5, 5];
            for (int i = 0; i < input.Length; i++)
            {
                ushort[] columns = input[i]
                                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                    .Select(nr => ushort.Parse(nr))
                                    .ToArray();

                for (int j = 0; j < columns.Length; j++)
                {
                    _data[i, j].Nr = columns[j];
                }
            }
        }

        protected virtual void OnBingoSolved(int value)
        {
            BingoSolved?.Invoke(this, value);
        }

        public void MarkNumber(ushort nr)
        {
            if(IsSolved)
            {
                throw new InvalidOperationException("BingoBlock is already solved!");
            }

            for (int rowIdx = 0; rowIdx < _data.GetLength(0); rowIdx++)
            {
                for (int colIdx = 0; colIdx < _data.GetLength(1); colIdx++)
                {
                    if (_data[rowIdx, colIdx].Nr == nr)
                    {
                        _data[rowIdx, colIdx].Marked = true;
                        bool isSolved = CheckIfSolved(rowIdx, colIdx);
                        if(isSolved)
                        {
                            IsSolved = true;
                        }
                        return;
                    }
                }
            }
        }

        public bool CheckIfSolved(int row, int col)
        {
            // check rows
            bool allChecked = true;
            for (int colIdx = 0; colIdx < _data.GetLength(1); colIdx++)
            {
                if (!_data[row, colIdx].Marked)
                {
                    allChecked = false;
                    break;
                }
            }

            if (allChecked)
            {
                OnBingoSolved(_data[row, col].Nr);
                return true;
            }

            // check columns
            allChecked = true;
            for (int rowIdx = 0; rowIdx < _data.GetLength(0); rowIdx++)
            {
                if (!_data[rowIdx, col].Marked)
                {
                    allChecked = false;
                    break;
                }
            }

            if (allChecked)
            {
                OnBingoSolved(_data[row, col].Nr);
                return true;
            }

            return false;
        }

        public int GetSumOfUnmarkedNumbers()
        {
            int sumOfUnmarkedNumbers = 0;
            for (int rowIdx = 0; rowIdx < _data.GetLength(0); rowIdx++)
            {
                for (int colIdx = 0; colIdx < _data.GetLength(1); colIdx++)
                {
                    if (!_data[rowIdx, colIdx].Marked)
                    {
                        sumOfUnmarkedNumbers += _data[rowIdx, colIdx].Nr;
                    }
                }
            }

            return sumOfUnmarkedNumbers;
        }
    }
}