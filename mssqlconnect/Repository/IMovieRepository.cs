using mssqlconnect.Models;

namespace mssqlconnect.Repository
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMovie();
        Task<Movie> GetMovieById(int Id);
        Task<Movie> AddMovie(Movie body);
        Task<Movie> UpdateMovie(Movie body);
        void DeleteMovie(int? id);
    }
}
