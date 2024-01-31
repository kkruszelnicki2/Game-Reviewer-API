﻿using GamesReviewApp.Data;
using GamesReviewApp.Interfaces;
using GamesReviewApp.Models;

namespace GamesReviewApp.Repository
{
    public class TagRepository : ITagRepository
    {
        private DataContext _context;

        public TagRepository(DataContext context)
        {
                _context = context;
        }
        public ICollection<Game> GetGameByTag(int tagId)
        {
            return _context.GameTags.Where(t => t.TagId == tagId).Select(g => g.Game).ToList();
        }

        public ICollection<Tag> GetTags()
        {
            return _context.Tags.OrderBy(t => t.Name).ToList();
        }

        public Tag GetTag(int id)
        {
            return _context.Tags.Where(t => t.Id == id).FirstOrDefault();
        }

        public bool TagExists(int id)
        {
            return _context.Tags.Any(t => t.Id == id);
        }
    }
}