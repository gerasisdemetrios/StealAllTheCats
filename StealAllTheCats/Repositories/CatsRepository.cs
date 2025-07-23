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

        public async Task AddOrUpdate(CatEntity cat)
        {
            var incomingTagNames = cat.Tags
                .Select(t => t.Name.Trim())
                .Distinct()
                .ToList();

            var existingTagsInDb = await _context.Tags
                .Where(t => incomingTagNames.Contains(t.Name))
                .ToListAsync();

            var tagsToAttach = new List<TagEntity>();

            foreach (var tagName in incomingTagNames)
            {
                var existing = existingTagsInDb
                    .FirstOrDefault(t => t.Name.Trim().Replace(" ","").ToLower() == tagName.Replace(" ", "").ToLower());

                if (existing != null)
                {
                    tagsToAttach.Add(existing);
                }
                else
                {
                    tagsToAttach.Add(new TagEntity
                    {
                        Name = tagName,
                        Created = DateTime.UtcNow
                    });
                }
            }

            var existingCat = await _context.Cats
                .Include(c => c.Tags)
                .FirstOrDefaultAsync(c => c.CatId == cat.CatId);

            if (existingCat == null)
            {
                cat.Tags = tagsToAttach;
                cat.Created = DateTime.UtcNow;
                _context.Cats.Add(cat);
            }
            else
            {
                existingCat.Tags = tagsToAttach;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CatEntity>> GetAllPaged(int page = 1, int pageSize = 10, string? tag = null)
        {
            var cats = Enumerable.Empty<CatEntity>();

            if (tag == null || tag == string.Empty)
            {
                cats = await GetAllAsync();
            }
            else
            {
                cats = await GetAllByQueryAsync(c => c.Tags.Select(x => x.Name).Contains(tag));
            }

            var pagedCats = cats
               .OrderBy(c => c.CatId)
               .Skip((page - 1) * pageSize)
               .Take(pageSize);

            return pagedCats;
        }
    }
}
