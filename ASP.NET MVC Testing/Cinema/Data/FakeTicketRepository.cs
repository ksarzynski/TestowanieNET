using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class FakeTicketRepository : ITicketRepository
    {
        private List<Ticket> tickets = new List<Ticket>();

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await Task.Run(() => tickets);
        }

        public async Task<Ticket> GetTicket(int id)
        {
            return await Task.Run(() => tickets.FirstOrDefault(x => x.TicketID == id));
        }

        public bool TicketExists(int id)
        {
            return tickets.Any(e => e.TicketID == id);
        }
        
        public void AddTicket(Ticket ticket)
        {
            tickets.Add(ticket);
        }

        public void DeleteTicket(int ticketId)
        {
            Ticket ticket = tickets.FirstOrDefault(x => x.TicketID == ticketId);
            tickets.Remove(ticket);
        }

        public void UpdateTicket(Ticket ticket)
        {
            var p = tickets.FirstOrDefault(x => x.TicketID == ticket.TicketID);
            p = ticket;
        }

        public async Task<bool> Save()
        {
            return await Task.Run(() => true);
        }
    }
}

