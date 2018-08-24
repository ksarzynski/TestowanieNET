using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Cinema.Data;
using Cinema.Controllers;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTests.Controllers
{
    [TestClass]
    public class TicketControllersTests
    {
        [TestMethod]
        public async Task Create_NoArguments_ReturnsViewResult()
        {
            var ticketRepository = new FakeTicketRepository();
            var customerRepository = new FakeCustomerRepository();
            var filmRepository = new FakeFilmRepository();
            
            var controller = new TicketsController(ticketRepository, customerRepository, filmRepository);

            var result = await controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_ValidTicket_ReturnsRedirectToActionResult()
        {
            var ticket = new Ticket();
            var ticketRepository = new FakeTicketRepository();
            var customerRepository = new FakeCustomerRepository();
            var filmRepository = new FakeFilmRepository();
            var controller = new TicketsController(ticketRepository, customerRepository, filmRepository);

            var result = await controller.Create(ticket);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Create_InValidTicket_ReturnsSameModel()
        {
            var ticket = new Ticket();
            var ticketRepository = new FakeTicketRepository();
            var customerRepository = new FakeCustomerRepository();
            var filmRepository = new FakeFilmRepository();
            var controller = new TicketsController(ticketRepository, customerRepository, filmRepository);
            controller.ModelState.AddModelError("", "");

            var result = await controller.Create(ticket) as ViewResult;

            Assert.AreEqual(ticket, result.Model);
        }

        [TestMethod]
        public async Task Delete_TicketInDb_ReturnsSameModel()
        {
            var ticket = new Ticket() { TicketID = 1 };
            var ticketRepository = new FakeTicketRepository();
            var customerRepository = new FakeCustomerRepository();
            var filmRepository = new FakeFilmRepository();
            var controller = new TicketsController(ticketRepository, customerRepository, filmRepository);
            ticketRepository.AddTicket(ticket);

            var result = await controller.Delete(1) as ViewResult;

            Assert.AreEqual(ticket, result.Model);
        }

        [TestMethod]
        public async Task Delete_TicketNotInDb_ReturnsNotFoundView()
        {
            var ticketRepository = new FakeTicketRepository();
            var customerRepository = new FakeCustomerRepository();
            var filmRepository = new FakeFilmRepository();
            var controller = new TicketsController(ticketRepository, customerRepository, filmRepository);

            var result = await controller.Delete(1) as ViewResult;

            Assert.AreEqual("NotFound", result.ViewName);
        }
           
    }
}
