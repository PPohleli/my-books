using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books.ActionResults;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublishersService _publishersService;
        private readonly ILogger<PublishersController> _logger;
        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            //throw new Exception("This is an exception thown from GetAllPublishers()");

            try
            {
                _logger.LogInformation("This is just a log in GetAllPublishers()");

                var _response = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(_response);
            }
            catch (Exception)
            {
                return BadRequest($"Sorry, we could not load the publishers");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
                //return Ok("Publisher added successfully");
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher Name: {ex.PublisherName}");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public ActionResult<Publisher> GetPublisherById(int id) //public IActionResult GetPublisherById(int id)
        {
            //throw new Exception($"This is an exception that will be handled by middleware");

            var _response = _publishersService.GetPublisherById(id);

            if (_response != null)
            {
                return _response; //return Ok(_response);
            }
            else
            {
                return NotFound(/*$"Publisher with ID {id} not found."*/);
            }
        }

        [HttpGet("Get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherData(int id) 
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
