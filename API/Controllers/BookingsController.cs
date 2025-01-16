using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<BookingsController> _logger;

        // In-Memory Database
        private static readonly List<Booking> _bookings = new();

        public BookingsController(ILogger<BookingsController> logger, IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreatingBooking(Booking newBooking)
        {
            if (!ModelState.IsValid) return BadRequest();

            _bookings.Add(newBooking);

            _messageProducer.SendingMessage(newBooking);

            return Ok();
        }
    }
}