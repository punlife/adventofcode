namespace AdventOfCode2025;

public class Day4 : Day
{
    public long RunTask1(string[] input)
    {
        var accessibleCount = 0;
        var warehouse = new WarehouseMap(input);

        for (var y = 0; y < warehouse.yLength; y++)
        {
            for (var x = 0; x < warehouse.xLength; x++)
            {
                var value = warehouse.map[y, x];
                if (!value.Equals('@')) continue;
                if (warehouse.IsItemAccessible(x, y))
                    accessibleCount++;
            }
        }

        return accessibleCount;
    }

    public long RunTask2(string[] input)
    {
        var totalRemoved = 0;
        var currentRemoved = 0;
        var warehouse = new WarehouseMap(input);

        do
        {
            currentRemoved = 0;
            for (var y = 0; y < warehouse.yLength; y++)
            {
                for (var x = 0; x < warehouse.xLength; x++)
                {
                    var value = warehouse.map[y, x];
                    if (!value.Equals('@')) continue;
                    if (!warehouse.IsItemAccessible(x, y)) continue;
                    warehouse.map[y, x] = '.';
                    currentRemoved++;
                }
            }
            totalRemoved += currentRemoved;
        } while (currentRemoved > 0);

        return totalRemoved;
    }


    class WarehouseMap
    {
        public int xLength;
        public int yLength;
        public char[,] map;

        public WarehouseMap(string[] input)
        {
            xLength = input[0].Length;
            yLength = input.Length;
            map = CreateMap(input);
        }

        public bool IsItemAccessible(int x, int y)
        {
            var itemCounter = 0;
            if (CheckNorth(x, y)) itemCounter++;
            if (CheckNorthEast(x, y)) itemCounter++;
            if (CheckEast(x, y)) itemCounter++;
            if (CheckSouthEast(x, y)) itemCounter++;
            if (CheckSouth(x, y)) itemCounter++;
            if (CheckSouthWest(x, y)) itemCounter++;
            if (CheckWest(x, y)) itemCounter++;
            if (CheckNorthWest(x, y)) itemCounter++;


            return itemCounter < 4;
        }

        private char[,] CreateMap(string[] input)
        {
            var newMap = new char[xLength, yLength];
            for (var i = 0; i < yLength; i++)
            {
                var line = input[i];
                for (var j = 0; j < line.Length; j++)
                {
                    newMap[i, j] = line[j];
                }
            }

            return newMap;
        }

        private bool CheckNorth(int x, int y)
        {
            if (y == 0)
                return false;

            var value = map[y - 1, x];
            return value.Equals('@');
        }

        private bool CheckSouth(int x, int y)
        {
            if (y >= yLength - 1)
                return false;

            var value = map[y + 1, x];
            return value.Equals('@');
        }

        private bool CheckWest(int x, int y)
        {
            if (x == 0)
                return false;

            var value = map[y, x - 1];
            return value.Equals('@');
        }

        private bool CheckEast(int x, int y)
        {
            if (x >= xLength - 1)
                return false;

            var value = map[y, x + 1];
            return value.Equals('@');
        }

        private bool CheckNorthEast(int x, int y)
        {
            if (x >= xLength - 1 || y == 0)
                return false;

            var value = map[y - 1, x + 1];
            return value.Equals('@');
        }

        private bool CheckNorthWest(int x, int y)
        {
            if (x == 0 || y == 0)
                return false;

            var value = map[y - 1, x - 1];
            return value.Equals('@');
        }

        private bool CheckSouthEast(int x, int y)
        {
            if (x >= xLength - 1 || y >= yLength - 1)
                return false;

            var value = map[y + 1, x + 1];
            return value.Equals('@');
        }

        private bool CheckSouthWest(int x, int y)
        {
            if (x == 0 || y >= yLength - 1)
                return false;

            var value = map[y + 1, x - 1];
            return value.Equals('@');
        }
    }
}