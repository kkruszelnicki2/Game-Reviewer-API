﻿using GamesReviewApp.Models;

namespace GamesReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAGame(int gameId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool Save();
    }
}
