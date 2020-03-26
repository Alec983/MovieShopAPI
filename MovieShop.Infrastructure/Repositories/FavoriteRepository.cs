using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repositories
{
    public class FavoriteRepository : EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<int> GetFavoriteCount(int id)
        {
            //var count = await _dbContext.Favorites.Where(f => f.MovieId == id).ToListAsync();
            var count = await _dbContext.Favorites.CountAsync(f => f.MovieId == id);
            return count;
        }
    }
}
