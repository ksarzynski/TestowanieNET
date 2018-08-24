using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Models;
using Cinema.Roles;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class FilmsController : Controller
    {
        private readonly IFilmRepository _filmRepository;
          
        public FilmsController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }
        
        public async Task<IActionResult> Index(string filmTitle)
        {
            if (!String.IsNullOrEmpty(filmTitle))
                return View(await _filmRepository.GetFilms(filmTitle));
            else
                return View(await _filmRepository.GetFilms());
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var film = await _filmRepository.GetFilm(id);
            if (film == null)
            {
                return View("NotFound");
            }
            return View(film);
        }
        
        public IActionResult Create()
        {
            return View();
        }
              
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmID,Title,Year,Length")] Film film)
        {
            if (ModelState.IsValid)
            {
                _filmRepository.AddFilm(film);
                await _filmRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var film = await _filmRepository.GetFilm(id);
            if (film == null)
            {
                return View("NotFound");
            }
            return View(film);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmID,Title,Year,Length")] Film film)
        {
            if (id != film.FilmID)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _filmRepository.UpdateFilm(film);
                    await _filmRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.FilmID))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _filmRepository.GetFilm(id);
            if (team == null)
            {
                return View("NotFound");
            }
            return View(team);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _filmRepository.GetFilm(id);
            _filmRepository.DeleteFilm(id);
            await _filmRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _filmRepository.FilmExists(id);
        }
    }
}
