﻿using Moq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoList.Console.UI.Controllers;
using TodoList.Console.Domain.Services;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Hosting;
using TodoList.Console.UI.Models;

namespace TodoList.Tests.UI.Controllers
{
    [TestClass]
    public class TodoControllerTests
    {
        TodoController _todoController;
        Mock<ITasksService> _tasksService;

        [TestInitialize]
        public void setUp()
        {
            _tasksService = new Mock<ITasksService>();
            _todoController = new TodoController(_tasksService.Object);
            LoadRequestInController(_todoController);
        }

        protected void LoadRequestInController(ApiController controller)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, string.Empty);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration(); ;
        }

        [TestMethod]
        public void TodoController_GetAll()
        {
            _tasksService.Setup(x => x.GetAll()).Throws(new Exception());

            var response = _todoController.Get();

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void TodoController_Get()
        {
            _tasksService.Setup(x => x.GetById(It.IsAny<Guid>())).Throws(new Exception());

            var response = _todoController.Get(Guid.NewGuid());

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void TodoController_Post()
        {
            _tasksService.Setup(x => x.Save(It.IsAny<Task>())).Throws(new Exception());

            var response = _todoController.Post(new Task());

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void TodoController_Put()
        {
            _tasksService.Setup(x => x.Update(It.IsAny<Task>())).Throws(new Exception());

            var response = _todoController.Put(new Task());

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void TodoController_Delete()
        {
            _tasksService.Setup(x => x.Delete(It.IsAny<Guid>())).Throws(new Exception());

            var response = _todoController.Delete(Guid.NewGuid());

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
