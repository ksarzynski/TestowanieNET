using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class FakeFilmRepository : IFilmRepository
    {
        private List<Film> films = new List<Film>();

        public async Task<IEnumerable<Film>> GetFilms()
        {
            return await Task.Run(() => films);
        }

        public async Task<IEnumerable<Film>> GetFilms(string title)
        {
            return await Task.Run(() => films.Where(x => x.Title.Contains(title)));
        }

        public async Task<Film> GetFilm(int id)
        {
            return await Task.Run(() => films.FirstOrDefault(x => x.FilmID == id));
        }

        public bool FilmExists(int id)
        {
            return films.Any(e => e.FilmID == id);
        }

        public void AddFilm(Film t)
        {
            films.Add(t);
        }

        public void DeleteFilm(int filmId)
        {
            Film t = films.FirstOrDefault(x => x.FilmID == filmId);
            films.Remove(t);
        }

        public void UpdateFilm(Film t)
        {
            var p = films.FirstOrDefault(x => x.FilmID == t.FilmID);
            p = t;
        }

        public async Task<bool> Save()
        {
            return await Task.Run(() => true);
        }
    }
}
