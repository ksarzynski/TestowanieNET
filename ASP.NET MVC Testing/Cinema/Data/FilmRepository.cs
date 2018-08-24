using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDbContext _context;

        public FilmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Film>> GetFilms()
        {
            return await _context.Films.Include(t => t.Tickets).ToListAsync();
        }

        public async Task<IEnumerable<Film>> GetFilms(string title)
        {
            return await _context.Films.Include(t => t.Tickets).Where(n => n.Title.Contains(title)).ToListAsync();
        }

        public async Task<Film> GetFilm(int id)
        {
            return await _context.Films.SingleOrDefaultAsync(x => x.FilmID == id);
        }

        public bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.FilmID == id);
        }

        public bool FilmExists(Film p)
        {
            return _context.Films.Any(e => e.Title == p.Title && e.Year == p.Year);
        }

        public void AddFilm(Film film)
        {
            _context.Films.Add(film);
        }

        public void DeleteFilm(int filmId)
        {
            Film film = _context.Films.Find(filmId);
            _context.Films.Remove(film);
        }

        public void UpdateFilm(Film film)
        {
            _context.Update(film);
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
