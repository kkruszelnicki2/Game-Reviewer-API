using GamesReviewApp.Models;

namespace GamesReviewApp.Interfaces
{
    public interface ITagRepository
    {
        ICollection<Tag> GetTags();
        Tag GetTag(int id);
        ICollection<Game> GetGameByTag(int tagId);
        bool TagExists(int id);
        bool CreateTag(Tag tag);
        bool UpdateTag(Tag tag);
        bool DeleteTag(Tag tag);
        bool Save();
    }
}
