using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetTickets();
        Task<Ticket> GetTicket(int ticketId);
        Task<bool> Save();
        bool TicketExists(int ticketId);
        void AddTicket(Ticket ticket);
        void DeleteTicket(int ticketId);
        void UpdateTicket(Ticket ticket);
    }
}
