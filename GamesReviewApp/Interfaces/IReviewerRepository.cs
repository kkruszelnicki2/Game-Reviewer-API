using GamesReviewApp.Models;

namespace GamesReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        ICollection<Review> GetReviewsByRewiever(int reviewerId);
        bool ReviewerExists(int reviewerId);
        bool CreateReviewer(Reviewer rewiever);
        bool Save();
    }
}
