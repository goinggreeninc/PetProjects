﻿using System;
using System.Linq;
using TodoList.Console.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoList.Console.Infrastructure.Data.Repositories;

namespace TodoList.Tests.Infrastructure.Data.Repositories
{
    [TestClass]
    public class TasksRepositoryTests
    {
        private TasksRepository _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = new TasksRepository();
        }

        [TestCleanup]
        public void TearDown()
        {
            var data = _repository.GetAll();
            data.ToList().ForEach(task => { _repository.Delete(task.Id); });
        }

        [TestMethod]
        public void TasksRepository_Save()
        {
            var expected = _repository.Save(new Task { Name = "Task One" });

            Assert.AreNotEqual(Guid.Empty, expected.Id);
            _repository.Delete(expected.Id);
        }

        [TestMethod]
        public void TasksRepository_Update()
        {
            var task = new Task { Name = "Task One" };
            var taskSaved = _repository.Save(task);
            taskSaved.Name = "Name Updated";

            var taskUpdated = _repository.Update(taskSaved);
            Assert.AreEqual(taskUpdated.Id, taskSaved.Id);
            Assert.AreEqual(taskUpdated.Name, taskSaved.Name);
            _repository.Delete(task.Id);
        }

        [TestMethod]
        public void TasksRepository_GetById()
        {
            var task = _repository.Save(new Task { Name = "Task One" });

            var taskExpected = _repository.GetById(task.Id);

            Assert.IsNotNull(taskExpected);
            Assert.AreEqual(task.Id, taskExpected.Id);
            Assert.AreEqual(task.Name, taskExpected.Name);

            _repository.Delete(task.Id);
        }

        [TestMethod]
        public void TasksRepository_Delete()
        {
            var expected = _repository.Save(new Task { Name = "Task One" });
            _repository.Delete(expected.Id);

            var taskRemoved = _repository.GetById(expected.Id);

            Assert.IsNull(taskRemoved);
        }

        [TestMethod]
        public void TasksRepository_GetAll()
        {
            var expected = _repository.Save(new Task { Name = "Task One" });

            var tasksFound = _repository.GetAll();

            Assert.IsTrue(tasksFound.Count > 0);
            _repository.Delete(expected.Id);
        }
    }
}