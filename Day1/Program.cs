var input = File.ReadAllLines(@"Input.txt");
var measures = input.Select(c => int.Parse(c.ToString())).ToList();

Part1();
Part2();

void Part1()
{
    var lastMeasure = measures.First();
    var increasesCount = 0;
    foreach (var measure in measures)
    {
        if (measure > lastMeasure)
        {
            increasesCount++;
        }
        lastMeasure = measure;
    }

    Console.WriteLine($"Number of increases: {increasesCount}"); // 1195
}

void Part2()
{
    var lastSumOfMeasures = measures[0] + measures[1] + measures[2];
    var increasesSumCount = 0;
    for (var index = 0; index < measures.Count-2; index++)
    {
        var sumOfMeasures = measures[index] + measures[index+1] + measures[index+2];
        if (sumOfMeasures > lastSumOfMeasures)
        {
            increasesSumCount++;
        }

        lastSumOfMeasures = sumOfMeasures;
    }

    Console.WriteLine($"Number of sum increases: {increasesSumCount}"); // 1235
}