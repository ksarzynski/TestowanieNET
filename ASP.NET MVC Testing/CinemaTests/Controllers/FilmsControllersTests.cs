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
    public class FilmsControllersTests
    {
        [TestMethod] 
        public async Task CorrectNumberOfFilms()
        {
            var films = new List<Film>()
            {
                new Film(),
                new Film(),
                new Film(),
                new Film(),
                new Film()
            };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilms()).ReturnsAsync(films);
            var controller = new FilmsController(service.Object);
                   
            var result = await controller.Index(String.Empty);
              
            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Film>;
            Assert.AreEqual(5, model.Count);
        }

        [TestMethod]
        public async Task EmptyDBofFilms()
        {
            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilms()).ReturnsAsync(new List<Film>());
            var controller = new FilmsController(service.Object);

            var result = await controller.Index(String.Empty);

            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Film>;
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public async Task CheckFilm()
        {
            var films = new List<Film>()
            {
                new Film { Title = "Godfather" },
                new Film { Title = "Godfellas" },
                new Film { Title = "Citizen Kane" }
            };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilms("Godfather")).ReturnsAsync(new List<Film>() { films[0] });
            service.Setup(x => x.GetFilms("Godfellas")).ReturnsAsync(new List<Film>() { films[1] });
            service.Setup(x => x.GetFilms("Citizen Kane")).ReturnsAsync(new List<Film>() { films[2] });
            var controller = new FilmsController(service.Object);

            var result = await controller.Index("Godfather");

            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Film>;
            Assert.AreEqual(films[0].Title, model[0].Title);
        }

    
        [TestMethod]
        public async Task FilmExists()
        {
            var film1 = new Film() { Title = "Godfather" };
            var film2 = new Film() { Title = "Godfellas" };
            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(1)).ReturnsAsync(film1);
            service.Setup(x => x.GetFilm(2)).ReturnsAsync(film2);
            var controller = new FilmsController(service.Object);

            var result = await controller.Details(1);

            var model = ((ViewResult)result).Model as Film;
            Assert.AreEqual(film1, model);
        }

        [TestMethod]
        public async Task FilmDoesNotExists()
        {
            var film1 = new Film() { Title = "Godfather" };
            var film2 = new Film() { Title = "Godfellas" };
            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(1)).ReturnsAsync(film1);
            service.Setup(x => x.GetFilm(2)).ReturnsAsync(film2);
            var controller = new FilmsController(service.Object);

            var result = await controller.Details(100);

            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public void CreateFilmTest()
        {
            var service = new Mock<IFilmRepository>();
            var controller = new FilmsController(service.Object);

            var result = controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task CreateValidFilm()
        {
            var validFilm = new Film() { Title = "Godfather", Year = 1972, Length = 175 };

            var service = new Mock<IFilmRepository>();
            var controller = new FilmsController(service.Object);

            var result = await controller.Create(validFilm);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task CreateInvalidFilm()
        {
            string invalidTitle = "Wrong title";
            var invalidFilm = new Film() { Title = invalidTitle, Year = 1972, Length = 175 };

            var service = new Mock<IFilmRepository>();
            var controller = new FilmsController(service.Object);
            controller.ModelState.AddModelError("", "");

            var result = await controller.Create(invalidFilm);

            var model = (Film)((ViewResult)result).Model;
            Assert.AreEqual(invalidTitle, model.Title);
        }

        [TestMethod]
        public async Task EditExistingFilm()
        {
            var film1 = new Film() { Title = "Godfather" };
            var film2 = new Film() { Title = "Godfellas" };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(1)).ReturnsAsync(film1);
            service.Setup(x => x.GetFilm(2)).ReturnsAsync(film2);
            var controller = new FilmsController(service.Object);

            var result = await controller.Edit(1);

            var model = ((ViewResult)result).Model as Film;
            Assert.AreEqual(film1, model);
        }

        [TestMethod]
        public async Task EditFilmThatDoesntExists()
        {
            var film1 = new Film() { Title = "Godfather" };
            var film2 = new Film() { Title = "Godfellas" };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(1)).ReturnsAsync(film1);
            service.Setup(x => x.GetFilm(2)).ReturnsAsync(film2);
            var controller = new FilmsController(service.Object);

            var result = await controller.Edit(100);

            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public async Task EditWithIdThatDoesntExists()
        {
            var film1 = new Film() { Title = "film1" };
            var film2 = new Film() { Title = "film2" };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(1)).ReturnsAsync(film1);
            service.Setup(x => x.GetFilm(2)).ReturnsAsync(film2);
            var controller = new FilmsController(service.Object);

            var result = await controller.Edit(1000, film1);

            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public async Task EditFilmWithExistsingIdButInvalid()
        {
            var invalidFilm = new Film() { FilmID = 0, Title = "Godfather", Year = 1972, Length = 175 };
            var teams = new List<Film>() { invalidFilm };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(0)).ReturnsAsync(invalidFilm);
            var controller = new FilmsController(service.Object);
            controller.ModelState.AddModelError("", "");

            var result = await controller.Edit(0, invalidFilm);

            var model = ((ViewResult)result).Model as Film;
            Assert.AreEqual(invalidFilm, model);
        }

        [TestMethod]
        public async Task EditFilmIsValid()
        {
            string validTitle = "Godfather";
            var film = new Film() { Title = validTitle, Year = 1970, Length = 175 };
            var teamToEditWith = new Film() { Title = validTitle, Year = 1972, Length = 1975 };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(0)).ReturnsAsync(film);
            service.Setup(x => x.FilmExists(0)).Returns(true);
            var controller = new FilmsController(service.Object);

            var result = await controller.Edit(0, teamToEditWith);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
           }

        [TestMethod]
        public async Task EditFilmWithInvalidData()
        {
            var film = new Film() { Title = "Godfather", Year = 1972, Length = 175 };
            var newFilmData = new Film() { Title = "Godfather II", Year = 1410, Length = 175 };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(0)).ReturnsAsync(film);
            service.Setup(x => x.FilmExists(0)).Returns(true);
            var controller = new FilmsController(service.Object);
            controller.ModelState.AddModelError("", "");

            var result = await controller.Edit(0, newFilmData);
            var model = ((ViewResult)result).Model as Film;
            Assert.AreEqual(newFilmData, model);
        }

        [TestMethod]
        public async Task DeleteExistingFilm()
        {
            var film1 = new Film() { Title = "Godfather" };
            var film2 = new Film() { Title = "Godfellas" };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(1)).ReturnsAsync(film1);
            service.Setup(x => x.GetFilm(2)).ReturnsAsync(film2);

            var controller = new FilmsController(service.Object);

            var result = await controller.Delete(1);
            var model = ((ViewResult)result).Model as Film;
            Assert.AreEqual(film1, model);
        }

        [TestMethod]
        public async Task DeleteFilmThatDoesntExists()
        {
            var film1 = new Film() { Title = "Godfather" };
            var film2 = new Film() { Title = "Godfellas" };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(1)).ReturnsAsync(film1);
            service.Setup(x => x.GetFilm(2)).ReturnsAsync(film2);

            var controller = new FilmsController(service.Object);

            var result = await controller.Delete(100);
            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public async Task DeleteFilmWithExistingId()
        {
            var film1 = new Film() { Title = "Godfather" };

            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(1)).ReturnsAsync(film1);
            var controller = new FilmsController(service.Object);

            var result = await controller.DeleteConfirmed(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task DeleteFilmThatDoesntExist()
        {
            var film1 = new Film() { Title = "Godfather" };
            var service = new Mock<IFilmRepository>();
            service.Setup(x => x.GetFilm(1)).ReturnsAsync(film1);
            var controller = new FilmsController(service.Object);

            var result = await controller.DeleteConfirmed(100);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
