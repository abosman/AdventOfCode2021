var input = File.ReadAllLines(@"Input.txt");
var steps = input.Select(line => new Step(line.Split(' ')[0], 
    int.Parse(line.Split(' ')[1]))).ToList();

Part1();
Part2();

void Part1()
{
    var pos = new Point(0,0);
    foreach (var (action, amount) in steps)
    {
        switch (action)
        {
            case "forward":
                pos.X += amount;
                break;
            case "up":
                pos.Y -= amount;
                break;
            case "down":
                pos.Y += amount;
                break;
        }
    }
    Console.WriteLine($"Position X:{pos.X}, Y:{pos.Y}");
    Console.WriteLine($"Multiply X and Y:{pos.X} x {pos.Y} = {pos.X * pos.Y}"); // 1989265
}

void Part2()
{
    var pos = new Point(0, 0);
    var aim = 0;
    foreach (var (action, amount) in steps)
    {
        switch (action)
        {
            case "forward":
                pos.X += amount;
                pos.Y += aim * amount;
                break;
            case "up":
                aim -= amount;
                break;
            case "down":
                aim += amount;
                break;
        }
    }
    Console.WriteLine($"Position X:{pos.X}, Y:{pos.Y}");
    Console.WriteLine($"Multiply X and Y:{pos.X} x {pos.Y} = {pos.X * pos.Y}"); // 2089174012
}

public record Step(string Action, int Amount)
{
    public string Action { get; } = Action;
    public int Amount { get; } = Amount;
}
public record Point(int X, int Y)
{
    public int X { get; set; } = X;
    public int Y { get; set; } = Y;
}