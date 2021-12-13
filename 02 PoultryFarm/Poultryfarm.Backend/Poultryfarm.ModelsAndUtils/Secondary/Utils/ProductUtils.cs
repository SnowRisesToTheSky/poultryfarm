using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.models_and_secondary.Secondary.Utils
{

    public static class ProductUtils
    {
        //КОНСТАНТЫ
        //1. Генератор случ. значений
        //2. Символы артикула
        private static readonly Random RAND = new Random();
        private static readonly string articleSyms = "АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЭЮЯ";
        //3. Наименования фото товаров
        public static string[] prodFotos = new string[] {
              "66829c6b872e4c52cea668b3aaba1e4c.jpg",
              "19488496-858678297615017-4679972512682369455-o.jpg",
              "0611201810_0be5270862116.jpg",
              "269640994445f71233e889771e2ac26b.jpg",
              "c8f2e5b90c9a80dc59b074624cb16ac0-2.jpg"
        };

        //МЕТОДЫ
        //1. Сгенерировать штрихкод
        public static string generateBarcode()
        {
            return $"{SimpleUtils.generateInteger(0, 999):D3}{SimpleUtils.generateInteger(0, 99999):D5}{SimpleUtils.generateInteger(0, 99999):D5}";
        }
        //2. Сгенерировать артикул
        public static string generateArticle(int symNumber)
        {
            //1. Набор символов артикла
            var syms = ProductUtils.articleSyms;
            //2. Будующие символы арткула. Заполняем их пустыми значениями
            var articleSyms1 = new char[symNumber];
            articleSyms1.Initialize();
            //3. Генерируем артикл
            var articleSyms2 = articleSyms1.Select(sym => syms[SimpleUtils.generateInteger(0, syms.Length)]);
            //4. Склеиваем символы в строку
            var article = string.Join("", articleSyms2);
            //5. Ответ
            return article;
        }


        //3. Сгенерировать уровень вложенности группы с некоторым мат. распределением
        public static int generateGroupLevel(params int[] lvlSizeRel)
        {
            //1. Общая сумма значений соотношения групп
            //2. Генерируем число в рамках этого значения
            int groupRelationSize = lvlSizeRel.Aggregate(0, (a, b) => a + b);
            int randRelationNumber = SimpleUtils.generateInteger(0, groupRelationSize);
            //3. Перебираем значения соотношений групп
            for (int i = 0, sum = 0; i < lvlSizeRel.Length; ++i)
            {
                //1. Суммируем их постепенно
                sum += lvlSizeRel[i];
                //2. Если сгенерированное число (позиция в этих соотношениях)
                //находится в рамках очередного значения соотношений
                if (randRelationNumber < sum)
                    //То возвращаем индекс значения
                    return i;
            }
            //4. Иначе ошибка
            return -1;
        }

        //4. Сгенерировать фото
        public static string generateProdFoto()
        {
            return $"/assets/products/{ProductUtils.prodFotos[SimpleUtils.generateInteger(0, ProductUtils.prodFotos.Length-1)]}";
        }

    }
}
