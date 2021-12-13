using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.models_and_secondary.Secondary.Utils
{
    public class SimpleUtils
    {
        //КОНСТАНТЫ
        private static readonly Random RAND = new Random();
        //Начальное и конечное время рассматриваемое при разработке приложения
        public static readonly DateTime START_TIME = new DateTime(2021, 9, 1);
        public static readonly DateTime FINISH_TIME = new DateTime(2022, 1, 1);


        //МЕТОДЫ
        //1. Сгенерировать вещественное
        public static double generateFloat(double from, double to)
        {
            return from + RAND.NextDouble() * (to - from);
        }

        //2. Сгенерировать целое
        public static int generateInteger(int from, int to)
        {
            return RAND.Next(from, to);
        }

        //3. Генерация булева значения через математическое соотношение выпадания true и false
        public static bool generateBoolByRelation(int trueRel, int falseRel)
        {
            //1. Математическое распределение true и false
            var distribution = new bool[trueRel + falseRel];
            //2. Заполнить его истиной
            for (var i = 0; i < trueRel; ++i)
            {
                distribution[i] = true;
            }
            //3. Заполнить его ложью
            for (var i = 0; i < falseRel; ++i)
            {
                distribution[i] = false;
            }
            //4. Результат (булево значение из распределения)
            var result = distribution[SimpleUtils.generateInteger(0, distribution.Length)];
            //5. Ответ.
            return result;
        }

        //4. Сгенерировать дату в виде строки
        public static DateTime generateDate(DateTime a,DateTime b)
        {
            //1. Отступ между начальной и конечной датой
            //2. Случайный отступ между начальной и конечной датой
            var timeSpan = b - a;
            var randSecondOffset = SimpleUtils.generateInteger(0, (int)timeSpan.TotalSeconds);
            //3. Новая дата
            //4. Ответ
            var result = a.AddSeconds(randSecondOffset);
            return result;
        }

        //5. Случайный выбор элемента из массива
        public static T randomChoose<T>(T[] array)
        {
            return array[SimpleUtils.generateInteger(0, array.Length)];
        }

        //6. Случайное исключение (определенного процента) элементов из массива
        public static T[] randomFilter<T>(T[] array, double percent)
        {
            return array.Where(elem=>SimpleUtils.generateFloat(0,100)<percent).ToArray();
        }

        //7. Сгенерировать диапазон
        public static int[] range(int from, int to)
        {
            //Набор чисел
            var nums = new List<int>();
            //Сгенерировать для него числа
            for (int num = from; num <= to; ++num)
            {
                nums.Add(num);
            }
            //Ответ
            return nums.ToArray(); 
        }
    }
}
