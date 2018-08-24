using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await _context.Tickets.Include(t => t.Film).Include(t => t.Customer).ToListAsync();
        }
           

        public async Task<Ticket> GetTicket(int id)
        {
            return await _context.Tickets.SingleOrDefaultAsync(x => x.TicketID == id);
        }

        public bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketID == id);
        }

        public void AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
        }

        public void DeleteTicket(int ticketId)
        {
            Ticket ticket = _context.Tickets.Find(ticketId);
            _context.Tickets.Remove(ticket);
        }

        public void UpdateTicket(Ticket ticket)
        {
            _context.Update(ticket);
        }

        public async Task<bool> Save()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; };
        }
    }
}
