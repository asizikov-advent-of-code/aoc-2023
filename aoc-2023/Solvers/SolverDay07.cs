namespace aoc_2023.Solvers;


public class SolverDay07 : Solver
{
    enum HandType
    {
        FiveOfAKind = 1000,
        FourOfAKind = 900,
        FullHouse = 800,
        ThreeOfAKind = 700,
        TwoPairs = 600,
        OnePair = 500,
        HighCard = 400
    }

    [PuzzleInput("07-01")]
    public override void Solve(string[] input)
    {
        var cardOrder = new Dictionary<char, int>
        {
            ['A'] = 1000,
            ['K'] = 900,
            ['Q'] = 800,
            ['T'] = 600,
            ['9'] = 500,
            ['8'] = 400,
            ['7'] = 300,
            ['6'] = 200,
            ['5'] = 100,
            ['4'] = 90,
            ['3'] = 80,
            ['2'] = 70,
            ['J'] = 10,

        };
        var cards = new List<(string hand, HandType type, int bid)>();
        foreach (var line in input)
        {
            var parts = line.Split(" ");
            var (hand, bid) = (parts[0], int.Parse(parts[1].Trim()));

            var counter = new Dictionary<char, int>();
            foreach (var ch in hand)
            {
                counter.TryAdd(ch, 0);
                counter[ch]++;
            }

            var handType = HandType.HighCard;
            counter.TryGetValue('J', out var jokers);

            var occurrences = counter.Where(kv => kv.Key != 'J')
                .Select(kv => kv.Value)
                .OrderByDescending(x => x)
                .ToList();

            var max = jokers;
            if (occurrences.Count > 0)
            {
                max+= occurrences[0];
            }

            handType = max switch
            {
                5 => HandType.FiveOfAKind,
                4 => HandType.FourOfAKind,
                3 when occurrences.Count > 1 && occurrences[1] == 2 => HandType.FullHouse,
                3 => HandType.ThreeOfAKind,
                2 when counter.Count == 3 => HandType.TwoPairs,
                2 => HandType.OnePair,
                _ => handType
            };

            cards.Add((hand, handType, bid));
        }

        cards.Sort((a, b) =>
        {
            if (a.type != b.type) return a.type - b.type;
            for (var i = 0; i < a.hand.Length; i++)
            {
                if (a.hand[i] != b.hand[i])
                {
                    return cardOrder[a.hand[i]] - cardOrder[b.hand[i]];
                }
            }

            return 0;
        });

        var answer = 0L;
        for (var i = 0; i < cards.Count; i++)
        {
            var (hand, type, bid) = cards[i];
            answer += bid * (i + 1);
        }
        Console.WriteLine(answer);
    }
}