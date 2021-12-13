using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Poultryfarm.Backend.Repositories.Workers;
using Poultryfarm.ModelsAndUtils.Db.Models.Persons;
using sidonia.db_contexts.Contexts;
using sidonia.db_contexts.Wrappers;
using System.Linq;

namespace Poultryfarm.Tests.Repositories
{
    public class BaseRepo
    {
        //��������
        public WorkerRepository WorkerRepo { get; set; }
        public PersonRepository PersonRepo { get; set; }

        //�������������
        [OneTimeSetUp]
        public void Setup()
        {
            WorkerRepo = new WorkerRepository(new ContextWrapper(new PoultryfarmGenTestingContext()));
            //����� ������������� ����� ��������
            PersonRepo = new PersonRepository(WorkerRepo.Wrapper);
        }

        //�����
        [Test]
        //1. �������� ���-�� ������� � ����������� � �������
        public void CheckWorkerNoteCount_IsGreaterZero()
        {
            //ARRANGE
            var expectedCountLowerBoundValue = 25;

            //ACT
            var count = WorkerRepo.GetCount();

            //ASSERT
            Assert.GreaterOrEqual(count, expectedCountLowerBoundValue, $"���������, ��� ���-�� ������� ����� ��� ����������� {expectedCountLowerBoundValue}");
        }

        [Test]
        //2. �������� ������ � ����������� � �������
        public void CheckWorkerNote_Success()
        {
            //ARRANGE
            var expectedId = 4;

            //ACT
            //� ��� �� ��������
            //Repo.Context.Set<Worker>().Include(w => w.Person);
            //var worker = Repo.GetOne(expectedId).FirstOrDefault();

            //��� �������� ���������
            var worker = WorkerRepo.GetOne(expectedId).Include(w => w.Person).FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(worker, "������ �� �.�. ����.");
            Assert.IsNotNull(worker.Person, "��������� ������ ������� �� �.�. ����.");
            Assert.AreEqual(expectedId, worker.Id, $"��������� ��� id=={expectedId}");
            Assert.IsNotEmpty(worker.Person.Name, "��������� ������� �����");
            Assert.IsNotEmpty(worker.Person.Patronymic, "��������� ������� ��������");
            Assert.IsNotEmpty(worker.Person.Surname, "��������� ������� �������");
            Assert.IsNotEmpty(worker.Person.Passport, "��������� ������� ��������");
        }

        [Test]
        //3. �������� ������� � ����������� � �������
        public void CheckWorkerNotes_Success()
        {
            //ARRANGE
            var workerIndex = 3;
            var expectedCountLowerBound = 5;

            //ACT
            var workers = WorkerRepo.GetAll().Include(w => w.Person).ToList();

            //ASSERT
            //1. �������� ���������
            Assert.IsNotNull(workers, "��������� �� �.�. ����.");
            Assert.IsNotEmpty(workers, "��������� �� �.�. ������.");
            Assert.Greater(workers.Count, expectedCountLowerBound, $"��������� ���-�� ������� ������� {expectedCountLowerBound}");

            //2. �������� ����� �� ��������� ���������
            Assert.IsNotNull(workers[workerIndex], "������ �� �.�. ����.");
            Assert.IsNotNull(workers[workerIndex].Person, "��������� ������ ������� �� �.�. ����.");
            Assert.IsNotEmpty(workers[workerIndex].Person.Name, "��������� ������� �����");
            Assert.IsNotEmpty(workers[workerIndex].Person.Patronymic, "��������� ������� ��������");
            Assert.IsNotEmpty(workers[workerIndex].Person.Surname, "��������� ������� �������");
            Assert.IsNotEmpty(workers[workerIndex].Person.Passport, "��������� ������� ��������");
        }

        [Test]
        //4. �������� ������� � ����������� � ������� ��� ������������� ���������� � ���������
        public void CheckWorkerNotesWithoutPersonInclude_Success()
        {
            //ARRANGE
            var workerIndex = 3;
            var expectedCountLowerBound = 5;
            var expectedSalaryMinBound = 5000;

            //ACT
            var workers = WorkerRepo.GetAll().ToList();

            //ASSERT
            //1. �������� ���������
            Assert.IsNotNull(workers, "��������� �� �.�. ����.");
            Assert.IsNotEmpty(workers, "��������� �� �.�. ������.");
            Assert.Greater(workers.Count, expectedCountLowerBound, $"��������� ���-�� ������� ������� {expectedCountLowerBound}");

            //2. �������� ����� �� ��������� ���������
            Assert.IsNotNull(workers[workerIndex], "������ �� �.�. ����.");
            Assert.GreaterOrEqual(workers[workerIndex].Salary,expectedSalaryMinBound, $"��������� �������� �������, ��� {expectedSalaryMinBound}");
        }

        [Test]
        //5. �������� ������� ������� � ����������� � �������
        public void CheckWorkerNoteSelection_Success()
        {
            //ARRANGE
            var workerSalaryLowerBound = 5000;
            var workerSalaryTopBound = 9000;

            var workerIndex = 0;
            var workerCountLowerBound = 0;
            //ACT
            var workers = WorkerRepo.GetSome(w=>w.Salary< workerSalaryTopBound).Include(w=>w.Person).ToList();

            //ASSERT
            //1. �������� ���������
            Assert.IsNotNull(workers, "��������� �� �.�. ����.");
            Assert.IsNotEmpty(workers, "��������� �� �.�. ������.");
            Assert.Greater(workers.Count, workerCountLowerBound, $"��������� ���-�� ������� ������� {workerCountLowerBound}");

            //2. �������� ����� �� ��������� ���������
            Assert.IsNotNull(workers[workerIndex], "������ �� �.�. ����.");
            Assert.IsNotNull(workers[workerIndex].Person, "��������� ������ ������� �� �.�. ����.");
            Assert.GreaterOrEqual(workers[workerIndex].Salary, workerSalaryLowerBound, $"��������� �������� �������, ��� {workerSalaryLowerBound}");
            Assert.LessOrEqual(workers[workerIndex].Salary, workerSalaryTopBound, $"��������� �������� �������, ��� {workerSalaryTopBound}");
        }

        [Test]
        //6. �������� ���������� ������ � �������
        public void CheckWorkerNoteCreation_Success()
        {
            //ARRANGE
            //1. ������ �������� 
            var name = "�������";
            var patronymic = "����������";
            var surname = "��������";
            var passport = "TYUO21323432";
            var salary = 7000;
            //2. ����� �������, ������� ������� � ����� �������� ��������
            var person = new Person
            {
                Name= name,
                Patronymic= patronymic,
                Surname= surname,
                Passport= passport
            };
            //3. ����� �������� ��������
            var worker = new Worker
            {
                Person = person,
                Salary = salary
            };

            //ACT
            //1. ����������� �������� ��������
            //2. � ��� ������� � ����, ���������� �� ������� � ����������� ��������
            var actualWorker = WorkerRepo.CreateOne(worker);
            var addedPerson = PersonRepo
                .GetSome(p => 
                p.Passport == passport 
                && p.Surname == surname 
                && p.Name == name 
                && p.Patronymic == patronymic)
                .FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(actualWorker, "������ �� �.�. ����.");
            Assert.IsNotNull(actualWorker.Person, "��������� ������ ������� �� �.�. ����.");
            Assert.IsNotNull(addedPerson, "������ ������� �� �.�. ����. ������ ��� �� ��������� � ��.");
            Assert.IsNotEmpty(actualWorker.Person.Name, "��������� ������� �����");
            Assert.IsNotEmpty(actualWorker.Person.Patronymic, "��������� ������� ��������");
            Assert.IsNotEmpty(actualWorker.Person.Surname, "��������� ������� �������");
            Assert.IsNotEmpty(actualWorker.Person.Passport, "��������� ������� ��������");
            Assert.AreEqual(actualWorker.Salary, salary, $"��������� �������� ������ {salary}");
        }

        [Test]
        //7. �������� ���������� ������� �������
        public void CheckWorkerNoteUpdating_Success()
        {
            //ARRANGE
            //1. ������ �������� 
            var newSalary = 8001;
            var newName = "����";
            //2. �������� �������� ��� ����������
            var worker = WorkerRepo.GetAll().Include(w=>w.Person).OrderByDescending(w=>w.Id).FirstOrDefault();
            //3. ��������������
            //worker.Salary = newSalary;
            //worker.Person.Name = newName;

            //ACT
            //1. ���������� �������� ��������
            //2. � ��� ������� � ����, ���������� �� ������� � ���������� ��������
            var updatedWorker = WorkerRepo.UpdateOne(worker.Id, w =>
            {
                w.Salary = newSalary;
                w.Person.Name = newName;
            });
            var updatedPerson = PersonRepo.GetOne(worker.Person.Id).FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(updatedWorker, "������ �� �.�. ����.");
            Assert.IsNotNull(updatedWorker.Person, "��������� ������ ������� �� �.�. ����.");
            Assert.IsNotNull(updatedPerson, "������ ������� �� �.�. ����.");
            Assert.AreEqual(updatedPerson.Name, newName, $"��������� ��� ������� {newName}");
            Assert.IsNotEmpty(updatedWorker.Person.Name, "��������� ������� �����");
            Assert.IsNotEmpty(updatedWorker.Person.Patronymic, "��������� ������� ��������");
            Assert.IsNotEmpty(updatedWorker.Person.Surname, "��������� ������� �������");
            Assert.IsNotEmpty(updatedWorker.Person.Passport, "��������� ������� ��������");
            Assert.AreEqual(updatedWorker.Salary, newSalary, $"��������� �������� ������ {newSalary}");
        }

        [Test]
        //8. �������� �� ����������� �������� ������ ��������
        //(�� ���� �.�. ���� ������, �.�. ����������� ��������� �������� ����������,
        //�� ��� �� ���. �� ������ ������ �����)
        public void CheckWorkerNoteNotSafeDeleting_Success()
        {
            //ARRANGE
            //1. �������� ��������, ������� ����� �������
            var worker = WorkerRepo.GetAll().Include(w=>w.Person).OrderByDescending(w => w.Id).FirstOrDefault();
            var entityId = worker.Id;

            //ACT
            //1. ��������� �������� ��������
            //2. ���� ����� �������� � ��. �.�. ����
            //3. ���� ������ ��������. ������ �����
            var deletedWorker = WorkerRepo.DeleteOne(entityId);
            var emptyWorker1 = WorkerRepo.GetOne(entityId).FirstOrDefault();
            var emptyWorker2 = WorkerRepo.GetOneEvenDeleted(entityId).FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(deletedWorker, "������ �� �.�. ����.");
            Assert.AreEqual(deletedWorker.Id,entityId, "Id ��������� � ��������� ������ ������ ���������");
            Assert.IsNotNull(deletedWorker.Person, "��������� ������ ������� �� �.�. ����.");
            Assert.IsNull(emptyWorker1, "� �� ������ �� ������ ���� ���� ���� ������, �.�. �� �������.");
            Assert.IsNotNull(emptyWorker2, "��� ������ ������ ���� ������� �� �������� \"�������\"");
            Assert.IsTrue(emptyWorker2.WasDeleted, "��������� ��� ������ ����� �������.");

            //END
            WorkerRepo.RestoreOne(entityId);
        }

        [Test]
        //9. �������� ����������� �������� ������ ��������
        public void CheckWorkerNoteSafeDeleting_Success()
        {
            //ARRANGE
            //1. �������� ��������, ������� ����� �������
            var worker = WorkerRepo.GetAll().OrderByDescending(w => w.Id).FirstOrDefault();
            var entityId = worker.Id;

            //ACT
            //1. ��������� �������� ��������
            //2. ���� ����� �������� � ��. �.�. ����
            var deletedWorker = WorkerRepo.DeleteOne(entityId);
            var emptyWorker = WorkerRepo.GetOne(entityId).FirstOrDefault();

            //ASSERT
            Assert.IsNotNull(deletedWorker, "������ �� �.�. ����.");
            Assert.AreEqual(deletedWorker.Id, entityId, "Id ��������� � ��������� ������ ������ ���������");
            Assert.IsNull(emptyWorker, "� �� ������ �� ������ ���� ���� ���� ������, �.�. �� �������.");

            //END
            WorkerRepo.RestoreOne(entityId);
        }
    }
}