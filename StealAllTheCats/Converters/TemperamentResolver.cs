using AutoMapper;
using StealAllTheCats.Models;
using StealAllTheCats.Models.Api;

public class TemperamentResolver : IValueResolver<CatApiModel, CatEntity, List<TagEntity>>
{
    public List<TagEntity> Resolve(CatApiModel source, CatEntity destination, List<TagEntity> destMember, ResolutionContext context)
    {
        var tags = new List<TagEntity>();

        if (source.Breeds == null)
            return tags;

        foreach (var breed in source.Breeds)
        {
            if (string.IsNullOrWhiteSpace(breed.Temperament))
                continue;

            var temperamentNames = breed.Temperament
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var name in temperamentNames)
            {
                tags.Add(new TagEntity
                {
                    Name = name,
                });
            }
        }

        return tags;
    }
}