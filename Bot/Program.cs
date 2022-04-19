var data = File.ReadAllLines("data.txt");
var questions = data.Select(line => line.Split("*")).Select(arr => (arr[0], arr[1])).ToList();

var random = new Random();
while (true)
{
    var ask = questions[random.Next(questions.Count)];
    Console.WriteLine(ask.Item1);

    int count = 0;
    string answer = string.Empty, helpWord = string.Empty;
    for (int i = 0; i < ask.Item2.Length; i++)
    {
        helpWord += "_";
    }

    while (count < ask.Item2.Length)
    {
        helpWord = Help(ask.Item2, helpWord);
        Console.WriteLine(helpWord);
        answer = Console.ReadLine();
        if (answer.ToLower() == ask.Item2)
        {
            Console.WriteLine("Верный ответ!");
            break;
        }
        else
        {
            Console.WriteLine("Неверный ответ!");
        }

        count++;
    }
}

static string Help(string word, string helpWord)
{
    int size = word.Length;
    var random = new Random();
    int x = helpWord.Length;
    while (true)
    {
        int randValue = random.Next(size);
        if (word[randValue] != helpWord[randValue])
        {
            helpWord = helpWord.Insert(randValue, word[randValue].ToString());
            helpWord = helpWord.Remove(randValue+1, 1);
            break;
        }
    }
    return helpWord;
}