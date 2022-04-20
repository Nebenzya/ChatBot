using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
    public static class QuestionData
    {
        static private List<(string,string)> _questions = File.ReadAllLines("data.txt").Select(line => line.Split("*")).Select(list => (list[0], list[1])).ToList();
        static private Random _random = new Random();
        static private string _currentQuestion = string.Empty;
        static private string _currentAnswer = string.Empty;
        static private string _hint = string.Empty;

        static public bool ToStart => _currentQuestion == string.Empty;


        public static string GetQuestion()
        {
            int randValue = _random.Next(_questions.Count);
            _currentQuestion = _questions[randValue].Item1;
            _currentAnswer = _questions[randValue].Item2;
            return _currentQuestion;
        }

        public static string GetHint()
        {
            int size = _currentAnswer.Length;

            if (_hint == string.Empty)
            {
                CreateHint(size);
            }

            while (true)
            {
                int randValue = _random.Next(size);
                if (_currentAnswer[randValue] != _hint[randValue])
                {
                    // помещает одну букву в подсказку
                    _hint = _hint.Insert(randValue, _currentAnswer[randValue].ToString());
                    // удаляет пустой символ, который заместила новая буква
                    _hint = _hint.Remove(randValue + 1, 1);
                    break;
                }
            }

            return _hint;
        }

        private static void CreateHint(int size)
        {
            for (int i = 0; i < size; i++)
            {
                _hint += "*";
            }
        }
    }
}
