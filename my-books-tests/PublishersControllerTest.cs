using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using my_books.Controllers;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books_tests
{
    public class PublishersControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "BookDbControllerTest").Options;

        AppDbContext context;
        PublishersService publishersService;
        PublishersController publishersController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();

            publishersService = new PublishersService(context);
            publishersController = new PublishersController(publishersService, new NullLogger<PublishersController>());
        }

        //Tests Cases for GetAllPublishers() method
        [Test, Order(1)]
        public void HTTP_GetAllPublishers_WithSortBySearchPageNr_ReturnOk_Test()
        {
            IActionResult actionResult = publishersController.GetAllPublishers("name_desc", "Publisher", 1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultData.First().Name, Is.EqualTo("Publisher 6"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(6));
            Assert.That(actionResultData.Count, Is.EqualTo(5));


            IActionResult actionResultSecondPage = publishersController.GetAllPublishers("name_desc", "Publisher", 2);
            Assert.That(actionResultSecondPage, Is.TypeOf<OkObjectResult>());

            var actionResultDataSecondPage = (actionResultSecondPage as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultDataSecondPage.First().Name, Is.EqualTo("Publisher 1"));
            Assert.That(actionResultDataSecondPage.First().Id, Is.EqualTo(1));
            Assert.That(actionResultDataSecondPage.Count, Is.EqualTo(1));
        }

        //Tests Cases for GetPublisherById() method
        [Test, Order(2)]
        public void HTTP_GetPublisherById_ExistingID_ReturnOk_Test()
        {
            int publisherId = 1;

            IActionResult actionResult = publishersController.GetPublisherById(publisherId);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var publisherData = (actionResult as OkObjectResult).Value as Publisher;
            Assert.That(publisherData.Id, Is.EqualTo(1));
            Assert.That(publisherData.Name, Is.EqualTo("publisher 1").IgnoreCase);
        }

        [Test, Order(3)]
        public void HTTP_GetPublisherById_NonExistingID_ReturnNotFound_Test()
        {
            int publisherId = 999;

            IActionResult actionResult = publishersController.GetPublisherById(publisherId);

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        //Tests Cases for AddPublisher() method
        [Test, Order(4)]
        public void HTTP_AddPublisher_ValidPublisherObject_ReturnsCreated_Test()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "New Publisher"
            };

            IActionResult actionResult = publishersController.AddPublisher(newPublisherVM);
            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        [Test, Order(5)]
        public void HTTP_AddPublisher_InvalidPublisherObject_ReturnsBadRequest_Test()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "999 Publisher"
            };

            IActionResult actionResult = publishersController.AddPublisher(newPublisherVM);
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }

        //Tests Cases for DeletePublisherData() method
        [Test, Order(6)]
        public void HTTP_DeletePublisherData_NonExistingId_ReturnsBadRequest_Test()
        {
            int id = 999;
            IActionResult actionResult = publishersController.DeletePublisherData(id);
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());

        }

        [Test, Order(7)]
        public void HTTP_DeletePublisherData_ExistingId_ReturnsOkresult_Test()
        {
            int id = 6;
            IActionResult actionResult = publishersController.DeletePublisherData(id);
            Assert.That(actionResult, Is.TypeOf<OkResult>());

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Id = 1,
                    Name = "Publisher 1"
                },
                new Publisher()
                {
                    Id = 2,
                    Name = "Publisher 2"
                },
                new Publisher()
                {
                    Id = 3,
                    Name = "Publisher 3"
                },
                new Publisher()
                {
                    Id = 4,
                    Name = "Publisher 4"
                },
                new Publisher()
                {
                    Id = 5,
                    Name = "Publisher 5"
                },
                new Publisher()
                {
                    Id = 6,
                    Name = "Publisher 6"
                },
            };
            context.Publishers.AddRange(publishers);

            context.SaveChanges();
        }
    }
}