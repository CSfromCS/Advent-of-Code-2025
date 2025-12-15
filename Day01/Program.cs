using Common;

const string day = "Day01";

var lines = InputReader.ReadLines(day, "input.txt");

static int GetEffectiveNumber(int number)
{
    return (number % 100 + 100) % 100;
}
static int GetIntFromInstruction(string instruction)
{
    return int.Parse(instruction[1..]) * (instruction.StartsWith("L") ? -1 : 1);
}

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
Console.WriteLine(countOfZeros);
