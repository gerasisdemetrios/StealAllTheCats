using Azure;
using System.ComponentModel.DataAnnotations.Schema;

namespace StealAllTheCats.Models
{
    [Table("Cats")]
    public class Cat
    {
        public int Id { get; set; }

        public string CatId { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Image { get; set; }

        public DateTime Created { get; set; }

        public List<Tag> Tags { get; } = [];
    }
}
