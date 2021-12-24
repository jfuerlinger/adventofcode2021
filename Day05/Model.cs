using System.Text;

namespace Day05
{
    internal record Position(int X, int Y);

    internal record NavigationLine(Position Start, Position End);

    internal class Field
    {
        private readonly ushort[,] _field;

        public Field(int fieldDimension)
        {
            _field = new ushort[fieldDimension, fieldDimension];
        }

        public ushort this[int row, int column]
        {
            get => _field[row, column];
            private set => _field[row, column] = value;
        }


        public ushort this[Position position]
        {
            get => _field[position.Y, position.X];
            private set => _field[position.Y, position.X] = value;
        }

        public void AddNavigationLine(NavigationLine line)
        {
            if (line.Start == line.End)
            {
                throw new InvalidOperationException("Start and End are at some Position");
            }

            Position currentPosition = line.Start;
            this[currentPosition]++;

            while (currentPosition != line.End)
            {
                currentPosition = GetNextPositionTowardsTarget(currentPosition, line.End);
                this[currentPosition]++;
            }
        }

        public int GetDangerousPositions()
        {
            int dangerCount = 0;
            for (int row = 0; row < _field.GetLength(0); row++)
            {
                for (int column = 0; column < _field.GetLength(1); column++)
                {
                    if(this[row, column] >= 2)
                    {
                        dangerCount++;
                    }
                }
            }

            return dangerCount;
        }

        private static Position GetNextPositionTowardsTarget(Position current, Position target)
        {
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            return new Position(
                GetDelta(current.X, target.X),
                GetDelta(current.Y, target.Y));
        }

        private static int GetDelta(int current, int target)
        {
            if (current == target)
            {
                return current;
            }

            return current < target ? current + 1 : current - 1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < _field.GetLength(0); row++)
            {
                for (int column = 0; column < _field.GetLength(1); column++)
                {
                    sb.Append(this[row, column] == 0 ? "." : this[row, column]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}