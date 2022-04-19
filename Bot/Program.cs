

var data = File.ReadAllLines("data.txt");

var questions = data.Select(line => line.Split("*")).Select(arr => (arr[0], arr[1])).ToList();
Console.WriteLine();