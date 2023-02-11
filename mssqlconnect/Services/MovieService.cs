using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using mssqlconnect.Models;
using mssqlconnect.Repository;

namespace mssqlconnect.Services
{
    public class MovieService : IMovieRepository
    {
        private readonly MovieContext _dbContext;

        public MovieService(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> AddMovie(Movie body)
        {
            var result = await _dbContext.Movies.AddAsync(body);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
      
        public async Task<IEnumerable<Movie>> GetMovie()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieById(int Id)
        {
            var result = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == Id);
            return result;
        }

        public async Task<Movie> UpdateMovie(Movie body)
        {
            var result = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == body.Id);
            if (result != null)
            {
                result.Name = body.Name;
                result.Description = body.Description;
                result.ReleaseDate = body.ReleaseDate;

                await _dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async void DeleteMovie(int? id)
        {
            //var result = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == Id);
            //if (result != null)
            //{
            //    _dbContext.Movies.Remove(result);
            //    await _dbContext.SaveChangesAsync();
            //}

            //using (var context = _dbContext)
            //{
            //    var res = context.Movies.FromSqlRaw("SELECT * FROM Movies Where Id = {0}", id).FirstOrDefault();
            //    _dbContext.Movies.Remove(res);
            //    await _dbContext.SaveChangesAsync();
            //}
            var pId = new SqlParameter("@p0", id);
            _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Movies Where Id = @p0", pId);


        }

    }
}
