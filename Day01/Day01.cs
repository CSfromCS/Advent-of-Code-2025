namespace AdventOfCode2025;

using Common;

public static class Day01
{
    const string day = "Day01";

    public static void Main()
    {
        day1part1();
        day1part2();
    }


    // Part 1
    static int GetEffectiveNumber(int number)
    {
        return (number % 100 + 100) % 100;
    }
    static int GetIntFromInstruction(string instruction)
    {
        return int.Parse(instruction[1..]) * (instruction.StartsWith("L") ? -1 : 1);
    }

    static void day1part1()
    {
        string[]? lines = InputReader.ReadLines(day, "input.txt");
        int currentDialPosition = 50;
        int instructionMoveCount = 0;

        int countOfZeros = 0;

        foreach (string line in lines)
        {
            instructionMoveCount = GetEffectiveNumber(GetIntFromInstruction(line));
            currentDialPosition = GetEffectiveNumber(currentDialPosition + instructionMoveCount);
            if(currentDialPosition == 0)
            {
                countOfZeros++;
            }
        }
        Console.WriteLine($"Day01 Part 1: {countOfZeros}");
    }

    // Part 2
    static int GetCompletedRotations(int startPosition, int moveCount)
    {
        int totalNumber = startPosition + moveCount;
        if (totalNumber > 0)
        {
            return totalNumber / 100;
        }
        return (-totalNumber / 100) + (startPosition != 0 ? 1 : 0);

        //1 -> 101 = 1
        //0-> 101 = 1
        //0-> 1 = 0

        //1 -> 0 = 1
        //1 -> -1 = 1
        //1 -> -101 = 2
        //0 -> 0 = 0
        //0 -> -1 = 0
        //0 -> -101 = 1
    }
    static void day1part2()
    {
        string[]? lines = InputReader.ReadLines(day, "input.txt");

        int currentDialPosition = 50;
        int instructionMoveCount = 0;
        int countOfZeros = 0;

        foreach (string line in lines)
        {
            instructionMoveCount = GetIntFromInstruction(line);
            int completedRotations = GetCompletedRotations(currentDialPosition, instructionMoveCount);
            // Console.WriteLine($"Instruction {line}, from {currentDialPosition} moving {instructionMoveCount} completes {completedRotations} rotations, ends up in {GetEffectiveNumber(currentDialPosition + instructionMoveCount)}.");
            currentDialPosition = GetEffectiveNumber(currentDialPosition + instructionMoveCount);
            countOfZeros += completedRotations;
        }
        Console.WriteLine($"Day01 Part 2: {countOfZeros}");
    }
}