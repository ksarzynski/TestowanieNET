using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cinema.Models;
using System.ComponentModel.DataAnnotations;

namespace CinemaTests.Models
{
    [TestClass]
    public class CustomerTests
    {
        string _firstname, _lastname, _email;

        [TestInitialize]
        public void InitializeTests()
        {
            _firstname = "Kamil";
            _lastname = "Sarzyński";
            _email = "kamilsarzynski@email.pl";
        }

        [TestMethod]
        public void CustomerData_IsValid()
        {
            var customer = new Customer()
            {
                FirstName = _firstname,
                LastName = _lastname,
                Email = _email
            };

            var context = new ValidationContext(customer);
            var result = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void FirstName_ShoudBeCapitalized_notValid()
        {
            var customer = new Customer()
            {
                FirstName = "kamil",
                LastName = _lastname,
                Email = _email
            };

            var result = TestModelHelper.Validate(customer);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void LastName_ShoudBeCapitalized_notValid()
        {
            var customer = new Customer()
            {
                FirstName = _firstname,
                LastName = "sarzynski",
                Email = _email
            };

            var result = TestModelHelper.Validate(customer);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Email_ShouldBeCorrect_notValid()
        {
            var customer = new Customer()
            {
                FirstName = _firstname,
                LastName = _lastname,
                Email = "kamilsarzynski.pl"
            };

            var result = TestModelHelper.Validate(customer);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void CustomerIsInvalid_notValid()
        {
            var customer = new Customer()
            {
                FirstName = "kamil",
                LastName = "sarzyński",
                Email = "kamilsarzynski.pl"
            };

            var result = TestModelHelper.Validate(customer);
            Assert.AreEqual(3, result.Count);
        }

        [TestCleanup]
        public void CleanupTests()
        {
            _firstname = null;
            _lastname = null;
            _email = null;
        }
    }
}
