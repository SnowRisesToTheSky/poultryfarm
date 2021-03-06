using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sidonia.models_and_secondary.Secondary.Utils
{
    class PersonUtils
    {
        //1. Сгенерировать фамилию
        public static string generateSurname()
        {
            return PersonUtils.surnames[SimpleUtils.generateInteger(0, PersonUtils.surnames.Length - 1)];
        }

        //2. Сгенерировать имя
        public static string generateName()
        {
            return PersonUtils.names[SimpleUtils.generateInteger(0, PersonUtils.names.Length - 1)];
        }

        //3. Сгенерировать отчество
        public static string generatePatronymic()
        {
            return PersonUtils.patronymics[SimpleUtils.generateInteger(0, PersonUtils.patronymics.Length - 1)];
        }

        //4. Сгенерировать ФИО
        public static string generateSurnameNP()
        {
            return PersonUtils.generateSurname() + " " + PersonUtils.generateName()[0] + ". " + PersonUtils.generatePatronymic()[0] + ".";
        }

        //5. Сгенерировать номер телефона
        public static string generatePhoneNumber()
        {
            //1. Все имеющиеся цифры
            var digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            //2. Будующие цифры номера
            var phoneNumberSyms = new char[7];
            phoneNumberSyms.Initialize();
            //3. Сгенерировать цифры номера
            phoneNumberSyms.Select(sym => digits[SimpleUtils.generateInteger(0, digits.Length)]);
            //4. Склеить в строку
            var phoneNumber = "+38 (071) " + string.Join("", phoneNumberSyms);
            //3. Ответ
            return phoneNumber;
        }

        //Сгенерировать диапазон номеров
        public static string[] generatePhoneNumberRange(int count)
        {
            //Набор номеров
            List<string> numbers = new List<string>();
            //Сгенерировать их
            for (var i = 0; i < count; ++i)
            {
                var number = Regex.Replace($"{i:D7}", @"(\d{3})(\d{2})(\d{2})", "+38 (071) $1-$2-$3");
                numbers.Add(number);
            }
            //Ответ
            return numbers.ToArray();
        }

        //Сгенерировать почтовые адреса
        public static string[] generateEmails(int count)
        {
            //Размер той части почты, что д.б. уникальной
            //Резмер части почты провайдера почты
            const int EMAIL_SIZE = 15;
            const int PROVIDER_NAME_SIZE = 5;
            //Символы почты
            //Будущие адреса
            string syms = "qwertyuiopasdfghjklzxcvbnm";
            var emails = new List<string>();
            //Генерируем адреса
            for (var k = 0; k < count; ++k)
            {
                string email = "";
                //Генерируем уникальную часть
                for (var i = 0; i < EMAIL_SIZE; ++i)
                {
                    email += syms[SimpleUtils.generateInteger(0, syms.Length - 1)];
                }
                email += "@";
                //Генерируем часть провайдера
                for (var i = 0; i < PROVIDER_NAME_SIZE; ++i)
                {
                    email += syms[SimpleUtils.generateInteger(0, syms.Length - 1)];
                }
                //Домен
                email += ".ru";
                //Запоминаем
                emails.Add(email);
            }
            //Ответ
            return emails.ToArray();
        }

        //6. Сгенерировать пароль
        public static string generateInn()
        {
            return $"{SimpleUtils.generateInteger(0, 9999):D4}{SimpleUtils.generateInteger(0, 9999):D4}{SimpleUtils.generateInteger(0, 9999):D4}";
        }

        //6. Сгенерировать пароль
        public static string generatePassword()
        {
            return $"{SimpleUtils.generateInteger(0, 9999):D4}{SimpleUtils.generateInteger(0, 9999):D4}";
        }

        //6. Сгенерировать серию/номер паспорта
        public static string generatePassport()
        {
            //1. Сгенерировать 4 символа из набора
            var start = TextUtils.generateSymbols(4, "ETYOPAHKXCBM");
            //2. Добавить цифры
            var res = $"{start}{SimpleUtils.generateInteger(0, 9999):D4}{SimpleUtils.generateInteger(0, 9999):D4}";
            //3. Ответ
            return res;
        }

        //МАССИВЫ ДЛЯ ГЕНЕРАЦИИ
        //1. Массив фамилий
        private static readonly string[] surnames = new string[] {
          "Андрусейко", "Гущин", "Корнейчук", "Князев", "Кононов",
          "Кабанов", "Лапин", "Кондратьев", "Кудрявцев", "Пахомов",
          "Палий", "Щукин", "Овчинников", "Мамонтов", "Кузьмин",
          "Смирнов", "Иванов", "Кузнецов", "Соколов", "Попов", "Лебедев",
          "Козлов", "Новиков", "Морозов", "Петров", "Волков", "Соловьёв",
          "Васильев", "Зайцев", "Павлов", "Семёнов", "Голубев", "Виноградов",
          "Богданов", "Воробьёв", "Фёдоров", "Михайлов", "Беляев", "Тарасов",
          "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Белов",
          "Петров", "Соколов", "Михайлов", "Новиков", "Федоров",
          "Морозов", "Волков", "Алексеев", "Лебедев", "Семенов",
          "Егоров", "Павлов", "Козлов", "Степанов", "Николаев",
          "Орлов", "Андреев", "Макаров", "Никитин", "Захаров",
          "Зайцев", "Соловьев", "Борисов", "Яковлев", "Григорьев",
          "Романов", "Воробьев", "Сергеев", "Кузьмин", "Фролов",
          "Александров", "Дмитриев", "Королев", "Гусев", "Киселев",
          "Ильин", "Максимов", "Поляков", "Сорокин", "Виноградов",
          "Ковалев", "Белов", "Медведев", "Антонов", "Тарасов",
          "Жуков", "Баранов", "Филиппов", "Комаров", "Давыдов",
          "Беляев", "Герасимов", "Богданов", "Осипов", "Сидоров",
          "Матвеев", "Титов", "Марков", "Миронов", "Крылов",
          "Куликов", "Карпов", "Власов", "Мельников", "Денисов",
          "Гаврилов", "Тихонов", "Казаков", "Афанасьев", "Данилов",
          "Савельев", "Тимофеев", "Фомин", "Чернов", "Абрамов",
          "Мартынов", "Ефимов", "Федотов", "Щербаков", "Назаров",
          "Калинин", "Исаев", "Чернышев", "Быков", "Маслов",
          "Родионов", "Коновалов", "Лазарев", "Воронин", "Климов",
          "Филатов", "Пономарев", "Голубев", "Кудрявцев", "Прохоров",
          "Наумов", "Потапов", "Журавлев", "Овчинников", "Трофимов",
          "Леонов", "Соболев", "Ермаков", "Колесников", "Гончаров",
          "Емельянов", "Никифоров", "Грачев", "Котов", "Гришин",
          "Ефремов", "Архипов", "Громов", "Кириллов", "Малышев",
          "Панов", "Моисеев", "Румянцев", "Акимов", "Кондратьев",
          "Бирюков", "Горбунов", "Анисимов", "Еремин", "Тихомиров",
          "Галкин", "Лукьянов", "Михеев", "Скворцов", "Юдин",
          "Белоусов", "Нестеров", "Симонов", "Прокофьев", "Харитонов",
          "Князев", "Цветков", "Левин", "Митрофанов", "Воронов",
          "Аксенов", "Софронов", "Мальцев", "Логинов", "Горшков",
          "Савин", "Краснов", "Майоров", "Демидов", "Елисеев",
          "Рыбаков", "Сафонов", "Плотников", "Демин", "",
          "Фадеев", "Молчанов", "Игнатов", "Литвинов", "Ершов",
          "Ушаков", "Дементьев", "Рябов", "Мухин", "Калашников",
          "Леонтьев", "Лобанов", "Кузин", "Корнеев", "Евдокимов",
          "Бородин", "Платонов", "Некрасов", "Балашов", "Бобров",
          "Жданов", "Блинов", "Игнатьев", "Коротков", "Муравьев",
          "Крюков", "Беляков", "Богомолов", "Дроздов", "Лавров",
          "Зуев", "Петухов", "Ларин", "Никулин", "Серов",
          "Терентьев", "Зотов", "Устинов", "Фокин", "Самойлов",
          "Константинов", "Сахаров", "Шишкин", "Самсонов", "Черкасов",
          "Чистяков", "Носов", "Спиридонов", "Карасев", "Авдеев",
          "Воронцов", "Зверев", "Владимиров", "Селезнев", "Нечаев",
          "Кудряшов", "Седов", "Фирсов", "Андрианов", "Панин",
          "Головин", "Терехов", "Ульянов", "Шестаков", "Агеев",
          "Никонов", "Селиванов", "Баженов", "Гордеев", "Кожевников",
          "Пахомов", "Зимин", "Костин", "Широков", "Филимонов",
          "Ларионов", "Овсянников", "Сазонов", "Суворов", "Нефедов",
          "Корнилов", "Любимов", "Львов", "Горбачев", "Копылов",
          "Лукин", "Токарев", "Кулешов", "Шилов", "Большаков",
          "Панкратов", "Родин", "Шаповалов", "Покровский", "Бочаров",
          "Никольский", "Маркин", "Горелов", "Агафонов", "Березин",
          "Ермолаев", "Зубков", "Куприянов", "Трифонов", "Масленников",
          "Круглов", "Третьяков", "Колосов", "Рожков", "Артамонов",
          "Шмелев", "Лаптев", "Лапшин", "Федосеев", "Зиновьев",
          "Зорин", "Уткин", "Столяров", "Зубов", "Ткачев",
          "Дорофеев", "Антипов", "Завьялов", "Свиридов", "Золотарев",
          "Кулаков", "Мещеряков", "Макеев", "Дьяконов", "Гуляев",
          "Петровский", "Бондарев", "Поздняков", "Панфилов", "Кочетков",
          "Суханов", "Рыжов", "Старостин", "Калмыков", "Колесов",
          "Золотов", "Кравцов", "Субботин", "Шубин", "Щукин",
          "Лосев", "Винокуров", "Лапин", "Парфенов", "Исаков",
          "Голованов", "Коровин", "Розанов", "Артемов", "Козырев",
          "Русаков", "Алешин", "Крючков", "Булгаков", "Кошелев",
          "Сычев", "Синицын", "Черных", "Рогов", "Кононов",
          "Лаврентьев", "Евсеев", "Пименов", "Пантелеев", "Горячев",
          "Аникин", "Лопатин", "Рудаков", "Одинцов", "Серебряков",
          "Панков", "Дегтярев", "Орехов", "Царев", "Шувалов",
          "Кондрашов", "Горюнов", "Дубровин", "Голиков", "Курочкин",
          "Латышев", "Севастьянов", "Вавилов", "Ерофеев", "Сальников",
          "Клюев", "Носков", "Озеров", "Кольцов", "Комиссаров",
          "Меркулов", "Киреев", "Хомяков", "Булатов", "Ананьев",
          "Буров", "Шапошников", "Дружинин", "Островский", "Шевелев",
          "Долгов", "Суслов", "Шевцов", "Пастухов", "Рубцов",
          "Бычков", "Глебов", "Ильинский", "Успенский", "Дьяков",
          "Кочетов", "Вишневский", "Высоцкий", "Глухов", "Дубов",
          "Бессонов", "Ситников", "Астафьев", "Мешков", "Шаров",
          "Яшин", "Козловский", "Туманов", "Басов", "Корчагин",
          "Болдырев", "Олейников", "Чумаков", "Фомичев", "Губанов",
          "Дубинин", "Шульгин", "Касаткин", "Пирогов", "Семин",
          "Трошин", "Горохов", "Стариков", "Щеглов", "Фетисов",
          "Колпаков", "Чесноков", "Зыков", "Верещагин", "Минаев",
          "Руднев", "Троицкий", "Окулов", "Ширяев", "Малинин",
          "Черепанов", "Измайлов", "Алехин", "Зеленин", "Касьянов",
          "Пугачев", "Павловский", "Чижов", "Кондратов", "Воронков",
          "Капустин", "Сотников", "Демьянов", "Косарев", "Беликов",
          "Сухарев", "Белкин", "Беспалов", "Кулагин", "Савицкий",
          "Жаров", "Хромов", "Еремеев", "Карташов", "Астахов",
          "Русанов", "Сухов", "Вешняков", "Волошин", "Козин",
          "Худяков", "Жилин", "Малахов", "Сизов", "Ежов",
          "Толкачев", "Анохин", "Вдовин", "Бабушкин", "Усов",
          "Лыков", "Горлов", "Коршунов", "Маркелов", "Постников",
          "Черный", "Дорохов", "Свешников", "Гущин", "Калугин",
          "Блохин", "Сурков", "Кочергин", "Греков", "Казанцев",
          "Швецов", "Ермилов", "Парамонов", "Агапов", "Минин",
          "Корнев", "Черняев", "Гуров", "Ермолов", "Сомов",
          "Добрынин", "Барсуков", "Глушков", "Чеботарев", "Москвин",
          "Уваров", "Безруков", "Муратов", "Раков", "Снегирев",
          "Гладков", "Злобин", "Моргунов", "Поликарпов", "Рябинин",
          "Судаков", "Кукушкин", "Калачев", "Грибов", "Елизаров",
          "Звягинцев", "Корольков", "Федосов"
        };

        //2. Массив имен
        private static string[] names = new string[] {
    "Жерар", "Никодим", "Казбек", "Осип", "Назар",
    "Спартак", "Донат", "Харитон", "Лука", "Гавриил",
    "Елисей", "Жигер", "Милан", "Геннадий", "Яромир",
    "Алан", "Александр", "Алексей", "Альберт", "Анатолий",
    "Андрей", "Антон", "Арсен", "Арсений", "Артем", "Артемий", "Артур", "Богдан", "Борис", "Вадим",
    "Валентин", "Валерий", "Василий", "Виктор", "Виталий", "Владимир", "Владислав", "Всеволод", "Вячеслав",
    "Геннадий", "Георгий", "Герман", "Глеб", "Гордей", "Григорий", "Давид", "Дамир", "Даниил", "Демид",
    "Демьян", "Денис", "Дмитрий", "Евгений", "Егор", "Елисей", "Захар", "Иван", "Игнат", "Игорь", "Илья",
    "Ильяс", "Камиль", "Карим", "Кирилл", "Клим", "Константин", "Лев", "Леонид", "Макар", "Максим", "Марат",
    "Марк", "Марсель", "Матвей", "Мирон", "Мирослав", "Михаил", "Назар", "Никита", "Николай", "Олег",
    "Павел", "Петр", "Платон", "Прохор", "Рамиль", "Ратмир", "Ринат", "Роберт", "Родион", "Роман",
    "Ростислав", "Руслан", "Рустам", "Савва", "Савелий", "Святослав", "Семен", "Сергей", "Станислав",
    "Степан", "Тамерлан", "Тимофей", "Тимур", "Тихон", "Федор", "Филипп", "Шамиль", "Эдуард", "Эльдар",
    "Эмиль", "Эрик", "Юрий", "Ян", "Ярослав"
  };

        //3. Массив отчеств
        private static string[] patronymics = new string[] {
    "Владимирович", "Фёдорович", "Максимович", "Богданович", "Васильевич",
    "Борисович", "Максимович", "Станиславович", "Романович", "Петрович",
    "Алексеевич", "Григорьевич", "Данилович", "Брониславович", "Юхимович"
  };
    }
}
