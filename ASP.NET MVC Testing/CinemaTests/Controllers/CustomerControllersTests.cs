using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cinema.Controllers;
using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CinemaTests.Controllers
{
    [TestClass]
    public class CustomersControllersTests
    {
        [TestMethod]
        public async Task IndexCorrectNumberOfCustomers()
        {
            var customers = new List<Customer>()
            {
                new Customer(),
                new Customer(),
                new Customer(),
                new Customer(),
                new Customer()
            };

            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomers()).ReturnsAsync(customers);
            var controller = new CustomersController(service.Object);

            var result = await controller.Index(String.Empty);

            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Customer>;
            Assert.AreEqual(5, model.Count);
        }

        [TestMethod]
        public async Task IndexEmptyDBofCustomers()
        {
            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomers()).ReturnsAsync(new List<Customer>());
            var controller = new CustomersController(service.Object);

            var result = await controller.Index(String.Empty);

            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Customer>;
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public async Task IndexCustomerExistsReturnCustomerWithSameName()
        {
            var customers = new List<Customer>()
            {
                new Customer { LastName = "Nowak" },
                new Customer { LastName = "Kowalski" },
                new Customer { LastName = "Wiśniewski" }
            };

            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomers("Nowak")).ReturnsAsync(new List<Customer>() { customers[0] });
            service.Setup(x => x.GetCustomers("Kowalski")).ReturnsAsync(new List<Customer>() { customers[1] });
            service.Setup(x => x.GetCustomers("Wiśniewski")).ReturnsAsync(new List<Customer>() { customers[2] });
            var controller = new CustomersController(service.Object);

            var result = await controller.Index("Nowak");

            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Customer>;
            Assert.AreEqual(customers[0].LastName, model[0].LastName);
        }
             
        [TestMethod]
        public async Task DetailsCustomerExist()
        {
            var customer1 = new Customer() { LastName = "Nowak" };
            var customer2 = new Customer() { LastName = "Kowalski" };
            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomer(1)).ReturnsAsync(customer1);
            service.Setup(x => x.GetCustomer(2)).ReturnsAsync(customer2);
            var controller = new CustomersController(service.Object);

            var result = await controller.Details(1);

            var model = ((ViewResult)result).Model as Customer;
            Assert.AreEqual(customer1, model);
        }

        [TestMethod]
        public async Task DetailsCustomerDoesNotExist()
        {
            var customer1 = new Customer() { LastName = "Nowak" };
            var customer2 = new Customer() { LastName = "Kowalski" };
            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomer(1)).ReturnsAsync(customer1);
            service.Setup(x => x.GetCustomer(2)).ReturnsAsync(customer2);
            var controller = new CustomersController(service.Object);

            var result = await controller.Details(100);

            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public void CreateCustomer()
        {
            var service = new Mock<ICustomerRepository>();
            var controller = new CustomersController(service.Object);

            var result = controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task CreateValidCustomer()
        {
            var validCustomer = new Customer() { FirstName = "Tomasz", LastName = "Nowak", Email = "tomasznowak@wp.pl" };

            var service = new Mock<ICustomerRepository>();
            var controller = new CustomersController(service.Object);

            var result = await controller.Create(validCustomer);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task CreateInvalidCustomer()
        {
            string invalidLastName = "Wrong customer name";
            var invalidCustomer = new Customer() { FirstName = "Tomasz", LastName = invalidLastName, Email = "tomasznowak@wp.pl" };

            var service = new Mock<ICustomerRepository>();
            var controller = new CustomersController(service.Object);
            controller.ModelState.AddModelError("", "");

            var result = await controller.Create(invalidCustomer);

            var model = (Customer)((ViewResult)result).Model;
            Assert.AreEqual(invalidLastName, model.LastName);
        }

        [TestMethod]
        public async Task EditExistingCustomer()
        {
            var customer1 = new Customer() { LastName = "Nowak" };
            var customer2 = new Customer() { LastName = "Kowalski" };

            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomer(1)).ReturnsAsync(customer1);
            service.Setup(x => x.GetCustomer(2)).ReturnsAsync(customer2);
            var controller = new CustomersController(service.Object);

            var result = await controller.Edit(1);

            var model = ((ViewResult)result).Model as Customer;
            Assert.AreEqual(customer1, model);
        }

        [TestMethod]
        public async Task EditCustomerThatDoesntExists()
        {
            var customer1 = new Customer() { LastName = "Nowak" };
            var customer2 = new Customer() { LastName = "Kowalski" };

            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomer(1)).ReturnsAsync(customer1);
            service.Setup(x => x.GetCustomer(2)).ReturnsAsync(customer2);
            var controller = new CustomersController(service.Object);

            var result = await controller.Edit(100);

            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }
             

        [TestMethod]
        public async Task EditCustomerWithExistsingIdButInvalid()
        {
            var invalidCustomer = new Customer() { FirstName = "Tomasz", LastName = "Nowak", Email = "tomasznowak@wp.pl" };
            var teams = new List<Customer>() { invalidCustomer };

            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomer(0)).ReturnsAsync(invalidCustomer);
            var controller = new CustomersController(service.Object);
            controller.ModelState.AddModelError("", "");

            var result = await controller.Edit(0, invalidCustomer);

            var model = ((ViewResult)result).Model as Customer;
            Assert.AreEqual(invalidCustomer, model);
        }

        [TestMethod]
        public async Task EditCustomerIsValid()
        {
            string validLastName = "Nowak";
            var customer = new Customer() { LastName = validLastName, FirstName = "Tomasz", Email = "tomasznowak@wp.pl" };
            var teamToEditWith = new Customer() { LastName = validLastName, FirstName = "Adam", Email = "tomasznowak@wp.pl" };

            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomer(0)).ReturnsAsync(customer);
            service.Setup(x => x.CustomerExists(0)).Returns(true);
            var controller = new CustomersController(service.Object);

            var result = await controller.Edit(0, teamToEditWith);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task EditCustomerWithInvalidData()
        {
            var customer = new Customer() { FirstName = "Tomasz", LastName = "Nowak", Email = "tomasznowak@wp.pl" };
            var newCustomerData = new Customer() { FirstName = "Tomasz", LastName = "Nowak", Email = "tomasznowakwp.pl" };

            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomer(0)).ReturnsAsync(customer);
            service.Setup(x => x.CustomerExists(0)).Returns(true);
            var controller = new CustomersController(service.Object);
            controller.ModelState.AddModelError("", "");

            var result = await controller.Edit(0, newCustomerData);
            var model = ((ViewResult)result).Model as Customer;
            Assert.AreEqual(newCustomerData, model);
        }

        [TestMethod]
        public async Task DeleteExistingCustomer()
        {
            var customer1 = new Customer() { LastName = "Nowak" };
            var customer2 = new Customer() { LastName = "Kowalski" };

            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomer(1)).ReturnsAsync(customer1);
            service.Setup(x => x.GetCustomer(2)).ReturnsAsync(customer2);

            var controller = new CustomersController(service.Object);

            var result = await controller.Delete(1);
            var model = ((ViewResult)result).Model as Customer;
            Assert.AreEqual(customer1, model);
        }

        [TestMethod]
        public async Task DeleteCustomerThatDoesntExists()
        {
            var customer1 = new Customer() { LastName = "Nowak" };
            var customer2 = new Customer() { LastName = "Kowalski" };

            var service = new Mock<ICustomerRepository>();
            service.Setup(x => x.GetCustomer(1)).ReturnsAsync(customer1);
            service.Setup(x => x.GetCustomer(2)).ReturnsAsync(customer2);

            var controller = new CustomersController(service.Object);

            var result = await controller.Delete(100);
            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

                    
    }
}
