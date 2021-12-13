using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.models_and_secondary.Secondary.Utils
{
    class TextUtils
    {
        //КОНСТАНТЫ
        private static readonly Random RAND = new Random();

        //СТАТИЧЕСКИЕ ПОЛЯ
        //1. Префиксы русских слов
        //2. Корни русских слов
        //3. Окончания русских слов
        private static string[] _prefixes = TextUtils.initPrefixes();
        private static string[] _roots = TextUtils.initRoots();
        private static string[] _ends = TextUtils.initEnds();

        //МЕТОДЫ
        //1. Сгенерировать наименование
        public static string generateWord()
        {
            //1. Случайный выбор префикса, корня и окончания некоего наименования
            var prefixIndex = (int)Math.Floor(RAND.NextDouble() * TextUtils._prefixes.Length);
            var rootIndex = (int)Math.Floor(RAND.NextDouble() * TextUtils._roots.Length);
            var endingIndex = (int)Math.Floor(RAND.NextDouble() * TextUtils._ends.Length);
            //2. Складывание из них слова
            return $"{ TextUtils._prefixes[prefixIndex]}{ TextUtils._roots[rootIndex]}{ TextUtils._ends[endingIndex]}";
        }
        //2. Сгенерировать список наименований
        public static string[] generateWordList(int count)
        {
            //1. Наименования
            var names = new List<string>();
            if (count > 0)
            {
                //2. Генерируем их
                for (int i = 0; i < count; ++i)
                    names.Add(TextUtils.generateWord());
            }
            //3. Ответ
            return names.ToArray();
        }

        //3. Сгенерировать текст (например полное наименование товара или его описание)
        public static string generateText(int minCount, int maxCount)
        {
            //1. Сгенерировать размер массива
            var wordCount = SimpleUtils.generateInteger(minCount, maxCount);
            //2. Сам массив
            var text = TextUtils.generateWordList(wordCount);
            //3. Склеиваем слова и возвращаем
            return string.Join(" ", text);
        }

        //4. Сгенерировать символ кириллицы
        public static string generateCyrillicSymbol()
        {
            return generateSymbols(1, "абвгдеёжзийклмнопрстуфхцчшщьыъэюя");

        }

        //4. Сгенерировать N символов кириллицы
        public static string generateCyrillicSymbols(int count)
        {
            return generateSymbols(count, "абвгдеёжзийклмнопрстуфхцчшщьыъэюя");
        }

        //5. Сгенерировать символ латиницы
        public static string generateLatinSymbol()
        {
            return generateSymbols(1, "qwertyuiopasdfhgjklzxcvbnm");
        }

        //4. Сгенерировать N символов латиницы
        public static string generateLatinSymbols(int count)
        {
            return generateSymbols(count, "qwertyuiopasdfhgjklzxcvbnm");
        }

        //5. Сгенерировать последовательность из N символов из указанной выборки
        public static string generateSymbols(int count, string baseChars)
        {
            //1. Хранилище ответа
            var res = new List<char>();
            //2. Генерируем целевой набор символов
            for (int i = 0; i < count; ++i)
                res.Add(baseChars[SimpleUtils.generateInteger(0, baseChars.Length)]);
            //3. Ответ
            return String.Join("",res);
        }

        //МЕТОДЫ ИНИЦИАЛИЗАЦИИ ПОЛЕЙ
        //1. Создание массива префиксов
        private static string[] initPrefixes()
        {
            return new string[] {
              "агро", "амфи", "ангио", "анти", "апо", "архи", "астро",
              "био", "бласто", "брахи", "брахио",
              "гастро", "гелио", "гемо", "гемато", "гео", "гипо",
              "дерма", "дендро", "ди", "диа", "дис",
              "изо",
              "ката", "крио",
              "ларинго", "лито",
              "макро", "мезо", "мело", "мета", "мело", "мио",
              "невро", "нейро", "нео",
              "олиго", "орто", "остео", "ото", "офтальмо",
              "палео", "пара", "пери", "поли", "про", "прото",
              "сейсмо", "син", "си", "сиг", "сил", "сим", "син", "сферо", "стерео",
              "теле", "телео", "термо",
              "фило", "фито",
              "хемо", "хромо", "хроно",
              "цикло", "цито",
              "эври", "экзо", "экто", "эндо", "эн",
              "эг", "эл", "эм", "эп", "экс", "эпи", "эу",
            };
        }

        //2. Создание массива корней
        private static string[] initRoots()
        {
            return new string[] {
              "бег", "бел", "бор", "бук", "важ", "вал", "век", "вен", "век", "вет", "вер",
              "вод", "вык", "год", "губ", "дар", "дев", "дел", "ден", "дет", "дом", "дух",
              "зел", "зен", "кос", "лез", "лек", "леп", "лес", "лех", "мел", "мен", "мех",
              "мыс", "неб", "нов", "ног", "нос", "ноч", "пап", "рад", "раз", "ров", "род",
              "рок", "рот", "рух", "ряд", "сел", "сор", "теч", "тер", "ток", "хот", "боров",
              "творог", "горох", "доров", "дорог", "корон", "норов", "хорош", "колод",
              "полон", "полос", "солов", "солом", "берег", "берез", "дерев", "жереб",
              "серед", "череш", "тереб", "черем", "черед", "блест", "брав", "брес", "бров",
              "влаг", "внук", "вращ", "слав", "слад", "слов",
            };
        }

        //3. Окончания русских слов
        private static string[] initEnds()
        {
            return new string[] {
              "а"
            };
        }
    }
}
