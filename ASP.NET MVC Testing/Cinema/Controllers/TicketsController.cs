using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Models

{
    [Authorize(Roles = Roles.Roles.Administrator)]
    public class TicketsController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IFilmRepository _filmRepository;

        public TicketsController(ITicketRepository ticketRepository, ICustomerRepository customerRepository, IFilmRepository filmRepository)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
            _filmRepository = filmRepository;
        }

        public async Task<IActionResult> Index(string price)
        {
            return View(await _ticketRepository.GetTickets());
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                return View("NotFound");
            }
            return View(ticket);
        }
        
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerID"] = new SelectList(await _customerRepository.GetCustomers(), "CustomerID", "Email");
            ViewData["FilmID"] = new SelectList(await _filmRepository.GetFilms(), "FilmID", "Title");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID,FilmID,CustomerID,Price,Date")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _ticketRepository.AddTicket(ticket);
                await _ticketRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(await _customerRepository.GetCustomers(), "CustomerID", "CustomerID", ticket.CustomerID);
            ViewData["FilmID"] = new SelectList(await _filmRepository.GetFilms(), "FilmID", "FilmID", ticket.FilmID);
            return View(ticket);
        }
        
        public async Task<IActionResult> Edit(int id)
        {

            var ticket = await _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                return View("NotFound");
            }
            ViewData["CustomerID"] = new SelectList(await _customerRepository.GetCustomers(), "CustomerID", "Email", ticket.CustomerID);
            ViewData["FilmID"] = new SelectList(await _filmRepository.GetFilms(), "FilmID", "Title", ticket.FilmID);
            return View(ticket);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketID,FilmID,CustomerID,Price,Date")] Ticket ticket)
        {
            if (id != ticket.TicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ticketRepository.UpdateTicket(ticket);
                    await _ticketRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(await _customerRepository.GetCustomers(), "CustomerID", "CustomerID", ticket.CustomerID);
            ViewData["FilmID"] = new SelectList(await _filmRepository.GetFilms(), "FilmID", "FilmID", ticket.FilmID);
            return View(ticket);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                return View("NotFound");
            }
            return View(ticket);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _ticketRepository.GetTicket(id);
            _ticketRepository.DeleteTicket(id);
            await _ticketRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        public bool TicketExists(int id)
        {
            return _ticketRepository.TicketExists(id);
        }
             
    }
}

