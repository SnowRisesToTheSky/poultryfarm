using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using Poultryfarm.Backend;
using Poultryfarm.ModelsAndUtils.Http.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Poultryfarm.Tests.Controllers
{
    public class Workers
    {
        //ПОЛЯ
        private HttpClient _client { get; set; }

        //ПОДГОТОВКА К ТЕСТАМ
        //Перед всеми тестами
        [OneTimeSetUp]
        public void Setup()
        {
            //1. Дескриптор тестового сервера
            var server = new TestServer(new WebHostBuilder().
                UseEnvironment("Development")
                .UseStartup<Startup>());
            //2. Дескриптор для отправки запросов на тестовый сервер
            _client = server.CreateClient();
        }

        //ОКОНЧАНИЕ ТЕСТОВ
        //После всех тестов
        [OneTimeTearDown]
        public void Teardown()
        {
        }

        //ТЕСТЫ
        //Получить число торговых марок
        [Test, Order(1)]
        public async Task WorkersGetAll_Success()
        {
            //ARRANGE
            //1. Действие контролллера которое вызываем
            var action = $"get-all";

            //2. Нижняя граница значений ид
            //3. Нижняя граница кол-ва записей
            var minIdValueBound = 0;
            var minListLengthBound = 0;

            //ACT
            //3. Получаем ответ на запрос
            //4. Получаем тело ответа
            var response = await _client.GetAsync($"api/workers/{action}");
            var body = await response.Content.ReadFromJsonAsync<List<ClientWorker>>();


            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не нулл
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Http-ответ д.б. вернуть ОК.");
            Assert.IsNotNull(body, "Тело ответа не д.б. со значением нул.");

            //3. Список ответа не пуст
            //4. Список ответа содержит более указанного значения
            Assert.IsNotEmpty(body, "Список ответа не д.б. пустым.");
            Assert.Greater(body.Count, minListLengthBound, $"Список ответ должен содержать более {minListLengthBound} элементов.");


            //5. Ид д.б. больше нуля
            //6. Имя не д.б. пустым
            Assert.Greater(body[0].id,minIdValueBound, $"Ид элемента д.б. больше {minIdValueBound}");
            Assert.IsNotEmpty(body[0].name, "Имя рабочего не д.б. пустым.");
        }

        //Выборка партии рабочих
        [Test, Order(2)]
        public async Task WorkersGetAllByChunk_Success()
        {
            //ARRANGE
            //1. Действие контролллера которое вызываем
            string action = "get-all/by-chunk";
            //2. Индекс искомой партии
            //3. Размер партий при разбивке
            var curChunkIndex = 0;
            var chunkSize = 5;
            //4. Нижняя граница значений ид
            var minIdValueBound = 0;


            //ACT
            //3. Получаем ответ на запрос
            //4. Получаем тело ответа
            var response = await _client.GetAsync($"api/workers/{action}?curChunkIndex={curChunkIndex}&chunkSize={chunkSize}");
            var body = await response.Content.ReadFromJsonAsync<List<ClientWorker>>();

            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не нулл
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Http-ответ д.б. вернуть ОК.");
            Assert.IsNotNull(body, "Тело ответа не д.б. со значением нул.");

            //3. Список ответа не пуст
            //4. Список ответа содержит более указанного значения
            Assert.IsNotEmpty(body, "Список ответа не д.б. пустым.");
            Assert.AreEqual(body.Count, chunkSize, $"Список ответ должен содержать {chunkSize} элементов.");


            //5. Ид д.б. больше нуля
            //6. Имя не д.б. пустым
            Assert.Greater(body[0].id, minIdValueBound, $"Ид элемента д.б. больше {minIdValueBound}");
            Assert.IsNotEmpty(body[0].name, "Имя рабочего не д.б. пустым.");
        }

        //Получить кол-во партий рабочих
        [Test, Order(3)]
        public async Task WorkersGetChunkCountFromAll_Success()
        {
            //ARRANGE
            //1. Действие контролллера которое вызываем
            string action = "get-chunk-count/from-all";
            //2. Размер партий при разбивке
            //3. Нижняя граница кол-ва партий
            var chunkSize = 5;
            var chunkCountMinBound = 1;


            //ACT
            //1. Получаем ответ на запрос
            //2. Получаем тело ответа
            var response = await _client.GetAsync($"api/workers/{action}?chunkSize={chunkSize}");
            var body = await response.Content.ReadFromJsonAsync<int?>();

            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не нулл
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Http-ответ д.б. вернуть ОК.");
            Assert.IsNotNull(body, "Тело ответа не д.б. со значением нул.");

            //3. Проверка кол-ва партий
            Assert.Greater(body, chunkCountMinBound, $"Кол-во партий ожидалось больше чем {chunkCountMinBound}.");
        }

        //Добавить одного рабочего
        [Test, Order(4)]
        public async Task WorkersAddOne_Success()
        {
            //ARRANGE
            //1. Действие контролллера которое вызываем
            //2. Ид д.б. больше чем
            //3. Фамилия добавляемого рабочего
            //4. Добавляемый рабочий
            var action = $"add-one";
            var greaterThan = 0;
            var salary = 4001;
            var worker = new ClientWorker
            {
                surname = "Васенев",
                name = "Аркадий",
                patronymic = "Петрович",
                passport="5465IURHJFNG",
                salary= salary
            };


            //ACT
            //1. Сериализуем отправляемые данные
            //2. Делаем запрос - получаем ответ
            //3. Получаем тело ответа
            var stringContent = new StringContent(JsonConvert.SerializeObject(worker), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"api/workers/{action}", stringContent);
            var body = await response.Content.ReadFromJsonAsync<ClientWorker>();

            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не д.б. нулл
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Http-ответ вернул не успешный StatusCode.");
            Assert.IsNotNull(body, "Тело ответа не д.б. со значением нул.");

            //3. Ид не д.б. нулл
            //4. Ид д.б. больше нуля
            //5. Имя не д.б. пустым
            //6. Проверка соответствия зарплат
            Assert.IsNotNull(body.id, "Ид не д.б. нулл");
            Assert.Greater(body.id, greaterThan, $"Id д.б. больше {greaterThan}. Это не соблюдается.");
            Assert.IsNotEmpty(body.surname, "Фамилия не д.б. пуста!");
            Assert.AreEqual(body.salary, salary, "Ожидалось другое значение зарплаты.");
        }

        //Обновить одного рабочего
        [Test, Order(5)]
        public async Task WorkersUpdateOne_Success()
        {
            //ARRANGE
            //1. Действие контролллера которое вызываем
            //2. Фамилия для обновления
            //3. Зарплата для обновления
            var action = $"update-one";
            var surname = "Васенев";
            var salary = 7002;

            //4. Получить всех актуальных рабочих
            var response1 = await _client.GetAsync($"api/workers/get-all");
            var allWorkers = await response1.Content.ReadFromJsonAsync<List<ClientWorker>>();

            //5. Выбираем первого для обновления
            var worker = allWorkers.FirstOrDefault();

            //6. Изменяем его
            worker.surname = surname;
            worker.salary = salary;
            //7. Запоминаем ид
            var workerId = worker.id;


            //ACT
            //1. Сериализуем отправляемые данные
            //2. Делаем запрос - получаем ответ
            //3. Получаем тело ответа
            var stringContent = new StringContent(JsonConvert.SerializeObject(worker), Encoding.UTF8, "application/json");
            var response2 = await _client.PutAsync($"api/workers/{action}", stringContent);
            var body2 = await response2.Content.ReadFromJsonAsync<ClientWorker>();
            
            //ACT
            //1. Делаем запрос - получаем ответ
            //2. Получаем тело ответа
            var response3 = await _client.GetAsync($"api/workers/get-one?id={workerId}");
            var body3 = await response3.Content.ReadFromJsonAsync<ClientWorker>();

            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не д.б. нулл
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode, "Http-ответ вернул не успешный StatusCode.");
            Assert.IsNotNull(body2, "Тело ответа не д.б. со значением нул.");
            Assert.IsNotNull(body3, "Тело ответа не д.б. со значением нул.");

            //5. Проверка соответствия ид
            //6. Проверка соответствия зарплат
            Assert.AreEqual(body3.id, workerId, "Ожидалось другое значение ид.");
            Assert.AreEqual(body3.salary, salary, "Ожидалось другое значение зарплаты.");
        }


        //Удалить одного рабочего
        [Test, Order(6)]
        public async Task WorkersDeleteOne_Success()
        {
            //ARRANGE
            //1. Действие контролллера которое вызываем
            var action = $"delete-one";

            //2. Получить всех актуальных рабочих
            var response1 = await _client.GetAsync($"api/workers/get-all");
            var allWorkers = await response1.Content.ReadFromJsonAsync<List<ClientWorker>>();

            //3. Выбираем первого для удаления
            var worker = allWorkers.FirstOrDefault();


            //ACT
            //1. Делаем запрос - получаем ответ
            //2. Получаем тело ответа
            var response2 = await _client.DeleteAsync($"api/workers/{action}?id={worker.id}");
            var body = await response2.Content.ReadFromJsonAsync<ClientWorker>();

            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не д.б. нулл
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode, "Http-ответ вернул не успешный StatusCode.");
            Assert.IsNotNull(body, "Тело ответа не д.б. со значением нул.");

            //5. Проверка соответствия ид
            //6. Проверка что рабочий удален
            Assert.AreEqual(body.id, worker.id, "Ожидалось другое значение ид.");
            Assert.IsTrue(body.wasDeleted, "Ожидалось что рабочий будет удален.");
        }

        //Получить одну запись
        [Test, Order(7)]
        public async Task WorkersGetOne_Success()
        {
            //ARRANGE
            //1. Действия контролллера которые вызываем
            var restoreAction = $"restore-one";
            var getOneAction = $"get-one";

            //2. Ид сущности, с которой будем работать
            var restoreId = 1;

            //3. Нижняя граница значений ид
            var minIdValueBound = 0;

            //4. Восстанавливаем сущность с которой будем работать в бд (если она была удалена)
            var response1 = await _client.DeleteAsync($"api/workers/{restoreAction}?id={restoreId}");

            //ACT
            //1. Получаем ответ на запрос
            //2. Получаем тело ответа
            var response2 = await _client.GetAsync($"api/workers/{getOneAction}?id={restoreId}");
            var body = await response2.Content.ReadFromJsonAsync<ClientWorker>();


            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не нулл
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode, "Http-ответ д.б. вернуть ОК.");
            Assert.IsNotNull(body, "Тело ответа не д.б. со значением нул.");


            //5. Ид д.б. больше нуля
            //6. Имя не д.б. пустым
            Assert.Greater(body.id, minIdValueBound, $"Ид элемента д.б. больше {minIdValueBound}");
            Assert.IsNotEmpty(body.name, "Имя рабочего не д.б. пустым.");
            Assert.IsFalse(body.wasDeleted, "Ожидалось что запись актуальна.");
        }

        //Получить одну запись в т.ч. среди удаленных
        [Test, Order(8)]
        public async Task WorkersGetOneEvenDeleted_Success()
        {
            //ARRANGE
            //1. Действия контролллера которые вызываем
            var deleteAction = $"delete-one";
            var restoreAction = $"restore-one";
            var getOneEvenDeletedAction = $"get-one/even-deleted";

            //2. Ид сущности, с которой будем работать
            var deleteOne = 1;

            //3. Нижняя граница значений ид
            var minIdValueBound = 0;

            //4. Восстанавливаем сущность с которой будем работать в бд (если она была удалена)
            var response1 = await _client.DeleteAsync($"api/workers/{deleteAction}?id={deleteOne}");

            //ACT
            //1. Получаем ответ на запрос
            //2. Получаем тело ответа
            var response2 = await _client.GetAsync($"api/workers/{getOneEvenDeletedAction}?id={deleteOne}");
            var body = await response2.Content.ReadFromJsonAsync<ClientWorker>();


            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не нулл
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode, "Http-ответ д.б. вернуть ОК.");
            Assert.IsNotNull(body, "Тело ответа не д.б. со значением нул.");


            //5. Ид д.б. больше нуля
            //6. Имя не д.б. пустым
            Assert.Greater(body.id, minIdValueBound, $"Ид элемента д.б. больше {minIdValueBound}");
            Assert.IsNotEmpty(body.name, "Имя рабочего не д.б. пустым.");
            Assert.IsTrue(body.wasDeleted, "Ожидалось что запись была удалена.");

            //END
            //Восстанавливаем сущность
            var response3 = await _client.DeleteAsync($"api/workers/{restoreAction}?id={deleteOne}");
        }

        //Получить одну запись в т.ч. среди удаленных
        [Test, Order(9)]
        public async Task WorkersRestoreOne_Success()
        {
            //ARRANGE
            //1. Действия контролллера которые вызываем
            var deleteAction = $"delete-one";
            var restoreAction = $"restore-one";
            var getOneEvenDeletedAction = $"get-one/even-deleted";

            //2. Ид сущности, с которой будем работать
            var id = 1;


            //ACT
            //1. Удаляем сущность с которой будем работать
            var response1 = await _client.DeleteAsync($"api/workers/{deleteAction}?id={id}");

            //2. Получаем ответ на запрос
            //3. Получаем тело ответа (Удаленная сущность)
            var response2 = await _client.GetAsync($"api/workers/{getOneEvenDeletedAction}?id={id}");
            var body2 = await response2.Content.ReadFromJsonAsync<ClientWorker>();

            //4. Восстанавливаем сущность
            var response3 = await _client.DeleteAsync($"api/workers/{restoreAction}?id={id}");

            //2. Получаем ответ на запрос
            //3. Получаем тело ответа (Восстановленная сущность)
            var response4 = await _client.GetAsync($"api/workers/{getOneEvenDeletedAction}?id={id}");
            var body4 = await response4.Content.ReadFromJsonAsync<ClientWorker>();

            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не нулл
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode, "Http-ответ д.б. вернуть ОК.");
            Assert.AreEqual(HttpStatusCode.OK, response4.StatusCode, "Http-ответ д.б. вернуть ОК.");
            Assert.IsNotNull(body2, "Тело ответа не д.б. со значением нул.");
            Assert.IsNotNull(body4, "Тело ответа не д.б. со значением нул.");


            //3. Проверка удаления записи
            //4. Проверка восстановления записи
            Assert.IsTrue(body2.wasDeleted, "Ожидалось что запись была удалена.");
            Assert.IsFalse(body4.wasDeleted, "Ожидалось что запись была восстановлена.");
        }

        //Получить все записи, даже удаленные
        [Test, Order(10)]
        public async Task WorkersGetAllEvenDeleted_Success()
        {
            //ARRANGE
            //1. Действие контролллера которое вызываем
            var deleteAction = $"delete-one?id=1";
            var restoreAction = $"restore-one?id=1";
            var getAllEvenDeletedAction = $"get-all/even-deleted";

            //2. Нижняя граница значений ид
            //3. Нижняя граница кол-ва записей
            var minIdValueBound = 0;
            var minListLengthBound = 0;

            //ACT
            //1. Удаляем запись
            var response1 = await _client.DeleteAsync($"api/workers/{deleteAction}");

            //2. Получаем ответ на запрос
            //3. Получаем тело ответа (вместе с удаленной записью)
            var response2 = await _client.GetAsync($"api/workers/{getAllEvenDeletedAction}");
            var body2 = await response2.Content.ReadFromJsonAsync<List<ClientWorker>>();



            //ASSERT
            //1. StatusCode - OK
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode, "Http-ответ д.б. вернуть ОК.");
            //2. Тело ответа не нулл
            Assert.IsNotNull(body2, "Тело ответа не д.б. со значением нул.");

            //3. Список ответа не пуст
            Assert.IsNotEmpty(body2, "Список ответа не д.б. пустым.");
            //4. Список ответа содержит более указанного значения
            Assert.Greater(body2.Count, minListLengthBound, $"Список ответ должен содержать более {minListLengthBound} элементов.");

            //5. Ид д.б. больше нуля
            Assert.Greater(body2[0].id, minIdValueBound, $"Ид элемента д.б. больше {minIdValueBound}");
            //6. Имя не д.б. пустым
            Assert.IsNotEmpty(body2[0].name, "Имя рабочего не д.б. пустым.");

            //7. Проверка чтения удаленных записей
            Assert.IsTrue(body2.Any(w => w.wasDeleted), "Ожидалось что хоть 1 запись будет удаленной.");

            //END
            //Восстанавливаем запись
            var response3 = await _client.DeleteAsync($"api/workers/{restoreAction}");
        }

        //Выборка партии рабочих в т.ч. среди удаленных
        [Test, Order(11)]
        public async Task WorkersGetAllByChunkEvenDeleted_Success()
        {
            //ARRANGE
            //1. Действие контролллера которое вызываем
            var deleteAction = $"delete-one?id=1";
            var restoreAction = $"restore-one?id=1";
            var action = $"get-all/by-chunk/even-deleted";
            //2. Индекс искомой партии
            //3. Размер партий при разбивке
            var curChunkIndex = 0;
            var chunkSize = 5;
            //4. Нижняя граница значений ид
            var minIdValueBound = 0;


            //ACT
            //1. Удаляем запись
            var response1 = await _client.DeleteAsync($"api/workers/{deleteAction}");

            //2. Получаем ответ на запрос
            //3. Получаем тело ответа
            var response2 = await _client.GetAsync($"api/workers/{action}?curChunkIndex={curChunkIndex}&chunkSize={chunkSize}");
            var body2 = await response2.Content.ReadFromJsonAsync<List<ClientWorker>>();

            //ASSERT
            //1. StatusCode - OK
            //2. Тело ответа не нулл
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode, "Http-ответ д.б. вернуть ОК.");
            Assert.IsNotNull(body2, "Тело ответа не д.б. со значением нул.");

            //3. Список ответа не пуст
            //4. Список ответа содержит более указанного значения
            Assert.IsNotEmpty(body2, "Список ответа не д.б. пустым.");
            Assert.AreEqual(body2.Count, chunkSize, $"Список ответ должен содержать {chunkSize} элементов.");


            //5. Ид д.б. больше нуля
            //6. Имя не д.б. пустым
            Assert.Greater(body2[0].id, minIdValueBound, $"Ид элемента д.б. больше {minIdValueBound}");
            Assert.IsNotEmpty(body2[0].name, "Имя рабочего не д.б. пустым.");

            //7. Проверка чтения удаленных записей
            Assert.IsTrue(body2.Any(w => w.wasDeleted), "Ожидалось что хоть 1 запись будет удаленной.");

            //END
            //Восстанавливаем запись
            var response3 = await _client.DeleteAsync($"api/workers/{restoreAction}");
        }

        //Выборка кол-ва партий в т.ч. среди удаленных
        [Test, Order(12)]
        public async Task WorkersGetChunkCountEvenDeleted_Success()
        {
            //ARRANGE
            //1. Действие контролллера которое вызываем
            var deleteAction = $"delete-one?id=1";
            var restoreAction = $"restore-one?id=1";
            var chunkCountAction = $"get-chunk-count/from-all";
            var chunkCountEvenDeletedAction = $"get-chunk-count/from-all/even-deleted";
            //2. Размер партий при разбивке (1, чтобы легче было протестировать)
            var chunkSize = 1;


            //ACT
            //1. Удаляем запись
            var response1 = await _client.DeleteAsync($"api/workers/{deleteAction}");

            //2. Получаем ответ на запрос
            //3. Получаем тело ответа
            var response2 = await _client.GetAsync($"api/workers/{chunkCountAction}?chunkSize={chunkSize}");
            var body2 = await response2.Content.ReadFromJsonAsync<int?>();

            //2. Получаем ответ на запрос
            //3. Получаем тело ответа
            var response3 = await _client.GetAsync($"api/workers/{chunkCountEvenDeletedAction}?chunkSize={chunkSize}");
            var body3 = await response3.Content.ReadFromJsonAsync<int?>();

            //ASSERT
            //1. StatusCode - OK
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode, "Http-ответ д.б. вернуть ОК.");
            Assert.AreEqual(HttpStatusCode.OK, response3.StatusCode, "Http-ответ д.б. вернуть ОК.");
            //2. Тело ответа не нулл
            Assert.IsNotNull(body2, "Тело ответа не д.б. со значением нул.");
            Assert.IsNotNull(body3, "Тело ответа не д.б. со значением нул.");

            //3. Список ответа содержит более указанного значения
            Assert.Greater(body3, body2, $"Ожидалось что кол-во партий среди \"в т.ч. удаленных\" записей будет больше чем тех что среди только \"актуальных\" записей.");

            //END
            //Восстанавливаем запись
            var response4 = await _client.DeleteAsync($"api/workers/{restoreAction}");
        }
    }
}
