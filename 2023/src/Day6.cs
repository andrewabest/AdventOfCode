using Xunit;

namespace AdventOfCode2023;

public class Day6
{
    [Fact]
    void PartOne()
    {
        var inputs = new (int time, int distance)[] {
            (63, 411),
            (78, 1274),
            (94, 2047),
            (68, 1035)
        };

        int GetPossibilities(int distance, int time)
        {
            for (var i = 0; i < time; i++)
            {
                var timeRemaining = time - i;

                if (timeRemaining * i > distance)
                {
                    return timeRemaining - i + 1;
                }
            }

            return 0;
        }
        
        var result = inputs.Aggregate(1, (acc, curr) => acc * GetPossibilities(curr.distance, curr.time));
        
        Assert.Equal(781200, result);
    }
    
    [Fact]
    void PartTwo()
    {
        var inputs = new (long time, long distance)[] {
            (63789468, 411127420471035)
        };

        long GetPossibilities(long distance, long time)
        {
            for (var i = 0; i < time; i++)
            {
                var timeRemaining = time - i;

                if (timeRemaining * i > distance)
                {
                    return timeRemaining - i + 1;
                }
            }

            return 0;
        }
        
        var result = inputs.Aggregate(1L, (acc, curr) => acc * GetPossibilities(curr.distance, curr.time));
        
        Assert.Equal(0, result);
    }
}