using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
    public static class QuestionData
    {
        private static readonly List<(string,string)> _questions = File.ReadAllLines("data.txt").Select(line => line.Split("*")).Select(list => (list[0], list[1])).ToList();
        private static readonly Random _random = new();
        static private string _currentQuestion = string.Empty;
        static private string _currentAnswer = string.Empty;
        static private string _hint = string.Empty;

        /// <summary>Проверяет текущее состояние вопроса. Если вопрос не был задан, то задаёт случайно выбранный вопрос.
        /// Если вопрос уже задан, проверяет верен ли ответ</summary>
        static public string GoTo(string message)
        {
            string answer;
            if (_currentQuestion == string.Empty) // если вопрос не был задан
            {
                answer = GetQuestion();
            }
            else if (message == _currentAnswer) // если дан верный ответ на вопрос
            {
                answer = $"{_currentAnswer} - верный ответ!";
                ResetQuestion();
            }
            else // иначе даём подсказку на вопрос
            {
                if (_hint != _currentAnswer)
                    answer = GetHint();
                else
                {
                    answer = $"Никто не дал верный ответ!";
                    ResetQuestion();
                }
            }

            return answer;
        }

        /// <summary> Создаёт новую строку в которую будут добавляться подсказки.</summary>
        private static void CreateHint(int size)
        {
            for (int i = 0; i < size; i++)
            {
                _hint += "*";
            }
        }

        /// <returns>Актуальная на текущем этапе подсказка</returns>
        private static string GetHint()
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
                    _hint = _hint.Insert(randValue, _currentAnswer[randValue].ToString()); // помещает одну букву в подсказку
                    _hint = _hint.Remove(randValue + 1, 1); // удаляет лишний символ
                    break;
                }
            }

            return _hint;
        }

        /// <summary> Выбирает из базы случайный вопрос/ответ</summary>
        /// <returns>Вопрос</returns>
        private static string GetQuestion()
        {
            int randValue = _random.Next(_questions.Count);
            _currentQuestion = _questions[randValue].Item1;
            _currentAnswer = _questions[randValue].Item2;
            return _currentQuestion;
        }

        /// <summary> Сбрасывает всю информацию о текущем вопросе </summary>
        private static void ResetQuestion()
        {
            _currentQuestion = string.Empty;
            _currentAnswer = string.Empty;
            _hint = string.Empty;
        }
    }
}
