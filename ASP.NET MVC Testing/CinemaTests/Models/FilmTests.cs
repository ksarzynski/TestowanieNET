using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Cinema.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinemaTests.Models
{
    [TestClass]
    public class FilmTests
    {
        string _title;
        int _year, _length;

        [TestInitialize]
        public void InitializeTests()
        {
            _title = "Godfather";
            _year = 1972;
            _length = 175;
         }

        [TestMethod]
        public void FilmData_isValid()
        {
            var film = new Film()
            {
                Title = _title,
                Year = _year,
                Length = _length
           };

            var context = new ValidationContext(film);
            var result = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(film, context, result, true);
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void Year_CannotBeInTheFuture_notValid()
        {
            var film = new Film()
            {
                Title = _title,
                Year = 2019,
                Length = _length
            };

            var result = TestModelHelper.Validate(film);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Year_MustBeLaterThanOldestFilm_notValid()
        {
            var film = new Film()
            {
                Title = _title,
                Year = 1894,
                Length = _length
            };
            var result = TestModelHelper.Validate(film);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Length_CannotBe0_notValid()
        {
            var film = new Film()
            {
                Title = _title,
                Year = _year,
                Length = 0
            };
            var result = TestModelHelper.Validate(film);
            Assert.AreEqual(1, result.Count);
        }
     
        [TestCleanup]
        public void CleanupTests()
        {
            _title = null;
            _year = 0;
            _length = 0;
        }
    }
}
