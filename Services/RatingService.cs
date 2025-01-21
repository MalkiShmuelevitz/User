using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {
        IRatingRepository _IRatingRepository;

        public RatingService(IRatingRepository iRatingRepository)
        {
            _IRatingRepository = iRatingRepository;
        }

        public async Task<Rating> AddRating(Rating rating)
        {
            return await _IRatingRepository.AddRating(rating);
        }

    }
}
