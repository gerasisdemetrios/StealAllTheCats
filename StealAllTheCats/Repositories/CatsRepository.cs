using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Models;
using StealAllTheCats.Repositories.Interfaces;

namespace StealAllTheCats.Repositories
{
    public class CatsRepository : Repository<CatEntity>, ICatsRepository
    {
        public CatsRepository(CatsDBContext context) : base(context)
        {
        }

        public async Task AddOrUpdateAsync(CatEntity cat)
        {
            var existingCat = await _context.Cats
                .Include(c => c.Tags)
                .FirstOrDefaultAsync(c => c.CatId == cat.CatId);

            // Normalize incoming tags: match by Name, not just Id
            var incomingTagNames = cat.Tags.Select(t => t.Name.Trim()).Distinct().ToList();

            // Fetch existing tags from DB by name
            var existingTagsInDb = await _context.Tags
                .Where(t => incomingTagNames.Contains(t.Name))
                .ToListAsync();

            var tagsToAttach = new List<TagEntity>();

            foreach (var tagName in incomingTagNames)
            {
                var existing = existingTagsInDb.FirstOrDefault(t => t.Name == tagName);

                if (existing != null)
                {
                    tagsToAttach.Add(existing);
                }
                else
                {
                    // Tag doesn't exist, so create new one
                    tagsToAttach.Add(new TagEntity { Name = tagName });
                }
            }

            if (existingCat == null)
            {
                cat.Tags = tagsToAttach;
                _context.Cats.Add(cat);
            }
            else
            {
                _context.Entry(existingCat).CurrentValues.SetValues(cat);

                // Sync tags
                var currentTagIds = existingCat.Tags.Select(t => t.Id).ToHashSet();
                var updatedTagIds = tagsToAttach.Select(t => t.Id).ToHashSet();

                // Add new tags
                foreach (var tag in tagsToAttach.Where(t => !currentTagIds.Contains(t.Id)))
                {
                    existingCat.Tags.Add(tag);
                }

                // Remove unselected tags
                foreach (var tag in existingCat.Tags.Where(t => !updatedTagIds.Contains(t.Id)).ToList())
                {
                    existingCat.Tags.Remove(tag);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
