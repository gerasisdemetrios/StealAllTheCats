using System.ComponentModel.DataAnnotations.Schema;

namespace StealAllTheCats.Models
{
    [Table("Tags")]
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public List<Cat> Cats { get; } = [];
    }
}
