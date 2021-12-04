using System.Text.RegularExpressions;

var input = File.ReadAllLines(@"Input.txt");
var bingoCards = new List<BingoCard>();
var drawNumbers = input[0].Split(',').Select(s => int.Parse(s.Trim())).ToList();
for (var index = 2; index < input.Length; index += 6)
{
    var card = new BingoCard();
    for (int row = 0; row < 5; row++)
    {
        var line = input[index + row];
        var regex = new Regex(@"\s*(\d{1,2})");
        var matches = regex.Matches(line);
        for (var col = 0; col < matches.Count; col++)
        {
            var match = matches[col];
            var number = int.Parse(match.Value.Trim());
            card.Numbers.Add(new BingoCardNumber(row, col, number));
        }
    }
    bingoCards.Add(card);
}


Part1();
Part2();

void Part1()
{
    var bingo = false;
    var i = 0;
    while (!bingo)
    {
        var drawNumber = drawNumbers[i];
        foreach (var bingoCard in bingoCards)
        {
            bingo = MarkNumber(drawNumber, bingoCard);
            if (bingo)
            {
                Console.WriteLine($"Bingo!!");
                var sumUnMarkedNumbers = bingoCard.Numbers.Where(c => !c.Marked).Sum(c => c.Number);
                Console.WriteLine(
                    $"final score =  {sumUnMarkedNumbers} (sum unmarked numbers) x {drawNumber} (winningNumber) " +
                    $"= {sumUnMarkedNumbers * drawNumber}"); // 8580
                break;
            }
        }
        i++;
    }
}

void Part2()
{
    bool allBingo = false;
    var i = 0;
    while (!allBingo)
    {
        var drawNumber = drawNumbers[i];
        foreach (var bingoCard in from bingoCard in bingoCards let bingo = MarkNumber(drawNumber, bingoCard) where bingo && !bingoCard.Bingo select bingoCard)
        {
            bingoCard.Bingo = true;
            if (bingoCards.All(c => c.Bingo))
            {
                var sumUnMarkedNumbers = bingoCard.Numbers.Where(c => !c.Marked).Sum(c => c.Number);
                Console.WriteLine(
                    $"final score =  {sumUnMarkedNumbers} (sum unmarked numbers) x {drawNumber} (winningNumber) " +
                    $"= {sumUnMarkedNumbers * drawNumber}"); // 9576
                allBingo = true;
            }
        }
        i++;
    }
}

bool MarkNumber(int number, BingoCard bingoCard)
{
    var bingoCardItem = bingoCard.Numbers.SingleOrDefault(c => c.Number == number);
    if (bingoCardItem != null)
    {
        bingoCardItem.Marked = true;
        return CheckForBingo(bingoCardItem.Row, bingoCardItem.Col, bingoCard);
    }

    return false;
}

bool CheckForBingo(int row, int col, BingoCard bingoCard)
{
    return bingoCard.Numbers.Count(
                bcn => bcn.Marked && bcn.Row == row) == 5 ||
            bingoCard.Numbers.Count(
                bcn => bcn.Marked && bcn.Col == col) == 5;
}

record BingoCard
{
    public List<BingoCardNumber> Numbers { get; set; }=new ();
    public bool Bingo { get; set; }
}
record BingoCardNumber(int Row, int Col, int Number)
{
    public int Row { get; set; } = Row;
    public int Col { get; set; }= Col;
    public int Number { get; set; } = Number;
    public bool Marked { get; set; }
}