using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.Include(t => t.Tickets).ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomers(string lastname)
        {
            return await _context.Customers.Include(t => t.Tickets).Where(n => n.LastName.Contains(lastname)).ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.CustomerID == id);
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

        public bool CustomerExists(Customer p)
        {
            return _context.Customers.Any(e => e.LastName == p.LastName && e.FirstName == p.FirstName);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            Customer customer = _context.Customers.Find(customerId);
            _context.Customers.Remove(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
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
