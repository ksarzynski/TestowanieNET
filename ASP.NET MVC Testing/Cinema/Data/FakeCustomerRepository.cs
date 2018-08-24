using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private List<Customer> customers = new List<Customer>();

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await Task.Run(() => customers);
        }

        public async Task<IEnumerable<Customer>> GetCustomers(string lastname)
        {
            return await Task.Run(() => customers.Where(x => x.LastName.Contains(lastname)));
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await Task.Run(() => customers.FirstOrDefault(x => x.CustomerID == id));
        }

        public bool CustomerExists(int id)
        {
            return customers.Any(e => e.CustomerID == id);
        }

        public void AddCustomer(Customer t)
        {
            customers.Add(t);
        }

        public void DeleteCustomer(int customerId)
        {
            Customer t = customers.FirstOrDefault(x => x.CustomerID == customerId);
            customers.Remove(t);
        }

        public void UpdateCustomer(Customer t)
        {
            var p = customers.FirstOrDefault(x => x.CustomerID == t.CustomerID);
            p = t;
        }

        public async Task<bool> Save()
        {
            return await Task.Run(() => true);
        }
    }
}
