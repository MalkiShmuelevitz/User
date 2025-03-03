using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {
        ManagerDbContext _managerDbContext;

        public RatingRepository(ManagerDbContext managerDbContext)
        {
            _managerDbContext = managerDbContext;
        }

        public async Task<Rating> AddRating(Rating rating)
        {
            await _managerDbContext.Ratings.AddAsync(rating);
            await _managerDbContext.SaveChangesAsync();
            return rating;
        }
    }
}
