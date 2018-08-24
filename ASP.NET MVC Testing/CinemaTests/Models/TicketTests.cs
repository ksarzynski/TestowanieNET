using System;
using System.Collections.Generic;
using System.Text;
using Cinema.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinemaTests.Models
{
    [TestClass]
    public class TicketTests
    {
        decimal _price;
        DateTime _date;

        [TestInitialize]
        public void InitializeTests()
        {
            _price = 13.0M;
            _date = new DateTime(2017, 12, 12);
        }

        [TestMethod]
        public void TicketData_IsValid()
        {
            var ticket = new Ticket()
            {
                Price = _price,
                Date = _date
             };

            var result = TestModelHelper.Validate(ticket);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Price_CannotBeLessThan0_notValid()
        {
            var ticket = new Ticket()
            {
                Price = -2.0M,
                Date = _date
            };

            var result = TestModelHelper.Validate(ticket);
            Assert.AreEqual(1, result.Count);
        }

        [TestCleanup]
        public void CleanupTests()
        {
            _price = 0;
            _date = new DateTime();
        }
    }
}
