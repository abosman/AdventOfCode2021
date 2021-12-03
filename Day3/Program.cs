using System.Text;

var input = File.ReadAllLines(@"Input.txt");

Part1();
Part2();

void Part1()
{
    var counters = new Dictionary<int,int>();
    for (var i = 0; i < input[0].Length; i++)
    {
        counters.Add(i, 0);
    }

    foreach (var line in input)
    {
        for (var i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if (c == '1')
            {
                counters[i] += 1;
            }
        }
    }

    var gammarate = new StringBuilder();
    var epsilonrate = new StringBuilder();
    foreach (var counter in counters)
    {
        if (counter.Value > input.Length / 2)
        {
            gammarate.Append("1");
            epsilonrate.Append("0");
        }
        else
        {
            gammarate.Append("0");
            epsilonrate.Append("1");
        }
    }

    var result = Convert.ToInt32(gammarate.ToString(), 2) *
                 Convert.ToInt32(epsilonrate.ToString(), 2);
    Console.WriteLine($"result: {gammarate} x {epsilonrate} = {result}"); // 2648450
}

void Part2()
{
    Console.WriteLine("Part 2");
    var oxygenGeneratorRating = DetermineNumber(input,true);
    var co2ScrubberRating = DetermineNumber(input, false);
    var result = Convert.ToInt32(oxygenGeneratorRating, 2) *
                 Convert.ToInt32(co2ScrubberRating, 2);
    Console.WriteLine($"result: {oxygenGeneratorRating} x {co2ScrubberRating} = {result}"); // 2845944

}

string DetermineNumber(string[] input, bool mostCommonValueOperation)
{
    var numbers = new List<string>(input);
    var index = 0;
    while (numbers.Count > 1)
    {
        var oneBitCount = numbers.Select(t => t[index]).Count(c => c == '1');
        var mostCommonValue = numbers.Count - oneBitCount <= oneBitCount ? '1' : '0';
        if (mostCommonValueOperation)
        {
            numbers.RemoveAll(n => n[index] != mostCommonValue);
        }
        else
        {
            numbers.RemoveAll(n => n[index] == mostCommonValue);
        }
        index++;
    }
    return numbers[0];
}