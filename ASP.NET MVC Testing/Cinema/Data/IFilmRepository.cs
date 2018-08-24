using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Models;

namespace Cinema.Data
{
    public interface IFilmRepository
    {
        Task<IEnumerable<Film>> GetFilms();
        Task<IEnumerable<Film>> GetFilms(string title);
        Task<Film> GetFilm(int filmId);
        Task<bool> Save();
        bool FilmExists(int filmId);
        void AddFilm(Film film);
        void DeleteFilm(int filmId);
        void UpdateFilm(Film film);
    }
}
